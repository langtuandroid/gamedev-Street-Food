using UnityEngine;
using System.Collections;

public class Radio : MonoBehaviour {


	public GameObject radio_Effect;


	public GameObject node1;
	public GameObject node2;
	public GameObject node3 ;
	public GameObject node4 ;
	public GameObject radiofront; 
	// Use this for initialization
	void Start () {
		Customer.radioPurchased = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Nodef1()
	{
		node2.SetActive (false);
		node3.SetActive (false);
		node4.SetActive (false);
		node1.SetActive (true);

	}
	public void Nodef2()
	{
		node3.SetActive (false);
		node4.SetActive (false);
		node1.SetActive (false);
		node2.SetActive (true);

	}
	public void Nodef3()
	{
		node4.SetActive (false);
		node1.SetActive (false);
		node2.SetActive (false);
		node3.SetActive (true);

	}
	public void Nodef4()
	{
		node1.SetActive (false);
		node2.SetActive (false);
		node3.SetActive (false);
		node4.SetActive (true);

	}
	public void Restart()
	{
		Nodef1();
		Invoke ("Nodef2", 0.671f);
		Invoke ("Nodef3", 1.171f);
		Invoke ("Nodef4", 2.0f);
		Invoke ("Restart", 2.5f);
	}
	void OnMouseDown()
	{
		if (!gameObject.GetComponent<AudioSource> ().isPlaying) {
			Customer.radioPurchased = true ;
			radiofront.GetComponent<Animator>().enabled = true ;

			gameObject.GetComponent<AudioSource> ().Play ();
			Nodef1();
			Invoke ("Nodef2", 0.671f);
			Invoke ("Nodef3", 1.171f);
			Invoke ("Nodef4", 2.0f);
			Invoke ("Restart", 2.5f);

		} else {
			Customer.radioPurchased = false ;
			gameObject.GetComponent<AudioSource> ().Stop();
			radiofront.GetComponent<Animator>().enabled = false ;
			node1.SetActive(false);
			node2.SetActive(false);
			node3.SetActive(false);
			node4.SetActive(false);
			CancelInvoke();
		}

	}
}
