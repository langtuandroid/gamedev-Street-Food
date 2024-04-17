using UnityEngine;
using System.Collections;

public class Setfalse : MonoBehaviour 
{
	public void Close()
	{
		Destroy (gameObject);
	}
	public void close2()
	{
		gameObject.SetActive (false);
	}
}
