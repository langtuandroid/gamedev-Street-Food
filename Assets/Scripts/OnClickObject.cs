using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnClickObject : MonoBehaviour {
	public List<EventDelegate> onClick = new List<EventDelegate>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
	
		EventDelegate.Execute(onClick);
	}
}
