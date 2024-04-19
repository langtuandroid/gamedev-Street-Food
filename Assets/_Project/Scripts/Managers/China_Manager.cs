using _Project.Scripts.Additional;
using _Project.Scripts.Entities.Customers;
using _Project.Scripts.Game;
using _Project.Scripts.Other;
using _Project.Scripts.UI_Scripts;
using _Project.Scripts.UI.Tutorial;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Scripts.Managers
{
	public class China_Manager : MonoBehaviour 
	{
		public static bool _endTutorial;
		[Inject] private SoundsAll _levelSoundManager;
		[Inject] private UIManager _uiManager; 
		
		private int _pansUpgradeNum;
		private int _soupContainer;
		private int _totalPlatesAvailable;
		private int _blowsAvailable;
		
		[FormerlySerializedAs("noodlePlates")] [SerializeField] private SpriteRenderer []_platesNoodle; //6
		[FormerlySerializedAs("noodlePlaces")] [SerializeField] private Availability []_placesNoodle; //6
		[FormerlySerializedAs("pans")] [SerializeField] private GameObject []_pansObjct; //3
		[FormerlySerializedAs("soupContainer")] [SerializeField] private GameObject []_containerSoup; //3
		[FormerlySerializedAs("bowlImages")] [SerializeField] private SpriteRenderer []_bowImage; //6
		[FormerlySerializedAs("bowlPlaces")] [SerializeField] private Availability []_bowlPlaces;  //6
		[FormerlySerializedAs("TheifPanel")] [SerializeField] private GameObject _theifPanel;
		[FormerlySerializedAs("dustbin")] [SerializeField] private GameObject _dustbin;
		[FormerlySerializedAs("cupCake")] [SerializeField] private ObjectMotion _cupCake;
		[FormerlySerializedAs("noodles")] [SerializeField] private ObjectMotion _noodles;
		[FormerlySerializedAs("soupVeg")] [SerializeField] private ObjectMotion _vegSoup;
		[FormerlySerializedAs("bowlAdd")] [SerializeField] private SpriteRenderer _addBowl;
		[FormerlySerializedAs("Radio")] [SerializeField] private GameObject _radioObject;
		[FormerlySerializedAs("Whistle")] [SerializeField] private GameObject _whstleObject;
		[FormerlySerializedAs("bell")] [SerializeField] private GameObject _bellObject;
		[FormerlySerializedAs("handcuff")] [SerializeField] private GameObject _handCuff;
		[FormerlySerializedAs("starting_text")] [SerializeField] private GameObject _textStart;
		[FormerlySerializedAs("tableTop")] [SerializeField] private SpriteRenderer _topTable;
		[FormerlySerializedAs("tableCover")] [SerializeField] private SpriteRenderer _tableCover;
		
		[FormerlySerializedAs("noodlesPlatesMotion")] public ObjectMotion []MotionNoodlesPlates; //6
		[FormerlySerializedAs("firstSoupBowl")] public ObjectMotion SoupBowlFirst;
		[FormerlySerializedAs("noodlesInPanVariations")] public Sprite []noodlesPan;   //4  0- noodles , 1 - noodles with veg ,2 only veg , 3 cooked noodles 4 burnt noodles
		[FormerlySerializedAs("noodlesInPlateVariations")] public Sprite []PlateVariations;   //4  0- uncooked , 1 cooked
		[FormerlySerializedAs("soupContainerVariations")] public Sprite []SoupContainer;  //2 -- 0 - water , 1 - cooked
		[FormerlySerializedAs("soupUtensils")] public ChineseUtils []soupUtils;
		[FormerlySerializedAs("panUtensil")] public ChineseUtils []panUtils;
		[FormerlySerializedAs("noodlesVeg")] public ObjectMotion vegNoodles;
		public Wisitor CustomerFirst { get; set; }
		public bool BowlClock { get; set; }
		public bool PlateTutClick { get; set; }
		public int FiledCountClick { get; set; }
		public int FilledBowls { get; set; }
		public bool ClikedNoodles { get; set; }
		public bool NoodlesVeg { get; set; }
		public bool IsClikedSoupVeg { get; set; }
		public bool IsPanClick { get; set; }
		public bool IsClickedSoupContainer { get; set; }
		public bool IsClickSoupBowl { get; set; }
		public bool IsClickedNoodlePlate { get; set; }
		public ChineseUtils clickedUtensilsDestinationFunction { get; set; }
		public ObjectMotion clickedItemDestinationFunction { get; set; }
		public int SoupPrice => 40;
		public int lessBakedNoodlesPrice => 30;
		public int perfectNoodlesPrice => 60;
		private void OnEnable()
		{
			if (LevelManager.levelNo == 11) {
				_textStart.SetActive(true);
			}
			if(PlayerPrefs.HasKey ("Radio"))
			{
				_radioObject.SetActive(true);
			}
			if(PlayerPrefs.HasKey ("Whistle"))
			{
				_whstleObject.SetActive(true);
			}
			if (PlayerPrefs.HasKey ("Bell"))
			{
				_bellObject.SetActive(true);
			}
			if(MenuManager.handcuffNo > 0)
			{
				_handCuff.SetActive(true);
			}
		
		}
		public void UtensilReach()
		{
			clickedUtensilsDestinationFunction.OnDestinationClick ();
		}
		
		public void ObjectReach()
		{
			clickedItemDestinationFunction.ClickedDestination ();
		}

		private void Start()
		{
			US_Manager.tutorialEnd = false;
			Italy_Manager._isEndTutorial = false;
			Australia_Manager.tutorialEnd = false;
			PlayerPrefs.SetInt ("ChinaOpen",1);
			_endTutorial = false;
			
			if(LevelManager.levelNo <= 12)
			{
				_vegSoup.gameObject.SetActive (false);
				_addBowl.gameObject.SetActive (false);
			}

			int platesUpgradeValue =  (int)Encryption.Decrypt (PlayerPrefs.GetString("ChinaPlateUpgrade")); 
			_totalPlatesAvailable = 2+(platesUpgradeValue*2);
			int bowlUpgrade =  (int)Encryption.Decrypt (PlayerPrefs.GetString("ChinaBowlsUpgrade")); 
			_blowsAvailable = 2+(bowlUpgrade*2);
			_pansUpgradeNum =  (int)Encryption.Decrypt (PlayerPrefs.GetString("ChinaPansUpgrade")); 
			_pansUpgradeNum++;

			for(int i = 0; i < _pansUpgradeNum ; i++)
			{
				_pansObjct[i].SetActive (true);
			}

			_soupContainer =  (int)Encryption.Decrypt (PlayerPrefs.GetString("ChinaSoupContainerUpgrade")); 
			_soupContainer++;

			if (LevelManager.levelNo >= 13) {
				for (int i = 0; i < _soupContainer; i++) {
					_containerSoup [i].SetActive (true);
				}
			}

			char []coverVal = PlayerPrefs.GetString ("China_TableCover").ToCharArray ();
			_uiManager._tabelcover = int.Parse (coverVal[coverVal.Length - 1].ToString ());
			char []coverVal2 = PlayerPrefs.GetString ("China_TableTop").ToCharArray ();
			_uiManager._tabeltop = int.Parse (coverVal[coverVal.Length - 1].ToString ());
			_tableCover.sprite = Resources.Load<Sprite> (PlayerPrefs.GetString ("China_TableTop")) as Sprite;
			_topTable.sprite = Resources.Load<Sprite> (PlayerPrefs.GetString ("China_TableCover")) as Sprite;
			if(MenuManager.cupcakeNo <= 0)
			{
				_cupCake.gameObject.SetActive (false);
			}
			_uiManager.ForCoinAdd ();
		}

		private void PanUnSelect()
		{
			for(int i = 0 ; i < _pansUpgradeNum ; i++)
			{
				panUtils[i]._isSelected = false;
				panUtils[i]._selection.SetActive (false);
			}
		}

		private void UnselectSoupContainer()
		{
			for(int i = 0 ; i < _soupContainer ; i++)
			{
				soupUtils[i]._isSelected = false;
				soupUtils[i]._selection.SetActive (false);
			}
		}

		public void AddMorePlates()
		{
			if(PlateTutClick || (_endTutorial && !TutorialPanel.popupPanelActive))
			{
				ResetBowlsCliked();
				if(FiledCountClick < _totalPlatesAvailable)
				{
					for(int i = 0 ; i < _totalPlatesAvailable ; i++)
					{
						if(_placesNoodle[i].available)
						{
							_levelSoundManager.bowlClickSound.Play();
							_platesNoodle[i].gameObject.SetActive (true);
							_platesNoodle[i].color = new Color(1,1,1,1);
							FiledCountClick++;
							_placesNoodle[i].available = false;
							break;
						}
					}
				}
				if(PlateTutClick)
				{
					_noodles.tutorialOn = true;
					_uiManager.tutorialPanelBg.OpenPopupChina ("TAP OR DRAG NOODLES TO\nTHE SKILLET.",false,false , 0);
				}
				PlateTutClick = false;
			}
		}

		private void UnSelectPlate()
		{
			for(int i = 0 ; i < _totalPlatesAvailable ; i++)
			{
				if(!_placesNoodle[i].available)
				{
					MotionNoodlesPlates[i].iAmSelected = false;
					MotionNoodlesPlates[i].mySelection.SetActive (false);
					MotionNoodlesPlates[i].transform.localScale = MotionNoodlesPlates[i].myLocalScale;
				}
			}
		}


		public void AddBowls()
		{
			if(BowlClock || (_endTutorial && !TutorialPanel.popupPanelActive))
			{
				ResetBowlsCliked();
				if(FilledBowls < _blowsAvailable)
				{
					for(int i = 0 ; i < _blowsAvailable ; i++)
					{
						if(_bowlPlaces[i].available)
						{
							_levelSoundManager.bowlClickSound.Play();
							_bowImage[i].gameObject.SetActive (true);
							_bowImage[i].color = new Color(1,1,1,1);
							FilledBowls++;
							_bowlPlaces[i].available = false;
							break;
						}
					}
				}

				if(BowlClock)
				{
					_vegSoup.tutorialOn = true;
					_uiManager.tutorialPanelBg.OpenPopupChina ("TAP OR DRAG INGREDIENTS\nTO STOCKPOT.",false,false , 4);
				}
				BowlClock = false;
			}
		}

		private void UnselectBowls()
		{
			for(int i = 0 ; i < _blowsAvailable ; i++)
			{
				if(_bowImage[i].gameObject.activeInHierarchy)
				{
					if(_bowImage[i].GetComponent<ObjectMotion>().iAmSelected)
					{
						_bowImage[i].GetComponent<ObjectMotion>().iAmSelected = false;
						_bowImage[i].GetComponent<ObjectMotion>().mySelection.SetActive (false);
					}
				}
			}
		}


		public void OnClickDustbn()
		{
			if(_endTutorial)
			{
				if(IsClickedSoupContainer)
				{
					clickedUtensilsDestinationFunction.otherObject = _dustbin;
					UtensilReach();
				}
				else if(IsPanClick)
				{
					clickedUtensilsDestinationFunction.otherObject = _dustbin;
					UtensilReach ();
				}
				ResetBowlsCliked();
			}
		}

		public void ResetBowlsCliked()
		{
			UnselectBowls();
			PanUnSelect();
			UnselectSoupContainer ();
			UnSelectPlate ();

			_cupCake.iAmSelected = false;
			_cupCake.mySelection.gameObject.SetActive (false);

			_noodles.iAmSelected = false;
			_noodles.mySelection.gameObject.SetActive (false);

			vegNoodles.iAmSelected = false;
			vegNoodles.mySelection.gameObject.SetActive (false);

			_vegSoup.iAmSelected = false;
			_vegSoup.mySelection.gameObject.SetActive (false);

			IsClickedNoodlePlate = false;
			NoodlesVeg = false;
			ClikedNoodles = false;
			IsClikedSoupVeg = false;
			IsPanClick = false;
			IsClickSoupBowl = false;
			IsClickedSoupContainer = false;
		}
	}
}
