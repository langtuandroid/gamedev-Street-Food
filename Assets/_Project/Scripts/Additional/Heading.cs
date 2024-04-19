using UnityEngine;

namespace _Project.Scripts.Additional
{
	public class Heading : MonoBehaviour {
		private void OnEnable()
		{
			Invoke (nameof(Deactivate), 3.0f);
		}
		public void Deactivate()
		{
			gameObject.SetActive (false);
		}
	}
}
