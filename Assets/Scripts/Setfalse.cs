using UnityEngine;
using System.Collections;

public class Setfalse : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Close()
	{
		Destroy (gameObject);
	}
	public void close2()
	{
		gameObject.SetActive (false);
	}
}
