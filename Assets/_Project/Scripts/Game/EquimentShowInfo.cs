using _Project.Scripts.UI_Scripts;
using UnityEngine;

namespace _Project.Scripts.Game
{
	public class EquimentShowInfo : MonoBehaviour {

		public static EquimentShowInfo _instance ;
		public string []itemUsage ;
		public string []itemUsage2 ;
		public string []itemUsage3 ;
		public string []itemUsage4 ;
		public string []itemUsage5 ;
		public int itemNo;

		private void Awake () 
		{
			_instance = this;
		}

		private GameObject GeneratePopupPanel()
		{
			GameObject popupPanel = ( GameObject )Instantiate(Resources.Load ("EqPopupPanel"));
			popupPanel.transform.SetParent(transform.parent,false);
			popupPanel.transform.localPosition = Vector3.zero;
			return popupPanel;
		}

		private void MenuPopup(string messagePopup,string messagePopup2,string messagePopup3,string messagePopup4,string messagePopup5)
		{
			MenuManager._instance.popupPanel2.gameObject.SetActive (true);
			MenuManager._instance.popupPanel2.EnablePopup (messagePopup,messagePopup2,messagePopup3,messagePopup4,messagePopup5,false);
		}
	
		public void ClickedHelp()
		{
			EquipmentPanel._instance.help_bttn.GetComponent<Animator> ().enabled = false;
			if(MenuManager._instance != null)
			{
				MenuPopup(itemUsage[itemNo],itemUsage2[itemNo],itemUsage3[itemNo],itemUsage4[itemNo],itemUsage5[itemNo]);
			}
		
		}
	}
}
