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
	public class Burger : MonoBehaviour 
	{
		[Inject] private LevelSoundManager _levelSoundManager;
		[Inject] private Australia_Manager _australiaManager;
		[Inject] private UIManager _uiManager;   
		private bool scaleUp;
		private bool reachedCustomer;
		private Vector3 colliderSize;
		private bool canMove;
		
		public SpriteRenderer myTikki;
		public GameObject myTomato;
		public GameObject myOnion;
		public GameObject myCabbage;
		public GameObject mySelection;
		
		[SerializeField] private Availability myPlate;
		[SerializeField] private Vector3 myOriginalPos;
		[SerializeField] private Vector3 myTouchPos;
		[SerializeField] private Vector3 myLocalScale;
		[SerializeField] private bool orderBurger;
		[SerializeField] private GameObject error;
		public bool tomato { get; set; }
		public bool onion { get; set; }
		public bool cabbage { get; set; }
		public bool tikki { get; set; }
		public LevelManager.Orders myType { get; set; }
		public bool perfect { get; set; }
		public bool iAmSelected { get; set; }
		public bool tutorialOn { get; set; }
		public GameObject otherObject { get; set; }
		public bool wrongOrderGiven { get; set; }
		public Customer customer { get; set; }
		private void Start()
		{
			_uiManager.n_Burger_served=PlayerPrefs.GetInt ("BurgerServed");
			myLocalScale = transform.localScale;
			myOriginalPos = transform.position;
			if(!orderBurger)
				colliderSize = transform.GetComponent<BoxCollider> ().size;
		}

		private void OnDisable()
		{
			wrongOrderGiven = false;
			tomato = false;
			onion  = false;
			cabbage  = false;
			tikki  = false;
			reachedCustomer = false;


			myTomato.SetActive (false);
			myCabbage.SetActive (false);
			myOnion.SetActive (false);

			if(transform.GetComponent<Availability>())
			{
				myTikki.gameObject.SetActive (false);
				transform.GetComponent<Availability>().available = true;
			}
			myType = LevelManager.Orders.NONE; 
			transform.localScale = myLocalScale;

			iAmSelected = false;
			if(mySelection != null)
				mySelection.SetActive (false);
			
		}
		
		private void OnMouseDown()
		{
			if(!TutorialPanel.popupPanelActive || Australia_Manager.tutorialEnd || Australia_Manager.tutorialEnd || tutorialOn)
			{
				_australiaManager.clickedHotDogDestinationFunction = this;
				canMove = true;
				Vector3 myPos = Camera.main.WorldToScreenPoint (transform.position);
				myTouchPos =  Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y , myPos.z));
				if(_australiaManager.clickedTikki && !tikki)
				{
					if(!_australiaManager.clickedTikkiDestinationFunction.isBurnt)
					{
						_australiaManager.clickedTikkiDestinationFunction.availableHotDog = this.GetComponent<Availability>();
						_australiaManager.TikkiReached ();

					}
					_australiaManager.AllClickedBoolsReset ();
				}
				else if(_australiaManager.clickedTomato && !tomato)
				{
					_australiaManager.clickedItemDestinationFunction.availableBurger = this;
					_australiaManager.ObjectReached ();
					_australiaManager.AllClickedBoolsReset ();
				}
				else if(_australiaManager.clickedOnion && !onion)
				{
					_australiaManager.clickedItemDestinationFunction.availableBurger = this;
					_australiaManager.ObjectReached ();
					_australiaManager.AllClickedBoolsReset ();
				}
				else if(_australiaManager.clickedCabbage && !cabbage)
				{
					_australiaManager.clickedItemDestinationFunction.availableBurger = this;
					_australiaManager.ObjectReached ();
					_australiaManager.AllClickedBoolsReset ();
				}
				else 
				{
					if(tikki)
					{
						if(tutorialOn)
						{
							_australiaManager.firstCustomer.tutorialOn = true;
						}
						_australiaManager.AllClickedBoolsReset ();
						_australiaManager.clickedBurger = true;
						
						scaleUp = true;
						if(!orderBurger)
							transform.GetComponent<BoxCollider> ().size = new Vector3(colliderSize.x/2f , colliderSize.y/2f , colliderSize.z);
						iAmSelected = true;
					}
					else
					{
						_australiaManager.clickedHotDogDestinationFunction = null;
						canMove = false;
						error.SetActive(true);
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
			error.SetActive (false);
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
			if(!orderBurger)
				transform.GetComponent<BoxCollider> ().size = colliderSize;
		}
		
		public void Stopa()
		{
			_uiManager.achievment_text.SetActive (false);
		}
		
		public void ClickedDestination()
		{
			_australiaManager.platesFilledCount--;
			myPlate.available = true;
			if(!otherObject.name.Contains ("dustbin"))
			{
				_uiManager.n_Burger_served++ ;
				_levelSoundManager.customerEat.Play();
				PlayerPrefs.SetInt ("BurgerServed", _uiManager.n_Burger_served);
				if(PlayerPrefs.GetInt("BurgerServed") > 9 && PlayerPrefs.GetInt ("BurgerLevel1")==0 )
				{
					PlayerPrefs.SetInt ("BurgerLevel1",1);
					_uiManager.achievment_text.SetActive(true);
					AchievementBlock._claimCheck++;
					PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
					Invoke(nameof(Stopa),4.0f);
				}
				if(PlayerPrefs.GetInt("BurgerServed") > 99 && PlayerPrefs.GetInt ("BurgerLevel2")==1)
				{
					PlayerPrefs.SetInt ("BurgerLevel2",1);
					_uiManager.achievment_text.SetActive(true);
					AchievementBlock._claimCheck++;
					PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
					Invoke(nameof(Stopa),4.0f);
				}
				if(PlayerPrefs.GetInt("BurgerServed") > 999 && PlayerPrefs.GetInt ("BurgerLevel3")==2)
				{
					PlayerPrefs.SetInt ("BurgerLevel3",1);
					_uiManager.achievment_text.SetActive(true);
					AchievementBlock._claimCheck++;
					PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
					Invoke(nameof(Stopa),4.0f);
				}
				if(tutorialOn)
				{
					tutorialOn = false;
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopupAustralia ("PUT BURNT TIKKI \n INTO THE DUSTBIN!",false,false ,7 , 1);
				}
				
				string myTypeToEatSub = "BURGER";
				
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
						customer.coinsSpent+=_australiaManager.perfectBurger;
						customer.perfect = true;
					}
					else
					{
						customer.coinsSpent+=(_australiaManager.perfectBurger/2);
					
					}
				
				}
				else
				{
					if(customer.myOrder.Count > 0)
						customer.myWaitingTime-= 20;
				
					if(!wrongOrderGiven)
					{
						customer.coinsSpent+=_australiaManager.lessBakedBurger;
					}
					else
					{
						customer.coinsSpent+=(_australiaManager.lessBakedBurger/2);
					}
				}
			
				if(cabbage)
				{
					customer.coinsSpent+=10;
				}
				if(tomato)
				{
					customer.coinsSpent+=10;
				}
				if(onion)
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
				if(perfect){
					_uiManager.totalCoins-=_australiaManager.perfectBurger;
					_levelSoundManager.dustbin.Play();
					if(_uiManager.totalCoins > 0){

						_uiManager.dustbin_textparent.SetActive(true);
						_uiManager.dustbin_text.text = "-"+_australiaManager.perfectBurger.ToString(); 
						Invoke(nameof(Deactivedustbin),1.0f);
					}}
				else
				{
					_uiManager.totalCoins-=_australiaManager.lessBakedBurger;
					_levelSoundManager.dustbin.Play();
					_uiManager.coinsText.text = _uiManager.totalCoins.ToString ();
					if(_uiManager.totalCoins > 0){

						_uiManager.dustbin_textparent.SetActive(true);
						_uiManager.dustbin_text.text = "-"+_australiaManager.lessBakedBurger.ToString(); 
						Invoke(nameof(Deactivedustbin),1.0f);
					}}
				if(_uiManager.totalCoins < 0)
				{
					_uiManager.totalCoins = 0;
					_uiManager.coinsText.text = "0";
				}
			}
			transform.position = myOriginalPos;
			transform.gameObject.SetActive(false);
		}

		public void Deactivedustbin()
		{
			_uiManager.dustbin_textparent.SetActive (false);
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
			if(other.name.Contains ("customer"))
			{

				otherObject = other.gameObject;
				customer = other.GetComponent<Customer>();
				for(int i = 0 ; i< customer.myOrder.Count ; i++)
				{
					if(myType == customer.myOrder[i])
					{
						wrongOrderGiven = false;
						reachedCustomer = true;
						wrongOrderGiven = false;
						break;
					}
				}
				if(!reachedCustomer && customer.iHaveAMultipleTypeOrder != LevelManager.Orders.NONE)
				{
					wrongOrderGiven = true;
					reachedCustomer = true;
				}
			}
			else if(other.name.Contains ("dustbin") && Australia_Manager.tutorialEnd== true )
			{
				wrongOrderGiven = false;
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
