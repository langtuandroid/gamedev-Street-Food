using _Project.Scripts.Additional;
using _Project.Scripts.Entities.Customers;
using _Project.Scripts.Food;
using _Project.Scripts.Other;
using _Project.Scripts.UI_Scripts;
using _Project.Scripts.UI.Tutorial;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Managers
{
	public class Australia_Manager : MonoBehaviour
	{
		[Inject] private LevelSoundManager _levelSoundManager;
		[Inject] private UIManager _uiManager;
		public GameObject TheifPanel;
		public int cokePrice => 10;
		public int lessBakedBurger => 20;
		public int perfectBurger => 50;
		public Sprite []burgerTikkiVariations;   //3	
		public SpriteRenderer []burgerTikkiOnPlates; //6
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
		public int grillsFilledCount{ get; set; }
		public int totalGrillsAvailable{ get; set; }
		public int platesFilledCount{ get; set; }
		public int totalPlatesAvailable{ get; set; }
		public int cokesFilled{ get; set; }
		public int totalCokesAvailable{ get; set; }
		public int friesFilled{ get; set; }
		public int totalFriesAvailable{ get; set; }
		public bool clickedBurger{ get; set; }
		public bool clickedTikki{ get; set; }
		public bool clickedTomato{ get; set; }
		public bool clickedOnion{ get; set; }
		public bool clickedCabbage{ get; set; }
		public bool cickedFries{ get; set; }
		public bool clickedCoke{ get; set; }
		public MakeTikki clickedTikkiDestinationFunction { get; set; }
		public ObjectMotion clickedItemDestinationFunction { get; set; }
		public Burger clickedHotDogDestinationFunction { get; set; }
		public GameObject dustbin;
		public ObjectMotion tomato;
		public ObjectMotion onion;
		public ObjectMotion cabbage;
		public ObjectMotion cupCake;
		public GameObject tomatoAdd;
		public GameObject onionAdd;
		public GameObject cabbageAdd;
		public GameObject friesAdd;
		public Burger firstBurger;
		public MakeTikki firstTikki;
		public ObjectMotion firstFries;
		public Customer firstCustomer { get; set; }
		public bool clickfirstBun { get; set; }
		public bool clickFirstTikki { get; set; }
		public bool clickFirstFryer { get; set; }
		public SpriteRenderer tableCover;
		public SpriteRenderer tableTop;
		public GameObject Radio ;
		public GameObject Whistle ;
		public GameObject bell ;
		public GameObject handcuff ;
		public GameObject starting_text ;
		public static bool tutorialEnd;

		private void OnEnable()
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

		private void Start()
		{
			PlayerPrefs.SetInt("AusOpen",1);
			US_Manager.tutorialEnd = false;
			Italy_Manager.tutorialEnd = false;
			China_Manager.tutorialEnd = false;
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
			else if(LevelManager.levelNo == 32)  
			{
				tomatoAdd.gameObject.SetActive (false);
			
				onionAdd.gameObject.SetActive (false);
			
				cabbageAdd.gameObject.SetActive (false);
			}
			else if(LevelManager.levelNo == 33) 
			{
				cabbageAdd.gameObject.SetActive (false);
			}
			
			int platesUpgradeValue =  (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("AusPlateUpgrade")); 
			totalPlatesAvailable = 2+(platesUpgradeValue*2);
			for(int i = 0; i < totalPlatesAvailable ; i++)
			{
				burgerPlates[i].color = new Color(1,1,1,1);
			}

			int friesUpgradeValue =  (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("FriesUpgrade")); 
			totalFriesAvailable = 2+(friesUpgradeValue*2);
			fryer.sprite = fryerVariations[friesUpgradeValue];
			for(int i = 0; i <= friesUpgradeValue ; i++)
			{
				friesCluster[i].SetActive (true);
			}

			int cokeUpgradeValue =  (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("AusCokeUpgrade")); 
			totalCokesAvailable = 3+(cokeUpgradeValue*3);
			int grillsToUpgrade =  (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("AusGrillsUpgrade")); 
			totalGrillsAvailable = 2+(grillsToUpgrade*2);
			int grillVal = (int)totalGrillsAvailable/2;
			for(int i = 0; i < grillVal ; i++)
			{
				grills[i].SetActive (true);
			}

			char []coverVal = PlayerPrefs.GetString ("Aus_TableCover").ToCharArray ();
			_uiManager._tabelcover = int.Parse (coverVal[coverVal.Length - 1].ToString ());
			char []coverVal2 = PlayerPrefs.GetString ("Aus_TableTop").ToCharArray ();
			_uiManager._tabeltop= int.Parse (coverVal[coverVal.Length - 1].ToString ());
			tableCover.sprite = Resources.Load<Sprite> (PlayerPrefs.GetString ("Aus_TableCover"));
			tableTop.sprite =  Resources.Load<Sprite> (PlayerPrefs.GetString ("Aus_TableTop"));
			if(MenuManager.cupcakeNo <= 0)
			{
				cupCake.gameObject.SetActive (false);
			}
			_uiManager.ForCoinAdd ();
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
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopupAustralia ("WAIT FOR THE TIKKI \n TO BAKE",false,false , 2);
					firstTikki.tutorialOn = true;
				}
				clickFirstTikki = false;
			}
		}

		private void DeactivateTikkiSelection()
		{
			for(int i = 0 ; i < totalGrillsAvailable ; i++)
			{
				if(!grillPlaces[i].available)
				{
					grillTikkis[i].transform.GetComponent<MakeTikki>().iAmSelected = false;
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
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopup ("TAP TIKKI \n TO PUT ON THE GRILLS",false,false , 1);
				}
				clickfirstBun = false;
			}
		}

		private void DeactivateBunSelection()
		{
			for(int i = 0 ; i < totalPlatesAvailable ; i++)
			{
				if(!burgerPlates[i].gameObject.GetComponent<Availability>().available)
				{
					Burger myBurger = burgerOnPlates[i].transform.GetComponent<Burger>();
					myBurger.iAmSelected = false;
					myBurger.mySelection.SetActive (false);
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
							_levelSoundManager.bottle_click.Play();
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

		private void DeactivateAllBottlesSelection()
		{
			for(int i = 0 ; i < totalCokesAvailable ; i++)
			{
				if(cokeBottles[i].gameObject.activeInHierarchy)
				{
					if(cokeBottles[i].GetComponent<ObjectMotion>().iAmSelected)
					{
						cokeBottles[i].GetComponent<ObjectMotion>().iAmSelected = false;
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
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopupAustralia ("TAP OR DRAG FRIES TO \n THE CUSTOMER.",false,false , 9);
					firstFries.tutorialOn = true;

				}
				clickFirstFryer = false;
			}
		}

		private void DeactivateAllFriesSelection()
		{
			for(int i = 0 ; i < totalFriesAvailable ; i++)
			{
				if(friesPack[i].gameObject.activeInHierarchy)
				{
					if(friesPack[i].iAmSelected)
					{
						friesPack[i].iAmSelected = false;
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
			
			onion.iAmSelected = false;
			tomato.iAmSelected = false;
			onion.transform.localScale = onion.myLocalScale;
			tomato.transform.localScale = tomato.myLocalScale;
			tomato.mySelection.SetActive (false);
			onion.mySelection.SetActive (false);
			
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
}
