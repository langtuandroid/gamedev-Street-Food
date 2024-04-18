using UnityEngine;

namespace _Project.Scripts.Other
{
	public class Radio : MonoBehaviour {
	
		public GameObject node1;
		public GameObject node2;
		public GameObject node3 ;
		public GameObject node4 ;
		public GameObject radiofront;

		private void Start () 
		{
			Customer.radioPurchased = false;
		}

		private void Nodef1()
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
			Invoke (nameof(Nodef2), 0.671f);
			Invoke (nameof(Nodef3), 1.171f);
			Invoke (nameof(Nodef4), 2.0f);
			Invoke (nameof(Restart), 2.5f);
		}

		private void OnMouseDown()
		{
			if (!gameObject.GetComponent<AudioSource> ().isPlaying) {
				Customer.radioPurchased = true ;
				radiofront.GetComponent<Animator>().enabled = true ;

				gameObject.GetComponent<AudioSource> ().Play ();
				Nodef1();
				Invoke (nameof(Nodef2), 0.671f);
				Invoke (nameof(Nodef3), 1.171f);
				Invoke (nameof(Nodef4), 2.0f);
				Invoke (nameof(Restart), 2.5f);

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
}
