using UnityEngine;
using System.Collections;
using UnityEngine.UI ;
public class Loader : MonoBehaviour {


	public static Loader _instance ;
	int rand ;
	public Text loader_text ;
	public Slider loader;
	bool started=true ;
	public static string levelToLoad;
	bool a=false;
	// Use this for initialization
	void Awake()
	{
		_instance = this;
		DontDestroyOnLoad (gameObject);
	}

	AsyncOperation asyncOp;

	void  OnEnable()
	{

		started = true;
		loader.value = 0.01f;
		rand = Random.Range (0, 8);
		if (rand == 0) {
			loader_text.text = "Use handcuffs to catch thieves";
		}
		else if (rand == 1) {
			loader_text.text = "Use the bell to call more customers.";
		}
		else if (rand == 2) {
			loader_text.text = "The customer waits longer when radio is switched on";
		}
		else if (rand == 3) {
			loader_text.text = "The whistle blows when the customer leaves without paying.";
		}
		else if (rand == 4) {
			loader_text.text = "Buy upgrades to increase the equipments capacity.";
		}
		else if (rand == 5) {
			loader_text.text = "The cupcake refills the customer waiting bar.";
		}
		else if (rand == 6) {
			loader_text.text = "Purchase gold to buy more upgrades.";
		}
		else if (rand == 7) {
			loader_text.text = "Better decorated stall fetches higher bonus.";
		}
	  if (a) 
		{
		//	asyncOp = Application.LoadLevelAsync (levelToLoad);

			//yield return asyncOp;
	
		}


	}


	// Update is called once per frame
	void Update () {

		loader.value += 0.009f ;
	   if (loader.value >= 0.9f) {
			a= true;
			Application.LoadLevel(levelToLoad);
		}
	//	if (asyncOp != null)
		{

		//	if(!asyncOp.isDone)
			{
				//loader.value  = asyncOp.progress/0.9f;
				//loader.value = asyncOp.progress ;

			
			}
//
//			//Debug.Log("loader"+loader.value);
//			if(loader.value >= 1f)
//			{
//				//gameObject.SetActive(false);
//				//started = false ;
//
//			}
		}
	    
	}
}
