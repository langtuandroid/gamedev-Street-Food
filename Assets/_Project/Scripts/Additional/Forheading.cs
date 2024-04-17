using UnityEngine;
using System.Collections;

public class Forheading : MonoBehaviour {
	
	void OnEnable()
	{
		Invoke ("deactive", 3.0f);
	}
	public void deactive()
	{
		gameObject.SetActive (false);
	}
}
