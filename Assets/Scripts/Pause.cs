using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public  void MenuPanel()
	{
//		if (GoogleMobileAdsDemoScript.bannerWasLoaded) {
//			GoogleMobileAdsDemoScript._instance.bannerView.Hide ();
//		}
		GameObject upgradePanel = (GameObject)Instantiate (Resources.Load ("EnvPanel"));
		upgradePanel.transform.SetParent (transform, false);
		upgradePanel.transform.localScale = Vector3.one;
		upgradePanel.transform.localPosition = Vector3.zero;
		//MenuManager._instance.EnableFadePanel ();

	}
	public void Restart()
	{
//		if (GoogleMobileAdsDemoScript.bannerWasLoaded) {
//			GoogleMobileAdsDemoScript._instance.bannerView.Hide ();
//		}
		Time.timeScale = 1;
		Application.LoadLevel(Application.loadedLevel);
	}

	public void Resume()
	{
//		if (GoogleMobileAdsDemoScript.bannerWasLoaded) {
//			GoogleMobileAdsDemoScript._instance.bannerView.Hide ();
//		}
		Time.timeScale = 1;
	//	Debug.Log (Time.timeScale);
		Invoke ("ForDestroy", 0.1f);


	}
	public void ForDestroy()
	{
		Destroy (gameObject);
	}

}
