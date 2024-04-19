using _Project.Scripts.UI_Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Scripts.Other
{
	public class EyesAnimation : MonoBehaviour 
	{
		
		[Inject] private MenuManager _menuManager;  
		private int _playScreen = 0;
		private float _motionTime;
		private bool _isEyeOpen;
		[FormerlySerializedAs("tutorial_panel")] [SerializeField] private GameObject _tutorialPanel ;
		[FormerlySerializedAs("play_eyes")] [SerializeField] private GameObject _playEyes;
		private void Update () 
		{
			if(_playScreen == 0)
			{
				_motionTime += Time.deltaTime;
			
				if (_isEyeOpen) 
				{
					if (_motionTime > 0.2f) 
					{
						_playEyes.SetActive (false);
					
						_motionTime = 0f;
						_isEyeOpen = false;
					}
				} 
				else 
				{
					if (_motionTime > 1.2f ) 
					{
						_playEyes.SetActive (true);
					
						_motionTime = 0f;
						_isEyeOpen = true;
					}
				}
			}
		}
		public void OpenTutorial()
		{
			_tutorialPanel.SetActive (true);
			_menuManager.EnableFadePanel ();
		}
		public void CloseTutorial()
		{
			_menuManager.EnableFadePanel ();
			_tutorialPanel.SetActive (false);
		}
	}
}
