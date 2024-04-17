using UnityEngine;
using System.Collections;

public class RotateSelection : MonoBehaviour {
	
	void Update () 
	{
		transform.localEulerAngles = new Vector3(0,0,transform.localEulerAngles.z+Time.deltaTime*40f);
	}
}
