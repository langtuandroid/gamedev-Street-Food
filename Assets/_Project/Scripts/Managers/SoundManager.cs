using _Project.Scripts.UI_Scripts;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Managers
{
	public class SoundManager : MonoBehaviour 
	{
		[Inject] private MenuManager _menuManager;  
		public GameObject SoundON ;
		public GameObject SoundOFF ;

		private void Start () {

			if (PlayerPrefs.GetInt ("SOUNDON") == 1)
			{
				AudioListener.volume = 0;
				SoundOFF.SetActive (true);
				SoundON.SetActive (false);
			}
			if(PlayerPrefs.GetInt ("SOUNDON") == 0)
			{
				AudioListener.volume = 1;
				SoundOFF.SetActive (false);
				SoundON.SetActive (true);
			}
		}

		private void OnEnable()
		{
			if (PlayerPrefs.GetInt ("SOUNDON") == 1)
			{
				AudioListener.volume = 0;
				SoundOFF.SetActive (true);
				SoundON.SetActive (false);
			}
			if(PlayerPrefs.GetInt ("SOUNDON") == 0)
			{
				AudioListener.volume = 1;
				SoundOFF.SetActive (false);
				SoundON.SetActive (true);
			}
		}

		private void OnDisable()
		{
			if (PlayerPrefs.GetInt ("SOUNDON") == 1)
			{
				AudioListener.volume = 0;
				SoundOFF.SetActive (true);
				SoundON.SetActive (false);
			}
			if(PlayerPrefs.GetInt ("SOUNDON") == 0)
			{
				AudioListener.volume = 1;
				SoundOFF.SetActive (false);
				SoundON.SetActive (true);
			}
		}

		public void OnSoundONClick()
		{
			PlayerPrefs.SetInt("SOUNDON",1);
			AudioListener.volume = 0;
			buttonsOn ();
			_menuManager.menuSoundButton.buttonsOn ();
		}
		public void OnSoundOFFClick()
		{
			PlayerPrefs.SetInt("SOUNDON",0);
			AudioListener.volume = 1;
			ButtonsOff ();
			_menuManager.menuSoundButton.ButtonsOff ();
		
		}

		public void buttonsOn()
		{
			SoundOFF.SetActive (true);
			SoundON.SetActive (false);
		}

		public void ButtonsOff()
		{
			SoundOFF.SetActive (false);
			SoundON.SetActive (true);
		}
	}
}
