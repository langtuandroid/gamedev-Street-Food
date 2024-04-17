using UnityEngine;
using System.Collections;

public class ResolutionFixer : MonoBehaviour 
{
	public float myScaleX , myScaleY;
	public float myX;
	public float myY;
	public GameObject left,right , up, down;

	void Awake () 
	{
		Vector3 leftPos = Camera.main.WorldToViewportPoint (left.transform.position);
		Vector3 rightPos = Camera.main.WorldToViewportPoint (right.transform.position);

		Vector3 upPos = Camera.main.WorldToViewportPoint (up.transform.position);
		Vector3 downPos = Camera.main.WorldToViewportPoint (down.transform.position);

		float widthOfBg =	-(rightPos.x - leftPos.x);

		float heightOfBg = upPos.y - downPos.y;

		float newScaleX = myScaleX/widthOfBg;
		float newScaleY = myScaleY/heightOfBg;
		transform.localScale = new Vector3(newScaleX , newScaleY,1);
	
	}
	
}
