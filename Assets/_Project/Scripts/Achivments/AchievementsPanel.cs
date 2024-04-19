using _Project.Scripts.UI_Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Achivments
{
	public class AchievementsPanel : MonoBehaviour 
	{
		[Inject] private MenuManager _menuManager;  
		[Inject] private UIManager _uiManager;   
		[FormerlySerializedAs("totalCoinsText")] [SerializeField] private Text _coinsText;
		[FormerlySerializedAs("totalGoldText")] [SerializeField] private Text _goldText;

		private void Start () 
		{
			_goldText.text = MenuManager.golds.ToString ();
			_coinsText.text = MenuManager.totalscore.ToString ();
		}
	
		public void ClosePanel()
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
