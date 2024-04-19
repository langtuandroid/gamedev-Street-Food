using UnityEngine;

namespace _Project.Scripts.Additional
{
	public class SortOrder : MonoBehaviour {
		private void Start() 
		{
			GetComponent<MeshRenderer>().sortingOrder = 23;
		}
	}
}
