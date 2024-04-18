using System.Collections;
using _Project.Scripts.Managers;
using _Project.Scripts.Other;
using _Project.Scripts.UI_Scripts;
using _Project.Scripts.UI.Tutorial;
using UnityEngine;

namespace _Project.Scripts.Food
{
	public class Pizza : MonoBehaviour 
	{
		private Vector3 colliderSize;
		private bool canMove;
		private bool reachedDestination;
		private bool tutorialPick;
		private bool isPicked;
		private float perfectTimer = 6f;
		private float burningTimer = 12f;

		public bool wrongOrderGiven;
		public float heatingTimer = 0;
		public Customer customer;
		public bool cheese;
		public bool vegetable;
		public SpriteRenderer myToppings;
		public GameObject myCheese;
		public Availability myPlate;
		public Vector3 myOriginalPos , myTouchPos;
		public LevelManager.Orders myType;
		public bool perfect;
		public bool iAmSelected;
		public Vector3 myLocalScale;
		public GameObject mySelection;
		public bool tutorialOn;
		public bool isInOven , isBurnt;
		public ParticleSystem mySmoke , pizzaCompletelyBaked;
		public SpriteRenderer myRenderer;
		public BoxCollider myContainerCollider;
		public SpriteRenderer pizzadot;
		public GameObject error;
	
		private void Start()
		{
			UIManager._instance.n_Pizzas_served = PlayerPrefs.GetInt ("PizzaServed");
			myLocalScale = transform.localScale;
			myOriginalPos = transform.position;
			colliderSize = transform.GetComponent<BoxCollider> ().size;
		}

		private void OnDisable()
		{
			perfect =false;
			wrongOrderGiven = false;
			if(!isInOven)
			{
				cheese = false;
				vegetable  = false;
			}
			reachedDestination = false;
			myToppings.gameObject.SetActive (false);
			myCheese.gameObject.SetActive (false);
			transform.GetComponent<SpriteRenderer>().sprite = Italy_Manager._instance.pizzaBakedVariations[0];
			myType = LevelManager.Orders.NONE; 
			transform.localScale = myLocalScale;
			iAmSelected = false;
			mySelection.SetActive (false);
			heatingTimer = 0;
			isPicked = false;
			if(mySmoke != null)
			{
				mySmoke.gameObject.SetActive (false);
				mySmoke.Stop ();
			}
			isBurnt = false;
		}

		private void Update () 
		{
			if(!isBurnt && !isPicked && isInOven)
			{
				heatingTimer+=Time.deltaTime;
				if(heatingTimer > perfectTimer && heatingTimer <= burningTimer)
				{
					if(tutorialOn)
					{
						tutorialPick = true;
						UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
						UIManager._instance.tutorialPanelBg.OpenPopupItaly ("TAP OR DRAG THIS TO \n THE CUSTOMER.",false,false , 4);
						tutorialOn = false;
					}
					if(myRenderer.sprite == Italy_Manager._instance.pizzaBakedVariations[0])
					{
						perfect = true;
						pizzaCompletelyBaked.gameObject.SetActive(true);
						pizzaCompletelyBaked.Play ();
						myCheese.gameObject.SetActive (false);
						myToppings.gameObject.SetActive (false);
						if(myType == LevelManager.Orders.VEG_PIZZA)
						{
							myRenderer.sprite = Italy_Manager._instance.pizzaBakedVariations[1];
				
						}
						else
						{
							myRenderer.sprite = Italy_Manager._instance.pizzaBakedVariations[2];

						}
					}
				}
				else if(heatingTimer > burningTimer && !isBurnt)
				{

					if(!TutorialPanel.popupPanelActive)
					{
						perfect =false;
						myRenderer.sprite = Italy_Manager._instance.pizzaBakedVariations[3];
						isBurnt = true;  // burnt
						pizzaCompletelyBaked.Stop();
						pizzaCompletelyBaked.gameObject.SetActive(false);
						mySmoke.gameObject.SetActive (true);
						mySmoke.Play ();
					}
				}
			}
		}

