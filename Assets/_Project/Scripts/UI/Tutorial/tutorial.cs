using UnityEngine;
using System.Collections;

public class tutorial : MonoBehaviour {

	public GameObject tutorialpanel;
	public GameObject image1;
	public GameObject image2 ;
	public GameObject tutorial_hand;
	public GameObject price_text;
	public GameObject select_bttn_text ;
	public GameObject i_bttn ;
	// Use this for initialization
	void Start () {

		//Invoke ("Stop", 6.5f);
	}

	void Stop()
	{
		//i_bttn.GetComponent<Animator> ().enabled = false;
	}
	// Update is called once per frame
	void Update () {
	
	}

	public void Tutorial_panel()
	{
		PlayerPrefs.SetInt ("Stop", 2);
		i_bttn.GetComponent<Animator> ().enabled = false;
		MenuManager._instance.EnableFadePanel ();
		tutorialpanel.SetActive (true);
		tutorial_hand.GetComponent<Animator> ().enabled = true;
		Invoke ("Ontutorialstart",1.3f);
		Invoke ("ONtext",2.5f);
	}

	void Ontutorialstart()
	{
		image1.SetActive (false);
		image2.SetActive (true);
	}
	void ONtext()
	{
		price_text.SetActive (false);
		select_bttn_text.SetActive (true);
		Invoke ("Ontutorialend",0.6f);
		Invoke ("ONtextend",1.9f);
	}
	void Ontutorialend()
	{
		image1.SetActive (true);
		image2.SetActive (false);
	}

	void ONtextend()
	{
		price_text.SetActive (true);
		select_bttn_text.SetActive (false);
		Invoke ("Ontutorialstart",1.3f);
		Invoke ("ONtext",2.5f);
	}









}
