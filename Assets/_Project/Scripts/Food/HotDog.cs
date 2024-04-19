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
		private bool reachedCustomer;
		private bool scaleUp;
		private Vector3 colliderSize;
		private bool canMove;
		private int a=1;
		[SerializeField] private GameObject errorObject;
		[SerializeField] private Availability myPlate;
		[SerializeField] private Vector3 myOriginalPos , myTouchPos;
		
		public Vector3 myLocalScale;
		public GameObject mySelection;
		public GameObject otherObject;
		public bool tikki;
		public bool tutorialOn { get; set; }
		public LevelManager.Orders myType { get; set; }
		public bool perfect { get; set; }
		public bool iAmSelected { get; set; }
		public bool wrongOrderGiven{ get; set; }
		public Wisitor customer{ get; set; }
		public bool redSauce { get; set; }
		public bool yellowSauce { get; set; }
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
					AchievementBlock._claimCheck++;
					PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
					Invoke(nameof(Stopa),4.0f);
				}
				if(PlayerPrefs.GetInt("hotdogServed") > 99 && PlayerPrefs.GetInt ("hotdogLevel2")==0)
				{
					PlayerPrefs.SetInt ("hotdogLevel2",1);
					_uiManager.achievment_text.SetActive(true);
					AchievementBlock._claimCheck++;
					PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
					Invoke(nameof(Stopa),4.0f);
				}
				if(PlayerPrefs.GetInt("hotdogServed") > 999 && PlayerPrefs.GetInt ("hotdogLevel3")==0)
				{
					PlayerPrefs.SetInt ("hotdogLevel3",1);
					_uiManager.achievment_text.SetActive(true);
					AchievementBlock._claimCheck++;
					PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
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
				for(int count = 0; count < customer._order.Count; count++)
				{
					if(customer._order[count].ToString().Contains(myTypeToEatSub.ToString()))
					{
						if(customer._order[count] == myType)
						{
							wrongOrderGiven = false;

						}
						else
						{
							wrongOrderGiven = true;

						}
						customer._order.RemoveAt(count);
					}
				
				}
				customer.RemoveOrderFromBoard (myType);



				customer.iHaveAMultipleTypeOrder = LevelManager.Orders.NONE;
			
				if(perfect)
				{
					if(customer._order.Count > 0)
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
					if(customer._order.Count > 0)
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
			
				if(customer._order.Count <= 0)
					customer.MoveToEnd();
			
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
				customer = other.GetComponent<Wisitor>();
				for(int i = 0 ; i< customer._order.Count ; i++)
				{
					if(myType == customer._order[i])
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