		private void OnMouseDown()
		{
			if((!TutorialPanel.popupPanelActive || Italy_Manager.tutorialEnd || tutorialPick || (tutorialOn && !isInOven)) && (vegetable && cheese) )
			{
				isPicked = true;
				canMove = true;
				Italy_Manager._instance.AllClickedBoolsReset ();
				Italy_Manager._instance.clickedPizzaDestinationFunction = this;
				iAmSelected = true;
				if(isInOven)
				{
					transform.GetComponent<BoxCollider> ().size = new Vector3(colliderSize.x/2f , colliderSize.y/2f , colliderSize.z);
					Italy_Manager._instance.clickedOvenPizza = true;
				}
				else
					Italy_Manager._instance.clickedPlatePizza = true;
				
				if(tutorialPick)
				{
					Italy_Manager._instance.firstCustomer.tutorialOn = true;
				}
				Vector3 myPos = Camera.main.WorldToScreenPoint (transform.position);
				myTouchPos =  Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y , myPos.z));
			}
			else if((!TutorialPanel.popupPanelActive || !Italy_Manager.tutorialEnd) && (!vegetable || !cheese)) // Click on it to place ingredients
			{

				if(Italy_Manager._instance.clickedVeg || Italy_Manager._instance.clickedNonVeg && !vegetable)
				{
					Italy_Manager._instance.clickedItemDestinationFunction.availablePizza = this;
					Italy_Manager._instance.ObjectReached ();
					Italy_Manager._instance.AllClickedBoolsReset ();
				}
				else if(Italy_Manager._instance.clickedCheese && !cheese)
				{
					Italy_Manager._instance.clickedItemDestinationFunction.availablePizza = this;
					Italy_Manager._instance.ObjectReached ();
					Italy_Manager._instance.AllClickedBoolsReset ();
				}
				else
				{
					error.SetActive(true);
				}
			}

		}

		private void OnMouseDrag()
		{
			if( canMove)
			{
				error.SetActive(false);
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
			error.SetActive(false);
			if(canMove)
			{
				if(!reachedDestination)
					StartCoroutine(MoveToPosition());
				else
				{

					ClickedDestination ();
					transform.position = myOriginalPos;
					transform.gameObject.SetActive(false);
				}
				canMove = false;
			}
			transform.GetComponent<BoxCollider> ().size = colliderSize;
		}
		public void Stopa()
		{
			UIManager._instance.achievment_text.SetActive (false);
		}
		public void ClickedDestination()
		{
			myContainerCollider.enabled = true;
			myPlate.available = true;
			if(otherObject.name.Contains ("customer"))
			{
				UIManager._instance.n_Pizzas_served++;
				LevelSoundManager._instance.customerEat.Play();
				PlayerPrefs.SetInt ("PizzaServed",UIManager._instance.n_Pizzas_served);
			
			
				if(PlayerPrefs.GetInt("PizzaServed") > 9 && PlayerPrefs.GetInt ("PizzaLevel1")==0)
				{
					PlayerPrefs.SetInt ("PizzaLevel1",1);
					UIManager._instance.achievment_text.SetActive(true);
					AchievementChild.check_claim++;
					PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
					Invoke(nameof(Stopa),4.0f);
				}
				if(PlayerPrefs.GetInt("PizzaServed") > 99 && PlayerPrefs.GetInt ("PizzaLevel2")==0)
				{
					PlayerPrefs.SetInt ("PizzaLevel2",1);

					AchievementChild.check_claim++;
					PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
					UIManager._instance.achievment_text.SetActive(true);
					Invoke(nameof(Stopa),4.0f);
				}
				if(PlayerPrefs.GetInt("PizzaServed") > 999 && PlayerPrefs.GetInt ("PizzaLevel3")==0)
				{
					PlayerPrefs.SetInt ("PizzaLevel3",1);
					UIManager._instance.achievment_text.SetActive(true);
					AchievementChild.check_claim++;
					PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
					Invoke(nameof(Stopa),4.0f);
				}
				Italy_Manager._instance.platesFilledCount--;
				string myTypeToEatSub = "PIZZA";
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

				if(tutorialPick)
				{
					UIManager._instance.tutorialPanelBg.OpenPopupItaly ("PUT BURNT PIZZA \n IN THE DUSTBIN!",false,false , 7 , 1);
					tutorialPick = false;
				}
			
				customer.iHaveAMultipleTypeOrder = LevelManager.Orders.NONE;
				if(perfect)
				{
					if(customer.myOrder.Count > 0)
						customer.myWaitingTime-= 30;
				
					if(!wrongOrderGiven)
					{
						customer.coinsSpent+=Italy_Manager._instance.perfectPizza;
						customer.perfect = true;
					}
					else
					{
						customer.coinsSpent+=(Italy_Manager._instance.perfectPizza/2);
					}
				}
				else
				{
					if(customer.myOrder.Count > 0)
						customer.myWaitingTime-= 20;
					if(!wrongOrderGiven)
					{
						customer.coinsSpent+=Italy_Manager._instance.lessBakedPizza;
					}
					else
					{
						customer.coinsSpent+=(Italy_Manager._instance.lessBakedPizza/2);
					
					}
				}
				if(customer.myWaitingTime < 0)
				{
					customer.myWaitingTime = 0;
				}
			
				if(customer.myOrder.Count <= 0)
					customer.MoveToEndPosition();
			
			}
			else if(otherObject.name.Contains ("ovenPlace"))
			{
				if(tutorialOn)
				{
					pizzaDestinationAvailable = Italy_Manager._instance.firstOvenAvailabe;
					UIManager._instance.tutorialPanelBg.OpenPopupItaly ("WAIT FOR PIZZA \n TO BAKE.",false,false , 4);
					Italy_Manager._instance.ovenPizzas[pizzaDestinationAvailable.myPositionInArray].tutorialOn = true;
					tutorialOn = false;
				}

				Italy_Manager._instance.platesFilledCount--;
				pizzaDestinationAvailable.available = false;
				Italy_Manager._instance.ovenPizzaRenderer[pizzaDestinationAvailable.myPositionInArray].gameObject.SetActive (true);


				Italy_Manager._instance.ovenPizzas[pizzaDestinationAvailable.myPositionInArray].myType = myType;
				Italy_Manager._instance.ovenPizzas[pizzaDestinationAvailable.myPositionInArray].myCheese.SetActive (true);
				Italy_Manager._instance.ovenPizzas[pizzaDestinationAvailable.myPositionInArray].myToppings.gameObject.SetActive (true);
				if(myType == LevelManager.Orders.VEG_PIZZA)
					Italy_Manager._instance.ovenPizzas[pizzaDestinationAvailable.myPositionInArray].myToppings.sprite = Italy_Manager._instance.pizzaToppings[0];
				else
					Italy_Manager._instance.ovenPizzas[pizzaDestinationAvailable.myPositionInArray].myToppings.sprite = Italy_Manager._instance.pizzaToppings[1];
				
				Italy_Manager._instance.ovenColliders[pizzaDestinationAvailable.myPositionInArray].enabled = false;

			}
			else  
			{
				if(!isInOven)
					Italy_Manager._instance.platesFilledCount--;
				if(perfect)
				{
					UIManager._instance.totalCoins-=Italy_Manager._instance.perfectPizza;
					UIManager._instance.coinsText.text = UIManager._instance.totalCoins.ToString ();
					LevelSoundManager._instance.dustbin.Play();
					if(UIManager._instance.totalCoins > 0){
						UIManager._instance.dustbin_textparent.SetActive(true);
						UIManager._instance.dustbin_text.text = "-"+Italy_Manager._instance.perfectPizza.ToString(); 
						Invoke(nameof(Deactivedustbin),1.0f);
					}
					if(UIManager._instance.totalCoins < 0)
					{
						UIManager._instance.totalCoins = 0;
						UIManager._instance.coinsText.text = "0";
					}
				}
				else
				{
					UIManager._instance.totalCoins-=Italy_Manager._instance.lessBakedPizza;
					UIManager._instance.coinsText.text = UIManager._instance.totalCoins.ToString ();
					LevelSoundManager._instance.dustbin.Play();
					if(UIManager._instance.totalCoins > 0){
						UIManager._instance.dustbin_textparent.SetActive(true);
						UIManager._instance.dustbin_text.text = "-"+Italy_Manager._instance.lessBakedPizza.ToString(); 
						Invoke(nameof(Deactivedustbin),1.0f);
					}
					if(UIManager._instance.totalCoins < 0)
					{
						UIManager._instance.totalCoins = 0;
						UIManager._instance.coinsText.text = "0";
					}
				}
			
			}
			transform.position = myOriginalPos;
			transform.gameObject.SetActive(false);
		}
		public void Deactivedustbin()
		{
			UIManager._instance.dustbin_textparent.SetActive (false);
			UIManager._instance.dustbin_textparent.transform.position = UIManager._instance.dustbintextintialposition;
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
			isPicked = false;
			if(iAmSelected)
				mySelection.SetActive (true);
			transform.position = myOriginalPos;
		}
	
	
		public GameObject otherObject;
		public Availability pizzaDestinationAvailable;

		private void OnTriggerStay(Collider other)
		{
			if(!isInOven)
			{
				if(other.name.Contains ("ovenPlace"))
				{
					otherObject = other.gameObject;
					pizzaDestinationAvailable = other.GetComponent<Availability>();
					if(pizzaDestinationAvailable.available)
					{
						reachedDestination = true;
					}
				}
			}
			else
			{
				if(other.name.Contains ("customer") && !isBurnt)
				{
					otherObject = other.gameObject;
					customer = other.GetComponent<Customer>();
					for(int i = 0 ; i< customer.myOrder.Count ; i++)
					{
						if(myType == customer.myOrder[i])
						{
							wrongOrderGiven = false;
							reachedDestination = true;
							break;
						}
					}
					if(!reachedDestination && customer.iHaveAMultipleTypeOrder != LevelManager.Orders.NONE)
					{
						wrongOrderGiven = true;
						reachedDestination = true;
					}
				}
			}
			if(other.name.Contains ("dustbin") && Italy_Manager.tutorialEnd == true)
			{
				otherObject = other.gameObject;
				reachedDestination = true;
			}
		
		}

		private void OnTriggerExit(Collider other)
		{
			if(other.name.Contains ("customer") || other.name.Contains ("dustbin"))
			{
				otherObject = null;
				wrongOrderGiven = false;
				reachedDestination = false;
			}
			else if(other.name.Contains ("ovenPlace") )
			{
				if(otherObject != null)
				{
					if(otherObject == other.gameObject)
					{
						otherObject = null;
						wrongOrderGiven = false;
						reachedDestination = false;
					}
				}
				else
				{
					wrongOrderGiven = false;
					reachedDestination = false;
				}
			}
		
		}
	}
}
