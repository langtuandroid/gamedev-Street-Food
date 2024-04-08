using UnityEngine;
using System.Collections;

public class DecativeUpgradepopup : MonoBehaviour {

	public GameObject panel ;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Close()
	{
		panel.SetActive (false);
	}
}
