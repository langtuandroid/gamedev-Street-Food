using _Project.Scripts.UI_Scripts;
using _Project.Scripts.UI.Tutorial;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
	public class LevelPanel : MonoBehaviour 
	{
		public Text totalCoinsText;
		public Text totalGoldText;

		private void OnEnable()
		{
			totalGoldText.text = MenuManager.golds.ToString ();
			totalCoinsText.text = MenuManager.totalscore.ToString ();
			TutorialPanel.popupPanelActive = true;
		}
	}
}
