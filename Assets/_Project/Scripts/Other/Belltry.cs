using _Project.Scripts.Additional;
using _Project.Scripts.Entities.Customers;
using _Project.Scripts.Managers;
using _Project.Scripts.UI_Scripts;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Other
{
	public class Belltry : MonoBehaviour 
	{
		[Inject] private WisitorHandler _customerHandler;
		[Inject] private USController _usManager;
		[Inject] private UIManager _uiManager;   
		private void OnEnable()
		{
			Invoke (nameof(Timestop), 0.9f);
	
		}
		public void ActiveBell()
		{
			_usManager.BellObject.SetActive (true);
			_customerHandler.stop = false ;
			gameObject.SetActive (false);
			PlayerPrefs.SetInt ("try", 2);
		}

		public void Buyhandcuff()
		{
			if (MenuManager.totalscore >= 1000) {
				MenuManager.totalscore -= 1000;
				PlayerPrefs.SetString ("TotalScore", Encryption.Encrypt (MenuManager.totalscore.ToString ()));
				MenuManager.handcuffNo++;
				PlayerPrefs.SetString ("Handcuff", Encryption.Encrypt (MenuManager.handcuffNo.ToString ()));
				Application.LoadLevel (Application.loadedLevel);
			} else {
				_uiManager.EarnCoin();
			}
			gameObject.SetActive (false);

		}
		public void BuyWhistle()
		{
			if (MenuManager.golds >= 20) {
				MenuManager.golds -= 20;
				PlayerPrefs.SetString ("Golds", Encryption.Encrypt (MenuManager.golds.ToString ()));
				PlayerPrefs.SetInt ("Whistle", 1);
				Application.LoadLevel (Application.loadedLevel);
			} else {
				_uiManager.EarnGold();
			}
		}
		public void BuyBell()
		{
			if (MenuManager.golds >= 30) {
				MenuManager.golds -= 30;
				PlayerPrefs.SetString ("Golds", Encryption.Encrypt (MenuManager.golds.ToString ()));
				_uiManager.goldText.text = MenuManager.golds.ToString ();
				PlayerPrefs.SetString ("Golds", Encryption.Encrypt (MenuManager.golds.ToString ()));
				PlayerPrefs.SetInt ("Bell", 1);
				_usManager.BellObject.SetActive(true);
				_customerHandler.stop = false ;
			} else
			{
				_uiManager.EarnGold();
			}
			gameObject.SetActive (false);
		}

		private void OnDisable()
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
			_uiManager.IAPGold ();
		}
		public void PlayON()
		{
			gameObject.SetActive(false);
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
}
