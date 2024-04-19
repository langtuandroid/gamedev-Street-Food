using System.Collections;
using _Project.Scripts.Managers;
using _Project.Scripts.UI_Scripts;
using _Project.Scripts.UI.Tutorial;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Audio
{
	public class AutoType : MonoBehaviour
	{
		[Inject] private LevelManager _levelManager;
		[Inject] private USController _usManager;
		[Inject] private ChinaController _chinaManager;
		[Inject] private AustraliaController _australiaManager;
		[Inject] private UIManager _uiManager;
		[Inject] private ItalyController _italyManager;
		[FormerlySerializedAs("myText")] [SerializeField] private Text _text;
		[FormerlySerializedAs("isGoalAchieved")] [SerializeField] private bool _isGoalAchived;
		private float _pauseLetter = 0.1f;
		private string _message;
		private int _extraPopUp;
		public GameObject _deactivateImage { get; set; }
		

		private void Start () 
		{
			if(!_isGoalAchived)
			{
				_message = "level goal: $"+_levelManager.targetScore[LevelManager.levelNo];
				_text.text = "";
				if (PlayerPrefs.GetInt ("BellsTut") == 1)
				{
					_extraPopUp++;
				}
				if(PlayerPrefs.GetInt ("WhistlesTut") == 1)
				{
					_extraPopUp++;
				}
				if(PlayerPrefs.GetInt ("cupcakeTut") == 1)
				{
					_extraPopUp++;
				}
				if(PlayerPrefs.GetInt ("radioTut") == 1)
				{
					_extraPopUp++;
				}

			}
		}
	
		public IEnumerator PrintAnimation () 
		{
			if(_isGoalAchived)
			{
				_message = "goal met!";
			}
			else
			{
				TutorialPanel.popupPanelActive = true;
			}
			foreach (char letter in _message.ToCharArray()) {
				_text.text += letter;
				yield return 0;
				yield return new WaitForSeconds (_pauseLetter);
			}      
			yield return new WaitForSeconds(1.0f);
			if(!_isGoalAchived)
			{
				_deactivateImage.SetActive (false);
				if(LevelManager.levelNo == 1)
				{
					_usManager.ClickBun = true;
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

					_chinaManager.PlateTutClick = true;
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopupChina ("TAP TO \n PLACE THE PLATE.",false,false , 9);
				}
				else if(LevelManager.levelNo == 13)
				{
					_chinaManager.BowlClock = true;
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopupChina ("CLICK BOWL TO\n PUT ON TRAY.",false,false , 10);
				}
				else if(LevelManager.levelNo == 21)
				{
					_italyManager.IsClickFirstBase = true;
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
					_australiaManager.ClickFirstBun = true;
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopupAustralia ("TAP THE BUN TO \n PUT IT ON THE PLATE.",false,false , 0);
				}
				else if(LevelManager.levelNo == 32)
				{
					_australiaManager.FryerClick = true;
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
					if(_extraPopUp > 0)
					{
						_uiManager.tutorialPanelBg.gameObject.SetActive (true);
						_uiManager.tutorialPanelBg.SpecialTutorials ();
					}
					else
					{
						if(LevelManager.levelNo <= 10)
						{
							USController._isEndTutorial = true;
						}
						else if(LevelManager.levelNo <= 20)
						{
							ChinaController._endTutorial = true;
						}
						else if(LevelManager.levelNo <= 30)
						{
							ItalyController._isEndTutorial = true;
						}
						else 
						{
							AustraliaController.tutorialEnd = true;
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