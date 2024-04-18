using _Project.Scripts.Additional;
using _Project.Scripts.UI_Scripts;
using UnityEngine;

namespace _Project.Scripts.Other
{
	public class RadioChech : MonoBehaviour 
	{
		public GameObject radio_bttn ;
		public GameObject cup_bttn ;
		public GameObject radio_text ;
		public GameObject cupcake_text ;

		void OnEnable()
		{
			if (MenuManager.cupcakeNo <= 0) {
				cup_bttn.SetActive (true);
				cupcake_text.SetActive(false);
			} else {
				cupcake_text.SetActive(true);
				cup_bttn.SetActive (false);
			}
			if (!PlayerPrefs.HasKey ("Radio")) {
				radio_bttn.SetActive (true);
				radio_text.SetActive(false);
			} else {
				radio_text.SetActive(true);
				radio_bttn.SetActive (false);
			}
		}
		public void BuyRadio()
		{
			if (MenuManager.golds >= 20)
			{
				MenuManager.golds -= 20;
				UIManager._instance.goldText.text = MenuManager.golds.ToString ();
				PlayerPrefs.SetString ("Golds", EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
				PlayerPrefs.SetInt ("Radio", 1);
			} 
			else
			{
				UIManager._instance.EarnGold();
			}
			gameObject.SetActive (false);
		}
		public void BuyCupcake()
		{
			if (MenuManager.totalscore >= 250) 
			{
				MenuManager.totalscore -= 250;
				PlayerPrefs.SetString ("TotalScore", EncryptionHandler64.Encrypt (MenuManager.totalscore.ToString ()));

				MenuManager.cupcakeNo = 3;
				PlayerPrefs.SetString ("Cupcake", EncryptionHandler64.Encrypt (MenuManager.cupcakeNo.ToString ()));
			} 
			else 
			{
				UIManager._instance.EarnCoin() ;
			}
			gameObject.SetActive (false);
		}
	}
}
