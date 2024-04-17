using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AutoType : MonoBehaviour {

	public bool isGoalAchieved;
	public float letterPause = 0.03f;
	string message;
	public Text myText;
	public GameObject imageToDeactivate;
	int noOfExtraPopup;

	void Start () {
		if(!isGoalAchieved)
		{
			message = "level goal: $"+LevelManager._instance.targetScore[LevelManager.levelNo];
			myText.text = "";
			if (PlayerPrefs.GetInt ("BellsTut") == 1)
			{
				noOfExtraPopup++;
			}
			if(PlayerPrefs.GetInt ("WhistlesTut") == 1)
			{
				noOfExtraPopup++;
			}
			if(PlayerPrefs.GetInt ("cupcakeTut") == 1)
			{
				noOfExtraPopup++;
			}
			if(PlayerPrefs.GetInt ("radioTut") == 1)
			{
				noOfExtraPopup++;
			}

		}
	}
	
	public IEnumerator TypeText () {
		if(isGoalAchieved)
		{
			message = "goal met!";
		}
		else
		{
			TutorialPanel.popupPanelActive = true;
		}
		foreach (char letter in message.ToCharArray()) {
			myText.text += letter;
			yield return 0;
			yield return new WaitForSeconds (letterPause);
		}      
		yield return new WaitForSeconds(1.0f);
		if(!isGoalAchieved)
		{
			imageToDeactivate.SetActive (false);
			if(LevelManager.levelNo == 1)
			{
				US_Manager._instance.clickfirstBun = true;
				UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
				UIManager._instance.tutorialPanelBg.OpenPopup ("TAP BUN \n TO PUT ON THE PLATE.",false,false , 0);
			}
			else if(LevelManager.levelNo == 2)
			{
				UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
				UIManager._instance.tutorialPanelBg.OpenPopup ("TWO NEW ITEMS \n HAVE BEEN ADDED.",false,false , 7 , 1);
			}
			else if(LevelManager.levelNo == 3)
			{
				UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
				UIManager._instance.tutorialPanelBg.OpenPopup ("A NEW ITEM \n HAS BEEN ADDED.",false,true , 8 , 1);
			}
			else if(LevelManager.levelNo == 11)
			{

				China_Manager._instance.clickPlateTut = true;
				UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
				UIManager._instance.tutorialPanelBg.OpenPopupChina ("TAP TO \n PLACE THE PLATE.",false,false , 9);
			}
			else if(LevelManager.levelNo == 13)
			{
				China_Manager._instance.clickBowlTut = true;
				UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
				UIManager._instance.tutorialPanelBg.OpenPopupChina ("CLICK BOWL TO\n PUT ON TRAY.",false,false , 10);
			}
			else if(LevelManager.levelNo == 21)
			{
				Italy_Manager._instance.clickfirstBase = true;
				UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
				UIManager._instance.tutorialPanelBg.OpenPopupItaly ("CLICK PIZZA\n BASE TO PUT ON \n THE PLATE",false,false , 0);
			}
			else if(LevelManager.levelNo == 22)
			{
				UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
				UIManager._instance.tutorialPanelBg.OpenPopupItaly ("NON VEG TOPPINGS \n FOR PIZZA \n HAS BEEN ADDED.",false,true , 5 , 1);
			}
			else if(LevelManager.levelNo == 23)
			{
				UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
				UIManager._instance.tutorialPanelBg.OpenPopupItaly ("A NEW ITEM \n HAS BEEN ADDED.",false,true , 6 , 1);
			}
			else if(LevelManager.levelNo == 31)
			{
				Australia_Manager._instance.clickfirstBun = true;
				UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
				UIManager._instance.tutorialPanelBg.OpenPopupAustralia ("TAP THE BUN TO \n PUT IT ON THE PLATE.",false,false , 0);
			}
			else if(LevelManager.levelNo == 32)
			{
				Australia_Manager._instance.clickFirstFryer = true;
				UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
				UIManager._instance.tutorialPanelBg.OpenPopupAustralia ("TAP DEEP FRIYER \n TO MAKE FRIES.",false,false , 8);
			}
			else if(LevelManager.levelNo == 33)
			{
				UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
				UIManager._instance.tutorialPanelBg.OpenPopupAustralia ("VEG TOPPINGS \n FOR BURGER \n HAS BEEN ADDED.",false,true , 4 , 1);
			}
			else if(LevelManager.levelNo == 34)
			{
				UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
				UIManager._instance.tutorialPanelBg.OpenPopupAustralia ("A NEW ITEM \n FOR BURGER \n HAS BEEN ADDED.",false,true , 5 , 1);
			}
			else
			{
				if(noOfExtraPopup > 0)
				{
					UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
					UIManager._instance.tutorialPanelBg.SpecialTutorials ();
				}
				else
				{
					if(LevelManager.levelNo <= 10)
					{
						US_Manager.tutorialEnd = true;
					}
					else if(LevelManager.levelNo <= 20)
					{
						China_Manager.tutorialEnd = true;
					}
					else if(LevelManager.levelNo <= 30)
					{
						Italy_Manager.tutorialEnd = true;
					}
					else 
					{
						Australia_Manager.tutorialEnd = true;
					}
					TutorialPanel.popupPanelActive = false;
					Destroy(UIManager._instance.tutorialPanelCanvas.gameObject); 
					Destroy(UIManager._instance.tutorialPanelBg.gameObject);
					Destroy (transform.parent.gameObject);
				}

			}
		}
		else
		{
			gameObject.SetActive (false);
			TutorialPanel.popupPanelActive = false;
		}

	}
}