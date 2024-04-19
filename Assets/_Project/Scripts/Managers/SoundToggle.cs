using _Project.Scripts.UI_Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Scripts.Managers
{
	public class SoundToggle : MonoBehaviour 
	{
		[Inject] private MenuManager _menuManager;  
		[FormerlySerializedAs("SoundON")] public GameObject _onObject ;
		[FormerlySerializedAs("SoundOFF")] public GameObject _offObject ;

		private void Start () {

			if (PlayerPrefs.GetInt ("SOUNDON") == 1)
			{
				AudioListener.volume = 0;
				_offObject.SetActive (true);
				_onObject.SetActive (false);
			}
			if(PlayerPrefs.GetInt ("SOUNDON") == 0)
			{
				AudioListener.volume = 1;
				_offObject.SetActive (false);
				_onObject.SetActive (true);
			}
		}

		private void OnEnable()
		{
			if (PlayerPrefs.GetInt ("SOUNDON") == 1)
			{
				AudioListener.volume = 0;
				_offObject.SetActive (true);
				_onObject.SetActive (false);
			}
			if(PlayerPrefs.GetInt ("SOUNDON") == 0)
			{
				AudioListener.volume = 1;
				_offObject.SetActive (false);
				_onObject.SetActive (true);
			}
		}

		private void OnDisable()
		{
			if (PlayerPrefs.GetInt ("SOUNDON") == 1)
			{
				AudioListener.volume = 0;
				_offObject.SetActive (true);
				_onObject.SetActive (false);
			}
			if(PlayerPrefs.GetInt ("SOUNDON") == 0)
			{
				AudioListener.volume = 1;
				_offObject.SetActive (false);
				_onObject.SetActive (true);
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
			_offObject.SetActive (true);
			_onObject.SetActive (false);
		}

		public void ButtonsOff()
		{
			_offObject.SetActive (false);
			_onObject.SetActive (true);
		}
	}
}
