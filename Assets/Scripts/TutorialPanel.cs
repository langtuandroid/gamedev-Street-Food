using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour {
	public Text popupMessage;

	public TextMesh popupMessageText , meshBg;

	public Image childObject;
	public bool inCanvas;

	public GameObject upArrowObject, downArrowObject;

	public static bool popupPanelActive;

	public Vector3 posBun /* 3,China 9 for Plate ,0 - Italy for Pizza Base */, posTikki /* 2 , China 10 for bowlSoup  */, posCoins /* 5 */, posGold/* 6 */ , positionPickCoins /* 4 */, posCustomer /* 0 */, posCoke /* 8 , 6 for italy*/, posSauce/* 7 */ , posDustBin /* 9 , china 11 , italy 7*/, posArrowOnCoke /* 0 */, posTikkiStack /* 1 */, posbunStack /* 0 */ , noArrow /*13 ; 8 for Italy*/;  

	public Vector3 posRawNoodles /* 0 */ , posNoodlesVeg /* 1 */,  posNoodlesPlate  /* 2 */, posPan/* 3 */, posSoupVeg /* 4*/ , posSoupContainer /* 5 */, posSoupBowl /* 6 */ , posArrowSoupContainer /*7*/, posArrowOnSoupBowl /*8*/ , posArrowDustbin /*12*/;  
	
	public Vector3 posCheese /*2*/, posVeg/*1*/ , posNonVeg/*5*/ , posOven /*4*/ , posPlate/*3*/;

	public Vector3 posCabbage /*2*/, posFryer/*1*/ , posFirstFries;

	public Vector3 posBell,posHandcuff,posCake,posWhistle,posRadio ;
	public TweenScale myScale;



	bool clickToNext;
	public static int tutNo;
	public int noOfExtraPopup = 0;
	public Vector3 posBellArrow,posWhistleArrow,posRadioArrow;

	public int whistlePosNo;
	public int bellPosNo;
	public int cupcakePosNo;
	public int radioPosNo;




	void Start()
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

		//Debug.Log ("noOfExtraPopup = "+noOfExtraPopup);
	}

	public void SpecialTutorials()
	{
		string variableOfPopup = "";
		int popNoOfPopup = 0;
		UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
		UIManager._instance.tutorialPanelCanvas.gameObject.SetActive (false);
		if (PlayerPrefs.GetInt ("BellsTut") == 1)
		{
			PlayerPrefs.SetInt ("BellsTut",2);
			variableOfPopup = " Click On the Bell \n to call more customers ";
			popNoOfPopup = bellPosNo;
		//Debug.Log("popNoOfPopup1 = "+popNoOfPopup);
			
		}
		else if(PlayerPrefs.GetInt ("WhistlesTut") == 1)
		{
			PlayerPrefs.SetInt ("WhistlesTut",2);
			variableOfPopup = "The whistle blows\n  when a customer leaves \n without paying.";
			popNoOfPopup = whistlePosNo;
		//Debug.Log("popNoOfPopup2 = "+popNoOfPopup);
			
		}
		else if(PlayerPrefs.GetInt ("cupcakeTut") == 1)
		{
			PlayerPrefs.SetInt ("cupcakeTut",2);
			variableOfPopup = "The cupcake refills\n the customer waiting bar.";
			popNoOfPopup = cupcakePosNo;
		//Debug.Log("popNoOfPopup3 = "+popNoOfPopup);
			
		}
		else if(PlayerPrefs.GetInt ("radioTut") == 1)
		{
			PlayerPrefs.SetInt ("radioTut",2);
			variableOfPopup = "Click on the radio to\nswitch it on. It Increases\n customer waiting time.";
			popNoOfPopup = radioPosNo;
		//Debug.Log("popNoOfPopup4 = "+popNoOfPopup);
			
		}

		if (Application.loadedLevel == 1) {

			UIManager._instance.tutorialPanelBg.OpenPopup (variableOfPopup, false, false, popNoOfPopup, 1);


		}
		else if (Application.loadedLevel == 2) {
			UIManager._instance.tutorialPanelBg.OpenPopupChina (variableOfPopup, false, false, popNoOfPopup, 1);
		
		}
		else if (Application.loadedLevel == 3) {
			
			UIManager._instance.tutorialPanelBg.OpenPopupItaly (variableOfPopup, false, false, popNoOfPopup, 1);
		} 
		else if (Application.loadedLevel == 4) {
			UIManager._instance.tutorialPanelBg.OpenPopupAustralia (variableOfPopup, false, false, popNoOfPopup, 1);
			
		}
		noOfExtraPopup--;
	}

	// Update is called once per frame
	void Update () {
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
						//Debug.Log("tut = 1");
						UIManager._instance.tutorialPanelCanvas.gameObject.SetActive (true);
						UIManager._instance.tutorialPanelCanvas.OpenPopup ("GOLD CAN BE PURCHASED OR BY 5 PERFECT SERVES!",true,false , 6 , 1);
					}
					else if(tutNo == 2)
					{
						//Debug.Log("tut = 2");
						UIManager._instance.tutorialPanelCanvas.gameObject.SetActive (false);
						UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
						UIManager._instance.tutorialPanelBg.OpenPopup ("PUT BURNT SAUSAGE \nIN THE DUSTBIN!",false,false , 9 , 1);
					}
					else if(tutNo > 2)
					{
						//Debug.Log("no of tuts??"+noOfExtraPopup);
						if(UIManager._instance.tutorialPanelBg.noOfExtraPopup > 0)
						{
							UIManager._instance.tutorialPanelBg.SpecialTutorials ();
						}
						else
						{
							UIManager._instance.tutorialPanelCanvas.gameObject.SetActive (false);
							UIManager._instance.tutorialPanelBg.gameObject.SetActive (false);
//							Destroy(UIManager._instance.tutorialPanelCanvas.gameObject); 
//							Destroy(UIManager._instance.tutorialPanelBg.gameObject);
							CustomerHandler._instance.InitializeCustomer ();
							US_Manager.tutorialEnd = true;
						}
					}
				}
				else if(LevelManager.levelNo < 4)
				{
					if(UIManager._instance.tutorialPanelBg.noOfExtraPopup > 0)
					{

						UIManager._instance.tutorialPanelBg.SpecialTutorials ();
					}
					else
					{
						CustomerHandler._instance.InitializeCustomer ();
						UIManager._instance.tutorialPanelCanvas.gameObject.SetActive (false);
						UIManager._instance.tutorialPanelBg.gameObject.SetActive (false);
						US_Manager.tutorialEnd = true;
					}
				}
				else if(LevelManager.levelNo == 11)
				{
					//Debug.Log("tut no = "+tutNo);
					if(tutNo == 0)
					{
						//Debug.Log("tut = 1");
						UIManager._instance.tutorialPanelBg.gameObject.SetActive (false);
						UIManager._instance.tutorialPanelCanvas.gameObject.SetActive (true);
						UIManager._instance.tutorialPanelCanvas.OpenPopupChina ("ONE PAN CAN FILL TWO NOODLE PLATES!",true,false , 16 , 1);
					}
					else if(tutNo >= 1)
					{
						if(UIManager._instance.tutorialPanelBg.noOfExtraPopup > 0)
						{
							UIManager._instance.tutorialPanelBg.SpecialTutorials ();
						}
						else
						{

							CustomerHandler._instance.InitializeCustomer ();
							UIManager._instance.tutorialPanelCanvas.gameObject.SetActive (false);
							UIManager._instance.tutorialPanelBg.gameObject.SetActive (false);
							China_Manager.tutorialEnd = true;
						}
					}

				}
				else if(LevelManager.levelNo == 13)
				{
					if(UIManager._instance.tutorialPanelBg.noOfExtraPopup > 0)
					{
						UIManager._instance.tutorialPanelBg.SpecialTutorials ();
					}
					else
					{
						China_Manager.tutorialEnd = true;
						CustomerHandler._instance.InitializeCustomer ();
						UIManager._instance.tutorialPanelCanvas.gameObject.SetActive (false);
						UIManager._instance.tutorialPanelBg.gameObject.SetActive (false);
					}
				}
				else if(LevelManager.levelNo == 21)
				{
					if(UIManager._instance.tutorialPanelBg.noOfExtraPopup > 0)
					{
						UIManager._instance.tutorialPanelBg.SpecialTutorials ();
					}
					else
					{
						CustomerHandler._instance.InitializeCustomer ();
						UIManager._instance.tutorialPanelCanvas.gameObject.SetActive (false);
						UIManager._instance.tutorialPanelBg.gameObject.SetActive (false);
						Italy_Manager.tutorialEnd = true;
					}
				}
				else if(LevelManager.levelNo == 22 || LevelManager.levelNo == 23)
				{
					if(UIManager._instance.tutorialPanelBg.noOfExtraPopup > 0)
					{
						UIManager._instance.tutorialPanelBg.SpecialTutorials ();
					}
					else
					{
						Italy_Manager.tutorialEnd = true;
						CustomerHandler._instance.InitializeCustomer ();
						UIManager._instance.tutorialPanelCanvas.gameObject.SetActive (false);
						UIManager._instance.tutorialPanelBg.gameObject.SetActive (false);
					}
				}
				else if(LevelManager.levelNo == 31 || LevelManager.levelNo == 33 || LevelManager.levelNo == 34)
				{
					if(UIManager._instance.tutorialPanelBg.noOfExtraPopup > 0)
					{
						UIManager._instance.tutorialPanelBg.SpecialTutorials ();
					}
					else
					{
						CustomerHandler._instance.InitializeCustomer ();
						UIManager._instance.tutorialPanelCanvas.gameObject.SetActive (false);
						UIManager._instance.tutorialPanelBg.gameObject.SetActive (false);
						Australia_Manager.tutorialEnd = true;
					}
				}
				else
				{
					if(UIManager._instance.tutorialPanelBg.noOfExtraPopup > 0)
					{
						UIManager._instance.tutorialPanelBg.SpecialTutorials ();
					}
					else
					{
						UIManager._instance.tutorialPanelCanvas.gameObject.SetActive (false);
						UIManager._instance.tutorialPanelBg.gameObject.SetActive (false);
						//Destroy(UIManager._instance.tutorialPanelCanvas.gameObject); 
						//Destroy(UIManager._instance.tutorialPanelBg.gameObject);
						CustomerHandler._instance.InitializeCustomer ();
						if(LevelManager.levelNo <= 10)
							US_Manager.tutorialEnd = true;
						else if(LevelManager.levelNo <= 20)
							China_Manager.tutorialEnd = true;
						if(LevelManager.levelNo <= 30)
							Italy_Manager.tutorialEnd = true;
						else
							Australia_Manager.tutorialEnd = true;
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
				gameObject.transform.localPosition = positionPickCoins;
				break;
			case 5:
				gameObject.transform.localPosition = posCoins;
				break;
			case 6:
				gameObject.transform.localPosition = posGold;
				break;
			case 7:
				gameObject.transform.localPosition = posSauce;
				break;
			case 8:
				gameObject.transform.localPosition = posCoke;
				break;
			case 9:
				gameObject.transform.localPosition = posDustBin;
				break;
			case 10:
				gameObject.transform.localPosition = noArrow;
				break;
			case 11:
				gameObject.transform.localPosition = posWhistle ;
				break;
			case 12 :
				gameObject.transform.localPosition =  posBell ;
				break;
			case 13 :
				gameObject.transform.localPosition =  posCake ;
				break;
			case 14 :
				gameObject.transform.localPosition = posRadio ;
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
				childObject.transform.localPosition = posCoins;
				break;
			case 6:
				childObject.transform.localPosition = posGold;
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
			//Debug.Log("click to next = "+true);
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
			//Debug.Log("click to next = "+true);
			clickToNext = true;
		}
		if(upArrow)
		{
			upArrowObject.SetActive (true);
			downArrowObject.SetActive (false);
		}
		else
		{
			if(posNo == 6) //coke
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
			//Debug.Log("click to next = "+true);
			clickToNext = true;
		}
		if(upArrow)
		{
			upArrowObject.SetActive (true);
			downArrowObject.SetActive (false);
		}
		else
		{
			if(posNo == 8) //fries
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
	//	Debug.Log ("posNo = "+posNo);
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
				gameObject.transform.localPosition = posBun; //plate to add
				break;
			case 10:
				gameObject.transform.localPosition = posTikki; //bowl to add
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
			//Debug.Log("click to next = "+true);
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
