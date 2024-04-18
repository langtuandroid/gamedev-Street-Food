using UnityEngine;

namespace _Project.Scripts.Game
{
	public class AllScence : MonoBehaviour 
	{
		public static AllScence _instance ;
		public GameObject loader ;
		private void Awake()
		{
			_instance = this;
			DontDestroyOnLoad (loader);
		}
	
	}
}
