using UnityEngine;
using System.Collections;

public class Eyes : MonoBehaviour {
	int play_screen=0 ;
	float timeofmotion0 ;
	public GameObject play_eyes ;
	bool isEyeOpen ;
	public GameObject tutorial_panel ;

	private void Update () {
		if(play_screen == 0)
		{
			timeofmotion0 += Time.deltaTime;
			
			if (isEyeOpen) {
				if (timeofmotion0 > 0.2f) {
					play_eyes.SetActive (false);
					
					timeofmotion0 = 0f;
					isEyeOpen = false;
				}
			} else {
				if (timeofmotion0 > 1.2f ) 
				{
					play_eyes.SetActive (true);
					
					timeofmotion0 = 0f;
					isEyeOpen = true;
				}
			}
		}

	
	}
	public void Ontotorialpanel()
	{
		tutorial_panel.SetActive (true);
		MenuManager._instance.EnableFadePanel ();
	}
	public void Closetutorialpanel()
	{
		MenuManager._instance.EnableFadePanel ();
		tutorial_panel.SetActive (false);
	}


}
