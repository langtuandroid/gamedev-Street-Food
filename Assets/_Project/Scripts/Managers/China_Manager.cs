using _Project.Scripts.Additional;
using _Project.Scripts.Entities.Customers;
using _Project.Scripts.Game;
using _Project.Scripts.Other;
using _Project.Scripts.UI_Scripts;
using _Project.Scripts.UI.Tutorial;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Managers
{
	public class China_Manager : MonoBehaviour 
	{
		[Inject] private LevelSoundManager _levelSoundManager;
		[Inject] private UIManager _uiManager;   
		public GameObject TheifPanel;
		public int soupPrice => 40;
		public int lessBakedNoodlesPrice => 30;
		public int perfectNoodlesPrice => 60;
		public Sprite []noodlesInPanVariations;   //4  0- noodles , 1 - noodles with veg ,2 only veg , 3 cooked noodles 4 burnt noodles
		public Sprite []noodlesInPlateVariations;   //4  0- uncooked , 1 cooked
		public Sprite []soupContainerVariations;  //2 -- 0 - water , 1 - cooked
		public SpriteRenderer []noodlePlates; //6
		public Availability []noodlePlaces; //6
		public ObjectMotion []noodlesPlatesMotion; //6
		public ObjectMotion firstSoupBowl;
		public GameObject []pans; //3
		public GameObject []soupContainer; //3
		public SpriteRenderer []bowlImages; //6
		public Availability []bowlPlaces;  //6
		public int platesFilledCount { get; set; }
		private int totalPlatesAvailable;
		public int bowlsFilled { get; set; }
		private int totalBowlsAvailable;
		public bool clickedNoodlesToCook { get; set; }
		public bool clickedNoodlesVeg { get; set; }
		public bool clickedSoupVeg { get; set; }
		public bool clickedPan { get; set; }
		public bool clickedSoupContainer { get; set; }
		public bool clickSoupBowl { get; set; }
		public bool clickedNoodlePlate { get; set; }
		public ChineseUtensils clickedUtensilsDestinationFunction { get; set; }
		public ObjectMotion clickedItemDestinationFunction { get; set; }
		public ChineseUtensils []soupUtensils;
		public ChineseUtensils []panUtensil;
		public GameObject dustbin;
		public ObjectMotion cupCake;
		public ObjectMotion noodles;
		public ObjectMotion noodlesVeg;
		public ObjectMotion soupVeg;
		public SpriteRenderer bowlAdd;
		public Customer firstCustomer { get; set; }
		public bool clickBowlTut { get; set; }
		public bool clickPlateTut { get; set; }
		public SpriteRenderer tableTop;
		public SpriteRenderer tableCover;
		public int pansUpgrade { get; set; }
		public int soupContainerUpgrade { get; set; }
		public static bool tutorialEnd;
		public GameObject Radio ;
		public GameObject Whistle ;
		public GameObject bell ;
		public GameObject handcuff ;
		public GameObject starting_text ;
		private void OnEnable()
		{
			if (LevelManager.levelNo == 11) {
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
		public void UtensilReached()
		{
			clickedUtensilsDestinationFunction.ClickedDestination ();
		}
		
		public void ObjectReached()
		{
			clickedItemDestinationFunction.ClickedDestination ();
		}

		private void Start()
		{
			US_Manager.tutorialEnd = false;
			Italy_Manager.tutorialEnd = false;
			Australia_Manager.tutorialEnd = false;
			PlayerPrefs.SetInt ("ChinaOpen",1);
			tutorialEnd = false;
			
			if(LevelManager.levelNo <= 12)
			{
				soupVeg.gameObject.SetActive (false);
				bowlAdd.gameObject.SetActive (false);
			}

			int platesUpgradeValue =  (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("ChinaPlateUpgrade")); 
			totalPlatesAvailable = 2+(platesUpgradeValue*2);
			int bowlUpgrade =  (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("ChinaBowlsUpgrade")); 
			totalBowlsAvailable = 2+(bowlUpgrade*2);
			pansUpgrade =  (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("ChinaPansUpgrade")); 
			pansUpgrade++;

			for(int i = 0; i < pansUpgrade ; i++)
			{
				pans[i].SetActive (true);
			}

			soupContainerUpgrade =  (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("ChinaSoupContainerUpgrade")); 
			soupContainerUpgrade++;

			if (LevelManager.levelNo >= 13) {
				for (int i = 0; i < soupContainerUpgrade; i++) {
					soupContainer [i].SetActive (true);
				}
			}

			char []coverVal = PlayerPrefs.GetString ("China_TableCover").ToCharArray ();
			_uiManager._tabelcover = int.Parse (coverVal[coverVal.Length - 1].ToString ());
			char []coverVal2 = PlayerPrefs.GetString ("China_TableTop").ToCharArray ();
			_uiManager._tabeltop = int.Parse (coverVal[coverVal.Length - 1].ToString ());
			tableCover.sprite = Resources.Load<Sprite> (PlayerPrefs.GetString ("China_TableTop")) as Sprite;
			tableTop.sprite = Resources.Load<Sprite> (PlayerPrefs.GetString ("China_TableCover")) as Sprite;
			if(MenuManager.cupcakeNo <= 0)
			{
				cupCake.gameObject.SetActive (false);
			}
			_uiManager.ForCoinAdd ();
		}

		private void DeactivatePanSelection()
		{
			for(int i = 0 ; i < pansUpgrade ; i++)
			{
				panUtensil[i].iAmSelected = false;
				panUtensil[i].mySelection.SetActive (false);
			}
		}

		private void DeactivateSoupContainerSelection()
		{
			for(int i = 0 ; i < soupContainerUpgrade ; i++)
			{
				soupUtensils[i].iAmSelected = false;
				soupUtensils[i].mySelection.SetActive (false);
			}
		}

		public void AddMorePlates()
		{
			if(clickPlateTut || (tutorialEnd && !TutorialPanel.popupPanelActive))
			{
				AllClickedBoolsReset();
				if(platesFilledCount < totalPlatesAvailable)
				{
					for(int i = 0 ; i < totalPlatesAvailable ; i++)
					{
						if(noodlePlaces[i].available)
						{
							_levelSoundManager.bowl_click.Play();
							noodlePlates[i].gameObject.SetActive (true);
							noodlePlates[i].color = new Color(1,1,1,1);
							platesFilledCount++;
							noodlePlaces[i].available = false;
							break;
						}
					}
				}
				if(clickPlateTut)
				{
					noodles.tutorialOn = true;
					_uiManager.tutorialPanelBg.OpenPopupChina ("TAP OR DRAG NOODLES TO\nTHE SKILLET.",false,false , 0);
				}
				clickPlateTut = false;
			}
		}

		private void DeactivatePlateSelection()
		{
			for(int i = 0 ; i < totalPlatesAvailable ; i++)
			{
				if(!noodlePlaces[i].available)
				{
					noodlesPlatesMotion[i].iAmSelected = false;
					noodlesPlatesMotion[i].mySelection.SetActive (false);
					noodlesPlatesMotion[i].transform.localScale = noodlesPlatesMotion[i].myLocalScale;
				}
			}
		}


		public void AddBowls()
		{
			if(clickBowlTut || (tutorialEnd && !TutorialPanel.popupPanelActive))
			{
				AllClickedBoolsReset();
				if(bowlsFilled < totalBowlsAvailable)
				{
					for(int i = 0 ; i < totalBowlsAvailable ; i++)
					{
						if(bowlPlaces[i].available)
						{
							_levelSoundManager.bowl_click.Play();
							bowlImages[i].gameObject.SetActive (true);
							bowlImages[i].color = new Color(1,1,1,1);
							bowlsFilled++;
							bowlPlaces[i].available = false;
							break;
						}
					}
				}

				if(clickBowlTut)
				{
					soupVeg.tutorialOn = true;
					_uiManager.tutorialPanelBg.OpenPopupChina ("TAP OR DRAG INGREDIENTS\nTO STOCKPOT.",false,false , 4);
				}
				clickBowlTut = false;
			}
		}

		private void DeactivateBowlsSelection()
		{
			for(int i = 0 ; i < totalBowlsAvailable ; i++)
			{
				if(bowlImages[i].gameObject.activeInHierarchy)
				{
					if(bowlImages[i].GetComponent<ObjectMotion>().iAmSelected)
					{
						bowlImages[i].GetComponent<ObjectMotion>().iAmSelected = false;
						bowlImages[i].GetComponent<ObjectMotion>().mySelection.SetActive (false);
					}
				}
			}
		}


		public void OnClickDustbn()
		{
			if(tutorialEnd)
			{
				if(clickedSoupContainer)
				{
					clickedUtensilsDestinationFunction.otherObject = dustbin;
					UtensilReached();
				}
				else if(clickedPan)
				{
					clickedUtensilsDestinationFunction.otherObject = dustbin;
					UtensilReached ();
				}
				AllClickedBoolsReset();
			}
		}

		public void AllClickedBoolsReset()
		{
			DeactivateBowlsSelection();
			DeactivatePanSelection();
			DeactivateSoupContainerSelection ();
			DeactivatePlateSelection ();

			cupCake.iAmSelected = false;
			cupCake.mySelection.gameObject.SetActive (false);

			noodles.iAmSelected = false;
			noodles.mySelection.gameObject.SetActive (false);

			noodlesVeg.iAmSelected = false;
			noodlesVeg.mySelection.gameObject.SetActive (false);

			soupVeg.iAmSelected = false;
			soupVeg.mySelection.gameObject.SetActive (false);

			clickedNoodlePlate = false;
			clickedNoodlesVeg = false;
			clickedNoodlesToCook = false;
			clickedSoupVeg = false;
			clickedPan = false;
			clickSoupBowl = false;
			clickedSoupContainer = false;
		}
	}
}
