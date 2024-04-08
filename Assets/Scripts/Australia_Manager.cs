using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Australia_Manager : MonoBehaviour {

	public GameObject TheifPanel;

	public int cokePrice;

	public int lessBakedBurger;

	public int perfectBurger;

	public int friesPrice;
	
	public Sprite []burgerTikkiVariations;   //3	
	public SpriteRenderer []burgerTomatoOnPlates; //6
	public SpriteRenderer []burgerTikkiOnPlates; //6
	public SpriteRenderer []burgerCabbageOnPlates; //6
	public SpriteRenderer []burgerOnionOnPlates; //6
	public SpriteRenderer []burgerPlates; //6
	public SpriteRenderer []burgerOnPlates; //6

	public GameObject []grills; //3
	public SpriteRenderer []grillTikkis;  //6
	public Availability []grillPlaces; //6

	public SpriteRenderer []cokeBottles; //9
	public Availability []cokePlaces;  //9

	public SpriteRenderer fryer;
	public Sprite []fryerVariations;   //3
	public GameObject []friesCluster;   //3
	public ObjectMotion []friesPack; //9
	public Availability []friesPlaces;  //9

	public int grillsFilledCount;

	public int totalGrillsAvailable;

	public int platesFilledCount;

	public int totalPlatesAvailable;

	public int cokesFilled;

	public int totalCokesAvailable;

	public int friesFilled;
	
	public int totalFriesAvailable;

	public static Australia_Manager _instance;

	public bool clickedBurger , clickedTikki , clickedTomato , clickedOnion , clickedCabbage , cickedFries , clickedCoke;

	public MakeTikki clickedTikkiDestinationFunction;

	public ObjectMotion clickedItemDestinationFunction;

	public Burger clickedHotDogDestinationFunction;

	public GameObject dustbin;

	public ObjectMotion tomato , onion , cabbage  , cupCake;

	public GameObject tomatoAdd , onionAdd , cabbageAdd , friesAdd;

	public static int noOfPerfects;

	public Burger firstBurger;

	public MakeTikki firstTikki;

	public ObjectMotion firstFries;
	
	public Customer firstCustomer;

	public bool clickfirstBun , clickFirstTikki , clickFirstFryer;

	public SpriteRenderer tableCover,tableTop;

	public GameObject Radio ;
	public GameObject Whistle ;
	public GameObject bell ;
	public GameObject handcuff ;
	public GameObject starting_text ;
	public static bool tutorialEnd;
	public bool a ;

	public GameObject upgrade_bttn ;
	void OnEnable()
	{
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
	// Use this for initialization
	void Awake () {
		_instance = this;
	}


	public void TikkiReached()
	{
		clickedTikkiDestinationFunction.ClickedDestination ();
	}

	public void HotDogReached()
	{
		clickedHotDogDestinationFunction.ClickedDestination ();
	}

	public void ObjectReached()
	{
		clickedItemDestinationFunction.ClickedDestination ();
	}

	void Start()
	{
		PlayerPrefs.SetInt("AusOpen",1);
		US_Manager.tutorialEnd = false;
		Italy_Manager.tutorialEnd = false;
		China_Manager.tutorialEnd = false;
		a = true ;
		if (LevelManager.levelNo == 31) {
			starting_text.SetActive(true);
		}
		tutorialEnd = false;
		if(LevelManager.levelNo == 31)
		{
			tomatoAdd.gameObject.SetActive (false);

			onionAdd.gameObject.SetActive (false);

			cabbageAdd.gameObject.SetActive (false);

			friesAdd.gameObject.SetActive (false);

		}
		else if(LevelManager.levelNo == 32)  // bring fries
		{
			tomatoAdd.gameObject.SetActive (false);
			
			onionAdd.gameObject.SetActive (false);
			
			cabbageAdd.gameObject.SetActive (false);
		}
		else if(LevelManager.levelNo == 33) // tomato n onion,, level 34 has cabbage
		{
			cabbageAdd.gameObject.SetActive (false);
		}
		else if(LevelManager.levelNo > 34)
		{
//			tutorialEnd = true;
		}

		int platesUpgradeValue =  (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("AusPlateUpgrade")); 
		//Debug.Log("platesUpgradeValue = "+platesUpgradeValue);
		
		totalPlatesAvailable = 2+(platesUpgradeValue*2);
		for(int i = 0; i < totalPlatesAvailable ; i++)
		{
			burgerPlates[i].color = new Color(1,1,1,1);
		}

		int friesUpgradeValue =  (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("FriesUpgrade")); 
		//Debug.Log("friesUpgradeValue = "+friesUpgradeValue);
		totalFriesAvailable = 2+(friesUpgradeValue*2);
		fryer.sprite = fryerVariations[friesUpgradeValue];
		for(int i = 0; i <= friesUpgradeValue ; i++)
		{
			friesCluster[i].SetActive (true);

		}

		int cokeUpgradeValue =  (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("AusCokeUpgrade")); 
		//Debug.Log("cokeUpgradeValue = "+cokeUpgradeValue);
		totalCokesAvailable = 3+(cokeUpgradeValue*3);

		int grillsToUpgrade =  (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("AusGrillsUpgrade")); 
		//Debug.Log("grillsToUpgrade = "+grillsToUpgrade);
		totalGrillsAvailable = 2+(grillsToUpgrade*2);

		int grillVal = (int)totalGrillsAvailable/2;
		//Debug.Log("grillVal = "+grillVal);
		for(int i = 0; i < grillVal ; i++)
		{
			grills[i].SetActive (true);
		}
		//Debug.Log(PlayerPrefs.GetString ("Aus_TableCover"));

		char []coverVal = PlayerPrefs.GetString ("Aus_TableCover").ToCharArray ();
		
		UIManager._instance._tabelcover = int.Parse (coverVal[coverVal.Length - 1].ToString ());
		
	//	Debug.Log( "val = "+AUS_last1);

		char []coverVal2 = PlayerPrefs.GetString ("Aus_TableTop").ToCharArray ();
		
		UIManager._instance._tabeltop= int.Parse (coverVal[coverVal.Length - 1].ToString ());
		
	//	Debug.Log( "val = "+AUS_last2);

		tableCover.sprite = Resources.Load<Sprite> (PlayerPrefs.GetString ("Aus_TableCover"));
		tableTop.sprite =  Resources.Load<Sprite> (PlayerPrefs.GetString ("Aus_TableTop"));
		if(MenuManager.cupcakeNo <= 0)
		{
			cupCake.gameObject.SetActive (false);
		}
		UIManager._instance.ForCoinAdd ();
	}


	public void AddMoreTikki()
	{
		if(clickFirstTikki || (tutorialEnd && !TutorialPanel.popupPanelActive))
		{
			AllClickedBoolsReset();
			if(grillsFilledCount < totalGrillsAvailable)
			{
				for(int i = 0 ; i < totalGrillsAvailable ; i++)
				{
					if(grillPlaces[i].available)
					{
						grillTikkis[i].gameObject.SetActive (true);
						grillTikkis[i].sprite = burgerTikkiVariations[0];
						grillPlaces[i].available = false;
						grillsFilledCount++;
						break;
					}
				}
			}
			if(clickFirstTikki)
			{
				UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
				UIManager._instance.tutorialPanelBg.OpenPopupAustralia ("WAIT FOR THE TIKKI \n TO BAKE",false,false , 2);
				firstTikki.tutorialOn = true;
			}
			clickFirstTikki = false;
		}
	}

	public void DeactivateTikkiSelection()
	{
		for(int i = 0 ; i < totalGrillsAvailable ; i++)
		{
			if(!grillPlaces[i].available)
			{
				grillTikkis[i].transform.GetComponent<MakeTikki>().iAmSelected = false;
				grillTikkis[i].transform.GetComponent<MakeTikki>().startAnimating = false;
				grillTikkis[i].transform.GetComponent<MakeTikki>().mySelection.SetActive (false);
			}
		}
	}


	public void AddBurgerBuns()
	{
		if(clickfirstBun || (tutorialEnd && !TutorialPanel.popupPanelActive))
		{
			AllClickedBoolsReset();
			if(platesFilledCount < totalPlatesAvailable)
			{
				for(int i = 0 ; i < totalPlatesAvailable ; i++)
				{
					if(burgerPlates[i].gameObject.GetComponent<Availability>().available)
					{
						burgerOnPlates[i].gameObject.SetActive (true);
//						burgerOnPlates[i].sprite = hotDogVariations[0];
						platesFilledCount++;
						burgerPlates[i].gameObject.GetComponent<Availability>().available = false;
						burgerOnPlates[i].transform.GetComponent<Burger>().perfect = false;
						break;
					}
				}
			}
			if(clickfirstBun)
			{
				clickFirstTikki = true;
				UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
				UIManager._instance.tutorialPanelBg.OpenPopup ("TAP TIKKI \n TO PUT ON THE GRILLS",false,false , 1);
			}
			clickfirstBun = false;
		}
	}

	public void DeactivateBunSelection()
	{
		for(int i = 0 ; i < totalPlatesAvailable ; i++)
		{
			if(!burgerPlates[i].gameObject.GetComponent<Availability>().available)
			{
				Burger myBurger = burgerOnPlates[i].transform.GetComponent<Burger>();
				myBurger.iAmSelected = false;
				myBurger.startAnimating = false;
				myBurger.mySelection.SetActive (false);
//				burgerOnPlates[i].transform.localScale = myBurger.myLocalScale;
			}
		}
	}


	public void AddCokeBottles()
	{
		if(tutorialEnd && !TutorialPanel.popupPanelActive)
		{
			AllClickedBoolsReset();
			if(cokesFilled < totalCokesAvailable)
			{
				for(int i = 0 ; i < totalCokesAvailable ; i++)
				{
					if(cokePlaces[i].available)
					{
						LevelSoundManager._instance.bottle_click.Play();
						cokeBottles[i].gameObject.SetActive (true);
						cokeBottles[i].color = new Color(1,1,1,1);
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
				}
			}
		}
	}

	public void AddFries()
	{
		if(tutorialEnd && !TutorialPanel.popupPanelActive || clickFirstFryer)
		{
			AllClickedBoolsReset();

			if(friesFilled < totalFriesAvailable)
			{
				for(int i = 0 ; i < totalFriesAvailable; i++)
				{
					if(friesPlaces[i].available)
					{

						friesPack[i].gameObject.SetActive (true);
						friesFilled++;
						friesPlaces[i].available = false;
						break;
					}
				}
			}

			if(clickFirstFryer)
			{
				UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
				UIManager._instance.tutorialPanelBg.OpenPopupAustralia ("TAP OR DRAG FRIES TO \n THE CUSTOMER.",false,false , 9);
				firstFries.tutorialOn = true;

			}
			clickFirstFryer = false;
		}
	}
	
	public void DeactivateAllFriesSelection()
	{
		for(int i = 0 ; i < totalFriesAvailable ; i++)
		{
			if(friesPack[i].gameObject.activeInHierarchy)
			{
				if(friesPack[i].iAmSelected)
				{
					friesPack[i].iAmSelected = false;
					friesPack[i].startAnimating = false;
					friesPack[i].mySelection.SetActive (false);
				}
			}
		}
	}

	

	public void OnClickDustbn()
	{
		if(tutorialEnd)
		{
			if(clickedBurger)
			{
				clickedHotDogDestinationFunction.otherObject = dustbin;
				HotDogReached();
			}
			else if(clickedTikki)
			{
				if(clickedTikkiDestinationFunction.isBurnt)
				{
					TikkiReached ();
				}
			}

			AllClickedBoolsReset();
		}
	}

	public void AllClickedBoolsReset()
	{
		DeactivateTikkiSelection();
		DeactivateAllBottlesSelection();
		DeactivateAllFriesSelection();

		onion.startAnimating = false;
		tomato.startAnimating = false;
		onion.iAmSelected = false;
		tomato.iAmSelected = false;
		onion.transform.localScale = onion.myLocalScale;
		tomato.transform.localScale = tomato.myLocalScale;
		tomato.mySelection.SetActive (false);
		onion.mySelection.SetActive (false);

		cabbage.startAnimating = false;
		cabbage.iAmSelected = false;
		cabbage.transform.localScale = cabbage.myLocalScale;
		cabbage.mySelection.SetActive (false);

		cupCake.iAmSelected = false;
		cupCake.mySelection.gameObject.SetActive (false);
		DeactivateBunSelection();
		clickedBurger = false;
		clickedTikki = false;
		clickedTomato = false;
		clickedOnion = false;
		clickedCabbage = false;
		cickedFries = false;
		clickedCoke = false;
	}
}
