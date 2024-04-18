using _Project.Scripts.Additional;
using _Project.Scripts.Entities.Customers;
using _Project.Scripts.Game;
using _Project.Scripts.Other;
using _Project.Scripts.UI_Scripts;
using _Project.Scripts.UI.Tutorial;
using UnityEngine;

namespace _Project.Scripts.Managers
{
	public class China_Manager : MonoBehaviour {

		public GameObject TheifPanel;
		public int soupPrice;
		public int lessBakedNoodlesPrice;
		public int perfectNoodlesPrice;
		public Sprite []noodlesInPanVariations;   //4  0- noodles , 1 - noodles with veg ,2 only veg , 3 cooked noodles 4 burnt noodles
		public Sprite []noodlesInPlateVariations;   //4  0- uncooked , 1 cooked
		public Sprite []soupContainerVariations;  //2 -- 0 - water , 1 - cooked
		public SpriteRenderer []noodlePlates; //6
		public Availability []noodlePlaces; //6
		public ObjectMotion []noodlesPlatesMotion; //6
		public ObjectMotion firstSoupBowl;
		public GameObject []pans; //3
		public Availability []panPlaces; //3
		public GameObject []soupContainer; //3
		public Availability []soupContainerPlaces; //3
		public SpriteRenderer []bowlImages; //6
		public Availability []bowlPlaces;  //6
		public int platesFilledCount;
		public int totalPlatesAvailable;
		public int bowlsFilled;
		public int totalBowlsAvailable;
		public static China_Manager _instance;
		public bool clickedNoodlesToCook;
		public bool clickedNoodlesVeg;
		public bool clickedSoupVeg;
		public bool clickedPan;
		public bool clickedSoupContainer;
		public bool clickSoupBowl;
		public bool clickedNoodlePlate;
		public ChineseUtensils clickedUtensilsDestinationFunction;
		public ObjectMotion clickedItemDestinationFunction;
		public ChineseUtensils []soupUtensils;
		public ChineseUtensils []panUtensil;
		public GameObject dustbin;
		public ObjectMotion cupCake;
		public ObjectMotion noodles;
		public ObjectMotion noodlesVeg;
		public ObjectMotion soupVeg;
		public SpriteRenderer bowlAdd;
		public Customer firstCustomer;
		public bool clickBowlTut;
		public bool clickPlateTut;
		public SpriteRenderer tableTop;
		public SpriteRenderer tableCover;
		public int pansUpgrade;
		public int soupContainerUpgrade;
		public static bool tutorialEnd;
		public GameObject Radio ;
		public GameObject Whistle ;
		public GameObject bell ;
		public GameObject handcuff ;
		public GameObject starting_text ;
		public bool c ;

		private void Awake () {

			_instance = this;
		}

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
			c = true;
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
			UIManager._instance._tabelcover = int.Parse (coverVal[coverVal.Length - 1].ToString ());
			char []coverVal2 = PlayerPrefs.GetString ("China_TableTop").ToCharArray ();
			UIManager._instance._tabeltop = int.Parse (coverVal[coverVal.Length - 1].ToString ());
			tableCover.sprite = Resources.Load<Sprite> (PlayerPrefs.GetString ("China_TableTop")) as Sprite;
			tableTop.sprite = Resources.Load<Sprite> (PlayerPrefs.GetString ("China_TableCover")) as Sprite;
			if(MenuManager.cupcakeNo <= 0)
			{
				cupCake.gameObject.SetActive (false);
			}
			UIManager._instance.ForCoinAdd ();
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
							LevelSoundManager._instance.bowl_click.Play();
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
					UIManager._instance.tutorialPanelBg.OpenPopupChina ("TAP OR DRAG NOODLES TO\nTHE SKILLET.",false,false , 0);
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
					noodlesPlatesMotion[i].startAnimating = false;
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
							LevelSoundManager._instance.bowl_click.Play();
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
					UIManager._instance.tutorialPanelBg.OpenPopupChina ("TAP OR DRAG INGREDIENTS\nTO STOCKPOT.",false,false , 4);
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
						bowlImages[i].GetComponent<ObjectMotion>().startAnimating = false;
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
