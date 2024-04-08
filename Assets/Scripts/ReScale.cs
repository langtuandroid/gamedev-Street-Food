using UnityEngine;
using System.Collections;

public class ReScale : MonoBehaviour {
	Transform myParent;
	public float myMaxScale , myMinScale;
	// Use this for initialization
	void Start () {
		myParent = transform.parent;
		transform.parent = null;
		float myXScale = transform.localScale.x;
//		Debug.Log("myXScale = "+myXScale); 
		if(myXScale > myMaxScale)
		{
			myXScale = myMaxScale;
		}
		else if(myXScale < myMinScale)
		{
			myXScale = myMinScale;
		}
		transform.localScale = new Vector3(myXScale,myXScale,1);
		transform.parent = myParent;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
