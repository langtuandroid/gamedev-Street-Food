using UnityEngine;
using System.Collections;

public class Sorting : MonoBehaviour {
	private void Start () 
	{
		GetComponent<MeshRenderer>().sortingOrder = 23;
	}
}
