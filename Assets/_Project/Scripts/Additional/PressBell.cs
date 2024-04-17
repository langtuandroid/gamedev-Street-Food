using UnityEngine;
using System.Collections;

public class PressBell : MonoBehaviour 
{
	
	void OnMouseDown()
	{
		CustomerHandler._instance.PressBell ();
		gameObject.GetComponent<AudioSource> ().Play();
	}
}
