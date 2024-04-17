using UnityEngine;


public class Belltry : MonoBehaviour {

	public int a ;
	
	void OnEnable()
	{
		Invoke ("Timestop", 0.9f);
	
	}
	public void ActiveBell()
	{
		US_Manager._instance.Bell.SetActive (true);
		CustomerHandler._instance.stop = false ;
		gameObject.SetActive (false);
		PlayerPrefs.SetInt ("try", 2);
	}

	public void Buyhandcuff()
	{
		if (MenuManager.totalscore >= 1000) {
			MenuManager.totalscore -= 1000;
			PlayerPrefs.SetString ("TotalScore", EncryptionHandler64.Encrypt (MenuManager.totalscore.ToString ()));
		    MenuManager.handcuffNo++;
			PlayerPrefs.SetString ("Handcuff", EncryptionHandler64.Encrypt (MenuManager.handcuffNo.ToString ()));
			Application.LoadLevel (Application.loadedLevel);
		} else {
			UIManager._instance.EarnCoin();
		}
		gameObject.SetActive (false);

	}
	public void BuyWhistle()
	{
		if (MenuManager.golds >= 20) {
			MenuManager.golds -= 20;
			PlayerPrefs.SetString ("Golds", EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
			PlayerPrefs.SetInt ("Whistle", 1);
			Application.LoadLevel (Application.loadedLevel);
		} else {
			UIManager._instance.EarnGold();
		}
	}
	public void BuyBell()
	{
		if (MenuManager.golds >= 30) {
			MenuManager.golds -= 30;
			PlayerPrefs.SetString ("Golds", EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
			UIManager._instance.goldText.text = MenuManager.golds.ToString ();
			PlayerPrefs.SetString ("Golds", EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
			PlayerPrefs.SetInt ("Bell", 1);
			US_Manager._instance.Bell.SetActive(true);
			CustomerHandler._instance.stop = false ;
		} else
		{
			UIManager._instance.EarnGold();
		}
		gameObject.SetActive (false);
	}
	void OnDisable()
	{
		Time.timeScale = 1f;
	}
	public void SetFalse()
	{
	gameObject.SetActive (false);
	}
	public void IAP()
	{
		gameObject.SetActive (false);
		UIManager._instance.IAPGold ();
	}
	public void PlayON()
	{
		{
			gameObject.SetActive(false);
		}
	}
	public void Timestop()
	{
		Time.timeScale = 0f ;
	}
	public void GetCoins()
	{

		gameObject.SetActive (false);
	}

}
