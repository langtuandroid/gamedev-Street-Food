using System;
using _Project.Scripts.Entities.Customers;
using _Project.Scripts.Managers;
using _Project.Scripts.UI_Scripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI.Tutorial
{
	public class TutorialPanel : MonoBehaviour 
	{
		public static bool popupPanelActive;
		[Inject] private WisitorHandler _customerHandler;
		[Inject] private UIManager _uiManager;   
		public Text popupMessage;
		public TextMesh popupMessageText;
		public Image childObject;
		public bool inCanvas;
		public GameObject upArrowObject;
		public GameObject downArrowObject;
		public TweenScale myScale;
		public Vector3 posBun /*13 ; 8 for Italy*/;
		public Vector3 posTikki /*13 ; 8 for Italy*/;
		public Vector3 posCoins /*13 ; 8 for Italy*/;
		public Vector3 posGold /*13 ; 8 for Italy*/;
		public Vector3 positionPickCoins /*13 ; 8 for Italy*/;
		public Vector3 posCoke /*13 ; 8 for Italy*/;
		public Vector3 posSauce /*13 ; 8 for Italy*/;
		public Vector3 posDustBin /*13 ; 8 for Italy*/;
		public Vector3 posArrowOnCoke /*13 ; 8 for Italy*/;
		public Vector3 posTikkiStack /*13 ; 8 for Italy*/;
		public Vector3 posbunStack /*13 ; 8 for Italy*/;
		public Vector3 noArrow /*13 ; 8 for Italy*/;
		public Vector3 posRawNoodles /*12*/;
		public Vector3 posNoodlesVeg /*12*/;
		public Vector3 posNoodlesPlate /*12*/;
		public Vector3 posPan /*12*/;
		public Vector3 posSoupVeg /*12*/;
		public Vector3 posSoupContainer /*12*/;
		public Vector3 posSoupBowl /*12*/;
		public Vector3 posArrowSoupContainer /*12*/;
		public Vector3 posArrowOnSoupBowl /*12*/;
		public Vector3 posArrowDustbin /*12*/;
		public Vector3 posCheese/*3*/;
		public Vector3 posVeg/*3*/;
		public Vector3 posNonVeg/*3*/;
		public Vector3 posOven/*3*/;
		public Vector3 posPlate/*3*/;
		public Vector3 posCabbage;
		public Vector3 posFryer;
		public Vector3 posFirstFries;
		public Vector3 posBell ;
		public Vector3 posCake ;
		public Vector3 posWhistle ;
		public Vector3 posRadio ;
	
		
		public Vector3 posBellArrow;
		public Vector3 posWhistleArrow;
		public Vector3 posRadioArrow;
		public int whistlePosNo;
		public int bellPosNo;
		public int cupcakePosNo;
		public int radioPosNo;
		
		private bool clickToNext;
		private static int tutNo;
		private int noOfExtraPopup;

		private void OnEnable()
		{
			transform.localScale = Vector3.one;
		}

		private void Start()
		{
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

		public void SpecialTutorials()
		{
			string variableOfPopup = "";
			int popNoOfPopup = 0;
			_uiManager.tutorialPanelBg.gameObject.SetActive (true);
			_uiManager.tutorialPanelCanvas.gameObject.SetActive (false);
			if (PlayerPrefs.GetInt ("BellsTut") == 1)
			{
				PlayerPrefs.SetInt ("BellsTut",2);
				variableOfPopup = " Click On the Bell \n to call more customers ";
				popNoOfPopup = bellPosNo;
			}
			else if(PlayerPrefs.GetInt ("WhistlesTut") == 1)
			{
				PlayerPrefs.SetInt ("WhistlesTut",2);
				variableOfPopup = "The whistle blows\n  when a customer leaves \n without paying.";
				popNoOfPopup = whistlePosNo;
			}
			else if(PlayerPrefs.GetInt ("cupcakeTut") == 1)
			{
				PlayerPrefs.SetInt ("cupcakeTut",2);
				variableOfPopup = "The cupcake refills\n the customer waiting bar.";
				popNoOfPopup = cupcakePosNo;
			}
			else if(PlayerPrefs.GetInt ("radioTut") == 1)
			{
				PlayerPrefs.SetInt ("radioTut",2);
				variableOfPopup = "Click on the radio to\nswitch it on. It Increases\n customer waiting time.";
				popNoOfPopup = radioPosNo;
			}

			if (Application.loadedLevel == 1) 
			{
				_uiManager.tutorialPanelBg.OpenPopup (variableOfPopup, false, false, popNoOfPopup, 1);
			}
			else if (Application.loadedLevel == 2) 
			{
				_uiManager.tutorialPanelBg.OpenPopupChina (variableOfPopup, false, false, popNoOfPopup, 1);
			}
			else if (Application.loadedLevel == 3) 
			{
				_uiManager.tutorialPanelBg.OpenPopupItaly (variableOfPopup, false, false, popNoOfPopup, 1);
			} 
			else if (Application.loadedLevel == 4) 
			{
				_uiManager.tutorialPanelBg.OpenPopupAustralia (variableOfPopup, false, false, popNoOfPopup, 1);
			}
			noOfExtraPopup--;
		}


		private void Update () 
		{
			if (!inCanvas)
			{
				Vector3 parentScale = transform.parent.localScale;
				transform.localScale = new Vector3(1 / parentScale.x, 1 / parentScale.y, 1) * 0.7f;
			}
			
			if(clickToNext)
			{
				if(Input.GetMouseButtonDown (0))
				{

					if(tutNo > 0)
						clickToNext = false;
					if(LevelManager.levelNo == 1)
					{

						if(tutNo == 1)
						{
							_uiManager.tutorialPanelCanvas.gameObject.SetActive (true);
							_uiManager.tutorialPanelCanvas.OpenPopup ("GOLD CAN BE OBTAINED BY 5 PERFECT SERVES!",true,false , 6 , 1);
						}
						else if(tutNo == 2)
						{
							_uiManager.tutorialPanelCanvas.gameObject.SetActive (false);
							_uiManager.tutorialPanelBg.gameObject.SetActive (true);
							_uiManager.tutorialPanelBg.OpenPopup ("PUT BURNT SAUSAGE \nIN THE DUSTBIN!",false,false , 9 , 1);
						}
						else if(tutNo > 2)
						{
							if(_uiManager.tutorialPanelBg.noOfExtraPopup > 0)
							{
								_uiManager.tutorialPanelBg.SpecialTutorials ();
							}
							else
							{
								_uiManager.tutorialPanelCanvas.gameObject.SetActive (false);
								_uiManager.tutorialPanelBg.gameObject.SetActive (false);
								_customerHandler.ConfigureCustomer ();
								USController._isEndTutorial = true;
							}
						}
					}
					else if(LevelManager.levelNo < 4)
					{
						if(_uiManager.tutorialPanelBg.noOfExtraPopup > 0)
						{
							_uiManager.tutorialPanelBg.SpecialTutorials ();
						}
						else
						{
							_customerHandler.ConfigureCustomer ();
							_uiManager.tutorialPanelCanvas.gameObject.SetActive (false);
							_uiManager.tutorialPanelBg.gameObject.SetActive (false);
							USController._isEndTutorial = true;
						}
					}
					else if(LevelManager.levelNo == 11)
					{
						if(tutNo == 0)
						{
							_uiManager.tutorialPanelBg.gameObject.SetActive (false);
							_uiManager.tutorialPanelCanvas.gameObject.SetActive (true);
							_uiManager.tutorialPanelCanvas.OpenPopupChina ("ONE PAN CAN FILL TWO NOODLE PLATES!",true,false , 16 , 1);
						}
						else if(tutNo >= 1)
						{
							if(_uiManager.tutorialPanelBg.noOfExtraPopup > 0)
							{
								_uiManager.tutorialPanelBg.SpecialTutorials ();
							}
							else
							{
								_customerHandler.ConfigureCustomer ();
								_uiManager.tutorialPanelCanvas.gameObject.SetActive (false);
								_uiManager.tutorialPanelBg.gameObject.SetActive (false);
								ChinaController._endTutorial = true;
							}
						}
					}
					else if(LevelManager.levelNo == 13)
					{
						if(_uiManager.tutorialPanelBg.noOfExtraPopup > 0)
						{
							_uiManager.tutorialPanelBg.SpecialTutorials ();
						}
						else
						{
							ChinaController._endTutorial = true;
							_customerHandler.ConfigureCustomer ();
							_uiManager.tutorialPanelCanvas.gameObject.SetActive (false);
							_uiManager.tutorialPanelBg.gameObject.SetActive (false);
						}
					}
					else if(LevelManager.levelNo == 21)
					{
						if(_uiManager.tutorialPanelBg.noOfExtraPopup > 0)
						{
							_uiManager.tutorialPanelBg.SpecialTutorials ();
						}
						else
						{
							_customerHandler.ConfigureCustomer ();
							_uiManager.tutorialPanelCanvas.gameObject.SetActive (false);
							_uiManager.tutorialPanelBg.gameObject.SetActive (false);
							ItalyController._isEndTutorial = true;
						}
					}
					else if(LevelManager.levelNo == 22 || LevelManager.levelNo == 23)
					{
						if(_uiManager.tutorialPanelBg.noOfExtraPopup > 0)
						{
							_uiManager.tutorialPanelBg.SpecialTutorials ();
						}
						else
						{
							ItalyController._isEndTutorial = true;
							_customerHandler.ConfigureCustomer ();
							_uiManager.tutorialPanelCanvas.gameObject.SetActive (false);
							_uiManager.tutorialPanelBg.gameObject.SetActive (false);
						}
					}
					else if(LevelManager.levelNo == 31 || LevelManager.levelNo == 33 || LevelManager.levelNo == 34)
					{
						if(_uiManager.tutorialPanelBg.noOfExtraPopup > 0)
						{
							_uiManager.tutorialPanelBg.SpecialTutorials ();
						}
						else
						{
							_customerHandler.ConfigureCustomer ();
							_uiManager.tutorialPanelCanvas.gameObject.SetActive (false);
							_uiManager.tutorialPanelBg.gameObject.SetActive (false);
							AustraliaController.tutorialEnd = true;
						}
					}
					else
					{
						if(_uiManager.tutorialPanelBg.noOfExtraPopup > 0)
						{
							_uiManager.tutorialPanelBg.SpecialTutorials ();
						}
						else
						{
							_uiManager.tutorialPanelCanvas.gameObject.SetActive (false);
							_uiManager.tutorialPanelBg.gameObject.SetActive (false);
							_customerHandler.ConfigureCustomer ();
							if(LevelManager.levelNo <= 10)
								USController._isEndTutorial = true;
							else if(LevelManager.levelNo <= 20)
								ChinaController._endTutorial = true;
							if(LevelManager.levelNo <= 30)
								ItalyController._isEndTutorial = true;
							else
								AustraliaController.tutorialEnd = true;
						}
					}
					tutNo++;
				}
			}
		}
		
		public void OpenPopup(string message , bool upArrow , bool isCoke , int posNo , int clickToGo = 0)
		{
			popupPanelActive = true;
			myScale.ResetToBeginning ();
			myScale.PlayForward ();
			if(!inCanvas)
			{
				switch(posNo)
				{
					case 0:
						transform.localPosition = posbunStack;
						break;
					case 1:
						transform.localPosition = posTikkiStack;
						break;
					case 2:
						transform.localPosition = posTikki;
						break;
					case 3:
						transform.localPosition = posBun;
						break;
					case 4:
						transform.localPosition = positionPickCoins;
						break;
					case 5:
						Debug.Log(1);
						transform.localPosition = _uiManager.coinsText.transform.position;
						break;
					case 6:
						Debug.Log(2);
						transform.localPosition = _uiManager.goldText.transform.position;
						break;
					case 7:
						transform.localPosition = posSauce;
						break;
					case 8:
						transform.localPosition = posCoke;
						break;
					case 9:
						transform.localPosition = posDustBin;
						break;
					case 10:
						transform.localPosition = noArrow;
						break;
					case 11:
						transform.localPosition = posWhistle ;
						break;
					case 12 :
						transform.localPosition =  posBell ;
						break;
					case 13 :
						transform.localPosition =  posCake ;
						break;
					case 14 :
						transform.localPosition = posRadio ;
						break ;
				}
			}
			else
			{
				switch(posNo)
				{
					case 0:

						childObject.rectTransform.position = posbunStack;
						break;
					case 1:
						childObject.transform.position = posTikkiStack;
						break;
					case 2:
						transform.position = posTikki;
						break;
					case 3:
						transform.position = posBun;
						break;
					case 4:
						transform.position = positionPickCoins;
						break;
					case 5:
						childObject.transform.position = _uiManager.coinsText.transform.position + Vector3.down * 2.5f;
						break;
					case 6:
						childObject.transform.position = _uiManager.goldText.transform.position + Vector3.down * 2.5f;
						break;
					case 7:
						childObject.rectTransform.position = posSauce;
						break;
					case 8:
						transform.position = posCoke;
						break;
					case 9:
						transform.position = posDustBin;
						break;
					case 10:
						childObject.transform.localPosition = noArrow;
						break;
			
				}
			}
			if(clickToGo == 1)
			{
				clickToNext = true;
			}
			if(upArrow)
			{
				upArrowObject.SetActive (true);
				downArrowObject.SetActive (false);
			}
			else
			{
				if(isCoke)
				{
					downArrowObject.transform.localPosition = posArrowOnCoke;
				}
				else if(posNo==12)
				{
					downArrowObject.transform.localPosition = posBellArrow;
				
				}
				else if(posNo==11)
				{
					downArrowObject.transform.localPosition = posWhistleArrow;
				
				}
				else if(posNo==14)
				{
					downArrowObject.transform.localPosition = posRadioArrow;
				
				}
				else
				{
					downArrowObject.transform.localPosition = Vector3.zero;
				}
				upArrowObject.SetActive (false);
				downArrowObject.SetActive (true);
			}
			if(posNo == 10)
			{
				upArrowObject.SetActive (false);
				downArrowObject.SetActive (false);
			}
			if(inCanvas)
				popupMessage.text = message;
			else 
			{
				popupMessageText.text = message;
			}
		}

		public void OpenPopupItaly(string message , bool upArrow , bool isCoke , int posNo , int clickToGo = 0)
		{
			popupPanelActive = true;
			myScale.ResetToBeginning ();
			myScale.PlayForward ();
			if(!inCanvas)
			{
				switch(posNo)
				{
					case 0:
						gameObject.transform.localPosition = posBun;
						break;
					case 1:
						gameObject.transform.localPosition = posVeg;
						break;
					case 2:
						gameObject.transform.localPosition = posCheese;
						break;
					case 3:
						gameObject.transform.localPosition = posPlate;
						break;
					case 4:
						gameObject.transform.localPosition = posOven;
						break;
					case 5:
						gameObject.transform.localPosition = posNonVeg;
						break;
					case 6:
						gameObject.transform.localPosition = posCoke;
						break;
					case 7:
						gameObject.transform.localPosition = posDustBin;
						break;
					case 8:
						gameObject.transform.localPosition = posWhistle ;
						break;
					case 9 :
						gameObject.transform.localPosition =  posBell ;
						break;
					case 10 :
						gameObject.transform.localPosition =  posCake ;
						break;
					case 11 :
						gameObject.transform.localPosition = posRadio ;
						break ;
				}
			}
			else
			{
				switch(posNo)
				{
					case 13:
						childObject.transform.localPosition = noArrow;
						break;
				}
			}
			if(clickToGo == 1)
			{
				clickToNext = true;
			}
			if(upArrow)
			{
				upArrowObject.SetActive (true);
				downArrowObject.SetActive (false);
			}
			else
			{
				if(posNo == 6) 
				{
					downArrowObject.transform.localPosition = posArrowOnCoke;
				}
				else if(posNo==9)
				{
					downArrowObject.transform.localPosition = posBellArrow;
				
				}
				else if(posNo==8)
				{
					downArrowObject.transform.localPosition = posWhistleArrow;
				
				}
				else if(posNo==11)
				{
					downArrowObject.transform.localPosition = posRadioArrow;
				
				}
				else 
				{
					downArrowObject.transform.localPosition = Vector3.zero;;
				
				}
				upArrowObject.SetActive (false);
				downArrowObject.SetActive (true);
			}

			if(posNo == 13)
			{
				upArrowObject.SetActive (false);
				downArrowObject.SetActive (false);
			}
			if(inCanvas)
				popupMessage.text = message;
			else 
			{
				popupMessageText.text = message;
			}
		}

		public void OpenPopupAustralia(string message , bool upArrow , bool isCoke , int posNo , int clickToGo = 0)
		{
			popupPanelActive = true;
			myScale.ResetToBeginning ();
			myScale.PlayForward ();
			if(!inCanvas)
			{
				switch(posNo)
				{
					case 0:
						gameObject.transform.localPosition = posbunStack;
						break;
					case 1:
						gameObject.transform.localPosition = posTikkiStack;
						break;
					case 2:
						gameObject.transform.localPosition = posTikki;
						break;
					case 3:
						gameObject.transform.localPosition = posBun;
						break;
					case 4:
						gameObject.transform.localPosition = posVeg;
						break;
					case 5:
						gameObject.transform.localPosition = posCabbage;
						break;
					case 6:
						gameObject.transform.localPosition = posCoke;
						break;
					case 7:
						gameObject.transform.localPosition = posDustBin;
						break;
					case 8:
						gameObject.transform.localPosition = posFryer;
						break;
					case 9:
						gameObject.transform.localPosition = posFirstFries;
						break;
					case 10:
						gameObject.transform.localPosition = posWhistle ;
						break;
					case 11 :
				
						gameObject.transform.localPosition =  posBell ;
						break;
					case 12 :
						gameObject.transform.localPosition =  posCake ;
						break;
					case 13 :
						gameObject.transform.localPosition = posRadio ;
						break ;
				}
			}
			else
			{
				switch(posNo)
				{
					case 14:
						childObject.transform.localPosition = noArrow;
						break;
				}
			}
			if(clickToGo == 1)
			{
				clickToNext = true;
			}
			if(upArrow)
			{
				upArrowObject.SetActive (true);
				downArrowObject.SetActive (false);
			}
			else
			{
				if(posNo == 8)
				{
					downArrowObject.transform.localPosition = posArrowDustbin;
				}
				else if(posNo==11)
				{
					downArrowObject.transform.localPosition = posBellArrow;
				
				}
				else if(posNo==10)
				{
					downArrowObject.transform.localPosition = posWhistleArrow;
				
				}
				else if(posNo==13)
				{
					downArrowObject.transform.localPosition = posRadioArrow;
				
				}
				else 
				{
					downArrowObject.transform.localPosition = Vector3.zero;
				
				}
				upArrowObject.SetActive (false);
				downArrowObject.SetActive (true);
			}
		
			if(posNo == 14)
			{
				upArrowObject.SetActive (false);
				downArrowObject.SetActive (false);
			}
			if(inCanvas)
				popupMessage.text = message;
			else 
			{
				popupMessageText.text = message;
			}
		}

		public void OpenPopupChina(string message , bool upArrow , bool isCoke , int posNo , int clickToGo = 0)
		{
			popupPanelActive = true;
			myScale.ResetToBeginning ();
			myScale.PlayForward ();
			if(!inCanvas)
			{
				switch(posNo)
				{
					case 0:
						gameObject.transform.localPosition = posRawNoodles;
						break;
					case 1:
						gameObject.transform.localPosition = posNoodlesVeg;
						break;
					case 2:
						gameObject.transform.localPosition =  posSoupBowl;
						break;
					case 3:
						gameObject.transform.localPosition = posPan;
						break;
					case 4:
						gameObject.transform.localPosition = posSoupVeg;
						break;
					case 5:
						gameObject.transform.localPosition = posSoupContainer;
						break;
					case 6:
						if(LevelManager.levelNo == 11)
						{
							gameObject.transform.localPosition =  posNoodlesPlate;
						}
						else
						{
							gameObject.transform.localPosition = posSoupBowl;
						}
						break;
					case 7:
						gameObject.transform.localPosition = posArrowSoupContainer;
						break;
					case 8:
						gameObject.transform.localPosition = posArrowOnSoupBowl;
						break;
					case 9:
						gameObject.transform.localPosition = posBun;
						break;
					case 10:
						gameObject.transform.localPosition = posTikki; 
						break;
					case 11:
						gameObject.transform.localPosition = posDustBin;
						break;
					case 12:
						gameObject.transform.localPosition = posWhistle ;
						break;
					case 13 :
						gameObject.transform.localPosition =  posBell ;
						break;
					case 14 :
						gameObject.transform.localPosition =  posCake ;
						break;
					case 15 :
						gameObject.transform.localPosition = posRadio ;
						break ;
				}
			}
			else
			{
				switch(posNo)
				{
					case 16:
						childObject.transform.localPosition = noArrow;
						break;
				}
			}
			if(clickToGo == 1)
			{
				clickToNext = true;
			}
			if(upArrow)
			{
				upArrowObject.SetActive (true);
				downArrowObject.SetActive (false);
			}
			else
			{
				if(posNo == 5) //soup container
				{
					downArrowObject.transform.localPosition = posArrowSoupContainer;
				}
				else if(posNo == 6) //soup bowl
				{
					downArrowObject.transform.localPosition = posArrowOnSoupBowl;
				}
				else if(posNo == 11) //dustbin
				{
					downArrowObject.transform.localPosition = posArrowDustbin;
				}
				else if(posNo==13)
				{
					downArrowObject.transform.localPosition = posBellArrow;
				
				}
				else if(posNo==12)
				{
					downArrowObject.transform.localPosition = posWhistleArrow;
				
				}
				else if(posNo==15)
				{
					downArrowObject.transform.localPosition = posRadioArrow;
				}
				else 
				{
					downArrowObject.transform.localPosition = Vector3.zero;
				}
				upArrowObject.SetActive (false);
				downArrowObject.SetActive (true);
			}
		
			if(posNo == 16)
			{
				upArrowObject.SetActive (false);
				downArrowObject.SetActive (false);
			}
			if(inCanvas)
				popupMessage.text = message;
			else 
			{
				popupMessageText.text = message;
			}
		}

		void OnDisable()
		{
			popupPanelActive = false;
		}
	}
}
