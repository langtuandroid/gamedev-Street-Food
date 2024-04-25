using _Project.Scripts.Additional;
using _Project.Scripts.Entities.Loader;
using _Project.Scripts.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI_Scripts
{
	public class OpenLevel : MonoBehaviour 
	{
		[Inject] private MenuManager _menuManager;
		[SerializeField] private TMP_Text _levelNum;
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
			_levelNum.text = myLevel.ToString();
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
		
			if (myLevel > noOfLevelsCompleted + 1)
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
			ScenesController._instance._loader.SetActive (true);
			DontDestroyOnLoad (ScenesController._instance._loader);
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
