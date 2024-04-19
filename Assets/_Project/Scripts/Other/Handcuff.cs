using _Project.Scripts.Additional;
using _Project.Scripts.Entities;
using _Project.Scripts.Managers;
using _Project.Scripts.UI_Scripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Other
{
	public class Handcuff : MonoBehaviour 
	{
		[Inject] private US_Manager _usManager;
		[Inject] private UIManager _uiManager;   
		public Text stolentext;
		private void OnEnable()
		{
			Invoke (nameof(Timestop), 0.9f);
			stolentext.text = ThiefWisitor._instance.coinsStolen.ToString();
		}
	
		public void Timestop()
		{
			Time.timeScale = 0f;
		}

		public void Buyhandcuff()
		{
			if (MenuManager.totalscore >= 1000) {
				MenuManager.totalscore -= 1000;
				PlayerPrefs.SetString ("TotalScore", Encryption.Encrypt (MenuManager.totalscore.ToString ()));
				MenuManager.handcuffNo++;
				PlayerPrefs.SetString ("Handcuff", Encryption.Encrypt (MenuManager.handcuffNo.ToString ()));
				if(MenuManager.handcuffNo > 0)
				{
					_usManager.handcuff.SetActive(true);
				}
				else
				{
					_usManager.handcuff.SetActive(false);
				}
			} else {
				_uiManager.EarnCoin();
			}
			gameObject.SetActive (false);
		}

		public void Cross()
		{
			gameObject.SetActive (false);
		}

		private void OnDisable()
		{
			Time.timeScale = 1.0f;
		}
	}
}
