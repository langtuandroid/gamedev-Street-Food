using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OpenLevel : MonoBehaviour {

	public GameObject myTick;

	public GameObject myLock;

	public int myLevel;

	public GameObject myStar;

	int finalLevel;

	string myName;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable()
	{

		myName = MenuManager.envNo+"Stars";
	//	LevelMade();
		int noOfLevelsCompleted = (int)EncryptionHandler64.Decrypt(PlayerPrefs.GetString(MenuManager.envNo+"Levels"));
	
		myTick.SetActive (false);

		if(myLevel <= noOfLevelsCompleted)
		{
			myTick.SetActive (true);
			myLock.SetActive (false);
		}
		else if(myLevel != ((noOfLevelsCompleted+1)))
		{

			myLock.SetActive (true);
			myTick.SetActive (false);
		}
		else if(myLevel == (noOfLevelsCompleted +1))
		{
			myLock.SetActive (false);
		}
		if (myLevel > (noOfLevelsCompleted + 1)) {
			transform.GetComponent<Button> ().enabled = false;
		} else {
			transform.GetComponent<Button> ().enabled = true;
		}
	

		if(PlayerPrefs.HasKey (myName+""+myLevel))
		{
			myStar.SetActive (true);
		}
		else
		{
			myStar.SetActive (false);
		}


		
	}

	public void SelectLevel()
	{

	
		AllScence._instance.loader.SetActive (true);

//	    Application.LoadLevelAsync("MyBigLevel");
			
			
		DontDestroyOnLoad (AllScence._instance.loader);
		switch (MenuManager.envNo)
		{
		case "US":
			//Debug.Log("select us");
			finalLevel  = myLevel;
			LevelManager.levelNo = finalLevel;

				MenuManager._instance.loader.SetActive (true);
			Loader.levelToLoad = "US_Scene";
			//Application.LoadLevel (1);
			break;
		case "China": 
			//Debug.Log("select china");
			finalLevel = 10+myLevel;
			LevelManager.levelNo = finalLevel;

			MenuManager._instance.loader.SetActive (true);
			Loader.levelToLoad = "ChinaScene";
			//Application.LoadLevel (2);
			break;
		case "Italy": 
			//Debug.Log("select italy");
			finalLevel = 20+myLevel;
			LevelManager.levelNo = finalLevel;

			MenuManager._instance.loader.SetActive (true);
			Loader.levelToLoad = "Italy_Scene";
			//Application.LoadLevel (3);
			break;
		case "Aus": 
			//Debug.Log("select aus");
			finalLevel = 30+myLevel;
			LevelManager.levelNo = finalLevel;

			MenuManager._instance.loader.SetActive (true);
			Loader.levelToLoad = "AustraliaScene";
			//Application.LoadLevel (4);
			break;
		}
	}

//	void LevelMade()
//	{
//		switch (MenuManager.envNo)
//		{
//		case "US":
//			myName = "USStars";
//			finalLevel  = myLevel;
//			break;
//		case "Japan": 
//			finalLevel = 10+myLevel;
//			break;
//		case "Italy": 
//			finalLevel = 20+myLevel;
//			break;
//		case "Aus": 
//			finalLevel = 30+myLevel;
//			break;
//		}
//	}


}
