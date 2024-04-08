using UnityEngine;
using System.Collections;

public class ForExit : MonoBehaviour {

	public static ForExit _instance ;
	public  GameObject exit_panel ;
	public GameObject loader_panel ;
	// Use this for initialization
	void Awake()
	{
		_instance = this;
	
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
#if UNITY_ANDROID
		if(Input.GetKeyDown (KeyCode.Escape))
		{
			
			MenuManager._instance.Exitpanel();
			
		}
#endif
	}
}
