using UnityEngine;

namespace _Project.Scripts.Effects
{
	public class ShineEffect : MonoBehaviour 
	{
		private void OnEnable()
		{
			Invoke (nameof(ViewShine), 0.45f);
	
		}

		private void ViewShine()
		{
			gameObject.SetActive (false);
			gameObject.GetComponent<Animator> ().enabled = false;
			Invoke (nameof(HideShine), 4.0f);
		}

		private void HideShine()
		{
			gameObject.SetActive (true);
			gameObject.GetComponent<Animator> ().enabled = true;
			Invoke (nameof(ViewShine), 0.45f);
		}

	}
}
