﻿using System.Collections;
using _Project.Scripts.Achivments;
using _Project.Scripts.Additional;
using _Project.Scripts.Entities.Customers;
using _Project.Scripts.Food;
using _Project.Scripts.Game;
using _Project.Scripts.Managers;
using _Project.Scripts.UI_Scripts;
using _Project.Scripts.UI.Tutorial;
using UnityEngine;

namespace _Project.Scripts.Other
{
	public class ObjectMotion : MonoBehaviour 
	{
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
			UIManager._instance.n_Cokes_served = PlayerPrefs.GetInt ("CokesServed");
			UIManager._instance.n_Noodles_served = PlayerPrefs.GetInt ("NoodlesServed");
			UIManager._instance.n_French_fries_served = PlayerPrefs.GetInt ("FrenchfriesServed");
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
			UIManager._instance.achievment_text.SetActive (false);
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
						if(US_Manager._instance != null)
						{
							US_Manager._instance.AllClickedBoolsReset ();
							US_Manager._instance.clickedItemDestinationFunction = this;
							US_Manager._instance.clickedCoke = true;
						}
						else if(Italy_Manager._instance != null)
						{
							Italy_Manager._instance.AllClickedBoolsReset ();
							Italy_Manager._instance.clickedItemDestinationFunction = this;
							Italy_Manager._instance.clickedCoke = true;
						}
						else if(Australia_Manager._instance != null)
						{
							Australia_Manager._instance.AllClickedBoolsReset ();
							Australia_Manager._instance.clickedItemDestinationFunction = this;
							Australia_Manager._instance.clickedCoke = true;
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
						US_Manager._instance.AllClickedBoolsReset ();
						US_Manager._instance.clickedItemDestinationFunction = this;
						US_Manager._instance.clickedRedSauce = true;
						iAmSelected = true;

					}
					else if(isYellowSauce)
					{
						US_Manager._instance.AllClickedBoolsReset ();
						US_Manager._instance.clickedItemDestinationFunction = this;
						US_Manager._instance.clickedYellowSauce = true;
						iAmSelected = true;
					}
					else if(isNoodlesToCook)
					{
						China_Manager._instance.AllClickedBoolsReset ();
						China_Manager._instance.clickedItemDestinationFunction = this;
						China_Manager._instance.clickedNoodlesToCook = true;
						iAmSelected = true;
					}
					else if(isNoodlesVeg)
					{
						China_Manager._instance.AllClickedBoolsReset ();
						China_Manager._instance.clickedItemDestinationFunction = this;
						China_Manager._instance.clickedNoodlesVeg = true;
						iAmSelected = true;
					}
					else if(isSoupVeg)
					{
						China_Manager._instance.AllClickedBoolsReset ();
						China_Manager._instance.clickedItemDestinationFunction = this;
						China_Manager._instance.clickedSoupVeg = true;
						iAmSelected = true;
					}
					else if(isNoodlesPlate)
					{
						if(China_Manager._instance.clickedPan)
						{
							if(!China_Manager._instance.clickedUtensilsDestinationFunction.isBurnt)
							{
								China_Manager._instance.clickedUtensilsDestinationFunction.otherObject = this.gameObject;
								China_Manager._instance.UtensilReached ();
							}
							China_Manager._instance.AllClickedBoolsReset ();
						}
						else
						{
							if((tutorialOn && !China_Manager._instance.panUtensil[0].tutorialOn ) || China_Manager.tutorialEnd)
							{
								transform.GetComponent<BoxCollider> ().size = new Vector3(colliderSize.x/2f , colliderSize.y/2f , colliderSize.z);
								China_Manager._instance.AllClickedBoolsReset ();
								China_Manager._instance.clickedItemDestinationFunction = this;
								China_Manager._instance.clickedNoodlePlate = true;
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
						if(China_Manager._instance.clickedSoupContainer)
						{
							China_Manager._instance.clickedUtensilsDestinationFunction.otherObject = this.gameObject;
							China_Manager._instance.UtensilReached ();
							China_Manager._instance.AllClickedBoolsReset ();
						}
						else
						{
							transform.GetComponent<BoxCollider> ().size = new Vector3(colliderSize.x/2f , colliderSize.y/2f , colliderSize.z);
							China_Manager._instance.AllClickedBoolsReset ();
							China_Manager._instance.clickedItemDestinationFunction = this;
							China_Manager._instance.clickSoupBowl = true;
							iAmSelected = true;
						}
					}
					else if(isPizzaVeg)
					{
						Italy_Manager._instance.AllClickedBoolsReset ();
						Italy_Manager._instance.clickedItemDestinationFunction = this;
						Italy_Manager._instance.clickedVeg = true;
						iAmSelected = true;
					}
					else if(isPizzaNonVeg)
					{
						Italy_Manager._instance.AllClickedBoolsReset ();
						Italy_Manager._instance.clickedItemDestinationFunction = this;
						Italy_Manager._instance.clickedNonVeg = true;
						iAmSelected = true;
					}
					else if(isCheese)
					{
						Italy_Manager._instance.AllClickedBoolsReset ();
						Italy_Manager._instance.clickedItemDestinationFunction = this;
						Italy_Manager._instance.clickedCheese = true;
						iAmSelected = true;
					}
					else if(isTomato)
					{
						Australia_Manager._instance.AllClickedBoolsReset ();
						Australia_Manager._instance.clickedItemDestinationFunction = this;
						Australia_Manager._instance.clickedTomato = true;
						iAmSelected = true;
					}
					else if(isOnion)
					{
						Australia_Manager._instance.AllClickedBoolsReset ();
						Australia_Manager._instance.clickedItemDestinationFunction = this;
						Australia_Manager._instance.clickedOnion = true;
						iAmSelected = true;
					}
					else if(isCabbage)
					{
						Australia_Manager._instance.AllClickedBoolsReset ();
						Australia_Manager._instance.clickedItemDestinationFunction = this;
						Australia_Manager._instance.clickedCabbage = true;
						iAmSelected = true;
					}
					else if(isFries)
					{
						Australia_Manager._instance.AllClickedBoolsReset ();
						Australia_Manager._instance.clickedItemDestinationFunction = this;
						Australia_Manager._instance.cickedFries = true;
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
			LevelSoundManager._instance.customerEat.Play ();
			UIManager._instance.n_French_fries_served++;
			PlayerPrefs.SetInt ("FrenchfriesServed", UIManager._instance.n_French_fries_served);
	
			if(PlayerPrefs.GetInt("FrenchfriesServed") > 9 && PlayerPrefs.GetInt ("FrenchfriesLevel1")==0)
			{
				PlayerPrefs.SetInt ("FrenchfriesLevel1",1);
				UIManager._instance.achievment_text.SetActive(true);
				AchievementChild.check_claim++;
				PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
				Invoke("Stopa",4.0f);
			}
			if(PlayerPrefs.GetInt("FrenchfriesServed") > 99 && PlayerPrefs.GetInt ("FrenchfriesLevel2")==0)
			{
				PlayerPrefs.SetInt ("FrenchfriesLevel2",1);
				UIManager._instance.achievment_text.SetActive(true);
				AchievementChild.check_claim++;
				PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
				Invoke("Stopa",4.0f);
			}
			if(PlayerPrefs.GetInt("FrenchfriesServed") > 999 && PlayerPrefs.GetInt ("FrenchfriesLevel3")==0)
			{
				PlayerPrefs.SetInt ("FrenchfriesLevel3" ,1);
				UIManager._instance.achievment_text.SetActive(true);
				AchievementChild.check_claim++;
				PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
				Invoke("Stopa",4.0f);
			}

			if(Australia_Manager._instance != null)
			{
				Australia_Manager._instance.friesFilled--;
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
			
			customer.coinsSpent+=Australia_Manager._instance.cokePrice;
			Australia_Manager._instance.cickedFries = false;

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
				Destroy(UIManager._instance.tutorialPanelCanvas.gameObject); 
				Destroy(UIManager._instance.tutorialPanelBg.gameObject);
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
				availablePizza.myToppings.sprite = Italy_Manager._instance.pizzaToppings[0];
				availablePizza.vegetable = true;
				Italy_Manager._instance.clickedVeg = false;
				if(tutorialOn)
				{
					tutorialOn = false;
					Italy_Manager._instance.cheese.tutorialOn = true;
					UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
					UIManager._instance.tutorialPanelBg.OpenPopupItaly ("TAP OR DRAG CHEESE TO \n PIZZA BASE.",false,false , 2);
				}
			}
		}

		void PizzaNonVegReachedDestination()
		{
			if(!availablePizza.vegetable)
			{
				Italy_Manager._instance.clickedNonVeg = false;
			
				availablePizza.myType = LevelManager.Orders.NON_VEG_PIZZA;
				availablePizza.myToppings.gameObject.SetActive (true);
				availablePizza.myToppings.sprite = Italy_Manager._instance.pizzaToppings[1];
				availablePizza.vegetable = true;
			}
		}

		void PizzaCheeseReachedDestination()
		{
			if(!availablePizza.cheese)
			{
				Italy_Manager._instance.clickedCheese = false;
				availablePizza.myCheese.SetActive (true);
				availablePizza.cheese = true;
				if(tutorialOn)
				{
					tutorialOn = false;
					Italy_Manager._instance.firstPizza.tutorialOn = true;
					UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
					UIManager._instance.tutorialPanelBg.OpenPopupItaly ("TAP OR DRAG PIZZA \n TO OVEN.",false,false , 3);
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
					utensil.scaledImage.GetComponent<SpriteRenderer>().sprite =  China_Manager._instance.noodlesInPanVariations[0];
				}
				else 
				{
					if(tutorialOn)
					{
						tutorialOn = false;
						China_Manager._instance.noodlesVeg.tutorialOn = true;
						UIManager._instance.tutorialPanelBg.OpenPopupChina ("TAP OR DRAG INGREDIENTS\nTO THE SKILLET.",false,false , 1);
					}
					utensil.myFood.sprite = China_Manager._instance.noodlesInPanVariations[0];
					utensil.myScale.enabled = true;
					utensil.myScale.ResetToBeginning ();
					utensil.myScale.PlayForward();
				}
			}
			China_Manager._instance.clickedNoodlesToCook = false;
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
						China_Manager._instance.panUtensil[0].tutorialOn = true;
						UIManager._instance.tutorialPanelBg.OpenPopupChina ("WAIT FOR THE \n NOODLES TO COOK",false,false , 3);
					}
					utensil.scaledImage.gameObject.SetActive (true);
					utensil.scaledImage.ResetToBeginning ();
					utensil.scaledImage.PlayForward();
					utensil.scaledImage.GetComponent<SpriteRenderer>().sprite =  China_Manager._instance.noodlesInPanVariations[2];
					utensil.noOfServingsAvailable = 2;
				}
				else 
				{

					utensil.myScale.enabled = true;
					utensil.myScale.ResetToBeginning ();
					utensil.myScale.PlayForward();
					utensil.myFood.sprite = China_Manager._instance.noodlesInPanVariations[2];
				}
			}
			China_Manager._instance.clickedNoodlesVeg = false;
		}

