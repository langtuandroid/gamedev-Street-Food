using _Project.Scripts.UI_Scripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Achivments
{
	public class AchievementsPanel : MonoBehaviour 
	{
		[Inject] private MenuManager _menuManager;  
		[Inject] private UIManager _uiManager;   
		[SerializeField] private  Text totalCoinsText;
		[SerializeField] private  Text totalGoldText;

		private void Start () {
			totalGoldText.text = MenuManager.golds.ToString ();
			totalCoinsText.text = MenuManager.totalscore.ToString ();
		}
	
		public void Close()
		{
			if(_menuManager != null)
				_menuManager.EnableFadePanel ();
			else
			{
				_uiManager.gameOverPanel.SetActive (true);
				_uiManager.EnableFadePanel ();
			}
			Destroy (gameObject);
		}
	}
}
