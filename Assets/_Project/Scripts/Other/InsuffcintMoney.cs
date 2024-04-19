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
		public void OpenGoldPanel()
		{
			GameObject specialPanel = _diContainer.InstantiatePrefab(Resources.Load ("GoldPanel"));
			specialPanel.transform.SetParent(transform.parent,false);
			specialPanel.transform.localScale = Vector3.one;
			specialPanel.transform.localPosition = Vector3.zero;
			if(_menuManager != null)
				_menuManager.EnableFadePanel ();
			else
				_uiManager.EnableFadePanel ();
			Destroy (gameObject);

			Destroy (_menuManager.lastPanel);
	    
		}

		public void ClosePanel()
		{
			_menuManager.lastPanel = null;
			_menuManager.lastPanelName = "";
			Destroy (gameObject);
		}
	}
}
