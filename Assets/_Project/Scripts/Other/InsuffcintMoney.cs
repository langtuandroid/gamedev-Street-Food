using _Project.Scripts.UI_Scripts;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Other
{
	public class InsuffcintMoney : MonoBehaviour 
	{
		[Inject] private DiContainer _diContainer;
		[Inject] private MenuManager _menuManager;  
		[Inject] private UIManager _uiManager;   
		private void OnEnable()
		{
			transform.SetAsLastSibling ();
		}
		public void ClosePanel()
		{
			_menuManager.lastPanelName = "";
			Destroy (gameObject);
		}
	}
}
