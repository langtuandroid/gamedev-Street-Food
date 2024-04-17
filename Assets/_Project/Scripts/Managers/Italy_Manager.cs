﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Italy_Manager : MonoBehaviour {

	public GameObject TheifPanel;
	public int cokePrice;

	public int lessBakedPizza;

	public int perfectPizza;

	public Sprite []pizzaBakedVariations;  //3 -- 0 - pizze base , 1-veg baked ,2- non veg baked  
	public Sprite[]pizzaDot ;
	public Sprite []pizzaToppings; //2  0 - veg , 1 - nonVeg
	public SpriteRenderer []pizzaToppingsOnPlate; //6
	public GameObject []pizzaCheeseOnPlate; //6
	public SpriteRenderer []pizzaPlates; //6
	public SpriteRenderer []pizzaOnPlates; //6
	public BoxCollider []plateColliders; //6


	public SpriteRenderer []ovens; //6
	public Sprite ovenHot;
	public SpriteRenderer []ovenPizzaRenderer;  //6
	public Availability []ovenPlaces; //6
	public BoxCollider []ovenColliders; //6
	public Pizza []ovenPizzas; //6
	
	public SpriteRenderer []cokeBottles; //9
	public Availability []cokePlaces;  //9
	public bool []fridePlaces;  //9
	public GameObject []frideBottles;  //9

	public int ovenFilledCount;

	public int totalOvensAvailable;

	public int platesFilledCount;

	public int totalPlatesAvailable;

	public int cokesFilled;

	public int totalCokesAvailable;

	public int fridgeFilledCount;

	public static Italy_Manager _instance;

	public bool clickedNonVeg , clickedVeg , clickedCheese , clickedChilledCoke , clickedCoke , clickedOvenPizza , clickedPlatePizza;

	public MakeTikki clickedTikkiDestinationFunction;

	public ObjectMotion clickedItemDestinationFunction;

	public Pizza clickedPizzaDestinationFunction;

	public GameObject dustbin;

	public ObjectMotion nonVeg , vegetables , cheese , cupCake;

	public SpriteRenderer cokeAdd , nonVegAdd ;

	public static int noOfPerfects;

	public Pizza firstPizza ;
	public Availability firstOvenAvailabe;
	public Pizza firstOvenPizza;
	
	public Coins firstCoins;

	public Customer firstCustomer;

	public bool clickfirstBase ;

	public SpriteRenderer tableTop, tableCover;

	public GameObject cokeFridge;

	public bool hasFridge;
	
	public static bool tutorialEnd;

	public GameObject Radio ;
	public GameObject Whistle ;
	public GameObject bell ;
	public GameObject handcuff ;
	public GameObject nonvegflag;  
	public GameObject starting_text ;

	public bool i2 ;
	public GameObject upgrade_bttn ;

	public Sprite []cokeBottlesSprites; //0 - simple , 1 - chilled
	// Use this for initialization
	void Awake () {
		_instance = this;
	}

	void OnEnable()
	{
		if (LevelManager.levelNo == 21) {
			starting_text.SetActive(true);
		}
		if(PlayerPrefs.HasKey ("Radio"))
		{
			
			Radio.SetActive(true);
		}
		if(PlayerPrefs.HasKey ("Whistle"))
		{

			Whistle.SetActive(true);
		}
		if (PlayerPrefs.HasKey ("Bell"))
		{
			
			bell.SetActive(true);
		}
		if(MenuManager.handcuffNo > 0)
		{
			handcuff.SetActive(true);
		}
		
	}

	public void PizzaReached()
	{
		clickedPizzaDestinationFunction.ClickedDestination ();
	}

	public void ObjectReached()
	{
		clickedItemDestinationFunction.ClickedDestination ();
	}

	void Start()
	{
		PlayerPrefs.SetInt ("ItalyOpen",1);
		China_Manager.tutorialEnd = false;
		US_Manager.tutorialEnd = false;
		Australia_Manager.tutorialEnd = false;
		tutorialEnd = false;
		//Debug.Log("LevelManager.levelNo = "+LevelManager.levelNo);
		if(LevelManager.levelNo == 21)
		{
			cokeAdd.gameObject.SetActive (false);
			nonVegAdd.gameObject.SetActive (false);
			nonvegflag.gameObject.SetActive(false);

		}
		else if(LevelManager.levelNo == 22)
		{
			cokeAdd.gameObject.SetActive (false);
		}
		else if(LevelManager.levelNo > 23)
		{
//			tutorialEnd = true;
		}

		int platesUpgradeValue =  (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("ItalyPlateUpgrade")); 
		//Debug.Log("platesUpgradeValue = "+platesUpgradeValue);
		
		totalPlatesAvailable = 2+(platesUpgradeValue*2);
		for(int i = 0; i < totalPlatesAvailable ; i++)
		{
			pizzaPlates[i].color = new Color(1,1,1,1);
		}

		int cokeUpgradeValue =  (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("ItalyCokeUpgrade")); 
		//Debug.Log("cokeUpgradeValue = "+cokeUpgradeValue);
		
		totalCokesAvailable = 2+(cokeUpgradeValue*2);

		int ovenToUpgrade =  (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("OvenUpgrade")); 
		//Debug.Log("ovenToUpgrade = "+ovenToUpgrade);
		totalOvensAvailable = 2+(ovenToUpgrade*2);
	
		ovenPlaces[0].available = true;
		ovenColliders[0].enabled = true;
		for(int i = 1; i < totalOvensAvailable ; i++)
		{
			ovens[i].sprite = ovenHot;
			ovenPlaces[i].available = true;
			ovenColliders[i].enabled = true;
		}

		tableCover.sprite = Resources.Load<Sprite> (PlayerPrefs.GetString ("Italy_TableCover"));
		tableTop.sprite = Resources.Load<Sprite> (PlayerPrefs.GetString ("Italy_TableTop")) as Sprite;
		
		char []coverVal = PlayerPrefs.GetString ("Italy_TableCover").ToCharArray ();
		
		UIManager._instance._tabelcover = int.Parse (coverVal[coverVal.Length - 1].ToString ());
		
		//Debug.Log( "val = "+Italy_last1);
		
		char []coverVal2 = PlayerPrefs.GetString ("Italy_TableTop").ToCharArray ();
		
		UIManager._instance._tabeltop = int.Parse (coverVal[coverVal.Length - 1].ToString ());
		
		//Debug.Log( "val = "+Italy_last2);
		if(MenuManager.cupcakeNo <= 0)
		{
			cupCake.gameObject.SetActive (false);
		}

		UIManager._instance.ForCoinAdd ();
		if(!PlayerPrefs.HasKey ("Fridge"))
		{
//			if(LevelManager.levelNo < 23)
//			{
				cokeFridge.SetActive (false);
				fridgeFilledCount = totalCokesAvailable;
				hasFridge = false;
//			}
		}
		else
		{

			if(LevelManager.levelNo < 23)
			{
				cokeFridge.SetActive (false);
				fridgeFilledCount = totalCokesAvailable;
				hasFridge = false;
			}
			else
			{
				hasFridge = true;
			}
		}
	}

	


	public void AddMoreBases()
	{
		if(clickfirstBase || (tutorialEnd && !TutorialPanel.popupPanelActive))
		{
			AllClickedBoolsReset();
			if(platesFilledCount < totalPlatesAvailable)
			{
				for(int i = 0 ; i < totalPlatesAvailable ; i++)
				{
					if(pizzaPlates[i].gameObject.GetComponent<Availability>().available)
					{
						plateColliders[i].enabled = false;
						pizzaOnPlates[i].gameObject.SetActive (true);
						platesFilledCount++;
						pizzaPlates[i].gameObject.GetComponent<Availability>().available = false;
						pizzaOnPlates[i].transform.GetComponent<Pizza>().perfect = false;
						break;
					}
				}
			}
			if(clickfirstBase)
			{
				vegetables.tutorialOn = true;
				UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
				UIManager._instance.tutorialPanelBg.OpenPopupItaly ("TAP OR DRAG VEGETABELS \n TO PIZZA BASE.",false,false , 1);
			}
			clickfirstBase = false;
		}
	}
	
	public void DeactivatePlateSelection()
	{
		for(int i = 0 ; i < totalPlatesAvailable ; i++)
		{
			if(!pizzaPlates[i].gameObject.GetComponent<Availability>().available)
			{
				Pizza myPizza = pizzaOnPlates[i].transform.GetComponent<Pizza>();
				myPizza.iAmSelected = false;
				myPizza.startAnimating = false;
				myPizza.mySelection.SetActive (false);
			}
		}
	}

	public void DeactivateOvenSelection()
	{
		for(int i = 0 ; i < totalOvensAvailable ; i++)
		{
			if(!ovenPlaces[i].available)
			{
				ovenPizzas[i].iAmSelected = false;
				ovenPizzas[i].startAnimating = false;
				ovenPizzas[i].mySelection.SetActive (false);
			}
		}
	}


	public void AddCokeBottlesFromFridge()
	{
		if(fridgeFilledCount > 0)
		{
			if(cokesFilled < totalCokesAvailable)
			{
				for(int i = 0 ; i < totalCokesAvailable ; i++)
				{
					if(cokePlaces[i].available)
					{
						LevelSoundManager._instance.bottle_click.Play();
						cokeBottles[i].gameObject.SetActive (true);
						cokeBottles[i].color = new Color(1,1,1,1);
						cokeBottles[i].sprite = cokeBottlesSprites[1];
						cokesFilled++;
						cokeBottles[i].gameObject.GetComponent<ObjectMotion>().isChilled = true;
						for(int j = 0 ; j < totalCokesAvailable ; j++)
						{
							if(!fridePlaces[j])
							{
								frideBottles[j].SetActive (false);
								fridgeFilledCount--;
								fridePlaces[j] = true;
								break;
							}
						}
						cokePlaces[i].available = false;
						break;
					}
				}
			}
		}
	}
	


	public void AddCokeBottles()
	{
		if(tutorialEnd && !TutorialPanel.popupPanelActive)
		{
			AllClickedBoolsReset();
		
			if(fridgeFilledCount < totalCokesAvailable && hasFridge)
			{
				for(int i = 0 ; i < totalCokesAvailable ; i++)
				{
					if(fridePlaces[i])
					{
						LevelSoundManager._instance.bottle_click.Play();
						frideBottles[i].SetActive (true);
						fridgeFilledCount++;
						fridePlaces[i] = false;
						break;
					}
				}
			}

			else if(cokesFilled < totalCokesAvailable)
			{
				for(int i = 0 ; i < totalCokesAvailable ; i++)
				{
					if(cokePlaces[i].available)
					{
						LevelSoundManager._instance.bottle_click.Play();
						cokeBottles[i].gameObject.SetActive (true);
						cokeBottles[i].color = new Color(1,1,1,1);
						cokeBottles[i].sprite = cokeBottlesSprites[0];
						cokesFilled++;
						cokePlaces[i].available = false;
						break;
					}
				}
			}
		}
	}

	public void DeactivateAllBottlesSelection()
	{
		for(int i = 0 ; i < totalCokesAvailable ; i++)
		{
			if(cokeBottles[i].gameObject.activeInHierarchy)
			{
				if(cokeBottles[i].GetComponent<ObjectMotion>().iAmSelected)
				{
					cokeBottles[i].GetComponent<ObjectMotion>().iAmSelected = false;
					cokeBottles[i].GetComponent<ObjectMotion>().startAnimating = false;
					cokeBottles[i].GetComponent<ObjectMotion>().mySelection.SetActive (false);
					cokeBottles[i].gameObject.transform.localScale = Vector3.one;
				}
			}
		}
	}


	

	public void OnClickDustbin()
	{
		if(tutorialEnd)
		{
			if(clickedOvenPizza || clickedPlatePizza)
			{
				clickedPizzaDestinationFunction.otherObject = dustbin;
				PizzaReached();
			}

			AllClickedBoolsReset();
		}
	}


	void OnClickOven(Availability ovenAvailability)
	{

		if(clickedPlatePizza)
		{
			clickedPizzaDestinationFunction.otherObject = ovenAvailability.gameObject;
			clickedPizzaDestinationFunction.pizzaDestinationAvailable = ovenAvailability;
			PizzaReached();
		}
		AllClickedBoolsReset();
	}
	public void OnClickOven1()
	{
		if(tutorialEnd || firstPizza.tutorialOn)
		{
			OnClickOven (ovenPlaces[0]);
		}
	}

	public void OnClickOven2()
	{
		if(tutorialEnd )
		{
			OnClickOven (ovenPlaces[1]);
		}
	}

	public void OnClickOven3()
	{
		if(tutorialEnd )
		{
			OnClickOven (ovenPlaces[2]);
		}
	}
	public void OnClickOven4()
	{
		if(tutorialEnd )
		{
			OnClickOven (ovenPlaces[3]);
		}
	}
	public void OnClickOven5()
	{
		if(tutorialEnd )
		{
			OnClickOven (ovenPlaces[4]);
		}
	}
	public void OnClickOven6()
	{
		if(tutorialEnd)
		{
			OnClickOven (ovenPlaces[5]);
		}
	}


	void OnClickPlate(Availability plateAvailability)
	{
		if(tutorialEnd || firstPizza.tutorialOn)
		{
			if(clickedOvenPizza)
			{
				clickedPizzaDestinationFunction.otherObject = plateAvailability.gameObject;
				clickedPizzaDestinationFunction.pizzaDestinationAvailable = plateAvailability;
				PizzaReached();
			}
			AllClickedBoolsReset();
		}
	}
	public void OnClickPlate1()
	{
		OnClickPlate (pizzaPlates[0].GetComponent<Availability>());
	}
	
	public void OnClickPlate2()
	{
		OnClickPlate (pizzaPlates[1].GetComponent<Availability>());
	}
	
	public void OnClickPlate3()
	{
		OnClickPlate (pizzaPlates[2].GetComponent<Availability>());
	}
	public void OnClickPlate4()
	{
		OnClickPlate (pizzaPlates[3].GetComponent<Availability>());
	}
	public void OnClickPlate5()
	{
		OnClickPlate (pizzaPlates[4].GetComponent<Availability>());
	}
	public void OnClickPlate6()
	{
		OnClickPlate (pizzaPlates[5].GetComponent<Availability>());
	}

	public void AllClickedBoolsReset()
	{
		DeactivateOvenSelection();
		DeactivatePlateSelection();
		DeactivateAllBottlesSelection();


		nonVeg.startAnimating = false;
		nonVeg.iAmSelected = false;
		nonVeg.mySelection.SetActive (false); 

		vegetables.startAnimating = false;
		vegetables.iAmSelected = false;
		vegetables.mySelection.SetActive (false);

		cheese.startAnimating = false;
		cheese.iAmSelected = false;
		cheese.mySelection.SetActive (false);

		cupCake.startAnimating = false;
		cupCake.iAmSelected = false;
		cupCake.mySelection.SetActive (false);;
	

		clickedNonVeg = false;
		clickedVeg = false;
		clickedCheese = false;
		clickedChilledCoke = false;
		clickedCoke = false;
		clickedOvenPizza = false;
		clickedPlatePizza = false;
	}
}