		private void VegForSoupReachedDestination()
		{
			utensil.vegAdded = true;
			utensil.myFood.gameObject.SetActive (true);
			utensil.noOfServingsAvailable = 2;
			utensil.myAlpha.gameObject.SetActive (true);
			utensil.myAlpha.ResetToBeginning ();
			utensil.myAlpha.PlayForward ();
			China_Manager._instance.clickedSoupVeg = false;
			if(tutorialOn)
			{
				tutorialOn = false;
				China_Manager._instance.soupUtensils[0].tutorialOn = true;
				UIManager._instance.tutorialPanelBg.OpenPopupChina ("TAP OR DRAG THIS TO \n  SOUP BOWL.",false,false , 5);
			}
		}

		private void NoodlesPlateReachedDestination()
		{
			LevelSoundManager._instance.customerEat.Play ();
			UIManager._instance.n_Noodles_served++;
			PlayerPrefs.SetInt ("NoodlesServed",UIManager._instance.n_Noodles_served);

			if(PlayerPrefs.GetInt("NoodlesServed") > 9 && PlayerPrefs.GetInt ("NoodlesLevel1")==0)
			{
				PlayerPrefs.SetInt ("NoodlesLevel1",1);
				MenuManager.totalscore+=100;
				UIManager._instance.achievment_text.SetActive(true);
				AchievementChild.check_claim++;
				PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
				Invoke(nameof(Stopa),4.0f);
			}
			if(PlayerPrefs.GetInt("NoodlesServed") > 99 && PlayerPrefs.GetInt ("NoodlesLevel2")==0)
			{
				PlayerPrefs.SetInt ("NoodlesLevel2",1);
				UIManager._instance.achievment_text.SetActive(true);
				AchievementChild.check_claim++;
				PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
				Invoke(nameof(Stopa),4.0f);

			}
			if(PlayerPrefs.GetInt("NoodlesServed") > 999 && PlayerPrefs.GetInt ("NoodlesLevel3")==0)
			{
				PlayerPrefs.SetInt ("NoodlesLevel3",1);
				UIManager._instance.achievment_text.SetActive(true);
				AchievementChild.check_claim++;
				PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
				Invoke(nameof(Stopa),4.0f);
			}
			China_Manager._instance.platesFilledCount--;
			myParentHolder.available = true;
			customer.myOrder.Remove (myType);
			customer.RemoveOrderFromBoard (myType);

			if(tutorialOn)
			{
				tutorialOn = false;
				China_Manager._instance.firstCustomer.tutorialOn = true;
				UIManager._instance.tutorialPanelCanvas.gameObject.SetActive (false);
				UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
				UIManager._instance.tutorialPanelBg.OpenPopupChina ("PUT BURNT NOODLES\nIN THE DUSTBIN!",false,false , 11 , 1);
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
				customer.coinsSpent+=China_Manager._instance.perfectNoodlesPrice;
				customer.perfect = true;
			}
			else
			{
				customer.coinsSpent+=China_Manager._instance.lessBakedNoodlesPrice;
			}

			if(customer.myOrder.Count <= 0)
				customer.MoveToEndPosition();
		
			transform.position = myOriginalPos;
			myNoodles.SetActive (false);
			transform.gameObject.SetActive(false);
			China_Manager._instance.clickedNoodlePlate = false;
		}

