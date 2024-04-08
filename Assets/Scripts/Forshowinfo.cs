using UnityEngine;
using System.Collections;

public class Forshowinfo : MonoBehaviour {

	public string []itemUsage;

	//public Text radio_text ;
	//public Text bell_text ;
	//public Text whistle_text ;
	//public Text handcuff_text ;
	//public Text Cupcake_text ;
	
	// Use this for initialization
	void Start () {
		
	
	}
	void Update()
	{
		//Onlevel ();
	}
	GameObject GeneratePopupPanel()
	{
		GameObject popupPanel = ( GameObject )Instantiate(Resources.Load ("PopupPanel"));
		popupPanel.transform.SetParent(transform.parent,false);
		popupPanel.transform.localScale = Vector3.one;
		popupPanel.transform.localPosition = Vector3.zero;
		return popupPanel;
	}

	void MenuPopup(string messagePopup)
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
		
		
		//		equipmentPanel.purchaseButton.GetComponent<Button>().onClick.RemoveAllListeners ();
		//		equipmentPanel.purchaseButton.GetComponent<Button>().onClick.AddListener (()=>OnPurchase());
	}
	

	
	void GamePopup(string messagePopup)
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

//	public int Onlevel(int item2)
//	{
//
//		if (MenuManager._instance.Level_loaded == 1 ) 
//		{
//			item2 = 0 ;
//		}
//		else if (MenuManager._instance.Level_loaded  == 2) 
//		{
//			item2 = 1 ;
//		}
//		else if (MenuManager._instance.Level_loaded  == 3) 
//		{
//			item2 = 2 ;
//		}
//		else if (MenuManager._instance.Level_loaded  == 4) 
//		{
//			item2 = 3 ;
//		}
//		return itemNo2 = item2;
//	}

	

	

	

	

	

	

	
}


