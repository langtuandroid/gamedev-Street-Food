using _Project.Scripts.Additional;
using _Project.Scripts.UI.Tutorial;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI_Scripts
{
	public class EnvironmentPanel : MonoBehaviour 
	{
		[Inject] private DiContainer _diContainer;
		[Inject] private MenuManager _menuManager;  
		[Inject] private UIManager _uiManager;
		private int noOfLevelsOpen;
		public Text totalCoinsText;
		public Text totalGoldText;
		public GameObject Aus_lock ;
		public GameObject Italy_lock ;
		public GameObject China_lock ;

		public void OnUpgradePanel()
		{
			GameObject upgradePanel = _diContainer.InstantiatePrefab(Resources.Load ("UpgradePanel"));
			upgradePanel.transform.SetParent(transform.parent,false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
			Destroy (gameObject);
			if(_menuManager != null)
				_menuManager.EnableFadePanel ();
			else
				_uiManager.EnableFadePanel ();
		}

		private void OnEnable()
		{
			if(PlayerPrefs.HasKey ("ChinaOpen")) {
				China_lock.SetActive (false);
			}
		
			if(PlayerPrefs.HasKey ("ItalyOpen") )
			{
				Italy_lock.SetActive (false);
			}
			if(PlayerPrefs.HasKey ("AusOpen")) 
			{
				Aus_lock.SetActive (false);
			}
			totalGoldText.text = MenuManager.golds.ToString ();
			totalCoinsText.text = MenuManager.totalscore.ToString ();
			TutorialPanel.popupPanelActive = true;
		}

		public void USLevel()
		{
			MenuManager.envNo = "US";
			_menuManager.EnableFadePanel ();
			Destroy (gameObject);
			_menuManager.levelPanel.SetActive (true);
		}

		public void AustraliaLevel()
		{
		
			MenuManager.envNo = "Aus";
			int noOfItalyLevelsOpen = (int)Encryption.Decrypt (PlayerPrefs.GetString("ItalyLevels"));
			if(noOfItalyLevelsOpen >= 30 || PlayerPrefs.HasKey ("AusOpen")) 
			{
				Aus_lock.SetActive (false);
				Destroy (gameObject);
				_menuManager.EnableFadePanel ();
				_menuManager.levelPanel.SetActive (true);
			}
		}

		void MenuPopup()
		{
			if(_menuManager.popupPanel != null)
			{
				_menuManager.popupPanel.gameObject.SetActive (true);
			}
			else
			{
				GameObject popupPanel = GeneratePopupPanel();
				_menuManager.popupPanel = popupPanel.GetComponent<PopupPanel>();
			}
		
		}

		private GameObject GeneratePopupPanel()
		{
			GameObject popupPanel = _diContainer.InstantiatePrefab(Resources.Load ("PopupPanel"));
			popupPanel.transform.SetParent(transform.parent,false);
			popupPanel.transform.localScale = Vector3.one;
			popupPanel.transform.localPosition = Vector3.zero;
			return popupPanel;
		}


		public void ItalyLevel()
		{
			MenuManager.envNo = "Italy";
			int noOfChinaLevelsOpen = (int)Encryption.Decrypt(PlayerPrefs.GetString("ChinaLevels"));
		
			if (noOfChinaLevelsOpen >= 20 || PlayerPrefs.HasKey("ItalyOpen")) 
			{
				Italy_lock.SetActive(false);
				Destroy(gameObject);
				_menuManager.EnableFadePanel();
				_menuManager.levelPanel.SetActive(true);
			}
		}

		private void PurchaseLevel(string toSetPlayerPref)
		{
			MenuManager.golds-=20;
			PlayerPrefs.SetString("Golds",Encryption.Encrypt (MenuManager.golds.ToString ()));
			totalGoldText.text = MenuManager.golds.ToString ();
	
			PlayerPrefs.SetInt (toSetPlayerPref,1);
			if(PlayerPrefs.HasKey ("ChinaOpen")) 
			{
				China_lock.SetActive (false);
			}

			if(PlayerPrefs.HasKey ("ItalyOpen")) 
			{
				Italy_lock.SetActive (false);
			}
			if(PlayerPrefs.HasKey ("AusOpen")) 
			{
				Aus_lock.SetActive (false);
			}
			_menuManager.EnableFadePanel ();
			_menuManager.popupPanel.gameObject.SetActive (false);
		
		}


		public void ChinaLevel()
		{
			MenuManager.envNo = "China";
			int noOfUSLevelsOpen = (int)Encryption.Decrypt (PlayerPrefs.GetString("USLevels"));
			
			if(noOfUSLevelsOpen >= 10 || PlayerPrefs.HasKey ("ChinaOpen")) 
			{
				China_lock.SetActive (false);
				Destroy (gameObject);
				_menuManager.EnableFadePanel ();
				_menuManager.levelPanel.SetActive (true);
			}
		}

		public void Cross()
		{
			_menuManager.EnableFadePanel ();
			Destroy (gameObject);
		}
	}
}