		private void SoupBowlReachedDestination()
		{
			LevelSoundManager._instance.drink.Play ();
			China_Manager._instance.bowlsFilled--;
			myParentHolder.available = true;
			customer.myOrder.Remove (myType);
			customer.RemoveOrderFromBoard (myType);
		
			if(tutorialOn)
			{

				tutorialOn = false;
				UIManager._instance.tutorialPanelBg.gameObject.SetActive (false);
				UIManager._instance.tutorialPanelCanvas.gameObject.SetActive (true);
				UIManager._instance.tutorialPanelCanvas.OpenPopupChina ("ONE SOUP CONTAINER CAN FILL TWO SOUP BOWLS!",true,false , 16 , 1);
			
			}
			if(customer.myOrder.Count > 0)
			{
				customer.myWaitingTime-= 35;
				if(customer.myWaitingTime < 0)
				{
					customer.myWaitingTime = 0;
				}
			}
			customer.coinsSpent+=China_Manager._instance.soupPrice;
			if(customer.shouldBePerfectIfServed)
			{
				customer.perfect = true;
			}
			if(customer.myOrder.Count <= 0)
				customer.MoveToEndPosition();

			mySoup.SetActive (false);
			transform.position = myOriginalPos;
			transform.gameObject.SetActive(false);
			China_Manager._instance.clickSoupBowl = false;
		
		}

