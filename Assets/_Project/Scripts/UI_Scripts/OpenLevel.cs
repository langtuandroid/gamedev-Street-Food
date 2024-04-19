using _Project.Scripts.Additional;
using _Project.Scripts.Entities.Loader;
using _Project.Scripts.Game;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI_Scripts
{
	public class OpenLevel : MonoBehaviour 
	{
		[Inject] private MenuManager _menuManager;  
		public GameObject myTick;
		public GameObject myLock;
		public int myLevel;
		public GameObject myStar;
		int finalLevel;
		string myName;
	
		void OnEnable()
		{
			myName = MenuManager.envNo + "Stars";
			int noOfLevelsCompleted = (int)Encryption.Decrypt(PlayerPrefs.GetString(MenuManager.envNo+"Levels"));
	
			myTick.SetActive (false);

			if(myLevel <= 1000) //TODO noOfLevelsCompleted
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
		
			if (myLevel > 100000)//TODO  (noOfLevelsCompleted + 1)
			{
				transform.GetComponent<Button> ().enabled = false;
			} 
			else 
			{
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
			DontDestroyOnLoad (AllScence._instance.loader);
			switch (MenuManager.envNo)
			{
				case "US":
					finalLevel  = myLevel;
					LevelManager.levelNo = finalLevel;
					_menuManager.loader.SetActive (true);
					Loader.levelToLoad = "US_Scene";
					break;
				case "China": 
					finalLevel = 10+myLevel;
					LevelManager.levelNo = finalLevel;
					_menuManager.loader.SetActive (true);
					Loader.levelToLoad = "ChinaScene";
					break;
				case "Italy": 
					finalLevel = 20+myLevel;
					LevelManager.levelNo = finalLevel;
					_menuManager.loader.SetActive (true);
					Loader.levelToLoad = "Italy_Scene";
					break;
				case "Aus": 
					finalLevel = 30+myLevel;
					LevelManager.levelNo = finalLevel;
					_menuManager.loader.SetActive (true);
					Loader.levelToLoad = "AustraliaScene";
					break;
			}
		}
	}
}
