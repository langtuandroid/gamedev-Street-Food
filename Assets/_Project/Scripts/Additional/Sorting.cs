using UnityEngine;

namespace _Project.Scripts.Additional
{
	public class Sorting : MonoBehaviour {
		private void Start () 
		{
			GetComponent<MeshRenderer>().sortingOrder = 23;
		}
	}
}
