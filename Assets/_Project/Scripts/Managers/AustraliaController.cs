using _Project.Scripts.Additional;
using _Project.Scripts.Entities.Customers;
using _Project.Scripts.Food;
using _Project.Scripts.Other;
using _Project.Scripts.UI_Scripts;
using _Project.Scripts.UI.Tutorial;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Scripts.Managers
{
	public class AustraliaController : MonoBehaviour
	{
		public static bool tutorialEnd;
		[Inject] private SoundsAll _levelSoundManager;
		[Inject] private UIManager _uiManager;
		private int totalGrillsAvailable;
		private int totalPlatesAvailable;
		private int totalCokesAvailable;
		private int totalFriesAvailable;
		private bool IsClickFirstTikki;
		[FormerlySerializedAs("TheifPanel")] [SerializeField] public GameObject _thiefPanel;
		[FormerlySerializedAs("burgerPlates")] [SerializeField] private SpriteRenderer []_burgerPanel; //6
		[FormerlySerializedAs("burgerOnPlates")] [SerializeField] private SpriteRenderer []_burgelOnPlate; //6
		[FormerlySerializedAs("grills")] [SerializeField] private GameObject []_grills; //3
		[FormerlySerializedAs("grillTikkis")] [SerializeField] private SpriteRenderer []_grillsTikkis;  //6
		[FormerlySerializedAs("grillPlaces")] [SerializeField] private Availability []grillPlaves; //6
		[FormerlySerializedAs("cokeBottles")] [SerializeField] private SpriteRenderer []_bottles; //9
		[FormerlySerializedAs("cokePlaces")] [SerializeField] private Availability []_cokePlaces;  //9
		[FormerlySerializedAs("fryer")] [SerializeField] private SpriteRenderer _fryer;
		[FormerlySerializedAs("fryerVariations")] [SerializeField] private Sprite []_fryerVariations;   //3
		[FormerlySerializedAs("friesCluster")] [SerializeField] private GameObject []_friesCluster;   //3
		[FormerlySerializedAs("friesPack")] [SerializeField] private ObjectMotion []_friesPack; //9
		[FormerlySerializedAs("friesPlaces")] [SerializeField] private Availability []_friesPositions;  //9
		[FormerlySerializedAs("dustbin")] [SerializeField] private GameObject _dustbin;
		[FormerlySerializedAs("tomato")] [SerializeField] private ObjectMotion _tomato;
		[FormerlySerializedAs("onion")] [SerializeField] private ObjectMotion _onionPrefab;
		[FormerlySerializedAs("cabbage")] [SerializeField] private ObjectMotion _cabbagePrefab;
		[FormerlySerializedAs("cupCake")] [SerializeField] private ObjectMotion _cupCakePrefab;
		[FormerlySerializedAs("tomatoAdd")] [SerializeField] private GameObject _tomatoAddPrefab;
		[FormerlySerializedAs("onionAdd")] [SerializeField] private GameObject _onionAddPrefab;
		[FormerlySerializedAs("cabbageAdd")] [SerializeField] private GameObject _cabbageAddPrefab;
		[FormerlySerializedAs("friesAdd")] [SerializeField] private GameObject _friesAddPrefab;
		[FormerlySerializedAs("firstTikki")] [SerializeField] private MakeTikki _firstTikkiPrefab;
		[FormerlySerializedAs("firstFries")] [SerializeField] private ObjectMotion _firstFriesObjects;
		[FormerlySerializedAs("tableCover")] [SerializeField] private SpriteRenderer _tableCover;
		[FormerlySerializedAs("tableTop")] [SerializeField] private SpriteRenderer _tableTop;
		[FormerlySerializedAs("Radio")] [SerializeField] private GameObject _radioPrefab ;
		[FormerlySerializedAs("Whistle")] [SerializeField] private GameObject _whistlePrefab ;
		[FormerlySerializedAs("bell")] [SerializeField] private GameObject _bellPrefab ;
		[FormerlySerializedAs("handcuff")] [SerializeField] private GameObject _handCuff ;
		[FormerlySerializedAs("starting_text")] [SerializeField] private GameObject _startingTextPrefab ;
		[FormerlySerializedAs("burgerTikkiVariations")] public Sprite []_burgerVariations;   //3	
		[FormerlySerializedAs("burgerTikkiOnPlates")] public SpriteRenderer []_tikkiPlates; //6
		[FormerlySerializedAs("firstBurger")] public BurgerFood _firstBurger;
		public bool FryerClick { get; set; }
		public int GrillsFilled{ get; set; }
		public int PlatesFilled{ get; set; }
		public int CokesFilled{ get; set; }
		public int FriesFilled{ get; set; }
		public bool IsBurgerClick{ get; set; }
		public bool IsClickedTikki{ get; set; }
		public bool IsClickedTomato{ get; set; }
		public bool IsClickedOnion{ get; set; }
		public bool IsClickedCabbage{ get; set; }
		public bool IsCickedFries{ get; set; }
		public bool IsClickedCoke{ get; set; }
		public MakeTikki ClickedFunktion { get; set; }
		public ObjectMotion _objectMotion { get; set; }
		public BurgerFood _burgerFood { get; set; }
		public int CokePrice => 10;
		public int LessBaked => 20;
		public int PerfectBurger => 50;
		public Wisitor FirstCustomer { get; set; }
		public bool ClickFirstBun { get; set; }
		


		private void OnEnable()
		{
			if(PlayerPrefs.HasKey ("Radio"))
			{
				_radioPrefab.SetActive(true);
			}
			if(PlayerPrefs.HasKey ("Whistle"))
			{
				_whistlePrefab.SetActive(true);
			}
			if (PlayerPrefs.HasKey ("Bell"))
			{
				_bellPrefab.SetActive(true);
			}
			if(MenuManager.handcuffNo > 0)
			{
				_handCuff.SetActive(true);
			}
		
		}
		
		public void TikkiDone()
		{
			ClickedFunktion.ClickedDestination ();
		}

		public void HotDogDone()
		{
			_burgerFood.OnDestinationClick ();
		}

		public void ReachObject()
		{
			_objectMotion.ClickedDestination ();
		}

		private void Start()
		{
			PlayerPrefs.SetInt("AusOpen",1);
			USController._isEndTutorial = false;
			ItalyController._isEndTutorial = false;
			ChinaController._endTutorial = false;
			if (LevelManager.levelNo == 31) {
				_startingTextPrefab.SetActive(true);
			}
			tutorialEnd = false;
			if(LevelManager.levelNo == 31)
			{
				_tomatoAddPrefab.gameObject.SetActive (false);

				_onionAddPrefab.gameObject.SetActive (false);

				_cabbageAddPrefab.gameObject.SetActive (false);

				_friesAddPrefab.gameObject.SetActive (false);

			}
			else if(LevelManager.levelNo == 32)  
			{
				_tomatoAddPrefab.gameObject.SetActive (false);
			
				_onionAddPrefab.gameObject.SetActive (false);
			
				_cabbageAddPrefab.gameObject.SetActive (false);
			}
			else if(LevelManager.levelNo == 33) 
			{
				_cabbageAddPrefab.gameObject.SetActive (false);
			}
			
			int platesUpgradeValue =  (int)Encryption.Decrypt (PlayerPrefs.GetString("AusPlateUpgrade")); 
			totalPlatesAvailable = 2+(platesUpgradeValue*2);
			for(int i = 0; i < totalPlatesAvailable ; i++)
			{
				_burgerPanel[i].color = new Color(1,1,1,1);
			}

			int friesUpgradeValue =  (int)Encryption.Decrypt (PlayerPrefs.GetString("FriesUpgrade")); 
			totalFriesAvailable = 2+(friesUpgradeValue*2);
			_fryer.sprite = _fryerVariations[friesUpgradeValue];
			for(int i = 0; i <= friesUpgradeValue ; i++)
			{
				_friesCluster[i].SetActive (true);
			}

			int cokeUpgradeValue =  (int)Encryption.Decrypt (PlayerPrefs.GetString("AusCokeUpgrade")); 
			totalCokesAvailable = 3+(cokeUpgradeValue*3);
			int grillsToUpgrade =  (int)Encryption.Decrypt (PlayerPrefs.GetString("AusGrillsUpgrade")); 
			totalGrillsAvailable = 2+(grillsToUpgrade*2);
			int grillVal = (int)totalGrillsAvailable/2;
			for(int i = 0; i < grillVal ; i++)
			{
				_grills[i].SetActive (true);
			}

			char []coverVal = PlayerPrefs.GetString ("Aus_TableCover").ToCharArray ();
			_uiManager._tabelcover = int.Parse (coverVal[coverVal.Length - 1].ToString ());
			char []coverVal2 = PlayerPrefs.GetString ("Aus_TableTop").ToCharArray ();
			_uiManager._tabeltop= int.Parse (coverVal[coverVal.Length - 1].ToString ());
			_tableCover.sprite = Resources.Load<Sprite> (PlayerPrefs.GetString ("Aus_TableCover"));
			_tableTop.sprite =  Resources.Load<Sprite> (PlayerPrefs.GetString ("Aus_TableTop"));
			if(MenuManager.cupcakeNo <= 0)
			{
				_cupCakePrefab.gameObject.SetActive (false);
			}
			_uiManager.ForCoinAdd ();
		}

		public void AddMoreTikki()
		{
			if(IsClickFirstTikki || (tutorialEnd && !TutorialPanel.popupPanelActive))
			{
				AllClickedBoolsReset();
				if(GrillsFilled < totalGrillsAvailable)
				{
					for(int i = 0 ; i < totalGrillsAvailable ; i++)
					{
						if(grillPlaves[i].available)
						{
							_grillsTikkis[i].gameObject.SetActive (true);
							_grillsTikkis[i].sprite = _burgerVariations[0];
							grillPlaves[i].available = false;
							GrillsFilled++;
							break;
						}
					}
				}
				if(IsClickFirstTikki)
				{
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopupAustralia ("WAIT FOR THE TIKKI \n TO BAKE",false,false , 2);
					_firstTikkiPrefab.tutorialOn = true;
				}
				IsClickFirstTikki = false;
			}
		}

		private void DeactivateTikki()
		{
			for(int i = 0 ; i < totalGrillsAvailable ; i++)
			{
				if(!grillPlaves[i].available)
				{
					_grillsTikkis[i].transform.GetComponent<MakeTikki>().iAmSelected = false;
					_grillsTikkis[i].transform.GetComponent<MakeTikki>().mySelection.SetActive (false);
				}
			}
		}


		public void AddBurgerBuns()
		{
			if(ClickFirstBun || (tutorialEnd && !TutorialPanel.popupPanelActive))
			{
				AllClickedBoolsReset();
				if(PlatesFilled < totalPlatesAvailable)
				{
					for(int i = 0 ; i < totalPlatesAvailable ; i++)
					{
						if(_burgerPanel[i].gameObject.GetComponent<Availability>().available)
						{
							_burgelOnPlate[i].gameObject.SetActive (true);
							PlatesFilled++;
							_burgerPanel[i].gameObject.GetComponent<Availability>().available = false;
							_burgelOnPlate[i].transform.GetComponent<BurgerFood>().isPrefavet = false;
							break;
						}
					}
				}
				if(ClickFirstBun)
				{
					IsClickFirstTikki = true;
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopup ("TAP TIKKI \n TO PUT ON THE GRILLS",false,false , 1);
				}
				ClickFirstBun = false;
			}
		}

		private void DeactivateSelectionBun()
		{
			for(int i = 0 ; i < totalPlatesAvailable ; i++)
			{
				if(!_burgerPanel[i].gameObject.GetComponent<Availability>().available)
				{
					BurgerFood myBurger = _burgelOnPlate[i].transform.GetComponent<BurgerFood>();
					myBurger.isSelected = false;
					myBurger._selectionObject.SetActive (false);
				}
			}
		}


		public void AddCokeBottles()
		{
			if(tutorialEnd && !TutorialPanel.popupPanelActive)
			{
				AllClickedBoolsReset();
				if(CokesFilled < totalCokesAvailable)
				{
					for(int i = 0 ; i < totalCokesAvailable ; i++)
					{
						if(_cokePlaces[i].available)
						{
							_levelSoundManager.bottleClickSound.Play();
							_bottles[i].gameObject.SetActive (true);
							_bottles[i].color = new Color(1,1,1,1);
							CokesFilled++;
							_cokePlaces[i].available = false;
							break;
						}
					}
				}
			}
		}

		private void DeactivateBottleSelect()
		{
			for(int i = 0 ; i < totalCokesAvailable ; i++)
			{
				if(_bottles[i].gameObject.activeInHierarchy)
				{
					if(_bottles[i].GetComponent<ObjectMotion>().iAmSelected)
					{
						_bottles[i].GetComponent<ObjectMotion>().iAmSelected = false;
						_bottles[i].GetComponent<ObjectMotion>().mySelection.SetActive (false);
					}
				}
			}
		}

		public void AddFries()
		{
			if(tutorialEnd && !TutorialPanel.popupPanelActive || FryerClick)
			{
				AllClickedBoolsReset();

				if(FriesFilled < totalFriesAvailable)
				{
					for(int i = 0 ; i < totalFriesAvailable; i++)
					{
						if(_friesPositions[i].available)
						{

							_friesPack[i].gameObject.SetActive (true);
							FriesFilled++;
							_friesPositions[i].available = false;
							break;
						}
					}
				}

				if(FryerClick)
				{
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopupAustralia ("TAP OR DRAG FRIES TO \n THE CUSTOMER.",false,false , 9);
					_firstFriesObjects.tutorialOn = true;

				}
				FryerClick = false;
			}
		}

		private void DeactivateFriesSelect()
		{
			for(int i = 0 ; i < totalFriesAvailable ; i++)
			{
				if(_friesPack[i].gameObject.activeInHierarchy)
				{
					if(_friesPack[i].iAmSelected)
					{
						_friesPack[i].iAmSelected = false;
						_friesPack[i].mySelection.SetActive (false);
					}
				}
			}
		}

	

		public void OnClickDustbn()
		{
			if(tutorialEnd)
			{
				if(IsBurgerClick)
				{
					_burgerFood.otherObject = _dustbin;
					HotDogDone();
				}
				else if(IsClickedTikki)
				{
					if(ClickedFunktion.isBurnt)
					{
						TikkiDone ();
					}
				}

				AllClickedBoolsReset();
			}
		}

		public void AllClickedBoolsReset()
		{
			DeactivateTikki();
			DeactivateBottleSelect();
			DeactivateFriesSelect();
			
			_onionPrefab.iAmSelected = false;
			_tomato.iAmSelected = false;
			_onionPrefab.transform.localScale = _onionPrefab.myLocalScale;
			_tomato.transform.localScale = _tomato.myLocalScale;
			_tomato.mySelection.SetActive (false);
			_onionPrefab.mySelection.SetActive (false);
			
			_cabbagePrefab.iAmSelected = false;
			_cabbagePrefab.transform.localScale = _cabbagePrefab.myLocalScale;
			_cabbagePrefab.mySelection.SetActive (false);

			_cupCakePrefab.iAmSelected = false;
			_cupCakePrefab.mySelection.gameObject.SetActive (false);
			DeactivateSelectionBun();
			IsBurgerClick = false;
			IsClickedTikki = false;
			IsClickedTomato = false;
			IsClickedOnion = false;
			IsClickedCabbage = false;
			IsCickedFries = false;
			IsClickedCoke = false;
		}
	}
}
