using System.Collections;
using _Project.Scripts.Managers;
using _Project.Scripts.Other;
using _Project.Scripts.UI_Scripts;
using _Project.Scripts.UI.Tutorial;
using UnityEngine;

namespace _Project.Scripts.Food
{
	public class Burger : MonoBehaviour 
	{
		private bool scaleUp;
		private bool reachedCustomer;
		private Vector3 colliderSize;
		private bool canMove;
		
		public GameObject otherObject;
		public bool wrongOrderGiven;
		public Customer customer;
		public SpriteRenderer myTikki;
		public GameObject myTomato;
		public GameObject myOnion;
		public GameObject myCabbage;
		public bool tomato;
		public bool onion;
		public bool cabbage;
		public bool tikki;
		public Availability myPlate;
		public Vector3 myOriginalPos;
		public Vector3 myTouchPos;
		public LevelManager.Orders myType;
		public bool perfect;
		public bool iAmSelected;
		public Vector3 myLocalScale;
		public GameObject mySelection;
		public bool tutorialOn;
		public bool orderBurger;
		public GameObject error;

		private void Start()
		{
			UIManager._instance.n_Burger_served=PlayerPrefs.GetInt ("BurgerServed");
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
				Australia_Manager._instance.clickedHotDogDestinationFunction = this;
				canMove = true;
				Vector3 myPos = Camera.main.WorldToScreenPoint (transform.position);
				myTouchPos =  Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y , myPos.z));
				if(Australia_Manager._instance.clickedTikki && !tikki)
				{
					if(!Australia_Manager._instance.clickedTikkiDestinationFunction.isBurnt)
					{
						Australia_Manager._instance.clickedTikkiDestinationFunction.availableHotDog = this.GetComponent<Availability>();
						Australia_Manager._instance.TikkiReached ();

					}
					Australia_Manager._instance.AllClickedBoolsReset ();
				}
				else if(Australia_Manager._instance.clickedTomato && !tomato)
				{
					Australia_Manager._instance.clickedItemDestinationFunction.availableBurger = this;
					Australia_Manager._instance.ObjectReached ();
					Australia_Manager._instance.AllClickedBoolsReset ();
				}
				else if(Australia_Manager._instance.clickedOnion && !onion)
				{
					Australia_Manager._instance.clickedItemDestinationFunction.availableBurger = this;
					Australia_Manager._instance.ObjectReached ();
					Australia_Manager._instance.AllClickedBoolsReset ();
				}
				else if(Australia_Manager._instance.clickedCabbage && !cabbage)
				{
					Australia_Manager._instance.clickedItemDestinationFunction.availableBurger = this;
					Australia_Manager._instance.ObjectReached ();
					Australia_Manager._instance.AllClickedBoolsReset ();
				}
				else 
				{
					if(tikki)
					{
						if(tutorialOn)
						{
							Australia_Manager._instance.firstCustomer.tutorialOn = true;
						}
						Australia_Manager._instance.AllClickedBoolsReset ();
						Australia_Manager._instance.clickedBurger = true;
						
						scaleUp = true;
						if(!orderBurger)
							transform.GetComponent<BoxCollider> ().size = new Vector3(colliderSize.x/2f , colliderSize.y/2f , colliderSize.z);
						iAmSelected = true;
					}
					else
					{
						Australia_Manager._instance.clickedHotDogDestinationFunction = null;
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
			UIManager._instance.achievment_text.SetActive (false);
		}
		
		public void ClickedDestination()
		{
			Australia_Manager._instance.platesFilledCount--;
			myPlate.available = true;
			if(!otherObject.name.Contains ("dustbin"))
			{
				UIManager._instance.n_Burger_served++ ;
				LevelSoundManager._instance.customerEat.Play();
				PlayerPrefs.SetInt ("BurgerServed",UIManager._instance.n_Burger_served);
				if(PlayerPrefs.GetInt("BurgerServed") > 9 && PlayerPrefs.GetInt ("BurgerLevel1")==0 )
				{
					PlayerPrefs.SetInt ("BurgerLevel1",1);
					UIManager._instance.achievment_text.SetActive(true);
					AchievementChild.check_claim++;
					PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
					Invoke(nameof(Stopa),4.0f);
				}
				if(PlayerPrefs.GetInt("BurgerServed") > 99 && PlayerPrefs.GetInt ("BurgerLevel2")==1)
				{
					PlayerPrefs.SetInt ("BurgerLevel2",1);
					UIManager._instance.achievment_text.SetActive(true);
					AchievementChild.check_claim++;
					PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
					Invoke(nameof(Stopa),4.0f);
				}
				if(PlayerPrefs.GetInt("BurgerServed") > 999 && PlayerPrefs.GetInt ("BurgerLevel3")==2)
				{
					PlayerPrefs.SetInt ("BurgerLevel3",1);
					UIManager._instance.achievment_text.SetActive(true);
					AchievementChild.check_claim++;
					PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
					Invoke(nameof(Stopa),4.0f);
				}
				if(tutorialOn)
				{
					tutorialOn = false;
					UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
					UIManager._instance.tutorialPanelBg.OpenPopupAustralia ("PUT BURNT TIKKI \n INTO THE DUSTBIN!",false,false ,7 , 1);
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
						customer.coinsSpent+=Australia_Manager._instance.perfectBurger;
						customer.perfect = true;
					}
					else
					{
						customer.coinsSpent+=(Australia_Manager._instance.perfectBurger/2);
					
					}
				
				}
				else
				{
					if(customer.myOrder.Count > 0)
						customer.myWaitingTime-= 20;
				
					if(!wrongOrderGiven)
					{
						customer.coinsSpent+=Australia_Manager._instance.lessBakedBurger;
					}
					else
					{
						customer.coinsSpent+=(Australia_Manager._instance.lessBakedBurger/2);
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
					UIManager._instance.totalCoins-=Australia_Manager._instance.perfectBurger;
					LevelSoundManager._instance.dustbin.Play();
					if(UIManager._instance.totalCoins > 0){

						UIManager._instance.dustbin_textparent.SetActive(true);

						UIManager._instance.dustbin_text.text = "-"+Australia_Manager._instance.perfectBurger.ToString(); 
						Invoke(nameof(Deactivedustbin),1.0f);
					}}
				else
				{
					UIManager._instance.totalCoins-=Australia_Manager._instance.lessBakedBurger;
					LevelSoundManager._instance.dustbin.Play();
					UIManager._instance.coinsText.text = UIManager._instance.totalCoins.ToString ();
					if(UIManager._instance.totalCoins > 0){

						UIManager._instance.dustbin_textparent.SetActive(true);
						UIManager._instance.dustbin_text.text = "-"+Australia_Manager._instance.lessBakedBurger.ToString(); 
						Invoke(nameof(Deactivedustbin),1.0f);
					}}
				if(UIManager._instance.totalCoins < 0)
				{
					UIManager._instance.totalCoins = 0;
					UIManager._instance.coinsText.text = "0";
				}
			}
			transform.position = myOriginalPos;
			transform.gameObject.SetActive(false);
		}

		public void Deactivedustbin()
		{
			UIManager._instance.dustbin_textparent.SetActive (false);
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
