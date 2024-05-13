
using System;
using Integration;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI_Scripts
{
	public class GoldShopPanel : MonoBehaviour
	{
		[Inject] private DiContainer _diContainer;
		[Inject] private MenuManager _menuManager;  
		[Inject] private UIManager _uiManager;
		[Inject] private IAPService _iapService;

		private void Start()
		{
			_iapService.OnGoldBuy += GetGold;
		}

		private void GetGold(int goldAmount)
		{
			MenuManager.golds += goldAmount;
		}

		public void Close()
		{
			_iapService.OnGoldBuy -= GetGold;
			GameObject upgradePanel = _diContainer.InstantiatePrefab(Resources.Load ("UpgradePanel"));
			upgradePanel.transform.SetParent(transform.parent,false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
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
