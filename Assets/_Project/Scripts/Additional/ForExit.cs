using _Project.Scripts.UI_Scripts;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Additional
{
	public class ForExit : MonoBehaviour 
	{
		[Inject] private MenuManager _menuManager;  
		private void Update () {
#if UNITY_ANDROID
			if(Input.GetKeyDown (KeyCode.Escape))
			{
				_menuManager.Exitpanel();
			}
#endif
		}
	}
}