		#endregion

		#region USA

		private void RedSauceReachedDestination()
		{
			availableHotDog.redSauce = true;
			US_Manager._instance.hotDogSaucesOnPlates[availableHotDog.GetComponent<Availability>().myPositionInArray].gameObject.SetActive (true);
			US_Manager._instance.hotDogSaucesOnPlates[availableHotDog.GetComponent<Availability>().myPositionInArray].sprite = US_Manager._instance.hotDogSauces[0];
			if(availableHotDog.tikki)
			{
				availableHotDog.transform.GetComponent<HotDog>().myType = LevelManager.Orders.HOTDOG_RED;
			}

			US_Manager._instance.clickedRedSauce = false;
		}

		private void YellowSauceReachedDestination()
		{
			availableHotDog.yellowSauce = true;
			US_Manager._instance.hotDogSaucesOnPlates[availableHotDog.GetComponent<Availability>().myPositionInArray].gameObject.SetActive (true);
			US_Manager._instance.hotDogSaucesOnPlates[availableHotDog.GetComponent<Availability>().myPositionInArray].sprite = US_Manager._instance.hotDogSauces[1];
			if(availableHotDog.tikki)
			{
				availableHotDog.transform.GetComponent<HotDog>().myType = LevelManager.Orders.HOTDOG_YELLOW;
			}
			US_Manager._instance.clickedYellowSauce = false;
		}

