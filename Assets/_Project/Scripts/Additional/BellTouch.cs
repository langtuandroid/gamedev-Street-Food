using _Project.Scripts.Entities.Customers;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Additional
{
	public class BellTouch : MonoBehaviour 
	{
		[Inject] private WisitorHandler _customerHandler;
		private void OnMouseDown()
		{
			_customerHandler.BellPress ();
			gameObject.GetComponent<AudioSource> ().Play();
		}
	}
}
