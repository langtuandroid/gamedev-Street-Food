using UnityEngine;

namespace _Project.Scripts.Additional
{
	public class Forheading : MonoBehaviour {
	
		void OnEnable()
		{
			Invoke (nameof(deactive), 3.0f);
		}
		public void deactive()
		{
			gameObject.SetActive (false);
		}
	}
}
