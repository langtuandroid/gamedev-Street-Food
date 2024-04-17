using UnityEngine;
using System.Collections;

public class ExitPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void EXIT()
	{
		Application.Quit ();
	}
	public void Not_Exit()
	{
		Destroy (gameObject);
	}
}
