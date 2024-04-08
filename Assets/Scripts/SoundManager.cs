using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {


	public GameObject SoundON ;
	public GameObject SoundOFF ;

	// Use this for initialization
	void Start () {

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
	
	// Update is called once per frame
	void Update () {

	}
	void OnEnable()
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
	void OnDisable()
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
		MenuManager._instance.menuSoundButton.buttonsOn ();
	}
	public void OnSoundOFFClick()
	{
		PlayerPrefs.SetInt("SOUNDON",0);
		AudioListener.volume = 1;
		ButtonsOff ();
		MenuManager._instance.menuSoundButton.ButtonsOff ();
		
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
