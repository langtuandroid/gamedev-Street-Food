﻿using System.Collections;
using _Project.Scripts.Achivments;
using _Project.Scripts.Additional;
using _Project.Scripts.Managers;
using _Project.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI_Scripts
{
	public class MenuManager : MonoBehaviour
	{
		[Inject] private DiContainer _diContainer;
		public static string envNo = "US";
		public GameObject levelPanel;
		public GameObject fadePanel;
		public Image fadePanelImage;
		public PopupPanel popupPanel; 
		public PopupPanel2 popupPanel2 ;
		
		public static int golds;
		public static int totalscore;
		public static int cupcakeNo;
		public static int handcuffNo;
		public GameObject lastPanel;
		public string lastPanelName;
		public SoundManager menuSoundButton;
		public GameObject loader; 
		public GameObject loader2 ;
		public GameObject achievement_bttn;
		public Animator achievment;


		void Start () 
		{
			Application.targetFrameRate = 120;
			AchievementChild.check_claim = (PlayerPrefs.GetInt("claimvalue"));

			if(!PlayerPrefs.HasKey ("PlateUpgrade"))
			{
				PlayerPrefs.SetString("Golds",EncryptionHandler64.Encrypt ("1"));
				PlayerPrefs.SetString ("TotalScore",EncryptionHandler64.Encrypt ("100"));
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
				PlayerPrefs.SetString ("ItalyStars",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("Italy_TableCover","Italy/1");
				PlayerPrefs.SetString ("Italy_TableTop","Italy/top-strip-1");
				PlayerPrefs.SetString ("ItalyPlateUpgrade",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("ItalyCokeUpgrade",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("OvenUpgrade",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetInt ("Italy/1" , 1);
				PlayerPrefs.SetInt ("Italy/top-strip-1" , 1);
				PlayerPrefs.SetString ("ChinaStars",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("China_TableCover","China/1");
				PlayerPrefs.SetString ("China_TableTop","China/a-1");
				PlayerPrefs.SetString ("ChinaPlateUpgrade",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("ChinaBowlsUpgrade",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("ChinaPansUpgrade",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("ChinaSoupContainerUpgrade",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetInt ("China/1" , 1);
				PlayerPrefs.SetInt ("China/a-1" , 1);
				PlayerPrefs.SetString ("AusStars",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("Aus_TableCover","Aus/1");
				PlayerPrefs.SetString ("Aus_TableTop","Aus/top-shed-1");
				PlayerPrefs.SetString ("AusPlateUpgrade",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("FriesUpgrade",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("AusCokeUpgrade",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetString ("AusGrillsUpgrade",EncryptionHandler64.Encrypt ("0"));
				PlayerPrefs.SetInt ("Aus/1" , 1);
				PlayerPrefs.SetInt ("Aus/top-shed-1" , 3);
				PlayerPrefs.SetInt ("Upgrade2", 1);
			}
		
			golds = (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("Golds"));
			totalscore = (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("TotalScore"));
			cupcakeNo = (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("Cupcake"));
			handcuffNo = (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("Handcuff"));
		}
	
		void Update () 
		{
			if (PlayerPrefs.GetInt("claimvalue") > 0) 
			{
				achievment.enabled = true;
			} 
			else 
			{

				achievment.enabled = false;
			}
		}
	
		public void USLevel()
		{
			GameObject upgradePanel = _diContainer.InstantiatePrefab(Resources.Load ("EnvPanel"));
			upgradePanel.transform.SetParent(transform,false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
			EnableFadePanel();
		}

		public void Upgrades()
		{
		
			GameObject upgradePanel = _diContainer.InstantiatePrefab(Resources.Load ("UpgradePanel"));
			upgradePanel.transform.SetParent(transform,false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
			EnableFadePanel();
		}
		public void loader2f()
		{
			loader2.SetActive (true);
		}
		public void Achievments()
		{
			GameObject upgradePanel = _diContainer.InstantiatePrefab(Resources.Load("AchievementsPanel"));
			upgradePanel.transform.SetParent(transform,false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
			loader2.SetActive (false);
			EnableFadePanel();

		}

		public void LevelPanelCross()
		{
			GameObject upgradePanel = _diContainer.InstantiatePrefab(Resources.Load ("EnvPanel"));
			upgradePanel.transform.SetParent(transform,false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
			levelPanel.SetActive (false);
			EnableFadePanel();
		}
		public void Exitpanel()
		{
			GameObject upgradePanel = _diContainer.InstantiatePrefab(Resources.Load ("ExitPanel"));
			upgradePanel.transform.SetParent(transform,false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
	
			EnableFadePanel();
		}

		public void FadeInOff()
		{
			fadePanel.SetActive (false);
		}

		public void EnableFadePanel()
		{
			fadePanel.transform.SetAsLastSibling ();
			fadePanel.SetActive (true);
			StartCoroutine (FadeIn());
		}

		IEnumerator FadeIn()
		{
			float alphaVal = 1;
			fadePanelImage.color = new Color(1,1,1,1);
			while(alphaVal > 0.1f)
			{
				alphaVal-=Time.deltaTime;
				fadePanelImage.color = new Color(1,1,1,alphaVal);
				yield return 0;
			}
			FadeInOff();
		}
		public void Insufficinetcoin()
		{
			GameObject upgradePanel = _diContainer.InstantiatePrefab(Resources.Load ("InsufficinetCoin"));
			if(transform)
				upgradePanel.transform.SetParent(transform,false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
		
			EnableFadePanel();
		}
		public void Insufficinetgold()
		{
			GameObject upgradePanel = _diContainer.InstantiatePrefab(Resources.Load ("InsufficinetGold"));
			upgradePanel.transform.SetParent(transform,false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
		
			EnableFadePanel();
		}
		public void Alreadypurchase()
		{
			GameObject upgradePanel = _diContainer.InstantiatePrefab(Resources.Load ("Already"));
			upgradePanel.transform.SetParent(transform,false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
		
			EnableFadePanel();
		}

		public void Radiopurchase()
		{
			GameObject upgradePanel = _diContainer.InstantiatePrefab(Resources.Load ("AlreadyRadiol"));
			upgradePanel.transform.SetParent(transform,false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
		
			EnableFadePanel();
		}
		public void Bellpurchase()
		{
			GameObject upgradePanel = _diContainer.InstantiatePrefab(Resources.Load ("Alreadybell"));
			upgradePanel.transform.SetParent(transform,false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
		
			EnableFadePanel();
		}
		public void Whistlepurchase()
		{
			GameObject upgradePanel = _diContainer.InstantiatePrefab(Resources.Load ("AlreadyWhistle"));
		    
			upgradePanel.transform.SetParent(transform,false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
		
			EnableFadePanel();
		}

		public void GoldPanel()
		{
			GameObject specialPanel = _diContainer.InstantiatePrefab(Resources.Load ("GoldPanel"));
			specialPanel.transform.SetParent(transform.parent,false);
			specialPanel.transform.localScale = Vector3.one;
			specialPanel.transform.localPosition = Vector3.zero;
			EnableFadePanel ();

			if (false)
			{
				//_uiManager.EnableFadePanel ();
			}
			Destroy (gameObject);
		}
		public GameObject GeneratePopupPanel()
		{
		
			GameObject popupPanel2 = _diContainer.InstantiatePrefab(Resources.Load ("EqPopupPanel"));
			popupPanel2.transform.SetParent(transform.parent,false);
			popupPanel2.transform.localPosition = Vector3.zero;
			return popupPanel2;
		}
	}
}
