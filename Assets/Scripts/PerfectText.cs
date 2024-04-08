using UnityEngine;
using System.Collections;

public class PerfectText : MonoBehaviour {
	public bool hasTweenScale;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable()
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

	IEnumerator PerfectOff()
	{
		yield return new WaitForSeconds(2.0f);
		gameObject.SetActive (false);
	}
}
