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
		[Inject] private WisitorHandler _customerHandler;
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

		public ChineseUtils utensil { get; set; }
		public bool isRedSauce;
		public bool isYellowSauce;
		public HotDog availableHotDog { get; set; }
		public bool isCoke;
		public Availability myParentHolder;
		public LevelManager.Orders myType = LevelManager.Orders.COKE;
		public bool isChilled { get; set; }
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
		public Pizza availablePizza { get; set; }
		public BurgerFood availableBurger { get; set; }
		public bool isTomato;
		public bool isOnion;
		public bool isCabbage;
		public bool isFries;
		public Wisitor customer { get; set; }
		public bool isCupCake;
		public ParticleSystem newCupcakeCame;
		public Vector3 myOriginalPos , myTouchPos;
		public bool iAmSelected { get; set; }
		public Vector3 myLocalScale;
		public GameObject mySelection;
		public bool  perfect { get; set; }
		public bool tutorialOn { get; set; }
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
			if(!TutorialPanel.popupPanelActive || US_Manager.tutorialEnd || China_Manager._endTutorial || Italy_Manager.tutorialEnd || tutorialOn || Australia_Manager.tutorialEnd)
			{
				if(isCoke)
				{
					if(!myParentHolder.available)
					{
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
							_australiaManager._objectMotion = this;
							_australiaManager.IsClickedCoke = true;
						}
						iAmSelected = true;
						_canMove = true;
					}
					else
						_canMove = false;
				}
				else
				{
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
						_chinaManager.ResetBowlsCliked ();
						_chinaManager.clickedItemDestinationFunction = this;
						_chinaManager.ClikedNoodles = true;
						iAmSelected = true;
					}
					else if(isNoodlesVeg)
					{
						_chinaManager.ResetBowlsCliked ();
						_chinaManager.clickedItemDestinationFunction = this;
						_chinaManager.NoodlesVeg = true;
						iAmSelected = true;
					}
					else if(isSoupVeg)
					{
						_chinaManager.ResetBowlsCliked ();
						_chinaManager.clickedItemDestinationFunction = this;
						_chinaManager.IsClikedSoupVeg = true;
						iAmSelected = true;
					}
					else if(isNoodlesPlate)
					{
						if(_chinaManager.IsPanClick)
						{
							if(!_chinaManager.clickedUtensilsDestinationFunction._isBurnt)
							{
								_chinaManager.clickedUtensilsDestinationFunction.otherObject = this.gameObject;
								_chinaManager.UtensilReach ();
							}
							_chinaManager.ResetBowlsCliked ();
						}
						else
						{
							if((tutorialOn && !_chinaManager.panUtils[0]._isTutorialOn ) || China_Manager._endTutorial)
							{
								transform.GetComponent<BoxCollider> ().size = new Vector3(colliderSize.x/2f , colliderSize.y/2f , colliderSize.z);
								_chinaManager.ResetBowlsCliked ();
								_chinaManager.clickedItemDestinationFunction = this;
								_chinaManager.IsClickedNoodlePlate = true;
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
						if(_chinaManager.IsClickedSoupContainer)
						{
							_chinaManager.clickedUtensilsDestinationFunction.otherObject = this.gameObject;
							_chinaManager.UtensilReach ();
							_chinaManager.ResetBowlsCliked ();
						}
						else
						{
							transform.GetComponent<BoxCollider> ().size = new Vector3(colliderSize.x/2f , colliderSize.y/2f , colliderSize.z);
							_chinaManager.ResetBowlsCliked ();
							_chinaManager.clickedItemDestinationFunction = this;
							_chinaManager.IsClickSoupBowl = true;
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
						_australiaManager._objectMotion = this;
						_australiaManager.IsClickedTomato = true;
						iAmSelected = true;
					}
					else if(isOnion)
					{
						_australiaManager.AllClickedBoolsReset ();
						_australiaManager._objectMotion = this;
						_australiaManager.IsClickedOnion = true;
						iAmSelected = true;
					}
					else if(isCabbage)
					{
						_australiaManager.AllClickedBoolsReset ();
						_australiaManager._objectMotion = this;
						_australiaManager.IsClickedCabbage = true;
						iAmSelected = true;
					}
					else if(isFries)
					{
						_australiaManager.AllClickedBoolsReset ();
						_australiaManager._objectMotion = this;
						_australiaManager.IsCickedFries = true;
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
						StartCoroutine(MoveToPosition(false));
					}
					else if(isNoodlesVeg)
					{
						VegForNoodlesReachedDestination();
						iAmSelected = false;
						StartCoroutine(MoveToPosition(false));
					}
					else if(isSoupVeg)
					{
						VegForSoupReachedDestination ();
						iAmSelected = false;
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
						StartCoroutine(MoveToPosition(false));
					}
					else if(isPizzaNonVeg)
					{
						PizzaNonVegReachedDestination ();
						iAmSelected = false;
						StartCoroutine(MoveToPosition(false));;
					}
					else if(isCheese)
					{
						PizzaCheeseReachedDestination ();
						iAmSelected = false;
						StartCoroutine(MoveToPosition(false));;
					}
					else if(isTomato)
					{
						TomatoReachedDestination();
						iAmSelected = false;
						StartCoroutine(MoveToPosition(false));
					}
					else if(isOnion)
					{
						OnionReachedDestination ();
						iAmSelected = false;
						StartCoroutine(MoveToPosition(false));;
					}
					else if(isCabbage)
					{
						CabbageReachedDestination ();
						iAmSelected = false;
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
			if(!availableBurger.IsTomato)
			{
				availableBurger.IsTomato = true;
				availableBurger.type = DefineBurgerType(availableBurger);
				availableBurger._tomatoPrefab.gameObject.SetActive (true);

			}
		}

		private void OnionReachedDestination()
		{
			if(!availableBurger.IsOnion)
			{
				availableBurger.IsOnion = true;
				availableBurger.type = DefineBurgerType(availableBurger);
				availableBurger._onionPrefab.gameObject.SetActive (true);

			}
		}

		private void CabbageReachedDestination()
		{
			if(!availableBurger.IsCabbage)
			{
				availableBurger.IsCabbage = true;
				availableBurger.type = DefineBurgerType(availableBurger);
				availableBurger._cabbagePrefab.gameObject.SetActive (true);

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
				AchievementBlock._claimCheck++;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
				Invoke(nameof(Stopa),4.0f);
			}
			if(PlayerPrefs.GetInt("FrenchfriesServed") > 99 && PlayerPrefs.GetInt ("FrenchfriesLevel2")==0)
			{
				PlayerPrefs.SetInt ("FrenchfriesLevel2",1);
				_uiManager.achievment_text.SetActive(true);
				AchievementBlock._claimCheck++;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
				Invoke(nameof(Stopa),4.0f);
			}
			if(PlayerPrefs.GetInt("FrenchfriesServed") > 999 && PlayerPrefs.GetInt ("FrenchfriesLevel3")==0)
			{
				PlayerPrefs.SetInt ("FrenchfriesLevel3" ,1);
				_uiManager.achievment_text.SetActive(true);
				AchievementBlock._claimCheck++;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
				Invoke(nameof(Stopa),4.0f);
			}

			if(_australiaManager != null)
			{
				_australiaManager.FriesFilled--;
			}

			myParentHolder.available = true;
			customer._order.Remove (myType);
			customer.RemoveOrderFromBoard (myType);
			if(customer._order.Count > 0)
			{
				customer.myWaitingTime-= 15;
				if(customer.myWaitingTime < 0)
				{
					customer.myWaitingTime = 0;
				}
			}
			
			customer.coinsSpent+=_australiaManager.CokePrice;
			_australiaManager.IsCickedFries = false;

			if(customer.shouldBePerfectIfServed)
			{
				customer.perfect = true;
			}
			if(customer._order.Count <= 0)
			{
				customer.MoveToEnd();
			}
		
			transform.position = myOriginalPos;
			transform.gameObject.SetActive(false);
			if(tutorialOn)
			{
				tutorialOn = false;
				_customerHandler.ConfigureCustomer ();
				Destroy(_uiManager.tutorialPanelCanvas.gameObject); 
				Destroy(_uiManager.tutorialPanelBg.gameObject);
				Australia_Manager.tutorialEnd = true;
			}
		}

		LevelManager.Orders DefineBurgerType(BurgerFood myBurger)
		{
			if(myBurger.IsTomato && myBurger.IsOnion && myBurger.IsCabbage)
				myBurger.type = LevelManager.Orders.BURGER_COMPLETE;
			else if(myBurger.IsTomato && myBurger.IsOnion)
				myBurger.type = LevelManager.Orders.BURGER_TOMATO_ONION;
			else if(myBurger.IsCabbage && myBurger.IsOnion)
				myBurger.type = LevelManager.Orders.BURGER_ONION_CABBAGE;
			else if(myBurger.IsTomato && myBurger.IsCabbage)
				myBurger.type = LevelManager.Orders.BURGER_TOMATO_CABBAGE;
			else if(myBurger.IsTomato)
				myBurger.type = LevelManager.Orders.BURGER_TOMATO;
			else if(myBurger.IsOnion)
				myBurger.type = LevelManager.Orders.BURGER_ONION;
			else if(myBurger.IsCabbage)
				myBurger.type = LevelManager.Orders.BURGER_CABBAGE;
			else
				myBurger.type = LevelManager.Orders.BURGER;

			return myBurger.type;
		}

		#endregion

		#region Italy

		void PizzaVegReachedDestination()
		{
			if(!availablePizza._isVegetable)
			{
				availablePizza.myType = LevelManager.Orders.VEG_PIZZA;
				availablePizza._topping.gameObject.SetActive (true);
				availablePizza._topping.sprite = _italyManager.pizzaToppings[0];
				availablePizza._isVegetable = true;
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
			if(!availablePizza._isVegetable)
			{
				_italyManager.clickedNonVeg = false;
			
				availablePizza.myType = LevelManager.Orders.NON_VEG_PIZZA;
				availablePizza._topping.gameObject.SetActive (true);
				availablePizza._topping.sprite = _italyManager.pizzaToppings[1];
				availablePizza._isVegetable = true;
			}
		}

		void PizzaCheeseReachedDestination()
		{
			if(!availablePizza._isCheese)
			{
				_italyManager.clickedCheese = false;
				availablePizza._cheesePrefav.SetActive (true);
				availablePizza._isCheese = true;
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
			if(!utensil._isNoodlesAdded)
			{
				utensil._isNoodlesAdded = true;
				utensil._foodSpriteRenderer.gameObject.SetActive (true);
				if(utensil._isVegAdded)
				{
					utensil._servingsAvailable = 2;
					utensil._scaledImage.gameObject.SetActive (true);
					utensil._scaledImage.ResetToBeginning ();
					utensil._scaledImage.PlayForward();
					utensil._scaledImage.GetComponent<SpriteRenderer>().sprite =  _chinaManager.noodlesPan[0];
				}
				else 
				{
					if(tutorialOn)
					{
						tutorialOn = false;
						_chinaManager.vegNoodles.tutorialOn = true;
						_uiManager.tutorialPanelBg.OpenPopupChina ("TAP OR DRAG INGREDIENTS\nTO THE SKILLET.",false,false , 1);
					}
					utensil._foodSpriteRenderer.sprite = _chinaManager.noodlesPan[0];
					utensil._tweenScale.enabled = true;
					utensil._tweenScale.ResetToBeginning ();
					utensil._tweenScale.PlayForward();
				}
			}
			_chinaManager.ClikedNoodles = false;
		}

		private void VegForNoodlesReachedDestination()
		{
			if(!utensil._isVegAdded)
			{
				utensil._isVegAdded = true;
				utensil._foodSpriteRenderer.gameObject.SetActive (true);

				if(utensil._isNoodlesAdded)
				{
					if(tutorialOn)
					{
						tutorialOn = false;
						_chinaManager.panUtils[0]._isTutorialOn = true;
						_uiManager.tutorialPanelBg.OpenPopupChina ("WAIT FOR THE \n NOODLES TO COOK",false,false , 3);
					}
					utensil._scaledImage.gameObject.SetActive (true);
					utensil._scaledImage.ResetToBeginning ();
					utensil._scaledImage.PlayForward();
					utensil._scaledImage.GetComponent<SpriteRenderer>().sprite =  _chinaManager.noodlesPan[2];
					utensil._servingsAvailable = 2;
				}
				else 
				{

					utensil._tweenScale.enabled = true;
					utensil._tweenScale.ResetToBeginning ();
					utensil._tweenScale.PlayForward();
					utensil._foodSpriteRenderer.sprite = _chinaManager.noodlesPan[2];
				}
			}
			_chinaManager.NoodlesVeg = false;
		}

		private void VegForSoupReachedDestination()
		{
			utensil._isVegAdded = true;
			utensil._foodSpriteRenderer.gameObject.SetActive (true);
			utensil._servingsAvailable = 2;
			utensil._alphaTween.gameObject.SetActive (true);
			utensil._alphaTween.ResetToBeginning ();
			utensil._alphaTween.PlayForward ();
			_chinaManager.IsClikedSoupVeg = false;
			if(tutorialOn)
			{
				tutorialOn = false;
				_chinaManager.soupUtils[0]._isTutorialOn = true;
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
				AchievementBlock._claimCheck++;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
				Invoke(nameof(Stopa),4.0f);
			}
			if(PlayerPrefs.GetInt("NoodlesServed") > 99 && PlayerPrefs.GetInt ("NoodlesLevel2")==0)
			{
				PlayerPrefs.SetInt ("NoodlesLevel2",1);
				_uiManager.achievment_text.SetActive(true);
				AchievementBlock._claimCheck++;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
				Invoke(nameof(Stopa),4.0f);

			}
			if(PlayerPrefs.GetInt("NoodlesServed") > 999 && PlayerPrefs.GetInt ("NoodlesLevel3")==0)
			{
				PlayerPrefs.SetInt ("NoodlesLevel3",1);
				_uiManager.achievment_text.SetActive(true);
				AchievementBlock._claimCheck++;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
				Invoke(nameof(Stopa),4.0f);
			}
			_chinaManager.FiledCountClick--;
			myParentHolder.available = true;
			customer._order.Remove (myType);
			customer.RemoveOrderFromBoard (myType);

			if(tutorialOn)
			{
				tutorialOn = false;
				_chinaManager.CustomerFirst.tutorialOn = true;
				_uiManager.tutorialPanelCanvas.gameObject.SetActive (false);
				_uiManager.tutorialPanelBg.gameObject.SetActive (true);
				_uiManager.tutorialPanelBg.OpenPopupChina ("PUT BURNT NOODLES\nIN THE DUSTBIN!",false,false , 11 , 1);
			}
			customer.iHaveAMultipleTypeOrder = LevelManager.Orders.NONE;
			if(customer._order.Count > 0)
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

			if(customer._order.Count <= 0)
				customer.MoveToEnd();
		
			transform.position = myOriginalPos;
			myNoodles.SetActive (false);
			transform.gameObject.SetActive(false);
			_chinaManager.IsClickedNoodlePlate = false;
		}

		private void SoupBowlReachedDestination()
		{
			_levelSoundManager.drink.Play ();
			_chinaManager.FilledBowls--;
			myParentHolder.available = true;
			customer._order.Remove (myType);
			customer.RemoveOrderFromBoard (myType);
		
			if(tutorialOn)
			{

				tutorialOn = false;
				_uiManager.tutorialPanelBg.gameObject.SetActive (false);
				_uiManager.tutorialPanelCanvas.gameObject.SetActive (true);
				_uiManager.tutorialPanelCanvas.OpenPopupChina ("ONE SOUP CONTAINER CAN FILL TWO SOUP BOWLS!",true,false , 16 , 1);
			
			}
			if(customer._order.Count > 0)
			{
				customer.myWaitingTime-= 35;
				if(customer.myWaitingTime < 0)
				{
					customer.myWaitingTime = 0;
				}
			}
			customer.coinsSpent+=_chinaManager.SoupPrice;
			if(customer.shouldBePerfectIfServed)
			{
				customer.perfect = true;
			}
			if(customer._order.Count <= 0)
				customer.MoveToEnd();

			mySoup.SetActive (false);
			transform.position = myOriginalPos;
			transform.gameObject.SetActive(false);
			_chinaManager.IsClickSoupBowl = false;
		
		}

		#endregion

		#region USA

		private void RedSauceReachedDestination()
		{
			availableHotDog.isSauce = true;
			_usManager.hotDogSaucesOnPlates[availableHotDog.GetComponent<Availability>().myPositionInArray].gameObject.SetActive (true);
			_usManager.hotDogSaucesOnPlates[availableHotDog.GetComponent<Availability>().myPositionInArray].sprite = _usManager.hotDogSauces[0];
			if(availableHotDog.isTikki)
			{
				availableHotDog.transform.GetComponent<HotDog>().isTape = LevelManager.Orders.HOTDOG_RED;
			}

			_usManager.clickedRedSauce = false;
		}

		private void YellowSauceReachedDestination()
		{
			availableHotDog.yellowSauce = true;
			_usManager.hotDogSaucesOnPlates[availableHotDog.GetComponent<Availability>().myPositionInArray].gameObject.SetActive (true);
			_usManager.hotDogSaucesOnPlates[availableHotDog.GetComponent<Availability>().myPositionInArray].sprite = _usManager.hotDogSauces[1];
			if(availableHotDog.isTikki)
			{
				availableHotDog.transform.GetComponent<HotDog>().isTape = LevelManager.Orders.HOTDOG_YELLOW;
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
				AchievementBlock._claimCheck++;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
				Invoke(nameof(Stopa),4.0f);
			}
			if(PlayerPrefs.GetInt("CokesServed") > 99 && PlayerPrefs.GetInt ("CokeLevel2")==0)
			{
				PlayerPrefs.SetInt ("CokeLevel2",1);
				_uiManager.achievment_text.SetActive(true);
				AchievementBlock._claimCheck++;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
				Invoke(nameof(Stopa),4.0f);
			}
			if(PlayerPrefs.GetInt("CokesServed") > 999 && PlayerPrefs.GetInt ("CokeLevel3")==0)
			{
				PlayerPrefs.SetInt ("CokeLevel3",1);
				_uiManager.achievment_text.SetActive(true);
				AchievementBlock._claimCheck++;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
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
				_australiaManager.CokesFilled--;
			}
			myParentHolder.available = true;
			customer._order.Remove (myType);
			customer.RemoveOrderFromBoard (myType);
			
			if(customer._order.Count > 0)
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
				customer.coinsSpent+=_australiaManager.CokePrice;
				_australiaManager.IsClickedCoke = false;
			}


			if(customer.shouldBePerfectIfServed)
			{
				customer.perfect = true;
			}
			if(customer._order.Count <= 0)
			{
				customer.MoveToEnd();
			}
		
			transform.position = myOriginalPos;
			transform.gameObject.SetActive(false);

		}


		private void CupCakeReachedDestination()
		{
			_levelSoundManager.customerEat.Play ();
			customer.myWaitingTime = 0;
			customer._facialExpression.sprite = customer._expressions[0];
			customer._sliderWait.color = Color.green;
			MenuManager.cupcakeNo--;
			PlayerPrefs.SetString ("Cupcake",Encryption.Encrypt (MenuManager.cupcakeNo.ToString ()));
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
					if(!availableHotDog.isSauce && !availableHotDog.yellowSauce)
					{
						reachedDestination = true;
					}
				}
			}
			else if(isCupCake)
			{
				if(other.name.Contains ("customer"))
				{
					customer = other.GetComponent<Wisitor>();
					reachedDestination = true;
				}
			}
			else if(isCoke || isFries)
			{
				if(other.name.Contains ("customer"))
				{

					otherObject = other.gameObject;
					customer = other.GetComponent<Wisitor>();
					for(int i = 0 ; i< customer._order.Count ; i++)
					{
						if(myType == customer._order[i])
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
					utensil = other.GetComponent<ChineseUtils>();
					if(utensil._servingsAvailable == 0)
					{
						if(isNoodlesToCook)
						{
							if(!utensil._isNoodlesAdded)
							{
								reachedDestination = true;
							}
						}
						else if(isNoodlesVeg)
						{
							if(!utensil._isVegAdded)
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
					utensil = other.GetComponent<ChineseUtils>();
					if(utensil._servingsAvailable == 0)
					{
						if(!utensil._isVegAdded)
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
						customer = other.GetComponent<Wisitor>();
						for(int i = 0 ; i< customer._order.Count ; i++)
						{
							if(myType == customer._order[i])
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
						customer = other.GetComponent<Wisitor>();
						for(int i = 0 ; i< customer._order.Count ; i++)
						{
							if(myType == customer._order[i])
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
						if(!availablePizza._isVegetable)
						{
							reachedDestination = true;
						}
					}
					else if(isCheese)
					{
						if(!availablePizza._isCheese)
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
					availableBurger = other.GetComponent<BurgerFood>();
					if(isTomato)
					{
						if(!availableBurger.IsTomato)
						{
							reachedDestination = true;
						}
					}
					else if(isOnion)
					{
						if(!availableBurger.IsOnion)
						{
							reachedDestination = true;
						}
					}
					else if(isCabbage)
					{
						if(!availableBurger.IsCabbage)
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
