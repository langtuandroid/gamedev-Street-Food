using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpgradePanel : MonoBehaviour {


	public Text coinsText;
	
	public Text goldText;
	
	public Text totalCoinsText;
	
	public Text totalGoldText;

	public GameObject popup_panel;
	

	void OnEnable()
	{

		if (PlayerPrefs.GetInt ("Active") == 0) {
			PlayerPrefs.SetInt("Active",1);
			popup_panel.SetActive(true);
		}
		if (PlayerPrefs.GetInt ("Upgrade2") != 2 && UIManager._instance != null ) 
		{
			PlayerPrefs.SetInt("Upgrade2",2);
			popup_panel.SetActive(true);
		}
		totalGoldText.text = MenuManager.golds.ToString ();
		totalCoinsText.text = MenuManager.totalscore.ToString ();
		TutorialPanel.popupPanelActive = true;

	}

	public void CallDecrementCoin()
	{
		StopCoroutine ("DecrementCoins");
		StopCoroutine ("DecrementGold");
		StartCoroutine ("DecrementCoins");
		StartCoroutine ("DecrementGold");
	}
	
	IEnumerator DecrementCoins()
	{
		int textCoins = int.Parse(totalCoinsText.text);
		while (textCoins > MenuManager.totalscore)
		{
			textCoins-=20;
			totalCoinsText.text = textCoins.ToString ();
			yield return 0;
		}
		totalCoinsText.text = MenuManager.totalscore.ToString ();
		
	}
	
	IEnumerator DecrementGold()
	{
		int textCoins = int.Parse(totalGoldText.text);
		while (textCoins > MenuManager.golds)
		{
			textCoins-=1;
			totalGoldText.text = textCoins.ToString ();
			yield return 0;
		}
		totalGoldText.text = MenuManager.golds.ToString ();
		
	}
	public void GoldPanel()
	{
//		GoogleMobileAdsDemoScript.bannerWasLoaded=true;
		
		if (!MenuManager.onceRequestBanner) {
			MenuManager.onceRequestBanner = true;
//			GoogleMobileAdsDemoScript._instance.RequestBanner ();
		}
		else {
//			if(GoogleMobileAdsDemoScript.bannerWasLoaded)
//				GoogleMobileAdsDemoScript._instance.bannerView.Show ();
		}
		GameObject specialPanel = ( GameObject )Instantiate(Resources.Load ("GoldPanel"));
		specialPanel.transform.SetParent(transform.parent,false);
		specialPanel.transform.localScale = Vector3.one;
		specialPanel.transform.localPosition = Vector3.zero;
		if(MenuManager._instance != null)
			MenuManager._instance.EnableFadePanel ();
		else
			UIManager._instance.EnableFadePanel();
		Destroy (gameObject);

	}

	public void EquipmentPanel()
	{
//		GoogleMobileAdsDemoScript.bannerWasLoaded=true;
		
		if (!MenuManager.onceRequestBanner) {
			MenuManager.onceRequestBanner = true;
//			GoogleMobileAdsDemoScript._instance.RequestBanner ();
		}
		else {
//			if(GoogleMobileAdsDemoScript.bannerWasLoaded)
//				GoogleMobileAdsDemoScript._instance.bannerView.Show ();
		}
		GameObject upgradePanel = ( GameObject )Instantiate(Resources.Load ("EquipmentUpdrade"));
		upgradePanel.transform.SetParent(transform.parent,false);
		upgradePanel.transform.localScale = Vector3.one;
		upgradePanel.transform.localPosition = Vector3.zero;
		if(MenuManager._instance != null)
			MenuManager._instance.EnableFadePanel ();
		else
			UIManager._instance.EnableFadePanel ();
		Destroy (gameObject);
	}

	public void SpecialPanel()
	{
//		GoogleMobileAdsDemoScript.bannerWasLoaded=true;

		if (!MenuManager.onceRequestBanner) {
			MenuManager.onceRequestBanner = true;
//			GoogleMobileAdsDemoScript._instance.RequestBanner ();
		}
		else {
//			if(GoogleMobileAdsDemoScript.bannerWasLoaded)
//				GoogleMobileAdsDemoScript._instance.bannerView.Show ();
		}
		GameObject specialPanel = ( GameObject )Instantiate(Resources.Load ("SpecialPanel"));
		specialPanel.transform.SetParent(transform.parent,false);
		specialPanel.transform.localScale = Vector3.one;
		specialPanel.transform.localPosition = Vector3.zero;
		if(MenuManager._instance != null)
			MenuManager._instance.EnableFadePanel ();
		else
			UIManager._instance.EnableFadePanel ();
		Destroy (gameObject);
	}

	public void DecorationlPanel()
	{
//		if (GoogleMobileAdsDemoScript.bannerWasLoaded) {
//			GoogleMobileAdsDemoScript._instance.bannerView.Hide ();
//		}
		GameObject decorationPanel = ( GameObject )Instantiate(Resources.Load ("DecorationPanel"));
		decorationPanel.transform.SetParent(transform.parent,false);
		decorationPanel.transform.localScale = Vector3.one;
		decorationPanel.transform.localPosition = Vector3.zero;
		if(MenuManager._instance != null)
			MenuManager._instance.EnableFadePanel ();
		else
			UIManager._instance.EnableFadePanel ();
		Destroy (gameObject);
	}

	public void Close()
	{
//		if (GoogleMobileAdsDemoScript.bannerWasLoaded) {
//			GoogleMobileAdsDemoScript._instance.bannerView.Hide ();
//		}
		if(MenuManager._instance != null)
			MenuManager._instance.EnableFadePanel ();
		else
		{
			UIManager._instance.gameOverPanel.SetActive (true);
			UIManager._instance.EnableFadePanel ();
			if(UIManager.upgrade_ground_sound){
				PlayerPrefs.SetInt("SOUNDON",0);
			
			}
		
		}
		Destroy (gameObject);
	}
}
