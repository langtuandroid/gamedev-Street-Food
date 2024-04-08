using UnityEngine;
using System.Collections;

public class EquimentShowInfo : MonoBehaviour {

	public static EquimentShowInfo _instance ;
	public string []itemUsage ;
	public string []itemUsage2 ;
	public string []itemUsage3 ;
	public string []itemUsage4 ;
	public string []itemUsage5 ;
	public int itemNo;
	// Use this for initialization

	void Awake () {
		_instance = this;
		//		PlayerPrefs.DeleteAll ();
	}
	// Update is called once per frame
	void Update () {

	}
	GameObject GeneratePopupPanel()
	{

		GameObject popupPanel = ( GameObject )Instantiate(Resources.Load ("EqPopupPanel"));
		popupPanel.transform.SetParent(transform.parent,false);
		//popupPanel.transform.localScale = Vector3.one;
		popupPanel.transform.localPosition = Vector3.zero;
		return popupPanel;
	}
	
	void MenuPopup(string messagePopup,string messagePopup2,string messagePopup3,string messagePopup4,string messagePopup5)
	{
		
		//if(MenuManager._instance.popupPanel2 != null)
		{
			MenuManager._instance.popupPanel2.gameObject.SetActive (true);
			MenuManager._instance.popupPanel2.EnablePopup (messagePopup,messagePopup2,messagePopup3,messagePopup4,messagePopup5,false);
		}
		//else
		{
		//	GameObject popupPanel2 = GeneratePopupPanel();
		//	MenuManager._instance.popupPanel2 = popupPanel2.GetComponent<PopupPanel2>();
			//MenuManager._instance.popupPanel2.EnablePopup (messagePopup,messagePopup2,false);
		}
		
		
		//		equipmentPanel.purchaseButton.GetComponent<Button>().onClick.RemoveAllListeners ();
		//		equipmentPanel.purchaseButton.GetComponent<Button>().onClick.AddListener (()=>OnPurchase());
	}
	
	
	


	public void ClickedHelp()
	{
		EquipmentPanel._instance.help_bttn.GetComponent<Animator> ().enabled = false;
		//Debug.Log ("itemno " +itemNo);
		if(MenuManager._instance != null)
		{
			MenuPopup(itemUsage[itemNo],itemUsage2[itemNo],itemUsage3[itemNo],itemUsage4[itemNo],itemUsage5[itemNo]);

		}
		else
		{

			//GamePopup(itemUsage[itemNo]);

		}
	}
}
