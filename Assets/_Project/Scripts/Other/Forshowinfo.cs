﻿using _Project.Scripts.UI_Scripts;
using UnityEngine;

namespace _Project.Scripts.Other
{
	public class Forshowinfo : MonoBehaviour 
	{
		public string []itemUsage;
		private GameObject GeneratePopupPanel()
		{
			GameObject popupPanel = ( GameObject )Instantiate(Resources.Load ("PopupPanel"));
			popupPanel.transform.SetParent(transform.parent,false);
			popupPanel.transform.localScale = Vector3.one;
			popupPanel.transform.localPosition = Vector3.zero;
			return popupPanel;
		}

		private void MenuPopup(string messagePopup)
		{
			if(MenuManager._instance.popupPanel != null)
			{
				MenuManager._instance.popupPanel.gameObject.SetActive (true);
				MenuManager._instance.popupPanel.EnablePopup (messagePopup,false);
			}
			else
			{
				GameObject popupPanel = GeneratePopupPanel();
				MenuManager._instance.popupPanel = popupPanel.GetComponent<PopupPanel>();
				MenuManager._instance.popupPanel.EnablePopup (messagePopup,false);
			}
		}

		private void GamePopup(string messagePopup)
		{
			if(UIManager._instance.popupPanel != null)
			{
				UIManager._instance.popupPanel.gameObject.SetActive (true);
				UIManager._instance.popupPanel.EnablePopup (messagePopup,false);
			}
			else
			{
				GameObject popupPanel = GeneratePopupPanel();
				UIManager._instance.popupPanel = popupPanel.GetComponent<PopupPanel>();
				UIManager._instance.popupPanel.EnablePopup (messagePopup,false);
			}
		}

		public void ClickedHelp(int itemNo)
		{

			if(MenuManager._instance != null)
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


