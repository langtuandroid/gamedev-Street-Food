using System.Collections;
using _Project.Scripts.Additional;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI_Scripts
{
	public class SpecialPanel : MonoBehaviour 
	{
		[Inject] private DiContainer _diContainer;
		[Inject] private MenuManager _menuManager;  
		[Inject] private UIManager _uiManager;   
		public int bellValue;
		public int whistleValue;
		public int handcuffValue;
		public int cupcakeValue;
		public int radioValue;
		public Text totalCoinsText;
		public Text totalGoldText;
		public string []itemUsage;
		public GameObject bell_tex;
		public GameObject bell_gold;
		public GameObject bell_tick ;
		public GameObject radio_tex;
		public GameObject radio_gold;
		public GameObject radio_tick ;
		public GameObject whistle_tex;
		public GameObject whistle_gold;
		public GameObject whistle_tick ;

		private void Start () {

			totalGoldText.text = MenuManager.golds.ToString ();
			totalCoinsText.text = MenuManager.totalscore.ToString ();
		}

		private void OnEnable()
		{
			if(PlayerPrefs.HasKey("Radio"))
			{
				radio_tex.SetActive(false);
				radio_tick.SetActive(true);
				radio_gold.SetActive(false);

			}
			if(PlayerPrefs.HasKey("Bell"))
			{
				bell_tex.SetActive(false);
				bell_tick.SetActive(true);
				bell_gold.SetActive(false);
			}
			if(PlayerPrefs.HasKey("Whistle"))
			{
				whistle_tex.SetActive(false);
				whistle_tick.SetActive(true);
				whistle_gold.SetActive(false);	
			}
		}

		private void MenuPopup(string messagePopup)
		{
			if(_menuManager.popupPanel != null)
			{
				_menuManager.popupPanel.gameObject.SetActive (true);
				_menuManager.popupPanel.EnablePopup (messagePopup,false);
			}
			else
			{
				GameObject popupPanel = GeneratePopupPanel();
				_menuManager.popupPanel = popupPanel.GetComponent<PopupPanel>();
				_menuManager.popupPanel.EnablePopup (messagePopup,false);
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

		private void GamePopup(string messagePopup)
		{
			if(_uiManager.popupPanel != null)
			{
				_uiManager.popupPanel.gameObject.SetActive (true);
				_uiManager.popupPanel.EnablePopup (messagePopup,false);
			}
			else
			{
				GameObject popupPanel = GeneratePopupPanel();
				_uiManager.popupPanel = popupPanel.GetComponent<PopupPanel>();
				_uiManager.popupPanel.EnablePopup (messagePopup,false);
			}
		}
	
		public void ClickedHelp(int itemNo)
		{
			if(_menuManager != null)
			{
				MenuPopup(itemUsage[itemNo]);
			}
			else
			{
				GamePopup(itemUsage[itemNo]);
			}
		}

		public void Bell()
		{
			if(!PlayerPrefs.HasKey ("Bell"))
			{

				if(MenuManager.golds >= bellValue)
				{

					if (!PlayerPrefs.HasKey ("BellsTut"))
					{
						PlayerPrefs.SetInt ("BellsTut",1);
					}

					MenuManager.golds-=bellValue;
					PlayerPrefs.SetString("Golds",Encryption.Encrypt (MenuManager.golds.ToString ()));
					PlayerPrefs.SetInt ("Bell" , 1);
					CallDecrementCoin();
					bell_tex.SetActive(false);
					bell_tick.SetActive(true);
					bell_gold.SetActive(false);
				}
				else
				{
					_menuManager.lastPanelName = "SpecialPanel";
					_menuManager.Insufficinetgold();
				}
			}
			else
			{
				_menuManager.Bellpurchase();
			}
		}

		public void Whistle()
		{
			if(!PlayerPrefs.HasKey ("Whistle"))
			{
				if(MenuManager.golds >= whistleValue)
				{
					if (!PlayerPrefs.HasKey ("WhistlesTut"))
					{
						PlayerPrefs.SetInt ("WhistlesTut",1);
					}
					MenuManager.golds-=whistleValue;
					PlayerPrefs.SetString("Golds",Encryption.Encrypt (MenuManager.golds.ToString ()));
					PlayerPrefs.SetInt ("Whistle" , 1);
					CallDecrementCoin();
					whistle_tex.SetActive(false);
					whistle_tick.SetActive(true);
					whistle_gold.SetActive(false);
				}
				else
				{
					_menuManager.lastPanelName = "SpecialPanel";
					_menuManager.Insufficinetgold();
				}
			}
			else
			{
				_menuManager.Whistlepurchase();
			}
		}

		public void Radio()
		{
			if(!PlayerPrefs.HasKey ("Radio"))
			{
				if(MenuManager.golds >= radioValue)
				{
					if (!PlayerPrefs.HasKey ("radioTut"))
					{
						PlayerPrefs.SetInt ("radioTut",1);
					}
					MenuManager.golds-=radioValue;
					PlayerPrefs.SetString("Golds",Encryption.Encrypt (MenuManager.golds.ToString ()));
					PlayerPrefs.SetInt ("Radio" , 1);
					CallDecrementCoin();
					radio_tex.SetActive(false);
					radio_tick.SetActive(true);
					radio_gold.SetActive(false);
				}
				else
				{
					_menuManager.lastPanelName = "SpecialPanel";
					_menuManager.Insufficinetgold();
				}
			}
			else
			{
				_menuManager.Radiopurchase();
			}
		}

		public void HandCuffs()
		{
			if(MenuManager.totalscore >= handcuffValue)
			{
				if (!PlayerPrefs.HasKey ("HandCuffTut"))
				{
					PlayerPrefs.SetInt ("HandCuffTut",1);
				}
				MenuManager.totalscore-=handcuffValue;
				PlayerPrefs.SetString("TotalScore",Encryption.Encrypt (MenuManager.totalscore.ToString ()));
				MenuManager.handcuffNo++;
				PlayerPrefs.SetString ("Handcuff",Encryption.Encrypt (MenuManager.handcuffNo.ToString ()));
			
				CallDecrementCoin();
			}
			else
			{
				_menuManager.lastPanelName = "SpecialPanel";
				_menuManager.Insufficinetcoin();
			}
		}

		public void Cupcake()
		{

			if(MenuManager.totalscore >= cupcakeValue)
			{
				if (!PlayerPrefs.HasKey ("cupcakeTut"))
				{
					PlayerPrefs.SetInt ("cupcakeTut",1);
				}
				MenuManager.totalscore-=cupcakeValue;
				PlayerPrefs.SetString("TotalScore",Encryption.Encrypt (MenuManager.totalscore.ToString ()));
				MenuManager.cupcakeNo = 3;
				PlayerPrefs.SetString ("Cupcake",Encryption.Encrypt (MenuManager.cupcakeNo.ToString ()));
			
				CallDecrementCoin();
			}
			else
			{
				_menuManager.Insufficinetcoin();
			}
		}

		public void Cross()
		{
			GameObject upgradePanel = _diContainer.InstantiatePrefab(Resources.Load ("UpgradePanel"));
			upgradePanel.transform.SetParent(transform.parent,false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
			if(_menuManager != null)
				_menuManager.EnableFadePanel ();
			else
				_uiManager.EnableFadePanel ();
			Destroy (gameObject);
		}

		private void CallDecrementCoin()
		{
			StopCoroutine(nameof(DecrementCoins));
			StopCoroutine(nameof(DecrementGold));
			StartCoroutine(nameof(DecrementCoins));
			StartCoroutine(nameof(DecrementGold));
		}

		private IEnumerator DecrementCoins()
		{
			int textCoins = int.Parse(totalCoinsText.text);

			while (textCoins > MenuManager.totalscore)
			{
				textCoins-=20;
				totalCoinsText.text = textCoins.ToString ();
				yield return 0;
			}
			totalCoinsText.text = MenuManager.totalscore.ToString ();

		}

		private IEnumerator DecrementGold()
		{
			int textCoins = int.Parse(totalGoldText.text);
			while (textCoins > MenuManager.golds)
			{
				textCoins-=1;
				totalGoldText.text = textCoins.ToString ();
				yield return 0;
			}
			totalGoldText.text = MenuManager.golds.ToString ();
		
		}

	}
}
