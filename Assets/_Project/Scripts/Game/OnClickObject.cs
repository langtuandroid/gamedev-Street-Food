using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnClickObject : MonoBehaviour 
{
	public List<EventDelegate> onClick = new List<EventDelegate>();
	void OnMouseDown()
	{
	
		EventDelegate.Execute(onClick);
	}
}
