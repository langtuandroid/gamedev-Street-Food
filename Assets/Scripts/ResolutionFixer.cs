using UnityEngine;
using System.Collections;

public class ResolutionFixer : MonoBehaviour {
	public float myScaleX , myScaleY;
	public float myX;
	public float myY;
	public GameObject left,right , up, down;
	// Use this for initialization
	void Awake () {

//		GameObject g = new GameObject("left");
//		g.transform.localPosition = Camera.main.ViewportToWorldPoint (new Vector3(1,0,0));
//		g.transform.parent = this.transform;
//
//		GameObject g1 = new GameObject("right");
//		g1.transform.localPosition = Camera.main.ViewportToWorldPoint (new Vector3(0,0,0));
//		g1.transform.parent = this.transform;
//
//		float width = g.transform.localPosition.x - g1.transform.localPosition.x;
//		Debug.Log("width = "+width);
//		float newScale = width/6.666667f;


		Vector3 leftPos = Camera.main.WorldToViewportPoint (left.transform.position);
		Vector3 rightPos = Camera.main.WorldToViewportPoint (right.transform.position);

		Vector3 upPos = Camera.main.WorldToViewportPoint (up.transform.position);
		Vector3 downPos = Camera.main.WorldToViewportPoint (down.transform.position);

		float widthOfBg =	-(rightPos.x - leftPos.x);

		float heightOfBg = upPos.y - downPos.y;
//		Debug.Log("widthOfBg = "+widthOfBg);
	//	Debug.Log("heightOfBg = "+heightOfBg);

		float newScaleX = myScaleX/widthOfBg;
		float newScaleY = myScaleY/heightOfBg;
		transform.localScale = new Vector3(newScaleX , newScaleY,1);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
