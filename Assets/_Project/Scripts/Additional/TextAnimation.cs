using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Additional
{
	public class TextAnimation : MonoBehaviour 
	{
		[FormerlySerializedAs("hasTweenScale")] [SerializeField] private bool _isHasScale;
		private void OnEnable()
		{
			if(_isHasScale)
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
			StartCoroutine (Destroy());
		}

		private IEnumerator Destroy()
		{
			yield return new WaitForSeconds(2.0f);
			gameObject.SetActive (false);
		}
	}
}
