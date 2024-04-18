using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnvironmentPanel : MonoBehaviour 
{
	int noOfLevelsOpen;

	public Text totalCoinsText;
	
	public Text totalGoldText;
	public GameObject Aus_lock ;
	public GameObject Italy_lock ;
	public GameObject China_lock ;

	public void OnUpgradePanel()
	{
		GameObject upgradePanel = ( GameObject )Instantiate(Resources.Load ("UpgradePanel"));
		upgradePanel.transform.SetParent(transform.parent,false);
		upgradePanel.transform.localScale = Vector3.one;
		upgradePanel.transform.localPosition = Vector3.zero;
		Destroy (gameObject);
		if(MenuManager._instance != null)
			MenuManager._instance.EnableFadePanel ();
		else
			UIManager._instance.EnableFadePanel ();
	}

	void OnEnable()
	{
		if(PlayerPrefs.HasKey ("ChinaOpen")) {
			China_lock.SetActive (false);
		}
		
		if(PlayerPrefs.HasKey ("ItalyOpen") )
		{
			Italy_lock.SetActive (false);
		}
		if(PlayerPrefs.HasKey ("AusOpen")) 
		{
			Aus_lock.SetActive (false);
		}
		totalGoldText.text = MenuManager.golds.ToString ();
		totalCoinsText.text = MenuManager.totalscore.ToString ();
		TutorialPanel.popupPanelActive = true;
	}

	public void USLevel()
	{

		MenuManager._instance.Level_loaded = 1 ;
	
		MenuManager.envNo = "US";
		MenuManager._instance.EnableFadePanel ();
		Destroy (gameObject);
		MenuManager._instance.levelPanel.SetActive (true);
	}

	public void AustraliaLevel()
	{
		MenuManager._instance.Level_loaded = 4 ;
		MenuManager.envNo = "Aus";
		int noOfItalyLevelsOpen = (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("ItalyLevels"));
		if(noOfItalyLevelsOpen >= 10 || PlayerPrefs.HasKey ("AusOpen"))
		{
			Aus_lock.SetActive (false);
			Destroy (gameObject);
			MenuManager._instance.EnableFadePanel ();
			MenuManager._instance.levelPanel.SetActive (true);
		}
	}

	void MenuPopup()
	{
		if(MenuManager._instance.popupPanel != null)
		{
			MenuManager._instance.popupPanel.gameObject.SetActive (true);
		}
		else
		{
			GameObject popupPanel = GeneratePopupPanel();
			MenuManager._instance.popupPanel = popupPanel.GetComponent<PopupPanel>();
		}
		
	}

	GameObject GeneratePopupPanel()
	{
		GameObject popupPanel = ( GameObject )Instantiate(Resources.Load ("PopupPanel"));
		popupPanel.transform.SetParent(transform.parent,false);
		popupPanel.transform.localScale = Vector3.one;
		popupPanel.transform.localPosition = Vector3.zero;
		return popupPanel;
	}


	public void ItalyLevel()
	{
		MenuManager._instance.Level_loaded = 3;
		MenuManager.envNo = "Italy";
		int noOfChinaLevelsOpen = (int)EncryptionHandler64.Decrypt(PlayerPrefs.GetString("ChinaLevels"));

		if (noOfChinaLevelsOpen >= 10 || PlayerPrefs.HasKey("ItalyOpen"))
		{
			Italy_lock.SetActive(false);
			Destroy(gameObject);
			MenuManager._instance.EnableFadePanel();
			MenuManager._instance.levelPanel.SetActive(true);
		}
	}

	void PurchaseLevel(string toSetPlayerPref)
	{
		MenuManager.golds-=20;
		PlayerPrefs.SetString("Golds",EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
		totalGoldText.text = MenuManager.golds.ToString ();
	
		PlayerPrefs.SetInt (toSetPlayerPref,1);
		if(PlayerPrefs.HasKey ("ChinaOpen")) 
		{
			China_lock.SetActive (false);
		}

		if(PlayerPrefs.HasKey ("ItalyOpen")) 
		{
			Italy_lock.SetActive (false);
		}
		if(PlayerPrefs.HasKey ("AusOpen")) 
		{
			Aus_lock.SetActive (false);
		}
		MenuManager._instance.EnableFadePanel ();
		MenuManager._instance.popupPanel.gameObject.SetActive (false);
		
	}


	public void ChinaLevel()
	{
		MenuManager._instance.Level_loaded = 2 ;
		MenuManager.envNo = "China";
		int noOfUSLevelsOpen = (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("USLevels"));
		if(noOfUSLevelsOpen >= 10 || PlayerPrefs.HasKey ("ChinaOpen"))
		{
			China_lock.SetActive (false);
			Destroy (gameObject);
			MenuManager._instance.EnableFadePanel ();
			MenuManager._instance.levelPanel.SetActive (true);
		}
	
	}

	public void Cross()
	{
		MenuManager._instance.EnableFadePanel ();
		Destroy (gameObject);
	}
}