		#endregion

		private void CokeReachedDestination()
		{
			UIManager._instance.n_Cokes_served++;
			int rad = Random.Range (0, 6);
			if (rad <= 4) {
				LevelSoundManager._instance.drink.Play ();
			} else {
				LevelSoundManager._instance.drink2.Play ();
			}
			PlayerPrefs.SetInt ("CokesServed",UIManager._instance.n_Cokes_served);
	
			if(PlayerPrefs.GetInt("CokesServed") > 9 && PlayerPrefs.GetInt ("CokeLevel1")==0)
			{
				PlayerPrefs.SetInt ("CokeLevel1",1);
				UIManager._instance.achievment_text.SetActive(true);
				AchievementChild.check_claim++;
				PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
				Invoke(nameof(Stopa),4.0f);
			}
			if(PlayerPrefs.GetInt("CokesServed") > 99 && PlayerPrefs.GetInt ("CokeLevel2")==0)
			{
				PlayerPrefs.SetInt ("CokeLevel2",1);
				UIManager._instance.achievment_text.SetActive(true);
				AchievementChild.check_claim++;
				PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
				Invoke(nameof(Stopa),4.0f);
			}
			if(PlayerPrefs.GetInt("CokesServed") > 999 && PlayerPrefs.GetInt ("CokeLevel3")==0)
			{
				PlayerPrefs.SetInt ("CokeLevel3",1);
				UIManager._instance.achievment_text.SetActive(true);
				AchievementChild.check_claim++;
				PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
				Invoke(nameof(Stopa),4.0f);
			}

			if(US_Manager._instance != null)
			{
				US_Manager._instance.cokesFilled--;
			}
			else if(Italy_Manager._instance != null)
			{
				Italy_Manager._instance.cokesFilled--;
			}
			else if(Australia_Manager._instance != null)
			{
				Australia_Manager._instance.cokesFilled--;
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

			if(US_Manager._instance != null)
			{
				customer.coinsSpent+=US_Manager._instance.cokePrice;
				US_Manager._instance.clickedCoke = false;
			}
			else if(Italy_Manager._instance != null)
			{
				customer.coinsSpent+=Italy_Manager._instance.cokePrice;
				if(isChilled)
				{
					customer.coinsSpent+=15;
				}
				Italy_Manager._instance.clickedCoke = false;
			}
			else if(Australia_Manager._instance != null)
			{
				customer.coinsSpent+=Australia_Manager._instance.cokePrice;
				Australia_Manager._instance.clickedCoke = false;
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
			LevelSoundManager._instance.customerEat.Play ();
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
