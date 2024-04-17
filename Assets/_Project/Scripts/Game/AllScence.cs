using UnityEngine;
using System.Collections;

public class AllScence : MonoBehaviour {

	public static AllScence _instance ;
	public GameObject loader ;

	void Awake()
	{
		_instance = this;
		DontDestroyOnLoad (loader);
	}
	
}
