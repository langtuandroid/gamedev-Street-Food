using System.Collections;
using _Project.Scripts.UI.Tutorial;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI_Scripts
{
	public class UpgradePanel : MonoBehaviour
	{
		[Inject] private DiContainer _diContainer;
		[Inject] private MenuManager _menuManager;  
		[Inject] private UIManager _uiManager;   
		public Text totalCoinsText;
		public Text totalGoldText;
		public GameObject popup_panel;

		private void OnEnable()
		{
			if (PlayerPrefs.GetInt ("Active") == 0) {
				PlayerPrefs.SetInt("Active",1);
				popup_panel.SetActive(true);
			}
			if (PlayerPrefs.GetInt ("Upgrade2") != 2 && _uiManager != null ) 
			{
				PlayerPrefs.SetInt("Upgrade2",2);
				popup_panel.SetActive(true);
			}
			totalGoldText.text = MenuManager.golds.ToString ();
			totalCoinsText.text = MenuManager.totalscore.ToString ();
			TutorialPanel.popupPanelActive = true;
		}

		public void CallDecrementCoin()
		{
			StopCoroutine (nameof(DecrementCoins));
			StopCoroutine (nameof(DecrementGold));
			StartCoroutine (nameof(DecrementCoins));
			StartCoroutine (nameof(DecrementGold));
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


		public void EquipmentPanel()
		{
			GameObject upgradePanel = _diContainer.InstantiatePrefab(Resources.Load ("EquipmentUpdrade"));
			upgradePanel.transform.SetParent(transform.parent,false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
			if(_menuManager != null)
				_menuManager.EnableFadePanel ();
			else
				_uiManager.EnableFadePanel ();
			Destroy (gameObject);
		}

		public void SpecialPanel()
		{
			GameObject specialPanel = _diContainer.InstantiatePrefab(Resources.Load ("SpecialPanel"));
			specialPanel.transform.SetParent(transform.parent,false);
			specialPanel.transform.localScale = Vector3.one;
			specialPanel.transform.localPosition = Vector3.zero;
			if(_menuManager != null)
				_menuManager.EnableFadePanel ();
			else
				_uiManager.EnableFadePanel ();
			Destroy (gameObject);
		}

		public void DecorationlPanel()
		{
			GameObject decorationPanel = _diContainer.InstantiatePrefab(Resources.Load ("DecorationPanel"));
			decorationPanel.transform.SetParent(transform.parent,false);
			decorationPanel.transform.localScale = Vector3.one;
			decorationPanel.transform.localPosition = Vector3.zero;
			if(_menuManager != null)
				_menuManager.EnableFadePanel ();
			else
				_uiManager.EnableFadePanel ();
			Destroy (gameObject);
		}
		
		public void GoldShopPanel()
		{
			GameObject decorationPanel = _diContainer.InstantiatePrefab(Resources.Load ("GoldShop"));
			decorationPanel.transform.SetParent(transform.parent,false);
			decorationPanel.transform.localScale = Vector3.one;
			decorationPanel.transform.localPosition = Vector3.zero;
			if(_menuManager != null)
				_menuManager.EnableFadePanel ();
			else
				_uiManager.EnableFadePanel ();
			Destroy (gameObject);
		}


		public void Close()
		{
			if(_menuManager != null)
				_menuManager.EnableFadePanel ();
			else
			{
				_uiManager.gameOverPanel.SetActive (true);
				_uiManager.EnableFadePanel ();
				if(UIManager.upgrade_ground_sound)
				{
					PlayerPrefs.SetInt("SOUNDON",0);
				}
			}
			Destroy (gameObject);
		}
	}
}
