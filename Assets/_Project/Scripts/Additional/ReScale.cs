using UnityEngine;
using System.Collections;

public class ReScale : MonoBehaviour 
{
	Transform myParent;
	public float myMaxScale , myMinScale;
	
	void Start () {
		myParent = transform.parent;
		transform.parent = null;
		float myXScale = transform.localScale.x;
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

}
