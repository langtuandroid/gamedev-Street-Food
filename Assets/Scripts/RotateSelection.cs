using UnityEngine;
using System.Collections;

public class RotateSelection : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.localEulerAngles = new Vector3(0,0,transform.localEulerAngles.z+Time.deltaTime*40f);
	}
}
