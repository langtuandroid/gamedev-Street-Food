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
	public class US_Manager : MonoBehaviour 
	{
		[Inject] private UIManager _uiManager;   
		[Inject] private LevelSoundManager _levelSoundManager;
		public GameObject TheifPanel;
		public int cokePrice => 10;
		public int lessBakedHotdog => 20;
		public int perfectHotDog => 40;
		public Sprite []hotDogTikkiVariations;   //3
		public Sprite []hotDogOrderVariations;   //3
		public Sprite []hotDogVariations;  //2 -- 0 - empty , 1 - filled
		public Sprite []hotDogSauces; //2  0 - red , 1 - yellow
		public SpriteRenderer []hotDogSaucesOnPlates; //6
		public SpriteRenderer []hotdogPlates; //6
		public SpriteRenderer []hotdogOnPlates; //6
		public GameObject []grills; //3
		public SpriteRenderer []grillTikkis;  //6
		public Availability []grillPlaces; //6
		public SpriteRenderer []cokeBottles; //9
		public Availability []cokePlaces;  //9
		public int grillsFilledCount { get; set; }
		public int totalGrillsAvailable { get; set; } = 2;
		public int platesFilledCount { get; set; }
		public int totalPlatesAvailable { get; set; } = 2;
		public int cokesFilled { get; set; }
		public int totalCokesAvailable { get; set; } = 3;
		public bool clickedHotDog { get; set; }
		public bool clickedTikki { get; set; }
		public bool clickedRedSauce { get; set; }
		public bool clickedYellowSauce { get; set; }
		public bool clickedCoke { get; set; }
		public MakeTikki clickedTikkiDestinationFunction { get; set; }
		public ObjectMotion clickedItemDestinationFunction { get; set; }
		public HotDog clickedHotDogDestinationFunction { get; set; }
		public GameObject dustbin;
		public ObjectMotion redSauce;
		public ObjectMotion yellowSauce;
		public ObjectMotion cupCake;
		public SpriteRenderer cokeAdd;
		public SpriteRenderer yellowSauceAdd;
		public SpriteRenderer redSauceAdd;
		public static int noOfPerfects;
		public HotDog firstHotDog;
		public MakeTikki firstTikki;
		public Coins firstCoins;
		public Wisitor firstCustomer { get; set; }
		public bool clickfirstBun { get; set; }
		public bool clickFirstTikki { get; set; }
		public SpriteRenderer tableTop, tableCover;
		public static bool tutorialEnd;
		public GameObject Radio ;
		public GameObject whistle ;
		public GameObject Bell ;
		public GameObject handcuff ;
		public GameObject starting_text ;
		
		private void OnEnable()
		{
			if (LevelManager.levelNo == 1) {
				starting_text.SetActive(true);
			}
			if(PlayerPrefs.HasKey ("Radio"))
			{
				Radio.SetActive(true);
			}
			if(PlayerPrefs.HasKey ("Whistle"))
			{
				whistle.SetActive(true);
			}
			if (PlayerPrefs.HasKey ("Bell"))
			{
				Bell.SetActive(true);
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
			tutorialEnd = false;
			if(LevelManager.levelNo == 1)
			{
				starting_text.SetActive(true);
				cokeAdd.gameObject.SetActive (false);
				cokeAdd.color = new Color(1,1,1,0.5f); 
				cokeAdd.transform.GetComponent<BoxCollider>().enabled = false;
				yellowSauceAdd.gameObject.SetActive (false);
				yellowSauceAdd.color = new Color(1,1,1,0.5f); 
				yellowSauceAdd.transform.GetComponent<BoxCollider>().enabled = false;
				redSauceAdd.gameObject.SetActive (false);
				redSauceAdd.color = new Color(1,1,1,0.5f); 
				redSauceAdd.transform.GetComponent<BoxCollider>().enabled = false;

			}
			else if(LevelManager.levelNo == 2)
			{
				cokeAdd.gameObject.SetActive (false);
				cokeAdd.transform.GetComponent<BoxCollider>().enabled = false;
				cokeAdd.color = new Color(1,1,1,0.5f); 
			}
		
			int platesUpgradeValue =  (int)Encryption.Decrypt (PlayerPrefs.GetString("PlateUpgrade")); 
			totalPlatesAvailable = 2+(platesUpgradeValue*2);
			for(int i = 0; i < totalPlatesAvailable ; i++)
			{
				hotdogPlates[i].color = new Color(1,1,1,1);
			}

			int cokeUpgradeValue =  (int)Encryption.Decrypt (PlayerPrefs.GetString("USCokeUpgrade")); 
			totalCokesAvailable = 3+(cokeUpgradeValue*3);
			int grillsTpgrade =  (int)Encryption.Decrypt (PlayerPrefs.GetString("GrillsUpgrade")); 
			totalGrillsAvailable = 2+(grillsTpgrade*2);
			int grillVal = (int)totalGrillsAvailable/2;
			for(int i = 0; i < grillVal ; i++)
			{
				grills[i].SetActive (true);
			}
			char []coverVal = PlayerPrefs.GetString ("US_TableCover").ToCharArray ();
			_uiManager._tabelcover = int.Parse (coverVal[coverVal.Length - 1].ToString ());
			_uiManager.ForCoinAdd ();
			tableCover.sprite = Resources.Load<Sprite> (PlayerPrefs.GetString ("US_TableCover"));
			tableTop.sprite = Resources.Load<Sprite> (PlayerPrefs.GetString ("US_TableTop")) as Sprite;
			if(MenuManager.cupcakeNo <= 0)
			{
				cupCake.gameObject.SetActive (false);
			}

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
							grillTikkis[i].sprite = hotDogTikkiVariations[0];
							grillPlaces[i].available = false;
							grillsFilledCount++;
							break;
						}
					}
				}
				if(clickFirstTikki)
				{
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopup ("WAIT FOR THE SAUSAGE \n TO BAKE",false,false , 2);
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
					grillTikkis[i].transform.localScale = Vector3.one;
				}
			}
		}
	
		public void AddHotDogBuns()
		{
			if(clickfirstBun || (tutorialEnd && !TutorialPanel.popupPanelActive))
			{
				AllClickedBoolsReset();
				if(platesFilledCount < totalPlatesAvailable)
				{
					for(int i = 0 ; i < totalPlatesAvailable ; i++)
					{
						if(hotdogPlates[i].gameObject.GetComponent<Availability>().available)
						{
							hotdogOnPlates[i].gameObject.SetActive (true);
							hotdogOnPlates[i].sprite = hotDogVariations[0];
							platesFilledCount++;
							hotdogPlates[i].gameObject.GetComponent<Availability>().available = false;
							hotdogOnPlates[i].transform.GetComponent<HotDog>().perfect = false;
							break;
						}
					}
				}
				if(clickfirstBun)
				{
					clickFirstTikki = true;
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopup ("TAP SAUSAGE TO PUT \n ON THE GRILLS",false,false , 1);
				}
				clickfirstBun = false;
			}
		}

		private void DeactivateBunSelection()
		{
			for(int i = 0 ; i < totalPlatesAvailable ; i++)
			{
				if(!hotdogPlates[i].gameObject.GetComponent<Availability>().available)
				{
					HotDog myHotDog = hotdogOnPlates[i].transform.GetComponent<HotDog>();
					myHotDog.iAmSelected = false;
					myHotDog.mySelection.SetActive (false);
					hotdogOnPlates[i].transform.localScale = myHotDog.myLocalScale;
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
						cokeBottles[i].gameObject.transform.localScale = Vector3.one;
					}
				}
			}
		}
		
		public void OnClickDustbn()
		{
			if(tutorialEnd)
			{
				if(clickedHotDog)
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
			yellowSauce.iAmSelected = false;
			redSauce.iAmSelected = false;
			yellowSauce.transform.localScale = yellowSauce.myLocalScale;
			redSauce.transform.localScale = redSauce.myLocalScale;
			redSauce.mySelection.SetActive (false);
			yellowSauce.mySelection.SetActive (false);
			cupCake.iAmSelected = false;
			cupCake.mySelection.gameObject.SetActive (false);
			DeactivateBunSelection();
			clickedHotDog = false;
			clickedTikki = false;
			clickedRedSauce = false;
			clickedYellowSauce = false;
			clickedCoke = false;
		}
	}
}
