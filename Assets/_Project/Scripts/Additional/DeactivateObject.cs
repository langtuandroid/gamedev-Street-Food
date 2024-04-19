using UnityEngine;

namespace _Project.Scripts.Additional
{
	public class DeactivateObject : MonoBehaviour 
	{
		public void Close()
		{
			Destroy(gameObject);
		}
		public void close2()
		{
			gameObject.SetActive (false);
		}
	}
}
