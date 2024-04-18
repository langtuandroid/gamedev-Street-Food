using _Project.Scripts.UI_Scripts;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Other
{
	public class Forshowinfo : MonoBehaviour 
	{
		[Inject] private DiContainer _diContainer;
		[Inject] private MenuManager _menuManager;  
		[Inject] private UIManager _uiManager;   
		public string []itemUsage;
		private GameObject GeneratePopupPanel()
		{
			GameObject popupPanel = _diContainer.InstantiatePrefab(Resources.Load ("PopupPanel"));
			popupPanel.transform.SetParent(transform.parent,false);
			popupPanel.transform.localScale = Vector3.one;
			popupPanel.transform.localPosition = Vector3.zero;
			return popupPanel;
		}

		private void MenuPopup(string messagePopup)
		{
			if(_menuManager.popupPanel != null)
			{
				_menuManager.popupPanel.gameObject.SetActive (true);
				_menuManager.popupPanel.EnablePopup (messagePopup,false);
			}
			else
			{
				GameObject popupPanel = GeneratePopupPanel();
				_menuManager.popupPanel = popupPanel.GetComponent<PopupPanel>();
				_menuManager.popupPanel.EnablePopup (messagePopup,false);
			}
		}

		private void GamePopup(string messagePopup)
		{
			if(_uiManager.popupPanel != null)
			{
				_uiManager.popupPanel.gameObject.SetActive (true);
				_uiManager.popupPanel.EnablePopup (messagePopup,false);
			}
			else
			{
				GameObject popupPanel = GeneratePopupPanel();
				_uiManager.popupPanel = popupPanel.GetComponent<PopupPanel>();
				_uiManager.popupPanel.EnablePopup (messagePopup,false);
			}
		}

		public void ClickedHelp(int itemNo)
		{

			if(_menuManager != null)
			{
				MenuPopup(itemUsage[itemNo]);
			}
			else
			{
				GamePopup(itemUsage[itemNo]);
			}
		}
	}
}


