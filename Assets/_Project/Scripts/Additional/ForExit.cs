using _Project.Scripts.UI_Scripts;
using UnityEngine;

namespace _Project.Scripts.Additional
{
	public class ForExit : MonoBehaviour 
	{
		private void Update () {
#if UNITY_ANDROID
			if(Input.GetKeyDown (KeyCode.Escape))
			{
				MenuManager._instance.Exitpanel();
			}
#endif
		}
	}
}
