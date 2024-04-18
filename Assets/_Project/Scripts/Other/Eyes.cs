using _Project.Scripts.UI_Scripts;
using UnityEngine;

namespace _Project.Scripts.Other
{
	public class Eyes : MonoBehaviour 
	{
		private int play_screen = 0;
		private float timeofmotion0;
		private bool isEyeOpen;
		public GameObject tutorial_panel ;
		public GameObject play_eyes;
		private void Update () 
		{
			if(play_screen == 0)
			{
				timeofmotion0 += Time.deltaTime;
			
				if (isEyeOpen) 
				{
					if (timeofmotion0 > 0.2f) 
					{
						play_eyes.SetActive (false);
					
						timeofmotion0 = 0f;
						isEyeOpen = false;
					}
				} 
				else 
				{
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
}
