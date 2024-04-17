using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class GoldPanel : MonoBehaviour {

	public static GoldPanel _instance ;
	public Text totalCoinsText;
	
	public Text totalGoldText;
	// Use this for initialization
	void Awake()
	{
		_instance = this;
//		#if UNITY_IOS
//		Advertisement.Initialize ("Insert Your IOS unity id here");
//		#else
//		Advertisement.Initialize ("your id here");
//		#endif
	}
	void Start () {
		totalGoldText.text = MenuManager.golds.ToString ();
		totalCoinsText.text = MenuManager.totalscore.ToString ();


	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Cross()
	{

		if (UIManager._instance == null) {
			GameObject upgradePanel = null;
			if (MenuManager._instance.lastPanelName == "") {
				upgradePanel = (GameObject)Instantiate (Resources.Load ("UpgradePanel"));
			} else {
				upgradePanel = (GameObject)Instantiate (Resources.Load (MenuManager._instance.lastPanelName));
			}
			upgradePanel.transform.SetParent (transform.parent, false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
			if (MenuManager._instance != null)
				MenuManager._instance.EnableFadePanel ();
			else
				UIManager._instance.EnableFadePanel ();
			Destroy (gameObject);
		} else {
			GameObject upgradePanel = ( GameObject )Instantiate(Resources.Load ("UpgradePanel"));
			upgradePanel.transform.SetParent(transform.parent,false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
			if(MenuManager._instance != null)
				MenuManager._instance.EnableFadePanel ();
			else
				UIManager._instance.EnableFadePanel ();
			Destroy (gameObject);
		}
	}
}
