using System.Collections;
using UnityEngine;

namespace _Project.Scripts.Additional
{
	public class PerfectText : MonoBehaviour 
	{
		[SerializeField] private bool hasTweenScale;

		private void OnEnable()
		{
			if(hasTweenScale)
			{
				transform.GetComponent<TweenScale>().enabled = true;
				transform.GetComponent<TweenScale>().ResetToBeginning ();
				transform.GetComponent<TweenScale>().PlayForward ();

			}
			else
			{
				transform.GetComponent<TweenPosition>().enabled = true;
				transform.GetComponent<TweenPosition>().ResetToBeginning ();
				transform.GetComponent<TweenPosition>().PlayForward ();
			}
			StartCoroutine (PerfectOff());
		}

		private IEnumerator PerfectOff()
		{
			yield return new WaitForSeconds(2.0f);
			gameObject.SetActive (false);
		}
	}
}
