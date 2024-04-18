using System.Collections;
using _Project.Scripts.Achivments;
using _Project.Scripts.Entities.Customers;
using _Project.Scripts.Managers;
using _Project.Scripts.Other;
using _Project.Scripts.UI_Scripts;
using _Project.Scripts.UI.Tutorial;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Food
{
	public class HotDog : MonoBehaviour 
	{
		[Inject] private LevelSoundManager _levelSoundManager;
		[Inject] private US_Manager _usManager;
		[Inject] private UIManager _uiManager;   
		bool reachedCustomer;
		bool scaleUp;
		Vector3 colliderSize;
		bool canMove;
		int a=1;
		
		public GameObject otherObject;
		public bool wrongOrderGiven;
		public Customer customer;
		public bool redSauce;
		public bool yellowSauce;
		public bool tikki;
		public Availability myPlate;
		public Vector3 myOriginalPos , myTouchPos;
		public LevelManager.Orders myType;
		public LevelManager.Orders myTypeToEat;
		public bool perfect;
		public bool iAmSelected , startAnimating;
		public Vector3 myLocalScale;
		public GameObject mySelection;
		public bool tutorialOn;
		public GameObject errorObject;

		private void Start()
		{
			_uiManager.n_Hotdogs_served = PlayerPrefs.GetInt ("hotdogServed");
			myLocalScale = transform.localScale;
			myOriginalPos = transform.position;
			colliderSize = transform.GetComponent<BoxCollider> ().size;
		}


		private void OnDisable()
		{
			wrongOrderGiven = false;
			redSauce = false;
	
			yellowSauce  = false;
			tikki  = false;
			reachedCustomer = false;
			transform.GetComponent<Availability>().available = true;
			_usManager.hotDogSaucesOnPlates[transform.GetComponent<Availability>().myPositionInArray].gameObject.SetActive (false);
			myType = LevelManager.Orders.NONE; 
			transform.localScale = myLocalScale;
			startAnimating = false;
			iAmSelected = false;
			mySelection.SetActive (false);
		}


		private void OnMouseDown()
		{
			if(!TutorialPanel.popupPanelActive || US_Manager.tutorialEnd || Australia_Manager.tutorialEnd || tutorialOn)
			{
				_usManager.clickedHotDogDestinationFunction = this;
				canMove = true;
				Vector3 myPos = Camera.main.WorldToScreenPoint (transform.position);
				myTouchPos =  Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y , myPos.z));
				if(_usManager.clickedTikki && myType == LevelManager.Orders.NONE)
				{
					if(!_usManager.clickedTikkiDestinationFunction.isBurnt)
					{
						_usManager.clickedTikkiDestinationFunction.availableHotDog = this.GetComponent<Availability>();
						_usManager.TikkiReached ();

					}
					_usManager.AllClickedBoolsReset ();
				}
				else if((_usManager.clickedYellowSauce || _usManager.clickedRedSauce) && (myType == LevelManager.Orders.NONE || myType == LevelManager.Orders.HOTDOG))
				{
					_usManager.clickedItemDestinationFunction.availableHotDog = this;
					_usManager.ObjectReached ();
					_usManager.AllClickedBoolsReset ();
				}
				else 
				{
					if(tikki)
					{
						if(tutorialOn)
						{
							_usManager.firstCustomer.tutorialOn = true;
						}
						_usManager.AllClickedBoolsReset ();
						_usManager.clickedHotDog = true;

						startAnimating = false;
						scaleUp = true;
						transform.GetComponent<BoxCollider> ().size = new Vector3(colliderSize.x/2f , colliderSize.y/2f , colliderSize.z);
						iAmSelected = true;
					}
					else
					{
						_usManager.clickedHotDogDestinationFunction = null;
						canMove = false;
						errorObject.SetActive(true);
					}
				}

			}
		}

		private void OnMouseDrag()
		{

			if( canMove)
			{
				Vector3 myPos = Camera.main.WorldToScreenPoint (transform.position);
				Vector3 newPos = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y , myPos.z));
				if(Vector3.Distance (newPos,myTouchPos) > 0.2f)
				{
					transform.position =  Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y , myPos.z));
				}
			}
		}

		private void OnMouseUp()
		{
			errorObject.SetActive (false);
			if(canMove)
			{

				startAnimating = true;

				if(!reachedCustomer)
					StartCoroutine(MoveToPosition());
				else
				{
					ClickedDestination ();

				}
				canMove = false;
			}
			transform.GetComponent<BoxCollider> ().size = colliderSize;
		}
		public void Stopa()
		{
			_uiManager.achievment_text.SetActive (false);
		}

		public void ClickedDestination()
		{
			_usManager.platesFilledCount--;
			myPlate.available = true;
			if(!otherObject.name.Contains ("dustbin"))
			{
		
				_uiManager.n_Hotdogs_served++;
				_usManager.clickedHotDog = false;
				_levelSoundManager.customerEat.Play();

				PlayerPrefs.SetInt ("hotdogServed", _uiManager.n_Hotdogs_served);
				if(PlayerPrefs.GetInt("hotdogServed") > 9 && PlayerPrefs.GetInt ("hotdogLevel1")==0)
				{

					PlayerPrefs.SetInt ("hotdogLevel1",1);
					_uiManager.achievment_text.SetActive(true);
					AchievementChild.check_claim++;
					PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
					Invoke(nameof(Stopa),4.0f);
				}
				if(PlayerPrefs.GetInt("hotdogServed") > 99 && PlayerPrefs.GetInt ("hotdogLevel2")==0)
				{
					PlayerPrefs.SetInt ("hotdogLevel2",1);
					_uiManager.achievment_text.SetActive(true);
					AchievementChild.check_claim++;
					PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
					Invoke(nameof(Stopa),4.0f);
				}
				if(PlayerPrefs.GetInt("hotdogServed") > 999 && PlayerPrefs.GetInt ("hotdogLevel3")==0)
				{
					PlayerPrefs.SetInt ("hotdogLevel3",1);
					_uiManager.achievment_text.SetActive(true);
					AchievementChild.check_claim++;
					PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
					Invoke(nameof(Stopa),4.0f);
				}
				if(tutorialOn)
				{
					tutorialOn = false;
					_usManager.firstCoins.tutorialOn = true;
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopup ("COLLECT THE COINS.",false,false , 4);
				}
				
				string myTypeToEatSub = "HOTDOG";
				for(int count = 0; count < customer.myOrder.Count; count++)
				{
					if(customer.myOrder[count].ToString().Contains(myTypeToEatSub.ToString()))
					{
						if(customer.myOrder[count] == myType)
						{
							wrongOrderGiven = false;

						}
						else
						{
							wrongOrderGiven = true;

						}
						customer.myOrder.RemoveAt(count);
					}
				
				}
				customer.RemoveOrderFromBoard (myType);



				customer.iHaveAMultipleTypeOrder = LevelManager.Orders.NONE;
			
				if(perfect)
				{
					if(customer.myOrder.Count > 0)
						customer.myWaitingTime-= 30;

					if(!wrongOrderGiven)
					{
						customer.coinsSpent+=_usManager.perfectHotDog;
						customer.perfect = true;
					}
					else
					{
						customer.coinsSpent+=(_usManager.perfectHotDog/2);
					}
				}
				else
				{
					if(customer.myOrder.Count > 0)
						customer.myWaitingTime-= 20;
				
					if(!wrongOrderGiven)
					{
						customer.coinsSpent+=_usManager.lessBakedHotdog;
					}
					else
					{
						customer.coinsSpent+=(_usManager.lessBakedHotdog/2);
					}
				}
			
				if(yellowSauce || redSauce)
				{
					customer.coinsSpent+=10;
				}
				if(customer.myWaitingTime < 0)
				{
					customer.myWaitingTime = 0;
				}
			
				if(customer.myOrder.Count <= 0)
					customer.MoveToEndPosition();
			
			}
			else
			{
				_levelSoundManager.dustbin.Play();
				_usManager.clickedHotDog = false;
				if(perfect)
					_uiManager.totalCoins-=_usManager.perfectHotDog;
				else
					_uiManager.totalCoins-=_usManager.lessBakedHotdog;
			
				_uiManager.coinsText.text = _uiManager.totalCoins.ToString ();
				if(_uiManager.totalCoins < 0)
				{
					_uiManager.totalCoins = 0;
					_uiManager.coinsText.text = "0";
				}
			}
			transform.position = myOriginalPos;
			transform.gameObject.SetActive(false);
		}

		private IEnumerator MoveToPosition()
		{

			float distance = Vector3.Distance (transform.position , myOriginalPos);
			float speed = 15;
			while(distance > 0.1f)
			{
				float step = speed * 0.02f;
				transform.position = Vector3.MoveTowards(transform.position, myOriginalPos, step);
				distance = Vector3.Distance (transform.position , myOriginalPos);
				yield return 0;
			}
			if(iAmSelected)
				mySelection.SetActive (true);
			transform.position = myOriginalPos;
		}


		private void OnTriggerStay(Collider other)
		{

			if(other.name.Contains ("customer") )
			{
				otherObject = other.gameObject;
				customer = other.GetComponent<Customer>();
				for(int i = 0 ; i< customer.myOrder.Count ; i++)
				{
					if(myType == customer.myOrder[i])
					{
						wrongOrderGiven = false;
						reachedCustomer = true;

						break;
					}
				}
				if(!reachedCustomer && customer.iHaveAMultipleTypeOrder != LevelManager.Orders.NONE)
				{
			
					wrongOrderGiven = true;
					reachedCustomer = true;
					myTypeToEat = customer.iHaveAMultipleTypeOrder;
				}
			}
			else if(other.name.Contains ("dustbin") && (US_Manager.tutorialEnd == true || Australia_Manager.tutorialEnd== true))
			{
				otherObject = other.gameObject;
				reachedCustomer = true;
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if(other.name.Contains ("customer") || other.name.Contains ("dustbin"))
			{
				wrongOrderGiven = false;
				reachedCustomer = false;
			}
		}
	}
}
