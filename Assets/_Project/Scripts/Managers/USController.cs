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
	public class USController : MonoBehaviour 
	{
		public static bool _isEndTutorial;
		public static int _perfectNums;
		[Inject] private UIManager _uiManager;   
		[Inject] private SoundsAll _levelSoundManager;
		
		private int _cokesAwailaable = 3;
		private bool _clickFirst;
		private int _girlsAwaiable = 2;
		private int _platesAwailable = 2;
		
		[FormerlySerializedAs("hotdogPlates")] [SerializeField] private SpriteRenderer []_hotdogPlates; 
		[FormerlySerializedAs("grills")] [SerializeField] private GameObject []_grills;
		[FormerlySerializedAs("grillTikkis")] [SerializeField] private SpriteRenderer []_grillTikkis;
		[FormerlySerializedAs("grillPlaces")] [SerializeField] private Availability []_grillPlaces;
		[FormerlySerializedAs("cokeBottles")] [SerializeField] private SpriteRenderer []_cokeBottles;
		[FormerlySerializedAs("cokePlaces")] [SerializeField] private Availability []_cokePlaces;
		[FormerlySerializedAs("dustbin")] [SerializeField] private GameObject _dustbin;
		[FormerlySerializedAs("redSauce")] [SerializeField] private ObjectMotion _redSauce;
		[FormerlySerializedAs("yellowSauce")] [SerializeField] private ObjectMotion _yellowSauce;
		[FormerlySerializedAs("cupCake")] [SerializeField] private ObjectMotion _cupCake;
		[FormerlySerializedAs("cokeAdd")] [SerializeField] private SpriteRenderer _cokeAdd;
		[FormerlySerializedAs("yellowSauceAdd")] [SerializeField] private SpriteRenderer _yellowSauceAdd;
		[FormerlySerializedAs("redSauceAdd")] [SerializeField] private SpriteRenderer _redSauceAdd;
		[FormerlySerializedAs("firstTikki")] [SerializeField] private MakeTikki _firstTikki;
		[FormerlySerializedAs("tableTop")] [SerializeField] private SpriteRenderer _tableTop;
		[FormerlySerializedAs("tableCover")] [SerializeField] private SpriteRenderer _tableCover;
		[FormerlySerializedAs("Radio")] [SerializeField] private GameObject _RadioObject ;
		[FormerlySerializedAs("whistle")] [SerializeField] private GameObject _whistleObject ;
		[FormerlySerializedAs("starting_text")] [SerializeField] private GameObject _textStart;
		
		[FormerlySerializedAs("TheifPanel")] public GameObject TheifBoard;
		[FormerlySerializedAs("hotDogTikkiVariations")] public Sprite []HotDogVariants;  
		[FormerlySerializedAs("hotDogOrderVariations")] public Sprite []HotDogOrderVariants;  
		[FormerlySerializedAs("hotDogVariations")] public Sprite []HotDogVariations;  
		[FormerlySerializedAs("hotDogSauces")] public Sprite []HotDogSaucesSprites; 
		[FormerlySerializedAs("hotDogSaucesOnPlates")] public SpriteRenderer []HotDogSaucesOnPlatesSR;
		[FormerlySerializedAs("hotdogOnPlates")] public SpriteRenderer []HotdogOnPlatesSR;
		[FormerlySerializedAs("firstHotDog")] public HotDog FirstHotDog;
		[FormerlySerializedAs("firstCoins")] public Money FirstCoins;
		[FormerlySerializedAs("Bell")] public GameObject BellObject ;
		[FormerlySerializedAs("handcuff")] public GameObject HandCuff;
		public Wisitor firstWisitor { get; set; }
		public bool ClickBun { get; set; }
		public int GrillsFilledCount { get; set; }
		public int PlatesFilledCount { get; set; }
		public int CokesFilledNum { get; set; }
		public bool IsClickedHotDog { get; set; }
		public bool IsClickedTikki { get; set; }
		public bool IsClickedRedSauce { get; set; }
		public bool IsClickedYellowSauce { get; set; }
		public bool IsClickedCoke { get; set; }
		public MakeTikki MakeTiki { get; set; }
		public ObjectMotion objectMotion { get; set; }
		public HotDog HotDog { get; set; }
		public int CokePrice => 10;
		public int HotdogBakeTime => 20;
		public int perfectHotDogTime => 40;
		private void OnEnable()
		{
			if (LevelManager.levelNo == 1) {
				_textStart.SetActive(true);
			}
			if(PlayerPrefs.HasKey ("Radio"))
			{
				_RadioObject.SetActive(true);
			}
			if(PlayerPrefs.HasKey ("Whistle"))
			{
				_whistleObject.SetActive(true);
			}
			if (PlayerPrefs.HasKey ("Bell"))
			{
				BellObject.SetActive(true);
			}
			if(MenuManager.handcuffNo > 0)
			{
				HandCuff.SetActive(true);
			}
		
		}
		public void OnTikkiDestination()
		{
			MakeTiki.ClickedDestination ();
		}

		public void OnDogDestination()
		{
			HotDog.OnDistinationClick ();
		}

		public void OnObjectReach()
		{
			objectMotion.ClickedDestination ();
		}

		private void Start()
		{
			_isEndTutorial = false;
			if(LevelManager.levelNo == 1)
			{
				_textStart.SetActive(true);
				_cokeAdd.gameObject.SetActive (false);
				_cokeAdd.color = new Color(1,1,1,0.5f); 
				_cokeAdd.transform.GetComponent<BoxCollider>().enabled = false;
				_yellowSauceAdd.gameObject.SetActive (false);
				_yellowSauceAdd.color = new Color(1,1,1,0.5f); 
				_yellowSauceAdd.transform.GetComponent<BoxCollider>().enabled = false;
				_redSauceAdd.gameObject.SetActive (false);
				_redSauceAdd.color = new Color(1,1,1,0.5f); 
				_redSauceAdd.transform.GetComponent<BoxCollider>().enabled = false;

			}
			else if(LevelManager.levelNo == 2)
			{
				_cokeAdd.gameObject.SetActive (false);
				_cokeAdd.transform.GetComponent<BoxCollider>().enabled = false;
				_cokeAdd.color = new Color(1,1,1,0.5f); 
			}
		
			int platesUpgradeValue =  (int)Encryption.Decrypt (PlayerPrefs.GetString("PlateUpgrade")); 
			_platesAwailable = 2+(platesUpgradeValue*2);
			for(int i = 0; i < _platesAwailable ; i++)
			{
				_hotdogPlates[i].color = new Color(1,1,1,1);
			}

			int cokeUpgradeValue =  (int)Encryption.Decrypt (PlayerPrefs.GetString("USCokeUpgrade")); 
			_cokesAwailaable = 3+(cokeUpgradeValue*3);
			int grillsTpgrade =  (int)Encryption.Decrypt (PlayerPrefs.GetString("GrillsUpgrade")); 
			_girlsAwaiable = 2+(grillsTpgrade*2);
			int grillVal = (int)_girlsAwaiable/2;
			for(int i = 0; i < grillVal ; i++)
			{
				_grills[i].SetActive (true);
			}
			char []coverVal = PlayerPrefs.GetString ("US_TableCover").ToCharArray ();
			_uiManager._tabelcover = int.Parse (coverVal[coverVal.Length - 1].ToString ());
			_uiManager.ForCoinAdd ();
			_tableCover.sprite = Resources.Load<Sprite> (PlayerPrefs.GetString ("US_TableCover"));
			_tableTop.sprite = Resources.Load<Sprite> (PlayerPrefs.GetString ("US_TableTop")) as Sprite;
			if(MenuManager.cupcakeNo <= 0)
			{
				_cupCake.gameObject.SetActive (false);
			}

		}
	

		public void AddMoreTikki()
		{
			if(_clickFirst || (_isEndTutorial && !TutorialPanel.popupPanelActive))
			{
				ResetBools();
				if(GrillsFilledCount < _girlsAwaiable)
				{
					for(int i = 0 ; i < _girlsAwaiable ; i++)
					{
						if(_grillPlaces[i].isAvailable)
						{
							_grillTikkis[i].gameObject.SetActive (true);
							_grillTikkis[i].sprite = HotDogVariants[0];
							_grillPlaces[i].isAvailable = false;
							GrillsFilledCount++;
							break;
						}
					}
				}
				if(_clickFirst)
				{
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopup ("WAIT FOR THE SAUSAGE \n TO BAKE",false,false , 2);
					_firstTikki.tutorialOn = true;
				}
				_clickFirst = false;
			}
		}

		private void DeselectionTikki()
		{
			for(int i = 0 ; i < _girlsAwaiable ; i++)
			{
				if(!_grillPlaces[i].isAvailable)
				{
					_grillTikkis[i].transform.GetComponent<MakeTikki>().iAmSelected = false;
					_grillTikkis[i].transform.GetComponent<MakeTikki>().mySelection.SetActive (false);
					_grillTikkis[i].transform.localScale = Vector3.one;
				}
			}
		}
	
		public void AddHotDogBuns()
		{
			if(ClickBun || (_isEndTutorial && !TutorialPanel.popupPanelActive))
			{
				ResetBools();
				if(PlatesFilledCount < _platesAwailable)
				{
					for(int i = 0 ; i < _platesAwailable ; i++)
					{
						if(_hotdogPlates[i].gameObject.GetComponent<Availability>().isAvailable)
						{
							HotdogOnPlatesSR[i].gameObject.SetActive (true);
							HotdogOnPlatesSR[i].sprite = HotDogVariations[0];
							PlatesFilledCount++;
							_hotdogPlates[i].gameObject.GetComponent<Availability>().isAvailable = false;
							HotdogOnPlatesSR[i].transform.GetComponent<HotDog>().isPerfect = false;
							break;
						}
					}
				}
				if(ClickBun)
				{
					_clickFirst = true;
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopup ("TAP SAUSAGE TO PUT \n ON THE GRILLS",false,false , 1);
				}
				ClickBun = false;
			}
		}

		private void DeselectionBun()
		{
			for(int i = 0 ; i < _platesAwailable ; i++)
			{
				if(!_hotdogPlates[i].gameObject.GetComponent<Availability>().isAvailable)
				{
					HotDog myHotDog = HotdogOnPlatesSR[i].transform.GetComponent<HotDog>();
					myHotDog.isSelected = false;
					myHotDog._selectionObject.SetActive (false);
					HotdogOnPlatesSR[i].transform.localScale = myHotDog._localScale;
				}
			}
		}


		public void AddCokeBottles()
		{
			if(_isEndTutorial && !TutorialPanel.popupPanelActive)
			{
				ResetBools();
				if(CokesFilledNum < _cokesAwailaable)
				{
					for(int i = 0 ; i < _cokesAwailaable ; i++)
					{
						if(_cokePlaces[i].isAvailable)
						{
							_levelSoundManager.bottleClickSound.Play();
							_cokeBottles[i].gameObject.SetActive (true);
							_cokeBottles[i].color = new Color(1,1,1,1);
							CokesFilledNum++;
							_cokePlaces[i].isAvailable = false;
							break;
						}
					}
				}
			}
		}

		private void DeselectionAllBottles()
		{
			for(int i = 0 ; i < _cokesAwailaable ; i++)
			{
				if(_cokeBottles[i].gameObject.activeInHierarchy)
				{
					if(_cokeBottles[i].GetComponent<ObjectMotion>().iAmSelected)
					{
						_cokeBottles[i].GetComponent<ObjectMotion>().iAmSelected = false;
						_cokeBottles[i].GetComponent<ObjectMotion>().mySelection.SetActive (false);
						_cokeBottles[i].gameObject.transform.localScale = Vector3.one;
					}
				}
			}
		}
		
		public void OnClickDustbn()
		{
			if(_isEndTutorial)
			{
				if(IsClickedHotDog)
				{
					HotDog._otherObject = _dustbin;
					OnDogDestination();
				}
				else if(IsClickedTikki)
				{
					if(MakeTiki.isBurnt)
					{
						OnTikkiDestination ();
					}
				}

				ResetBools();
			}
		}

		public void ResetBools()
		{
			DeselectionTikki();
			DeselectionAllBottles();
			_yellowSauce.iAmSelected = false;
			_redSauce.iAmSelected = false;
			_yellowSauce.transform.localScale = _yellowSauce.myLocalScale;
			_redSauce.transform.localScale = _redSauce.myLocalScale;
			_redSauce.mySelection.SetActive (false);
			_yellowSauce.mySelection.SetActive (false);
			_cupCake.iAmSelected = false;
			_cupCake.mySelection.gameObject.SetActive (false);
			DeselectionBun();
			IsClickedHotDog = false;
			IsClickedTikki = false;
			IsClickedRedSauce = false;
			IsClickedYellowSauce = false;
			IsClickedCoke = false;
		}
	}
}
