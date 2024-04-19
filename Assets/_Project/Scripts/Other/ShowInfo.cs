using _Project.Scripts.UI_Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Scripts.Other
{
	public class ShowInfo : MonoBehaviour 
	{
		[Inject] private DiContainer _diContainer;
		[Inject] private MenuManager _menuManager;  
		[Inject] private UIManager _uiManager;   
		[FormerlySerializedAs("itemUsage")] public string []_tiems;
		private GameObject SpawnPopupPanel()
		{
			GameObject popupPanel = _diContainer.InstantiatePrefab(Resources.Load ("PopupPanel"));
			popupPanel.transform.SetParent(transform.parent,false);
			popupPanel.transform.localScale = Vector3.one;
			popupPanel.transform.localPosition = Vector3.zero;
			return popupPanel;
		}

		private void OpenmenuPopUp(string messagePopup)
		{
			if(_menuManager.popupPanel != null)
			{
				_menuManager.popupPanel.gameObject.SetActive (true);
				_menuManager.popupPanel.EnablePopup (messagePopup,false);
			}
			else
			{
				GameObject popupPanel = SpawnPopupPanel();
				_menuManager.popupPanel = popupPanel.GetComponent<PopupPanel>();
				_menuManager.popupPanel.EnablePopup (messagePopup,false);
			}
		}

		private void OpenGamePopup(string messagePopup)
		{
			if(_uiManager.popupPanel != null)
			{
				_uiManager.popupPanel.gameObject.SetActive (true);
				_uiManager.popupPanel.EnablePopup (messagePopup,false);
			}
			else
			{
				GameObject popupPanel = SpawnPopupPanel();
				_uiManager.popupPanel = popupPanel.GetComponent<PopupPanel>();
				_uiManager.popupPanel.EnablePopup (messagePopup,false);
			}
		}

		public void HelpClick(int itemNo)
		{
			if(_menuManager != null)
			{
				OpenmenuPopUp(_tiems[itemNo]);
			}
			else
			{
				OpenGamePopup(_tiems[itemNo]);
			}
		}
	}
}


