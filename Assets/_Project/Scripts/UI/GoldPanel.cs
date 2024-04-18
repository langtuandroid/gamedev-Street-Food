using _Project.Scripts.UI_Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
	public class GoldPanel : MonoBehaviour 
	{
		public Text totalCoinsText;
		public Text totalGoldText;

		private void Start () 
		{
			totalGoldText.text = MenuManager.golds.ToString ();
			totalCoinsText.text = MenuManager.totalscore.ToString ();
		}
		
		public void Cross()
		{

			if (UIManager._instance == null) {
				GameObject upgradePanel = null;
				if (MenuManager._instance.lastPanelName == "") {
					upgradePanel = (GameObject)Instantiate (Resources.Load ("UpgradePanel"));
				} else {
					upgradePanel = (GameObject)Instantiate (Resources.Load (MenuManager._instance.lastPanelName));
				}
				upgradePanel.transform.SetParent (transform.parent, false);
				upgradePanel.transform.localScale = Vector3.one;
				upgradePanel.transform.localPosition = Vector3.zero;
				if (MenuManager._instance != null)
					MenuManager._instance.EnableFadePanel ();
				else
					UIManager._instance.EnableFadePanel ();
				Destroy (gameObject);
			} else {
				GameObject upgradePanel = ( GameObject )Instantiate(Resources.Load ("UpgradePanel"));
				upgradePanel.transform.SetParent(transform.parent,false);
				upgradePanel.transform.localScale = Vector3.one;
				upgradePanel.transform.localPosition = Vector3.zero;
				if(MenuManager._instance != null)
					MenuManager._instance.EnableFadePanel ();
				else
					UIManager._instance.EnableFadePanel ();
				Destroy (gameObject);
			}
		}
	}
}
