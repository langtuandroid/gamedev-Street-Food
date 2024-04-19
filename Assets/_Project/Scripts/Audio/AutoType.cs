using System.Collections;
using _Project.Scripts.Managers;
using _Project.Scripts.UI_Scripts;
using _Project.Scripts.UI.Tutorial;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Audio
{
	public class AutoType : MonoBehaviour
	{
		[Inject] private US_Manager _usManager;
		[Inject] private China_Manager _chinaManager;
		[Inject] private Australia_Manager _australiaManager;
		[Inject] private UIManager _uiManager;
		[Inject] private Italy_Manager _italyManager;
		private string _message;
		private int noOfExtraPopup;
		
		[SerializeField] private bool isGoalAchieved;
		[SerializeField] private float letterPause = 0.03f;
		[SerializeField] private Text myText;
		public GameObject imageToDeactivate;
		

		private void Start () 
		{
			if(!isGoalAchieved)
			{
				_message = "level goal: $"+LevelManager._instance.targetScore[LevelManager.levelNo];
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
				_message = "goal met!";
			}
			else
			{
				TutorialPanel.popupPanelActive = true;
			}
			foreach (char letter in _message.ToCharArray()) {
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
					_usManager.clickfirstBun = true;
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopup ("TAP BUN \n TO PUT ON THE PLATE.",false,false , 0);
				}
				else if(LevelManager.levelNo == 2)
				{
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopup ("TWO NEW ITEMS \n HAVE BEEN ADDED.",false,false , 7 , 1);
				}
				else if(LevelManager.levelNo == 3)
				{
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopup ("A NEW ITEM \n HAS BEEN ADDED.",false,true , 8 , 1);
				}
				else if(LevelManager.levelNo == 11)
				{

					_chinaManager.clickPlateTut = true;
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopupChina ("TAP TO \n PLACE THE PLATE.",false,false , 9);
				}
				else if(LevelManager.levelNo == 13)
				{
					_chinaManager.clickBowlTut = true;
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopupChina ("CLICK BOWL TO\n PUT ON TRAY.",false,false , 10);
				}
				else if(LevelManager.levelNo == 21)
				{
					_italyManager.clickfirstBase = true;
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopupItaly ("CLICK PIZZA\n BASE TO PUT ON \n THE PLATE",false,false , 0);
				}
				else if(LevelManager.levelNo == 22)
				{
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopupItaly ("NON VEG TOPPINGS \n FOR PIZZA \n HAS BEEN ADDED.",false,true , 5 , 1);
				}
				else if(LevelManager.levelNo == 23)
				{
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopupItaly ("A NEW ITEM \n HAS BEEN ADDED.",false,true , 6 , 1);
				}
				else if(LevelManager.levelNo == 31)
				{
					_australiaManager.clickfirstBun = true;
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopupAustralia ("TAP THE BUN TO \n PUT IT ON THE PLATE.",false,false , 0);
				}
				else if(LevelManager.levelNo == 32)
				{
					_australiaManager.clickFirstFryer = true;
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopupAustralia ("TAP DEEP FRIYER \n TO MAKE FRIES.",false,false , 8);
				}
				else if(LevelManager.levelNo == 33)
				{
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopupAustralia ("VEG TOPPINGS \n FOR BURGER \n HAS BEEN ADDED.",false,true , 4 , 1);
				}
				else if(LevelManager.levelNo == 34)
				{
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopupAustralia ("A NEW ITEM \n FOR BURGER \n HAS BEEN ADDED.",false,true , 5 , 1);
				}
				else
				{
					if(noOfExtraPopup > 0)
					{
						_uiManager.tutorialPanelBg.gameObject.SetActive (true);
						_uiManager.tutorialPanelBg.SpecialTutorials ();
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
						Destroy(_uiManager.tutorialPanelCanvas.gameObject); 
						Destroy(_uiManager.tutorialPanelBg.gameObject);
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
}