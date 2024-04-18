using System.Collections;
using _Project.Scripts.Additional;
using _Project.Scripts.Audio;
using _Project.Scripts.Entities.Loader;
using _Project.Scripts.Managers;
using _Project.Scripts.UI.Tutorial;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI_Scripts
{
	public class UIManager : MonoBehaviour 
	{
		public int totalCoins;
		public GameObject uiPanel;
		public GameObject fadePanel;
		public Image fadePanelImage;
		public Text goldText; 
		public GameObject gameOverPanel;
		public Text gameoverHeading;
		public Text dayEarningsHeading , dayEarningsLabel;
		public Image []stars;
		public Slider sliderValue;
		public Image expertStar;
		public Text totalScore;
		public GameObject loader ;
		public Sprite starSprite;
		public Sprite unfillSprite;
		public Image gameStartImage;
		public AutoType targetText;
		public TutorialPanel tutorialPanelCanvas;
		public TutorialPanel tutorialPanelBg;
		public GameObject goalMet;
		public ParticleSystem expertStarAchieved;
		public PopupPanel popupPanel;
		public Text coinsText;
		public Button nextButton;
		public GameObject Pausepanel;
		public GameObject Pop_panel ;
		public ParticleSystem coincollect ;
		public int _tabelcover ;
		public int _tabeltop ;
		public int Bonus_coin ;
		public int n_Customer_served ;
		public int n_Perfect_achieved ;
		public int n_Burger_served ;
		public int n_French_fries_served ;
		public int n_Cokes_served ;
		public int n_Thieves_caught ;
		public int n_Noodles_served ;
		public int n_Pizzas_served ;
		public int n_Hotdogs_served ;
		public GameObject gold_Collected ;
		public GameObject upgrade_bttn ;
		public GameObject gameover_effect ;
		public GameObject achievment_text ;
		public GameObject upback;
		public GameObject achivment_bttn ;
		public GameObject upgrade_hand;
		public TextMesh dustbin_text ;
		public GameObject dustbin_textparent ;
	
		[HideInInspector]
		public Vector3 dustbintextintialposition ;
		public int Unsucces;
		public GameObject whistle;
	
		public GameObject blow ;
		public GameObject loader2;
		public AudioSource radio_audio ;
		public static bool upgrade_ground_sound;
	
		private bool _once;
		private int levelNoOfEnv;
		private Vector3 whistleinitposition ;
	
		private void Awake () 
		{
			if(!PlayerPrefs.HasKey ("PlateUpgrade"))
			{
				PlayerPrefs.SetString("Golds",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("TotalScore",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("Cupcake",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("Handcuff",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("GrillsUpgrade",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("USCokeUpgrade",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("PlateUpgrade",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("USStars",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("USLevels",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("US_TableCover","US/base-flat-1");
				PlayerPrefs.SetString ("US_TableTop","US/top-floor-1");
				PlayerPrefs.SetInt ("US/base-flat-1" , 1);
				PlayerPrefs.SetInt ("US/top-floor-1" , 1);
				PlayerPrefs.SetString ("AusLevels",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("ItalyLevels",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("ChinaLevels",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("ChinaStars",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("China_TableCover","China/1");
				PlayerPrefs.SetString ("China_TableTop","China/a-1");
				PlayerPrefs.SetString ("ChinaPlateUpgrade",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("ChinaBowlsUpgrade",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("ChinaPansUpgrade",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("ChinaSoupContainer",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("ItalyStars",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("Italy_TableCover","Italy/1");
				PlayerPrefs.SetString ("Italy_TableTop","Italy/top-strip-1");
				PlayerPrefs.SetString ("ItalyPlateUpgrade",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("ItalyCokeUpgrade",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("OvenUpgrade",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetInt ("Italy/1" , 1);
				PlayerPrefs.SetInt ("Italy/top-strip-1" , 1);
				PlayerPrefs.SetInt ("China/1" , 1);
				PlayerPrefs.SetInt ("China/a-1" , 1);
				PlayerPrefs.SetString ("AusStars",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("Aus_TableCover","Aus/1");
				PlayerPrefs.SetString ("Aus_TableTop","Aus/top-shed-1");
				PlayerPrefs.SetString ("AusPlateUpgrade",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("FriesUpgrade",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("AusCokeUpgrade",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("AusGrillsUpgrade",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetInt ("Aus/1", 1);
				PlayerPrefs.SetInt ("Aus/top-shed-1",3);

			}
			PlayerPrefs.SetString("Golds",EncryptionHandler64.Encrypt ("10000")); //TODO Comnet 2 lines
			PlayerPrefs.SetString ("TotalScore",EncryptionHandler64.Encrypt ("10000")); //TODO
			goldText.text = EncryptionHandler64.Decrypt (PlayerPrefs.GetString("Golds")).ToString ();
			StartCoroutine (ShowTarget());
		}
	
		private void Start()
		{
			upgrade_ground_sound = false;
			if (AudioListener.volume > 0) {

				upgrade_ground_sound = true ;
			}
			gameover_effect.SetActive (false);
			Unsucces = PlayerPrefs.GetInt ("Unsuccessful");
			whistleinitposition = whistle.transform.localScale; 
		
		}
		public void WhistleInitialpos()
		{
			whistle.transform.localScale = whistleinitposition;
			blow.SetActive (false);
		}

		private IEnumerator ShowTarget()
		{
			TutorialPanel.popupPanelActive = true;
			while(gameStartImage.rectTransform.sizeDelta.x < 1100)
			{
				gameStartImage.rectTransform.sizeDelta = new Vector2(gameStartImage.rectTransform.sizeDelta.x+50,gameStartImage.rectTransform.sizeDelta.y);
				yield return 0;
			}
			targetText.imageToDeactivate = gameStartImage.gameObject;
			StartCoroutine (targetText.TypeText());
		}
	
	
		public void OnGameOver()
		{
			levelNoOfEnv = LevelManager.levelNo%10;
			if(levelNoOfEnv == 0)
			{
				levelNoOfEnv = 10;
			}
			if (LevelManager.levelNo == 1)
			{
				upgrade_bttn.GetComponent<Animator> ().enabled = true;
				upback.SetActive(true);
				upgrade_hand.SetActive(true);
			}
			if (LevelManager.levelNo == 2)
			{
				achivment_bttn.GetComponent<Animator> ().enabled = true;
			}

			uiPanel.SetActive (false);
			gameOverPanel.SetActive (true);
			radio_audio.Stop ();
			if(totalCoins >= LevelManager._instance.targetScore[LevelManager.levelNo])
			{
				gameover_effect.SetActive(true);

				LevelSoundManager._instance.successful_level.Play();

				Invoke("gameoversoundf",1.5f);
				gameoverHeading.text = "successful day!";
				print ("game over text set true hereeeeeeeeeeeeeeee");
				int noOfLevelsOpen = (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString(MenuManager.envNo+"Levels"));

				if(levelNoOfEnv > noOfLevelsOpen)
				{
					PlayerPrefs.SetString (MenuManager.envNo+"Levels",EncryptionHandler64.Encrypt (levelNoOfEnv.ToString ()));
				}

			}
			else
			{
			
				nextButton.interactable = false;
				LevelSoundManager._instance.unsuccessful_level.Play();
				gameoverHeading.text = "unsuccessful day!";
				if(LevelManager.levelNo == 4 && ((!PlayerPrefs.HasKey("Radio") || MenuManager.handcuffNo <= 0)) )
				{
					Unsucces++;
					PlayerPrefs.SetInt("Unsuccessful",Unsucces);
					{
						if(PlayerPrefs.GetInt("Unsuccessful") ==2 )
						{
							Invoke("BringRadiopopup",1.25f);
							print ("bring radio popup");

						}
				
					}
				}
			}
			dayEarningsHeading.text = "Day "+levelNoOfEnv+" Earnings";
			dayEarningsLabel.text = totalCoins.ToString ();
			int noOfStars = (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString(MenuManager.envNo+"Stars"));

			for(int i = 0;i< stars.Length ; i++)
			{
				if(i < noOfStars)
				{
					stars[i].gameObject.SetActive (true);
				}
			}

			MenuManager.totalscore+=totalCoins;
			PlayerPrefs.SetString("TotalScore",EncryptionHandler64.Encrypt (MenuManager.totalscore.ToString ()));

			totalScore.text = "Total Coins: "+MenuManager.totalscore.ToString ();
			StartCoroutine (EarningsSlider());


		}
		public void gameoversoundf()
		{
			LevelSoundManager._instance.Gameoverpanel.Play ();
		}

		private void BringRadiopopup()
		{
			Radio();
		}

		private IEnumerator EarningsSlider()
		{
			float a = 1.5f;
			float finalSliderValue = (totalCoins*1f)/(LevelManager._instance.targetScore[LevelManager.levelNo]*(a));
			if(finalSliderValue > 1)
			{
				finalSliderValue = 1;
			}
			while (sliderValue.value < finalSliderValue)
			{
				sliderValue.value+=0.02f;
				yield return 0;
			}
			sliderValue.value = finalSliderValue;

			int expertValue = (int)(LevelManager._instance.targetScore[LevelManager.levelNo]*(a));
			int usStars = (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString(MenuManager.envNo+"Stars"));
			if(totalCoins >= expertValue)
			{
				expertStar.gameObject.SetActive (true);
				expertStarAchieved.Play ();
				if(!PlayerPrefs.HasKey (MenuManager.envNo+"Stars"+levelNoOfEnv) )
				{
					PlayerPrefs.SetInt (MenuManager.envNo+"Stars"+levelNoOfEnv , 1);
					usStars++;
					PlayerPrefs.SetString(MenuManager.envNo+"Stars",EncryptionHandler64.Encrypt (usStars.ToString ()));
				}
				expertStar.transform.localScale = Vector3.zero;
				expertStar.sprite = starSprite;
				StartCoroutine (ExpertStar());
				int noOfStars = usStars;
				stars[noOfStars-1].gameObject.SetActive (true);
			}

		}

		private IEnumerator ExpertStar()
		{
			while(expertStar.transform.localScale.x < 0.55f)
			{
				expertStar.transform.localScale = new Vector3(expertStar.transform.localScale.x+0.06f,expertStar.transform.localScale.y+0.06f,1);
				yield return 0;
			}
			expertStar.transform.localScale = new Vector3(0.5591921f, 0.5522887f,1);
		}

		public void Restart()
		{
			Application.LoadLevel(Application.loadedLevel);
		}

		public void NextLevel()
		{
			if(totalCoins >= LevelManager._instance.targetScore[LevelManager.levelNo])
			{

				LevelManager.levelNo++;
				if(LevelManager.levelNo > 0 && LevelManager.levelNo <= 10 )
				{
					MenuManager.envNo = "US";
					Application.LoadLevel(1);
				}
				else if (LevelManager.levelNo > 10 && LevelManager.levelNo <= 20 ) 
				{
					MenuManager.envNo = "China";
					Application.LoadLevel(2);
				}
				else if (LevelManager.levelNo > 20 && LevelManager.levelNo <= 30 ) 
				{
					MenuManager.envNo = "Italy";
				
					Application.LoadLevel(3);
				}
				else if (LevelManager.levelNo > 30 && LevelManager.levelNo <= 40 ) 
				{
					MenuManager.envNo = "Aus";
					Application.LoadLevel(4);
				}
				else if (LevelManager.levelNo > 40 ) 
				{
					Application.LoadLevel(0);			

				}
			}
		}

		public void MainMenu()
		{
			Time.timeScale = 1;
			gameover_effect.SetActive(false);
			loader.SetActive (true);
			Loader.levelToLoad = "MenuScene";
		}
	 
		public void UpgradePanel()
		{
			if (upgrade_ground_sound ) 
			{
				PlayerPrefs.SetInt("SOUNDON",1);
			}
			gameover_effect.SetActive(false);
			upgrade_hand.SetActive(false);
			upback.SetActive (false);
			upgrade_bttn.GetComponent<Animator> ().enabled = false;
			gameOverPanel.SetActive (false);
			GameObject upgradePanel = ( GameObject )Instantiate(Resources.Load ("UpgradePanel"));
			upgradePanel.transform.SetParent(transform,false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
			EnableFadePanel();
			PlayerPrefs.SetInt ("Upgrade2", 2);

		}
		public void Achievments()
		{
			gameover_effect.SetActive(false);
			achivment_bttn.GetComponent<Animator> ().enabled = false;
			gameOverPanel.SetActive (false);
			GameObject upgradePanel = ( GameObject )Instantiate(Resources.Load ("AchievementsPanel"));
			upgradePanel.transform.SetParent(transform,false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
			loader2.SetActive (false);
			EnableFadePanel();
		
		}
		public void BellPanelTry()
		{
			GameObject upgradePanel = ( GameObject )Instantiate(Resources.Load ("BellPopupPanel"));
			upgradePanel.transform.SetParent(transform,false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
			EnableFadePanel();
		
		}
		public void BellPanelBuy()
		{
			GameObject upgradePanel = ( GameObject )Instantiate(Resources.Load ("BellPopupPanel2"));
			upgradePanel.transform.SetParent(transform,false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
			EnableFadePanel();
		}
		public void EarnGold()
		{
			Debug.Log ("GOld");
			GameObject upgradePanel = ( GameObject )Instantiate(Resources.Load ("EarnGold"));
			upgradePanel.transform.SetParent(transform,false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
			EnableFadePanel();
		}

		public void EarnCoin()
		{
			Debug.Log ("Coin");
			GameObject upgradePanel = ( GameObject )Instantiate(Resources.Load ("EarnCoin"));
			upgradePanel.transform.SetParent(transform,false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
			EnableFadePanel();
		}
		public void IAPGold()
		{
			GameObject upgradePanel = ( GameObject )Instantiate(Resources.Load ("GoldPanel"));
			upgradePanel.transform.SetParent(transform,false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
			EnableFadePanel();
		}

		private void Radio()
		{
			GameObject upgradePanel = ( GameObject )Instantiate(Resources.Load ("Radio"));
			upgradePanel.transform.SetParent(transform,false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
			EnableFadePanel();
		}

		public void Whistle()
		{
			GameObject upgradePanel = ( GameObject )Instantiate(Resources.Load ("Whistle"));
			upgradePanel.transform.SetParent(transform,false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
			EnableFadePanel();
		}
		public void Handcuff()
		{
			GameObject upgradePanel = ( GameObject )Instantiate(Resources.Load ("Handcuff"));
			upgradePanel.transform.SetParent(transform,false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
			EnableFadePanel();
		}


		public void PausePanel()
		{
			Pausepanel.SetActive (true);
			Time.timeScale = 0;
		}


		public void Resume()
		{
			Time.timeScale = 1;
			Pausepanel.SetActive (false);
		}


		private void FadeInOff()
		{
			fadePanel.SetActive (false);
		}
	
		public void EnableFadePanel()
		{
			fadePanel.transform.SetAsLastSibling ();
			fadePanel.SetActive (true);
			StartCoroutine (FadeIn());
		}

		private IEnumerator FadeIn()
		{
			float alphaVal = 1;
			fadePanelImage.color = new Color(1,1,1,1);
			while(alphaVal > 0.1f)
			{
				alphaVal-=Time.deltaTime*1.5f;
				fadePanelImage.color = new Color(1,1,1,alphaVal);
				yield return 0;
			}
			FadeInOff();
		}

		public void CallIncrementCoint()
		{
			StopCoroutine (nameof(IncrementCoins));
			StartCoroutine (nameof(IncrementCoins));
		}

		private IEnumerator IncrementCoins()
		{
			LevelSoundManager._instance.coinAdd.Play ();
			int textCoins = int.Parse(coinsText.text);
			coinsText.transform.localScale = new Vector3(1.6f , 2.4f,1);
			while (textCoins < totalCoins)
			{
				textCoins+=1;
				coinsText.text = textCoins.ToString ();
				yield return 0;
			}
			coinsText.text = totalCoins.ToString ();
			if(totalCoins > LevelManager._instance.targetScore[LevelManager.levelNo] && !_once)
			{
				goalMet.SetActive(true);
				StartCoroutine (goalMet.GetComponent<AutoType>().TypeText());
				_once = true;
			}

			coinsText.transform.localScale = new Vector3(1.76f , 1.6f,1);
		
		}
		public void ForCoinAdd()
		{
			if (LevelManager.levelNo > 0 && LevelManager.levelNo <= 10) {
				Bonus_coin = (_tabelcover - 1) * 5;	
		
			}
			else
			{
				if (_tabelcover > 0)
					Bonus_coin = ((_tabelcover - 1) * 3) + ((_tabeltop - 1) * 3);
				else
					Bonus_coin = 0;
			}
		}
	}
}
