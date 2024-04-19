using _Project.Scripts.Entities.Customers;
using UnityEngine;

namespace _Project.Scripts.Additional
{
	public class BellTouch : MonoBehaviour 
	{
		private void OnMouseDown()
		{
			CustomerHandler._instance.PressBell ();
			gameObject.GetComponent<AudioSource> ().Play();
		}
	}
}
