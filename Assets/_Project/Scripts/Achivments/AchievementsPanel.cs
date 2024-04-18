using _Project.Scripts.UI_Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Achivments
{
	public class AchievementsPanel : MonoBehaviour 
	{
		public Text totalCoinsText;
		public Text totalGoldText;

		private void Start () {
			totalGoldText.text = MenuManager.golds.ToString ();
			totalCoinsText.text = MenuManager.totalscore.ToString ();
		}
	
		public void Close()
		{
			if(MenuManager._instance != null)
				MenuManager._instance.EnableFadePanel ();
			else
			{
				UIManager._instance.gameOverPanel.SetActive (true);
				UIManager._instance.EnableFadePanel ();
			}
			Destroy (gameObject);
		}
	}
}
