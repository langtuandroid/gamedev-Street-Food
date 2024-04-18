using _Project.Scripts.Managers;
using _Project.Scripts.UI_Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Other
{
	public class Handcuff : MonoBehaviour 
	{
		public Text stolentext;
		private void OnEnable()
		{
			Invoke (nameof(Timestop), 0.9f);
			stolentext.text = Thief._instance.coinsStolen.ToString();
		}
	
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

		private void OnDisable()
		{
			Time.timeScale = 1.0f;
		}
	}
}
