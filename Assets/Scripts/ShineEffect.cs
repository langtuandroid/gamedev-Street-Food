using UnityEngine;
using System.Collections;

public class ShineEffect : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//gameObject.GetComponent<Animator> ().enabled = true;

	
	}
	void OnEnable()
	{
		//gameObject.SetActive (false);
		Invoke ("ViewShine", 0.45f);
	
	}
	void ViewShine()
	{
		gameObject.SetActive (false);
		gameObject.GetComponent<Animator> ().enabled = false;
		Invoke ("HideShine", 4.0f);
	}
	void HideShine()
	{
		gameObject.SetActive (true);
		gameObject.GetComponent<Animator> ().enabled = true;
		Invoke ("ViewShine", 0.45f);
	}

}
