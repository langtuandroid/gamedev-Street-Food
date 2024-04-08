using UnityEngine;
using System.Collections;

public class PressBell : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseDown()
	{
		CustomerHandler._instance.PressBell ();
		gameObject.GetComponent<AudioSource> ().Play();
	}
	void OnMouseUp()
	{
		//gameObject.GetComponent<AudioSource> ().Stop();
	}
}
