using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Game
{
	public class ScenesController : MonoBehaviour 
	{
		public static ScenesController _instance ;
		[FormerlySerializedAs("loader")] public GameObject _loader ;
		private void Awake()
		{
			_instance = this;
			DontDestroyOnLoad (_loader);
		}
	
	}
}
