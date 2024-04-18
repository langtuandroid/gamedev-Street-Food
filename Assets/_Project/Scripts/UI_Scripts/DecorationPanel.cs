using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DecorationPanel : MonoBehaviour {
	public int noOfPanels;

	public GameObject leftArrow , rightArrow;

	public GameObject []panels;

	public int selectedPanel = 1;

	public Text totalCoinsText;
	
	public Text totalGoldText;

	public static DecorationPanel _instance;

	public GameObject China_tabletop_lock ;
	public GameObject China_tablecover_lock ;
	public GameObject Italy_tabletop_lock ;
	public GameObject Italy_tablecover_lock ;
	public GameObject Aus_tabletop_lock ;
	public GameObject Aus_tablecover_lock ;
	
	void Start () {
		_instance = this;
		totalGoldText.text = MenuManager.golds.ToString ();
		totalCoinsText.text = MenuManager.totalscore.ToString ();

		if(selectedPanel == noOfPanels)
		{
			leftArrow.SetActive (false);
			rightArrow.SetActive (false);
		}
	}
	
	void OnEnable()
	{
		if(PlayerPrefs.HasKey ("ChinaOpen")) {
			China_tabletop_lock.SetActive(false); 
			China_tablecover_lock.SetActive(false); 
		}
		
		if(PlayerPrefs.HasKey ("ItalyOpen")) {
			Italy_tabletop_lock.SetActive(false); 
			Italy_tablecover_lock.SetActive(false);
		}
		if(PlayerPrefs.HasKey ("AusOpen")) {
			Aus_tabletop_lock.SetActive(false); 
			Aus_tablecover_lock.SetActive(false);

		}
		totalGoldText.text = MenuManager.golds.ToString ();
		totalCoinsText.text = MenuManager.totalscore.ToString ();
		TutorialPanel.popupPanelActive = true;
	}

	public void Close()
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

	public void MoveRight()
	{
		if(selectedPanel < noOfPanels )
		{
			MenuManager._instance.EnableFadePanel();
			panels[selectedPanel].SetActive (false);
			selectedPanel++;
			panels[selectedPanel].SetActive (true);
			if(selectedPanel == noOfPanels)
			{


				rightArrow.SetActive (false);
			}
			leftArrow.SetActive (true);
		}
	}

	public void MoveLeft()
	{
		if(selectedPanel > 1)
		{
			MenuManager._instance.EnableFadePanel();
			panels[selectedPanel].SetActive (false);
			selectedPanel--;
			panels[selectedPanel].SetActive (true);
			if(selectedPanel == 1)
			{

				leftArrow.SetActive (false);
			}
	
			rightArrow.SetActive (true);
		}
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

}
