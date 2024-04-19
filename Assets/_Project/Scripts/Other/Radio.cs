using _Project.Scripts.Entities.Customers;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Other
{
	public class Radio : MonoBehaviour 
	{
		[FormerlySerializedAs("node1")] [SerializeField] private GameObject play1;
		[FormerlySerializedAs("node2")] [SerializeField] private GameObject play2;
		[FormerlySerializedAs("node3")] [SerializeField] private GameObject play3 ;
		[FormerlySerializedAs("node4")] [SerializeField] private GameObject play4 ;
		[FormerlySerializedAs("radiofront")] [SerializeField] private GameObject _frontRadio;

		private void Start () 
		{
			Wisitor._isRadioBought = false;
		}

		private void Play1()
		{
			play2.SetActive (false);
			play3.SetActive (false);
			play4.SetActive (false);
			play1.SetActive (true);

		}
		public void Play2()
		{
			play3.SetActive (false);
			play4.SetActive (false);
			play1.SetActive (false);
			play2.SetActive (true);

		}
		public void Play3()
		{
			play4.SetActive (false);
			play1.SetActive (false);
			play2.SetActive (false);
			play3.SetActive (true);

		}
		public void Play4()
		{
			play1.SetActive (false);
			play2.SetActive (false);
			play3.SetActive (false);
			play4.SetActive (true);

		}
		public void Reset()
		{
			Play1();
			Invoke (nameof(Play2), 0.671f);
			Invoke (nameof(Play3), 1.171f);
			Invoke (nameof(Play4), 2.0f);
			Invoke (nameof(Reset), 2.5f);
		}

		private void OnMouseDown()
		{
			if (!gameObject.GetComponent<AudioSource> ().isPlaying) {
				Wisitor._isRadioBought = true ;
				_frontRadio.GetComponent<Animator>().enabled = true ;

				gameObject.GetComponent<AudioSource> ().Play ();
				Play1();
				Invoke (nameof(Play2), 0.671f);
				Invoke (nameof(Play3), 1.171f);
				Invoke (nameof(Play4), 2.0f);
				Invoke (nameof(Reset), 2.5f);

			} else {
				Wisitor._isRadioBought = false ;
				gameObject.GetComponent<AudioSource> ().Stop();
				_frontRadio.GetComponent<Animator>().enabled = false ;
				play1.SetActive(false);
				play2.SetActive(false);
				play3.SetActive(false);
				play4.SetActive(false);
				CancelInvoke();
			}

		}
	}
}
