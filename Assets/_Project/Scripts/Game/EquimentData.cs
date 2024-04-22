using _Project.Scripts.UI_Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Scripts.Game
{
	public class EquimentData : MonoBehaviour 
	{
		[Inject] private DiContainer _diContainer;
		[Inject] private MenuManager _menuManager;  
		public static EquimentData _instance ;
		[FormerlySerializedAs("itemUsage")] public string []_itemsUsage1 ;
		[FormerlySerializedAs("itemUsage2")] public string []_itemsUsage2 ;
		[FormerlySerializedAs("itemUsage3")] public string []itemUsed3 ;
		[FormerlySerializedAs("itemUsage4")] public string []itemUsed4 ;
		[FormerlySerializedAs("itemUsage5")] public string []itemUsed5 ;
		public int itemNumber { get; set; }

		private void Awake () 
		{
			_instance = this;
		}

		private GameObject GeneratePopupPanel()
		{
			GameObject popupPanel = _diContainer.InstantiatePrefab(Resources.Load ("EqPopupPanel"));
			popupPanel.transform.SetParent(transform.parent,false);
			popupPanel.transform.localPosition = Vector3.zero;
			return popupPanel;
		}

		private void PopUpMenu(string messagePopup,string messagePopup2,string messagePopup3,string messagePopup4,string messagePopup5)
		{
			_menuManager.popupPanel2.gameObject.SetActive (true);
			_menuManager.popupPanel2.EnablePopup (messagePopup,messagePopup2,messagePopup3,messagePopup4,messagePopup5,false);
		}
	
		public void OpenHelp()
		{
			PopUpMenu(_itemsUsage1[itemNumber], _itemsUsage2[itemNumber], itemUsed3[itemNumber], itemUsed4[itemNumber],
				itemUsed5[itemNumber]);
		}
	}
}
