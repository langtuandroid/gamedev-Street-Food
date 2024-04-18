using _Project.Scripts.UI_Scripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI
{
	public class GoldPanel : MonoBehaviour 
	{
		[Inject] private DiContainer _diContainer;
		[Inject] private MenuManager _menuManager;  
		[Inject] private UIManager _uiManager;   
		public Text totalCoinsText;
		public Text totalGoldText;

		private void Start () 
		{
			totalGoldText.text = MenuManager.golds.ToString ();
			totalCoinsText.text = MenuManager.totalscore.ToString ();
		}
		
		public void Cross()
		{

			if (_uiManager == null) {
				GameObject upgradePanel = null;
				if (_menuManager.lastPanelName == "") {
					upgradePanel = _diContainer.InstantiatePrefab(Resources.Load ("UpgradePanel"));
				} else {
					upgradePanel = _diContainer.InstantiatePrefab(Resources.Load (_menuManager.lastPanelName));
				}
				upgradePanel.transform.SetParent (transform.parent, false);
				upgradePanel.transform.localScale = Vector3.one;
				upgradePanel.transform.localPosition = Vector3.zero;
				if (_menuManager != null)
					_menuManager.EnableFadePanel ();
				else
					_uiManager.EnableFadePanel ();
				Destroy (gameObject);
			} else {
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
		}
	}
}
