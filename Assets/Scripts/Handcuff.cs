using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Handcuff : MonoBehaviour {

	public Text stolentext;
	// Use this for initialization
	void Start () {
	     
	}
	void OnEnable()
	{
		Invoke ("Timestop", 0.9f);
		stolentext.text = Thief._instance.coinsStolen.ToString();
	}
	// Update is called once per frame
//	void Update () {
//	
//	}

	public void Timestop()
	{
		Time.timeScale = 0f;
	}

	public void Buyhandcuff()
	{
		if (MenuManager.totalscore >= 1000) {
			MenuManager.totalscore -= 1000;
			PlayerPrefs.SetString ("TotalScore", EncryptionHandler64.Encrypt (MenuManager.totalscore.ToString ()));
			MenuManager.handcuffNo++;
			PlayerPrefs.SetString ("Handcuff", EncryptionHandler64.Encrypt (MenuManager.handcuffNo.ToString ()));
			if(MenuManager.handcuffNo > 0)
			{
				US_Manager._instance.handcuff.SetActive(true);
			}
			else
			{
				US_Manager._instance.handcuff.SetActive(false);
			}
		} else {
			UIManager._instance.EarnCoin();
		}
		gameObject.SetActive (false);
	}

	public void Cross()
	{
		gameObject.SetActive (false);
	}
	void OnDisable()
	{
		Time.timeScale = 1.0f;
	}
}
