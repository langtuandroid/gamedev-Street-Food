using System.Collections;
using _Project.Scripts.Achivments;
using _Project.Scripts.Additional;
using _Project.Scripts.Entities.Customers;
using _Project.Scripts.Food;
using _Project.Scripts.Game;
using _Project.Scripts.Managers;
using _Project.Scripts.UI_Scripts;
using _Project.Scripts.UI.Tutorial;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Other
{
	public class ObjectMotion : MonoBehaviour 
	{
		[Inject] private LevelSoundManager _levelSoundManager;
		[Inject] private US_Manager _usManager;
		[Inject] private Italy_Manager _italyManager;
		[Inject] private China_Manager _chinaManager;
		[Inject] private Australia_Manager _australiaManager;
		[Inject] private UIManager _uiManager;   
		private bool reachedDestination;
		private bool scaleUp;
		private bool _canMove;
		private GameObject otherObject;

		public ChineseUtensils utensil;
		public bool isRedSauce;
		public bool isYellowSauce;
		public HotDog availableHotDog;
		public bool isCoke;
		public Availability myParentHolder;
		public LevelManager.Orders myType = LevelManager.Orders.COKE;
		public bool isChilled;
		public bool isNoodlesToCook;
		public bool isSoupVeg;
		public bool isNoodlesVeg;
		public bool isSoupBowl;
		public GameObject mySoup;
		public TweenAlpha soupScale;
		public bool isNoodlesPlate;
		public GameObject myNoodles;
		public bool isPizzaVeg;
		public bool isPizzaNonVeg;
		public bool isCheese;
		public Pizza availablePizza;
		public Burger availableBurger;
		public bool isTomato;
		public bool isOnion;
		public bool isCabbage;
		public bool isFries;
		public Customer customer;
		public bool isCupCake;
		public ParticleSystem newCupcakeCame;
		public Vector3 myOriginalPos , myTouchPos;
		public bool iAmSelected , startAnimating;
		public Vector3 myLocalScale;
		public GameObject mySelection;
		public bool  perfect;
		public bool tutorialOn;
		public Vector3 colliderSize;

		private void Start () 
		{
			colliderSize = transform.GetComponent<BoxCollider> ().size;
			_uiManager.n_Cokes_served = PlayerPrefs.GetInt ("CokesServed");
			_uiManager.n_Noodles_served = PlayerPrefs.GetInt ("NoodlesServed");
			_uiManager.n_French_fries_served = PlayerPrefs.GetInt ("FrenchfriesServed");
			myOriginalPos = transform.position;
			myLocalScale = transform.localScale;
		}


		private void OnDisable()
		{
			isChilled = false;
			perfect = false;
			reachedDestination = false;
			startAnimating = false;
			iAmSelected = false;
			customer = null;
			mySelection.SetActive (false);
			if(isNoodlesPlate || isSoupBowl)
			{
				myType = LevelManager.Orders.NONE;
			}
			else if (!isFries)
			{
				transform.localScale = Vector3.one;
			}
		}
	
		public void Stopa()
		{
			_uiManager.achievment_text.SetActive (false);
		}
		

		private void OnMouseDown()
		{
			if(!TutorialPanel.popupPanelActive || US_Manager.tutorialEnd || China_Manager.tutorialEnd || Italy_Manager.tutorialEnd || tutorialOn || Australia_Manager.tutorialEnd)
			{
				if(isCoke)
				{
					if(!myParentHolder.available)
					{
						startAnimating = false;
						scaleUp = true;
						if(_usManager != null)
						{
							_usManager.AllClickedBoolsReset ();
							_usManager.clickedItemDestinationFunction = this;
							_usManager.clickedCoke = true;
						}
						else if(_italyManager != null)
						{
							_italyManager.AllClickedBoolsReset ();
							_italyManager.clickedItemDestinationFunction = this;
							_italyManager.clickedCoke = true;
						}
						else if(_australiaManager != null)
						{
							_australiaManager.AllClickedBoolsReset ();
							_australiaManager.clickedItemDestinationFunction = this;
							_australiaManager.clickedCoke = true;
						}
						iAmSelected = true;
						_canMove = true;
					}
					else
						_canMove = false;
				}
				else
				{
					startAnimating = false;
					scaleUp = true;
					_canMove = true;
					if(isRedSauce)
					{
						_usManager.AllClickedBoolsReset ();
						_usManager.clickedItemDestinationFunction = this;
						_usManager.clickedRedSauce = true;
						iAmSelected = true;

					}
					else if(isYellowSauce)
					{
						_usManager.AllClickedBoolsReset ();
						_usManager.clickedItemDestinationFunction = this;
						_usManager.clickedYellowSauce = true;
						iAmSelected = true;
					}
					else if(isNoodlesToCook)
					{
						_chinaManager.AllClickedBoolsReset ();
						_chinaManager.clickedItemDestinationFunction = this;
						_chinaManager.clickedNoodlesToCook = true;
						iAmSelected = true;
					}
					else if(isNoodlesVeg)
					{
						_chinaManager.AllClickedBoolsReset ();
						_chinaManager.clickedItemDestinationFunction = this;
						_chinaManager.clickedNoodlesVeg = true;
						iAmSelected = true;
					}
					else if(isSoupVeg)
					{
						_chinaManager.AllClickedBoolsReset ();
						_chinaManager.clickedItemDestinationFunction = this;
						_chinaManager.clickedSoupVeg = true;
						iAmSelected = true;
					}
					else if(isNoodlesPlate)
					{
						if(_chinaManager.clickedPan)
						{
							if(!_chinaManager.clickedUtensilsDestinationFunction.isBurnt)
							{
								_chinaManager.clickedUtensilsDestinationFunction.otherObject = this.gameObject;
								_chinaManager.UtensilReached ();
							}
							_chinaManager.AllClickedBoolsReset ();
						}
						else
						{
							if((tutorialOn && !_chinaManager.panUtensil[0].tutorialOn ) || China_Manager.tutorialEnd)
							{
								transform.GetComponent<BoxCollider> ().size = new Vector3(colliderSize.x/2f , colliderSize.y/2f , colliderSize.z);
								_chinaManager.AllClickedBoolsReset ();
								_chinaManager.clickedItemDestinationFunction = this;
								_chinaManager.clickedNoodlePlate = true;
								iAmSelected = true;
							}
							else
							{
								_canMove = false;
							}
						}
					}
					else if(isSoupBowl)
					{
						if(_chinaManager.clickedSoupContainer)
						{
							_chinaManager.clickedUtensilsDestinationFunction.otherObject = this.gameObject;
							_chinaManager.UtensilReached ();
							_chinaManager.AllClickedBoolsReset ();
						}
						else
						{
							transform.GetComponent<BoxCollider> ().size = new Vector3(colliderSize.x/2f , colliderSize.y/2f , colliderSize.z);
							_chinaManager.AllClickedBoolsReset ();
							_chinaManager.clickedItemDestinationFunction = this;
							_chinaManager.clickSoupBowl = true;
							iAmSelected = true;
						}
					}
					else if(isPizzaVeg)
					{
						_italyManager.AllClickedBoolsReset ();
						_italyManager.clickedItemDestinationFunction = this;
						_italyManager.clickedVeg = true;
						iAmSelected = true;
					}
					else if(isPizzaNonVeg)
					{
						_italyManager.AllClickedBoolsReset ();
						_italyManager.clickedItemDestinationFunction = this;
						_italyManager.clickedNonVeg = true;
						iAmSelected = true;
					}
					else if(isCheese)
					{
						_italyManager.AllClickedBoolsReset ();
						_italyManager.clickedItemDestinationFunction = this;
						_italyManager.clickedCheese = true;
						iAmSelected = true;
					}
					else if(isTomato)
					{
						_australiaManager.AllClickedBoolsReset ();
						_australiaManager.clickedItemDestinationFunction = this;
						_australiaManager.clickedTomato = true;
						iAmSelected = true;
					}
					else if(isOnion)
					{
						_australiaManager.AllClickedBoolsReset ();
						_australiaManager.clickedItemDestinationFunction = this;
						_australiaManager.clickedOnion = true;
						iAmSelected = true;
					}
					else if(isCabbage)
					{
						_australiaManager.AllClickedBoolsReset ();
						_australiaManager.clickedItemDestinationFunction = this;
						_australiaManager.clickedCabbage = true;
						iAmSelected = true;
					}
					else if(isFries)
					{
						_australiaManager.AllClickedBoolsReset ();
						_australiaManager.clickedItemDestinationFunction = this;
						_australiaManager.cickedFries = true;
						iAmSelected = true;
					}
				}

				if(_canMove)
				{
					Vector3 myPos = Camera.main.WorldToScreenPoint (transform.position);
					myTouchPos =  Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y , myPos.z));

				}
			}
		}

		private void OnMouseDrag()
		{
			if( _canMove)
			{
				Vector3 myPos = Camera.main.WorldToScreenPoint (transform.position);
				Vector3 newPos = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y , myPos.z));
				if(Vector3.Distance (newPos,myTouchPos) > 0.2f)
				{
					transform.position =  newPos;
					if(isYellowSauce || isRedSauce)
					{
						transform.localEulerAngles = new Vector3(0,0,60);
					}
				}

			}
		}

		private void OnMouseUp()
		{
			if(_canMove)
			{

				startAnimating = true;
			
				if(!reachedDestination)
					StartCoroutine(MoveToPosition());
				else
				{
					if(isCoke)
					{
						CokeReachedDestination();
					}
					else if(isYellowSauce)
					{
						YellowSauceReachedDestination ();
						StartCoroutine(MoveToPosition(false));
					}
					else if(isRedSauce)
					{
						RedSauceReachedDestination();
						StartCoroutine(MoveToPosition(false));
					}
					else if(isCupCake)
					{
						CupCakeReachedDestination();
					}
					else if(isNoodlesToCook)
					{
						NoodlesToCookReachedDestination();
						iAmSelected = false;
						startAnimating = false;
						StartCoroutine(MoveToPosition(false));
					}
					else if(isNoodlesVeg)
					{
						VegForNoodlesReachedDestination();
						iAmSelected = false;
						startAnimating = false;
						StartCoroutine(MoveToPosition(false));
					}
					else if(isSoupVeg)
					{
						VegForSoupReachedDestination ();
						iAmSelected = false;
						startAnimating = false;
						StartCoroutine(MoveToPosition(false));;
					}
					else if(isNoodlesPlate)
					{
						NoodlesPlateReachedDestination();
					}
					else if(isSoupBowl)
					{
						SoupBowlReachedDestination ();
					}
					else if(isPizzaVeg)
					{
						PizzaVegReachedDestination();
						iAmSelected = false;
						startAnimating = false;
						StartCoroutine(MoveToPosition(false));
					}
					else if(isPizzaNonVeg)
					{
						PizzaNonVegReachedDestination ();
						iAmSelected = false;
						startAnimating = false;
						StartCoroutine(MoveToPosition(false));;
					}
					else if(isCheese)
					{
						PizzaCheeseReachedDestination ();
						iAmSelected = false;
						startAnimating = false;
						StartCoroutine(MoveToPosition(false));;
					}
					else if(isTomato)
					{
						TomatoReachedDestination();
						iAmSelected = false;
						startAnimating = false;
						StartCoroutine(MoveToPosition(false));
					}
					else if(isOnion)
					{
						OnionReachedDestination ();
						iAmSelected = false;
						startAnimating = false;
						StartCoroutine(MoveToPosition(false));;
					}
					else if(isCabbage)
					{
						CabbageReachedDestination ();
						iAmSelected = false;
						startAnimating = false;
						StartCoroutine(MoveToPosition(false));;
					}
					else if(isFries)
					{
						FriesReachedDestination();
					}
				}
				_canMove = false;
			}

			if (isNoodlesPlate || isSoupBowl) {
				transform.GetComponent<BoxCollider> ().size = colliderSize;
			}
		}

		private IEnumerator MoveToPosition(bool showSelection = true)
		{
			transform.localEulerAngles = Vector3.zero;
			reachedDestination = false;
			float distance = Vector3.Distance (transform.position , myOriginalPos);
			float speed = 15;
			while(distance > 0.1f)
			{
				float step = speed * Time.deltaTime;
				transform.position = Vector3.MoveTowards(transform.position, myOriginalPos, step);
				distance = Vector3.Distance (transform.position , myOriginalPos);
				yield return 0;
			}
			if(iAmSelected && showSelection)
				mySelection.SetActive (true);
			transform.position = myOriginalPos;
		}


		public void ClickedDestination()
		{
			if(isCoke)
			{
				CokeReachedDestination();
			}
			else if(isYellowSauce)
			{
				YellowSauceReachedDestination ();
			}
			else if(isRedSauce)
			{
				RedSauceReachedDestination();
			}
			else if(isCupCake)
			{
				CupCakeReachedDestination();
			}
			else if(isNoodlesToCook)
			{
				NoodlesToCookReachedDestination();
			}
			else if(isNoodlesVeg)
			{
				VegForNoodlesReachedDestination();
			}
			else if(isSoupVeg)
			{
				VegForSoupReachedDestination ();
			}
			else if(isNoodlesPlate)
			{
				NoodlesPlateReachedDestination();
			}
			else if(isSoupBowl)
			{
				SoupBowlReachedDestination ();
			}
			else if(isPizzaVeg)
			{
				PizzaVegReachedDestination ();
			}
			else if(isPizzaNonVeg)
			{
				PizzaNonVegReachedDestination();
			}
			else if(isCheese)
			{
				PizzaCheeseReachedDestination ();
			}
			else if(isTomato)
			{
				TomatoReachedDestination ();
			}
			else if(isOnion)
			{
				OnionReachedDestination();
			}
			else if(isCabbage)
			{
				CabbageReachedDestination ();
			}
			else if(isFries)
			{
				FriesReachedDestination();
			}
		}

		#region Australia

		private void TomatoReachedDestination()
		{
			if(!availableBurger.tomato)
			{
				availableBurger.tomato = true;
				availableBurger.myType = DefineBurgerType(availableBurger);
				availableBurger.myTomato.gameObject.SetActive (true);

			}
		}

		private void OnionReachedDestination()
		{
			if(!availableBurger.onion)
			{
				availableBurger.onion = true;
				availableBurger.myType = DefineBurgerType(availableBurger);
				availableBurger.myOnion.gameObject.SetActive (true);

			}
		}

		private void CabbageReachedDestination()
		{
			if(!availableBurger.cabbage)
			{
				availableBurger.cabbage = true;
				availableBurger.myType = DefineBurgerType(availableBurger);
				availableBurger.myCabbage.gameObject.SetActive (true);

			}
		}

		private void FriesReachedDestination()
		{
			_levelSoundManager.customerEat.Play ();
			_uiManager.n_French_fries_served++;
			PlayerPrefs.SetInt ("FrenchfriesServed", _uiManager.n_French_fries_served);
	
			if(PlayerPrefs.GetInt("FrenchfriesServed") > 9 && PlayerPrefs.GetInt ("FrenchfriesLevel1")==0)
			{
				PlayerPrefs.SetInt ("FrenchfriesLevel1",1);
				_uiManager.achievment_text.SetActive(true);
				AchievementChild.check_claim++;
				PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
				Invoke(nameof(Stopa),4.0f);
			}
			if(PlayerPrefs.GetInt("FrenchfriesServed") > 99 && PlayerPrefs.GetInt ("FrenchfriesLevel2")==0)
			{
				PlayerPrefs.SetInt ("FrenchfriesLevel2",1);
				_uiManager.achievment_text.SetActive(true);
				AchievementChild.check_claim++;
				PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
				Invoke(nameof(Stopa),4.0f);
			}
			if(PlayerPrefs.GetInt("FrenchfriesServed") > 999 && PlayerPrefs.GetInt ("FrenchfriesLevel3")==0)
			{
				PlayerPrefs.SetInt ("FrenchfriesLevel3" ,1);
				_uiManager.achievment_text.SetActive(true);
				AchievementChild.check_claim++;
				PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
				Invoke(nameof(Stopa),4.0f);
			}

			if(_australiaManager != null)
			{
				_australiaManager.friesFilled--;
			}

			myParentHolder.available = true;
			customer.myOrder.Remove (myType);
			customer.RemoveOrderFromBoard (myType);
			if(customer.myOrder.Count > 0)
			{
				customer.myWaitingTime-= 15;
				if(customer.myWaitingTime < 0)
				{
					customer.myWaitingTime = 0;
				}
			}
			
			customer.coinsSpent+=_australiaManager.cokePrice;
			_australiaManager.cickedFries = false;

			if(customer.shouldBePerfectIfServed)
			{
				customer.perfect = true;
			}
			if(customer.myOrder.Count <= 0)
			{
				customer.MoveToEndPosition();
			}
		
			transform.position = myOriginalPos;
			transform.gameObject.SetActive(false);
			if(tutorialOn)
			{
				tutorialOn = false;
				CustomerHandler._instance.InitializeCustomer ();
				Destroy(_uiManager.tutorialPanelCanvas.gameObject); 
				Destroy(_uiManager.tutorialPanelBg.gameObject);
				Australia_Manager.tutorialEnd = true;
			}
		}

		LevelManager.Orders DefineBurgerType(Burger myBurger)
		{
			if(myBurger.tomato && myBurger.onion && myBurger.cabbage)
				myBurger.myType = LevelManager.Orders.BURGER_COMPLETE;
			else if(myBurger.tomato && myBurger.onion)
				myBurger.myType = LevelManager.Orders.BURGER_TOMATO_ONION;
			else if(myBurger.cabbage && myBurger.onion)
				myBurger.myType = LevelManager.Orders.BURGER_ONION_CABBAGE;
			else if(myBurger.tomato && myBurger.cabbage)
				myBurger.myType = LevelManager.Orders.BURGER_TOMATO_CABBAGE;
			else if(myBurger.tomato)
				myBurger.myType = LevelManager.Orders.BURGER_TOMATO;
			else if(myBurger.onion)
				myBurger.myType = LevelManager.Orders.BURGER_ONION;
			else if(myBurger.cabbage)
				myBurger.myType = LevelManager.Orders.BURGER_CABBAGE;
			else
				myBurger.myType = LevelManager.Orders.BURGER;

			return myBurger.myType;
		}

		#endregion

		#region Italy

		void PizzaVegReachedDestination()
		{
			if(!availablePizza.vegetable)
			{
				availablePizza.myType = LevelManager.Orders.VEG_PIZZA;
				availablePizza.myToppings.gameObject.SetActive (true);
				availablePizza.myToppings.sprite = _italyManager.pizzaToppings[0];
				availablePizza.vegetable = true;
				_italyManager.clickedVeg = false;
				if(tutorialOn)
				{
					tutorialOn = false;
					_italyManager.cheese.tutorialOn = true;
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopupItaly ("TAP OR DRAG CHEESE TO \n PIZZA BASE.",false,false , 2);
				}
			}
		}

		void PizzaNonVegReachedDestination()
		{
			if(!availablePizza.vegetable)
			{
				_italyManager.clickedNonVeg = false;
			
				availablePizza.myType = LevelManager.Orders.NON_VEG_PIZZA;
				availablePizza.myToppings.gameObject.SetActive (true);
				availablePizza.myToppings.sprite = _italyManager.pizzaToppings[1];
				availablePizza.vegetable = true;
			}
		}

		void PizzaCheeseReachedDestination()
		{
			if(!availablePizza.cheese)
			{
				_italyManager.clickedCheese = false;
				availablePizza.myCheese.SetActive (true);
				availablePizza.cheese = true;
				if(tutorialOn)
				{
					tutorialOn = false;
					_italyManager.firstPizza.tutorialOn = true;
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopupItaly ("TAP OR DRAG PIZZA \n TO OVEN.",false,false , 3);
				}
			}
		}

		#endregion

		#region CHINA

		private void NoodlesToCookReachedDestination()
		{
			if(!utensil.noodlesAdded)
			{
				utensil.noodlesAdded = true;
				utensil.myFood.gameObject.SetActive (true);
				if(utensil.vegAdded)
				{
					utensil.noOfServingsAvailable = 2;
					utensil.scaledImage.gameObject.SetActive (true);
					utensil.scaledImage.ResetToBeginning ();
					utensil.scaledImage.PlayForward();
					utensil.scaledImage.GetComponent<SpriteRenderer>().sprite =  _chinaManager.noodlesInPanVariations[0];
				}
				else 
				{
					if(tutorialOn)
					{
						tutorialOn = false;
						_chinaManager.noodlesVeg.tutorialOn = true;
						_uiManager.tutorialPanelBg.OpenPopupChina ("TAP OR DRAG INGREDIENTS\nTO THE SKILLET.",false,false , 1);
					}
					utensil.myFood.sprite = _chinaManager.noodlesInPanVariations[0];
					utensil.myScale.enabled = true;
					utensil.myScale.ResetToBeginning ();
					utensil.myScale.PlayForward();
				}
			}
			_chinaManager.clickedNoodlesToCook = false;
		}

		private void VegForNoodlesReachedDestination()
		{
			if(!utensil.vegAdded)
			{
				utensil.vegAdded = true;
				utensil.myFood.gameObject.SetActive (true);

				if(utensil.noodlesAdded)
				{
					if(tutorialOn)
					{
						tutorialOn = false;
						_chinaManager.panUtensil[0].tutorialOn = true;
						_uiManager.tutorialPanelBg.OpenPopupChina ("WAIT FOR THE \n NOODLES TO COOK",false,false , 3);
					}
					utensil.scaledImage.gameObject.SetActive (true);
					utensil.scaledImage.ResetToBeginning ();
					utensil.scaledImage.PlayForward();
					utensil.scaledImage.GetComponent<SpriteRenderer>().sprite =  _chinaManager.noodlesInPanVariations[2];
					utensil.noOfServingsAvailable = 2;
				}
				else 
				{

					utensil.myScale.enabled = true;
					utensil.myScale.ResetToBeginning ();
					utensil.myScale.PlayForward();
					utensil.myFood.sprite = _chinaManager.noodlesInPanVariations[2];
				}
			}
			_chinaManager.clickedNoodlesVeg = false;
		}

		private void VegForSoupReachedDestination()
		{
			utensil.vegAdded = true;
			utensil.myFood.gameObject.SetActive (true);
			utensil.noOfServingsAvailable = 2;
			utensil.myAlpha.gameObject.SetActive (true);
			utensil.myAlpha.ResetToBeginning ();
			utensil.myAlpha.PlayForward ();
			_chinaManager.clickedSoupVeg = false;
			if(tutorialOn)
			{
				tutorialOn = false;
				_chinaManager.soupUtensils[0].tutorialOn = true;
				_uiManager.tutorialPanelBg.OpenPopupChina ("TAP OR DRAG THIS TO \n  SOUP BOWL.",false,false , 5);
			}
		}

		private void NoodlesPlateReachedDestination()
		{
			_levelSoundManager.customerEat.Play ();
			_uiManager.n_Noodles_served++;
			PlayerPrefs.SetInt ("NoodlesServed",_uiManager.n_Noodles_served);

			if(PlayerPrefs.GetInt("NoodlesServed") > 9 && PlayerPrefs.GetInt ("NoodlesLevel1")==0)
			{
				PlayerPrefs.SetInt ("NoodlesLevel1",1);
				MenuManager.totalscore+=100;
				_uiManager.achievment_text.SetActive(true);
				AchievementChild.check_claim++;
				PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
				Invoke(nameof(Stopa),4.0f);
			}
			if(PlayerPrefs.GetInt("NoodlesServed") > 99 && PlayerPrefs.GetInt ("NoodlesLevel2")==0)
			{
				PlayerPrefs.SetInt ("NoodlesLevel2",1);
				_uiManager.achievment_text.SetActive(true);
				AchievementChild.check_claim++;
				PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
				Invoke(nameof(Stopa),4.0f);

			}
			if(PlayerPrefs.GetInt("NoodlesServed") > 999 && PlayerPrefs.GetInt ("NoodlesLevel3")==0)
			{
				PlayerPrefs.SetInt ("NoodlesLevel3",1);
				_uiManager.achievment_text.SetActive(true);
				AchievementChild.check_claim++;
				PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
				Invoke(nameof(Stopa),4.0f);
			}
			_chinaManager.platesFilledCount--;
			myParentHolder.available = true;
			customer.myOrder.Remove (myType);
			customer.RemoveOrderFromBoard (myType);

			if(tutorialOn)
			{
				tutorialOn = false;
				_chinaManager.firstCustomer.tutorialOn = true;
				_uiManager.tutorialPanelCanvas.gameObject.SetActive (false);
				_uiManager.tutorialPanelBg.gameObject.SetActive (true);
				_uiManager.tutorialPanelBg.OpenPopupChina ("PUT BURNT NOODLES\nIN THE DUSTBIN!",false,false , 11 , 1);
			}
			customer.iHaveAMultipleTypeOrder = LevelManager.Orders.NONE;
			if(customer.myOrder.Count > 0)
			{
				customer.myWaitingTime-= 35;
				if(customer.myWaitingTime < 0)
				{
					customer.myWaitingTime = 0;
				}
			}
			if(perfect)
			{
				customer.coinsSpent+=_chinaManager.perfectNoodlesPrice;
				customer.perfect = true;
			}
			else
			{
				customer.coinsSpent+=_chinaManager.lessBakedNoodlesPrice;
			}

			if(customer.myOrder.Count <= 0)
				customer.MoveToEndPosition();
		
			transform.position = myOriginalPos;
			myNoodles.SetActive (false);
			transform.gameObject.SetActive(false);
			_chinaManager.clickedNoodlePlate = false;
		}

		private void SoupBowlReachedDestination()
		{
			_levelSoundManager.drink.Play ();
			_chinaManager.bowlsFilled--;
			myParentHolder.available = true;
			customer.myOrder.Remove (myType);
			customer.RemoveOrderFromBoard (myType);
		
			if(tutorialOn)
			{

				tutorialOn = false;
				_uiManager.tutorialPanelBg.gameObject.SetActive (false);
				_uiManager.tutorialPanelCanvas.gameObject.SetActive (true);
				_uiManager.tutorialPanelCanvas.OpenPopupChina ("ONE SOUP CONTAINER CAN FILL TWO SOUP BOWLS!",true,false , 16 , 1);
			
			}
			if(customer.myOrder.Count > 0)
			{
				customer.myWaitingTime-= 35;
				if(customer.myWaitingTime < 0)
				{
					customer.myWaitingTime = 0;
				}
			}
			customer.coinsSpent+=_chinaManager.soupPrice;
			if(customer.shouldBePerfectIfServed)
			{
				customer.perfect = true;
			}
			if(customer.myOrder.Count <= 0)
				customer.MoveToEndPosition();

			mySoup.SetActive (false);
			transform.position = myOriginalPos;
			transform.gameObject.SetActive(false);
			_chinaManager.clickSoupBowl = false;
		
		}

		#endregion

		#region USA

		private void RedSauceReachedDestination()
		{
			availableHotDog.redSauce = true;
			_usManager.hotDogSaucesOnPlates[availableHotDog.GetComponent<Availability>().myPositionInArray].gameObject.SetActive (true);
			_usManager.hotDogSaucesOnPlates[availableHotDog.GetComponent<Availability>().myPositionInArray].sprite = _usManager.hotDogSauces[0];
			if(availableHotDog.tikki)
			{
				availableHotDog.transform.GetComponent<HotDog>().myType = LevelManager.Orders.HOTDOG_RED;
			}

			_usManager.clickedRedSauce = false;
		}

		private void YellowSauceReachedDestination()
		{
			availableHotDog.yellowSauce = true;
			_usManager.hotDogSaucesOnPlates[availableHotDog.GetComponent<Availability>().myPositionInArray].gameObject.SetActive (true);
			_usManager.hotDogSaucesOnPlates[availableHotDog.GetComponent<Availability>().myPositionInArray].sprite = _usManager.hotDogSauces[1];
			if(availableHotDog.tikki)
			{
				availableHotDog.transform.GetComponent<HotDog>().myType = LevelManager.Orders.HOTDOG_YELLOW;
			}
			_usManager.clickedYellowSauce = false;
		}

		#endregion

		private void CokeReachedDestination()
		{
			_uiManager.n_Cokes_served++;
			int rad = Random.Range (0, 6);
			if (rad <= 4) {
				_levelSoundManager.drink.Play ();
			} else {
				_levelSoundManager.drink2.Play ();
			}
			PlayerPrefs.SetInt ("CokesServed",_uiManager.n_Cokes_served);
	
			if(PlayerPrefs.GetInt("CokesServed") > 9 && PlayerPrefs.GetInt ("CokeLevel1")==0)
			{
				PlayerPrefs.SetInt ("CokeLevel1",1);
				_uiManager.achievment_text.SetActive(true);
				AchievementChild.check_claim++;
				PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
				Invoke(nameof(Stopa),4.0f);
			}
			if(PlayerPrefs.GetInt("CokesServed") > 99 && PlayerPrefs.GetInt ("CokeLevel2")==0)
			{
				PlayerPrefs.SetInt ("CokeLevel2",1);
				_uiManager.achievment_text.SetActive(true);
				AchievementChild.check_claim++;
				PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
				Invoke(nameof(Stopa),4.0f);
			}
			if(PlayerPrefs.GetInt("CokesServed") > 999 && PlayerPrefs.GetInt ("CokeLevel3")==0)
			{
				PlayerPrefs.SetInt ("CokeLevel3",1);
				_uiManager.achievment_text.SetActive(true);
				AchievementChild.check_claim++;
				PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
				Invoke(nameof(Stopa),4.0f);
			}

			if(_usManager != null)
			{
				_usManager.cokesFilled--;
			}
			else if(_italyManager != null)
			{
				_italyManager.cokesFilled--;
			}
			else if(_australiaManager != null)
			{
				_australiaManager.cokesFilled--;
			}
			myParentHolder.available = true;
			customer.myOrder.Remove (myType);
			customer.RemoveOrderFromBoard (myType);
			
			if(customer.myOrder.Count > 0)
			{
				customer.myWaitingTime-= 15;
				if(customer.myWaitingTime < 0)
				{
					customer.myWaitingTime = 0;
				}
			}

			if(_usManager != null)
			{
				customer.coinsSpent+=_usManager.cokePrice;
				_usManager.clickedCoke = false;
			}
			else if(_italyManager != null)
			{
				customer.coinsSpent+=_italyManager.cokePrice;
				if(isChilled)
				{
					customer.coinsSpent+=15;
				}
				_italyManager.clickedCoke = false;
			}
			else if(_australiaManager != null)
			{
				customer.coinsSpent+=_australiaManager.cokePrice;
				_australiaManager.clickedCoke = false;
			}


			if(customer.shouldBePerfectIfServed)
			{
				customer.perfect = true;
			}
			if(customer.myOrder.Count <= 0)
			{
				customer.MoveToEndPosition();
			}
		
			transform.position = myOriginalPos;
			transform.gameObject.SetActive(false);

		}


		private void CupCakeReachedDestination()
		{
			_levelSoundManager.customerEat.Play ();
			customer.myWaitingTime = 0;
			customer.myFacialExpression.sprite = customer.expressions[0];
			customer.waitingSlider.color = Color.green;
			MenuManager.cupcakeNo--;
			PlayerPrefs.SetString ("Cupcake",EncryptionHandler64.Encrypt (MenuManager.cupcakeNo.ToString ()));
			if(MenuManager.cupcakeNo <= 0)
			{
				gameObject.SetActive (false);
			}
			else
			{
				newCupcakeCame.Play ();
				transform.position = myOriginalPos;
				iAmSelected = false;
			
			}

		}


		private void OnTriggerStay(Collider other)
		{

			if(isRedSauce || isYellowSauce)
			{
				if(other.name.Contains ("hotdog"))
				{
					otherObject = other.gameObject;
					availableHotDog = other.GetComponent<HotDog>();
					if(!availableHotDog.redSauce && !availableHotDog.yellowSauce)
					{
						reachedDestination = true;
					}
				}
			}
			else if(isCupCake)
			{
				if(other.name.Contains ("customer"))
				{
					customer = other.GetComponent<Customer>();
					reachedDestination = true;
				}
			}
			else if(isCoke || isFries)
			{
				if(other.name.Contains ("customer"))
				{

					otherObject = other.gameObject;
					customer = other.GetComponent<Customer>();
					for(int i = 0 ; i< customer.myOrder.Count ; i++)
					{
						if(myType == customer.myOrder[i])
						{
							reachedDestination = true;
							break;
						}
					}
				}
			}
			else if(isNoodlesToCook || isNoodlesVeg)
			{
				otherObject = other.gameObject;
				if(other.name.Contains ("pan"))
				{
					utensil = other.GetComponent<ChineseUtensils>();
					if(utensil.noOfServingsAvailable == 0)
					{
						if(isNoodlesToCook)
						{
							if(!utensil.noodlesAdded)
							{
								reachedDestination = true;
							}
						}
						else if(isNoodlesVeg)
						{
							if(!utensil.vegAdded)
							{
								reachedDestination = true;
							}
						}
					}

				}
			}
			else if(isSoupVeg)
			{
				otherObject = other.gameObject;
				if(other.name.Contains ("pot"))
				{
					utensil = other.GetComponent<ChineseUtensils>();
					if(utensil.noOfServingsAvailable == 0)
					{
						if(!utensil.vegAdded)
						{
							reachedDestination = true;
						}
					}
				}
			}
			else if(isNoodlesPlate)
			{
				if(myType != LevelManager.Orders.NONE)
				{
					if(other.name.Contains ("customer"))
					{
						otherObject = other.gameObject;
						customer = other.GetComponent<Customer>();
						for(int i = 0 ; i< customer.myOrder.Count ; i++)
						{
							if(myType == customer.myOrder[i])
							{
								reachedDestination = true;
								break;
							}
						}
						if(!reachedDestination)
						{
							otherObject = null;
							customer = null;
						}
					}
					else
					{
						otherObject = null;
						customer = null;
					}
				}
			}
			else if(isSoupBowl)
			{
				if(myType != LevelManager.Orders.NONE)
				{
					if(other.name.Contains ("customer"))
					{
						otherObject = other.gameObject;
						customer = other.GetComponent<Customer>();
						for(int i = 0 ; i< customer.myOrder.Count ; i++)
						{
							if(myType == customer.myOrder[i])
							{
								reachedDestination = true;
								break;
							}
						}
						if(!reachedDestination)
						{
							otherObject = null;
							customer = null;
						}
					}
					else
					{
						otherObject = null;
						customer = null;
					}
				}
			}
			else if(isCheese || isPizzaVeg || isPizzaNonVeg)
			{
				if(other.name.Contains ("pizza"))
				{
					otherObject = other.gameObject;
					availablePizza = other.GetComponent<Pizza>();
					if(isPizzaNonVeg || isPizzaVeg)
					{
						if(!availablePizza.vegetable)
						{
							reachedDestination = true;
						}
					}
					else if(isCheese)
					{
						if(!availablePizza.cheese)
						{
							reachedDestination = true;
						}
					}
				}
			}
			else if(isCabbage || isTomato || isOnion)
			{
				if(other.name.Contains ("burger"))
				{
					otherObject = other.gameObject;
					availableBurger = other.GetComponent<Burger>();
					if(isTomato)
					{
						if(!availableBurger.tomato)
						{
							reachedDestination = true;
						}
					}
					else if(isOnion)
					{
						if(!availableBurger.onion)
						{
							reachedDestination = true;
						}
					}
					else if(isCabbage)
					{
						if(!availableBurger.cabbage)
						{
							reachedDestination = true;
						}
					}
				}
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if(LevelManager.levelNo <= 10)
			{
				if(isRedSauce || isYellowSauce)
				{
					if(other.name.Contains ("hotdog"))
					{
						if(otherObject != null)
						{
							if(otherObject == other.gameObject)
							{
								otherObject = null;
								reachedDestination = false;
							}
						}
						else
						{
							reachedDestination = false;
						}
					}
				
				}
				else
				{
					if(other.name.Contains ("customer"))
					{
						reachedDestination = false;
					}
				}
			}
			else if(LevelManager.levelNo > 10 && LevelManager.levelNo <= 20)
			{
				if(isNoodlesToCook || isNoodlesVeg)
				{
					if(other.name.Contains ("pan"))
					{
						if(otherObject != null)
						{
							if(otherObject == other.gameObject)
							{
								otherObject = null;
								reachedDestination = false;
							}
						}
						else
						{
							reachedDestination = false;
						}
					}
				}
				else if(isSoupVeg)
				{
					if(other.name.Contains ("pot"))
					{
						if(otherObject != null)
						{
							if(otherObject == other.gameObject)
							{
								otherObject = null;
								reachedDestination = false;
							}
						}
						else
						{
							reachedDestination = false;
						}
					}
				}
				else if(isNoodlesPlate)
				{
					if(myType != LevelManager.Orders.NONE)
					{
						if(other.name.Contains ("customer"))
						{
							if(otherObject != null)
							{
								if(otherObject == other.gameObject)
								{
									otherObject = null;
									reachedDestination = false;
								}
							}
							else
							{
								reachedDestination = false;
							}
						}
					}
				}
				else if(isSoupBowl)
				{
					if(myType != LevelManager.Orders.NONE)
					{
						if(other.name.Contains ("customer"))
						{
							if(otherObject != null)
							{
								if(otherObject == other.gameObject)
								{
									otherObject = null;
									reachedDestination = false;
								}
							}
							else
							{
								reachedDestination = false;
							}
						}
					}
				}
			}
			else if(LevelManager.levelNo > 20 && LevelManager.levelNo <= 30)
			{
				if(isCheese || isPizzaVeg || isPizzaNonVeg)
				{
					if(other.name.Contains ("pizza"))
					{
						if(otherObject != null)
						{
							if(otherObject == other.gameObject)
							{
								otherObject = null;
								reachedDestination = false;
							}
						}
						else
						{
							reachedDestination = false;
						}
					}
				}
				else
				{
					if(other.name.Contains ("customer"))
					{
						reachedDestination = false;
					}
				}
			}
			else
			{
				if(isCabbage || isTomato || isOnion)
				{
					if(other.name.Contains ("burger"))
					{
						if(otherObject != null)
						{
							if(otherObject == other.gameObject)
							{
								otherObject = null;
								reachedDestination = false;
							}
						}
						else
						{
							reachedDestination = false;
						}
					}
				}
				else
				{
					if(other.name.Contains ("customer"))
					{
						reachedDestination = false;
					}
				}
			}
		}



	}
}
