using UnityEngine;

namespace _Project.Scripts.Effects
{
	public class LevelShineParicle : MonoBehaviour {
		private void OnEnable()
		{
			Invoke (nameof(Activate), 0.553f);
		}

		private void Activate()
		{
			gameObject.SetActive (false);
			gameObject.GetComponent<Animator> ().enabled = false;
			Invoke (nameof(Deactivate), 4.0f);
		}

		private void Deactivate()
		{
			gameObject.SetActive (true);
			gameObject.GetComponent<Animator> ().enabled = true;
			Invoke (nameof(Activate), 0.553f);
		}
	}
}
