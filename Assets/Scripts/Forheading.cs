using UnityEngine;
using System.Collections;

public class Forheading : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnEnable()
	{
		Invoke ("deactive", 3.0f);
	}
	public void deactive()
	{
		gameObject.SetActive (false);
	}
}
