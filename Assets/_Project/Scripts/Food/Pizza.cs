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
	public class Pizza : MonoBehaviour
	{
		[Inject] private LevelSoundManager _levelSoundManager;
		[Inject] private Italy_Manager _italyManager;
		[Inject] private UIManager _uiManager;   
		private Vector3 colliderSize;
		private bool canMove;
		private bool reachedDestination;
		private bool tutorialPick;
		private bool isPicked;
		private float perfectTimer = 6f;
		private float burningTimer = 12f;

		public bool wrongOrderGiven { get; set; }
		public float heatingTimer { get; set; }
		public Customer customer{ get; set; }
		public bool cheese;
		public bool vegetable;
		public SpriteRenderer myToppings;
		public GameObject myCheese;
		public Availability myPlate;
		public Vector3 myOriginalPos , myTouchPos;
		public LevelManager.Orders myType { get; set; }
		public bool perfect{ get; set; }
		public bool iAmSelected{ get; set; }
		public Vector3 myLocalScale;
		public GameObject mySelection;
		public bool tutorialOn{ get; set; }
		public bool isInOven , isBurnt;
		public ParticleSystem mySmoke , pizzaCompletelyBaked;
		public SpriteRenderer myRenderer;
		public BoxCollider myContainerCollider;
		public SpriteRenderer pizzadot;
		public GameObject error;
	
		private void Start()
		{
			_uiManager.n_Pizzas_served = PlayerPrefs.GetInt ("PizzaServed");
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
			transform.GetComponent<SpriteRenderer>().sprite = _italyManager.pizzaBakedVariations[0];
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
						_uiManager.tutorialPanelBg.gameObject.SetActive (true);
						_uiManager.tutorialPanelBg.OpenPopupItaly ("TAP OR DRAG THIS TO \n THE CUSTOMER.",false,false , 4);
						tutorialOn = false;
					}
					if(myRenderer.sprite == _italyManager.pizzaBakedVariations[0])
					{
						perfect = true;
						pizzaCompletelyBaked.gameObject.SetActive(true);
						pizzaCompletelyBaked.Play ();
						myCheese.gameObject.SetActive (false);
						myToppings.gameObject.SetActive (false);
						if(myType == LevelManager.Orders.VEG_PIZZA)
						{
							myRenderer.sprite = _italyManager.pizzaBakedVariations[1];
				
						}
						else
						{
							myRenderer.sprite = _italyManager.pizzaBakedVariations[2];

						}
					}
				}
				else if(heatingTimer > burningTimer && !isBurnt)
				{

					if(!TutorialPanel.popupPanelActive)
					{
						perfect =false;
						myRenderer.sprite = _italyManager.pizzaBakedVariations[3];
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
				_italyManager.AllClickedBoolsReset ();
				_italyManager.clickedPizzaDestinationFunction = this;
				iAmSelected = true;
				if(isInOven)
				{
					transform.GetComponent<BoxCollider> ().size = new Vector3(colliderSize.x/2f , colliderSize.y/2f , colliderSize.z);
					_italyManager.clickedOvenPizza = true;
				}
				else
					_italyManager.clickedPlatePizza = true;
				
				if(tutorialPick)
				{
					_italyManager.firstCustomer.tutorialOn = true;
				}
				Vector3 myPos = Camera.main.WorldToScreenPoint (transform.position);
				myTouchPos =  Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y , myPos.z));
			}
			else if((!TutorialPanel.popupPanelActive || !Italy_Manager.tutorialEnd) && (!vegetable || !cheese)) // Click on it to place ingredients
			{

				if(_italyManager.clickedVeg || _italyManager.clickedNonVeg && !vegetable)
				{
					_italyManager.clickedItemDestinationFunction.availablePizza = this;
					_italyManager.ObjectReached ();
					_italyManager.AllClickedBoolsReset ();
				}
				else if(_italyManager.clickedCheese && !cheese)
				{
					_italyManager.clickedItemDestinationFunction.availablePizza = this;
					_italyManager.ObjectReached ();
					_italyManager.AllClickedBoolsReset ();
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
			_uiManager.achievment_text.SetActive (false);
		}
		public void ClickedDestination()
		{
			myContainerCollider.enabled = true;
			myPlate.available = true;
			if(otherObject.name.Contains ("customer"))
			{
				_uiManager.n_Pizzas_served++;
				_levelSoundManager.customerEat.Play();
				PlayerPrefs.SetInt ("PizzaServed", _uiManager.n_Pizzas_served);
			
			
				if(PlayerPrefs.GetInt("PizzaServed") > 9 && PlayerPrefs.GetInt ("PizzaLevel1")==0)
				{
					PlayerPrefs.SetInt ("PizzaLevel1",1);
					_uiManager.achievment_text.SetActive(true);
					AchievementBlock._claimCheck++;
					PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
					Invoke(nameof(Stopa),4.0f);
				}
				if(PlayerPrefs.GetInt("PizzaServed") > 99 && PlayerPrefs.GetInt ("PizzaLevel2")==0)
				{
					PlayerPrefs.SetInt ("PizzaLevel2",1);

					AchievementBlock._claimCheck++;
					PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
					_uiManager.achievment_text.SetActive(true);
					Invoke(nameof(Stopa),4.0f);
				}
				if(PlayerPrefs.GetInt("PizzaServed") > 999 && PlayerPrefs.GetInt ("PizzaLevel3")==0)
				{
					PlayerPrefs.SetInt ("PizzaLevel3",1);
					_uiManager.achievment_text.SetActive(true);
					AchievementBlock._claimCheck++;
					PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
					Invoke(nameof(Stopa),4.0f);
				}
				_italyManager.platesFilledCount--;
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
					_uiManager.tutorialPanelBg.OpenPopupItaly ("PUT BURNT PIZZA \n IN THE DUSTBIN!",false,false , 7 , 1);
					tutorialPick = false;
				}
			
				customer.iHaveAMultipleTypeOrder = LevelManager.Orders.NONE;
				if(perfect)
				{
					if(customer.myOrder.Count > 0)
						customer.myWaitingTime-= 30;
				
					if(!wrongOrderGiven)
					{
						customer.coinsSpent+=_italyManager.perfectPizza;
						customer.perfect = true;
					}
					else
					{
						customer.coinsSpent+=(_italyManager.perfectPizza/2);
					}
				}
				else
				{
					if(customer.myOrder.Count > 0)
						customer.myWaitingTime-= 20;
					if(!wrongOrderGiven)
					{
						customer.coinsSpent+=_italyManager.lessBakedPizza;
					}
					else
					{
						customer.coinsSpent+=(_italyManager.lessBakedPizza/2);
					
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
					pizzaDestinationAvailable = _italyManager.firstOvenAvailabe;
					_uiManager.tutorialPanelBg.OpenPopupItaly ("WAIT FOR PIZZA \n TO BAKE.",false,false , 4);
					_italyManager.ovenPizzas[pizzaDestinationAvailable.myPositionInArray].tutorialOn = true;
					tutorialOn = false;
				}

				_italyManager.platesFilledCount--;
				pizzaDestinationAvailable.available = false;
				_italyManager.ovenPizzaRenderer[pizzaDestinationAvailable.myPositionInArray].gameObject.SetActive (true);


				_italyManager.ovenPizzas[pizzaDestinationAvailable.myPositionInArray].myType = myType;
				_italyManager.ovenPizzas[pizzaDestinationAvailable.myPositionInArray].myCheese.SetActive (true);
				_italyManager.ovenPizzas[pizzaDestinationAvailable.myPositionInArray].myToppings.gameObject.SetActive (true);
				if(myType == LevelManager.Orders.VEG_PIZZA)
					_italyManager.ovenPizzas[pizzaDestinationAvailable.myPositionInArray].myToppings.sprite = _italyManager.pizzaToppings[0];
				else
					_italyManager.ovenPizzas[pizzaDestinationAvailable.myPositionInArray].myToppings.sprite = _italyManager.pizzaToppings[1];
				
				_italyManager.ovenColliders[pizzaDestinationAvailable.myPositionInArray].enabled = false;

			}
			else  
			{
				if(!isInOven)
					_italyManager.platesFilledCount--;
				if(perfect)
				{
					_uiManager.totalCoins-= _italyManager.perfectPizza;
					_uiManager.coinsText.text = _uiManager.totalCoins.ToString ();
					_levelSoundManager.dustbin.Play();
					if(_uiManager.totalCoins > 0){
						_uiManager.dustbin_textparent.SetActive(true);
						_uiManager.dustbin_text.text = "-"+_italyManager.perfectPizza.ToString(); 
						Invoke(nameof(Deactivedustbin),1.0f);
					}
					if(_uiManager.totalCoins < 0)
					{
						_uiManager.totalCoins = 0;
						_uiManager.coinsText.text = "0";
					}
				}
				else
				{
					_uiManager.totalCoins-=_italyManager.lessBakedPizza;
					_uiManager.coinsText.text = _uiManager.totalCoins.ToString ();
					_levelSoundManager.dustbin.Play();
					if(_uiManager.totalCoins > 0){
						_uiManager.dustbin_textparent.SetActive(true);
						_uiManager.dustbin_text.text = "-"+_italyManager.lessBakedPizza.ToString(); 
						Invoke(nameof(Deactivedustbin),1.0f);
					}
					if(_uiManager.totalCoins < 0)
					{
						_uiManager.totalCoins = 0;
						_uiManager.coinsText.text = "0";
					}
				}
			
			}
			transform.position = myOriginalPos;
			transform.gameObject.SetActive(false);
		}
		public void Deactivedustbin()
		{
			_uiManager.dustbin_textparent.SetActive (false);
			_uiManager.dustbin_textparent.transform.position = _uiManager.dustbintextintialposition;
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
