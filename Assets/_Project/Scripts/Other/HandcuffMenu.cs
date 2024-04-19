using _Project.Scripts.Additional;
using _Project.Scripts.Entities;
using _Project.Scripts.Managers;
using _Project.Scripts.UI_Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Other
{
	public class HandcuffMenu : MonoBehaviour 
	{
		[Inject] private USController _usManager;
		[Inject] private UIManager _uiManager;   
		[FormerlySerializedAs("stolentext")] [SerializeField] private Text _textStolen;
		private void OnEnable()
		{
			Invoke (nameof(Pause), 0.9f);
			_textStolen.text = ThiefWisitor._instance.coinsStolen.ToString();
		}
	
		public void Pause()
		{
			Time.timeScale = 0f;
		}

		public void Buy()
		{
			if (MenuManager.totalscore >= 1000) {
				MenuManager.totalscore -= 1000;
				PlayerPrefs.SetString ("TotalScore", Encryption.Encrypt (MenuManager.totalscore.ToString ()));
				MenuManager.handcuffNo++;
				PlayerPrefs.SetString ("Handcuff", Encryption.Encrypt (MenuManager.handcuffNo.ToString ()));
				if(MenuManager.handcuffNo > 0)
				{
					_usManager.HandCuff.SetActive(true);
				}
				else
				{
					_usManager.HandCuff.SetActive(false);
				}
			} else {
				_uiManager.EarnCoin();
			}
			gameObject.SetActive (false);
		}

		public void Deactivate()
		{
			gameObject.SetActive (false);
		}

		private void OnDisable()
		{
			Time.timeScale = 1.0f;
		}
	}
}
