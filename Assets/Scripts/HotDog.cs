using UnityEngine;
using System.Collections;

public class HotDog : MonoBehaviour {

	bool reachedCustomer;

	public bool wrongOrderGiven;

	public Customer customer;

	public bool redSauce;
	public bool yellowSauce;
	public bool tikki;

	public Availability myPlate;

	/// <summary>
	/// My original position - the position at which the object will return
	/// </summary>
	public Vector3 myOriginalPos , myTouchPos;

	/// <summary>
	/// The type of the myOrder
	/// </summary>
	public LevelManager.Orders myType;

	public LevelManager.Orders myTypeToEat;

	public bool perfect;

	public bool iAmSelected , startAnimating;

	public Vector3 myLocalScale;

	bool scaleUp;

	public GameObject mySelection;

	public bool tutorialOn;
	public GameObject errorObject;
	Vector3 colliderSize;
	void Start()
	{
		MakeTikki.maketiiki = false ;
		UIManager._instance.n_Hotdogs_served = PlayerPrefs.GetInt ("hotdogServed");
		myLocalScale = transform.localScale;
		myOriginalPos = transform.position;
		colliderSize = transform.GetComponent<BoxCollider> ().size;
	}

//	void Update()
//	{
//		if(iAmSelected && startAnimating)
//		{
//			if(scaleUp)
//			{
//				transform.localScale = new Vector3(transform.localScale.x+Time.deltaTime*0.3f,transform.localScale.x+Time.deltaTime*0.3f,1);
//				if(transform.localScale.x >(myLocalScale.x+0.1f))
//				{
//					scaleUp = false;
//				}
//			}
//			else
//			{
//				transform.localScale = new Vector3(transform.localScale.x-Time.deltaTime*0.3f,transform.localScale.x-Time.deltaTime*0.3f,1);
//				if(transform.localScale.x < myLocalScale.x)
//				{
//					scaleUp = true;
//				}
//				
//			}
//		}
//	}

	void OnDisable()
	{
		wrongOrderGiven = false;
		redSauce = false;
	
		yellowSauce  = false;
		tikki  = false;
		reachedCustomer = false;
		transform.GetComponent<Availability>().available = true;
		US_Manager._instance.hotDogSaucesOnPlates[transform.GetComponent<Availability>().myPositionInArray].gameObject.SetActive (false);
		myType = LevelManager.Orders.NONE; 
		transform.localScale = myLocalScale;
		startAnimating = false;
		iAmSelected = false;
		mySelection.SetActive (false);
	}

	bool canMove;
	/// <summary>
	/// Raises the mouse down event.
	/// </summary>
	void OnMouseDown()
	{
		if(!TutorialPanel.popupPanelActive || US_Manager.tutorialEnd || Australia_Manager.tutorialEnd || tutorialOn)
		{
			US_Manager._instance.clickedHotDogDestinationFunction = this;
			canMove = true;
			Vector3 myPos = Camera.main.WorldToScreenPoint (transform.position);
			myTouchPos =  Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y , myPos.z));
			if(US_Manager._instance.clickedTikki && myType == LevelManager.Orders.NONE)
			{
				if(!US_Manager._instance.clickedTikkiDestinationFunction.isBurnt)
				{
					US_Manager._instance.clickedTikkiDestinationFunction.availableHotDog = this.GetComponent<Availability>();
					US_Manager._instance.TikkiReached ();

				}
				US_Manager._instance.AllClickedBoolsReset ();
			}
			else if((US_Manager._instance.clickedYellowSauce || US_Manager._instance.clickedRedSauce) && (myType == LevelManager.Orders.NONE || myType == LevelManager.Orders.HOTDOG))
			{
				US_Manager._instance.clickedItemDestinationFunction.availableHotDog = this;
				US_Manager._instance.ObjectReached ();
				US_Manager._instance.AllClickedBoolsReset ();
			}
			else 
			{
				if(tikki)
				{
					if(tutorialOn)
					{
						US_Manager._instance.firstCustomer.tutorialOn = true;
					}
					US_Manager._instance.AllClickedBoolsReset ();
					US_Manager._instance.clickedHotDog = true;

					startAnimating = false;
					scaleUp = true;
					transform.GetComponent<BoxCollider> ().size = new Vector3(colliderSize.x/2f , colliderSize.y/2f , colliderSize.z);
					iAmSelected = true;
				}
				else
				{
					US_Manager._instance.clickedHotDogDestinationFunction = null;
					canMove = false;
					errorObject.SetActive(true);
				}
			}

		}
	}
	
	/// <summary>
	/// Raises the mouse drag event.
	/// </summary>
	void OnMouseDrag()
	{

		if( canMove)
		{
			Vector3 myPos = Camera.main.WorldToScreenPoint (transform.position);
			Vector3 newPos = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y , myPos.z));
			if(Vector3.Distance (newPos,myTouchPos) > 0.2f)
			{
				transform.position =  Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y , myPos.z));
			}
		}
	}
	
	
	/// <summary>
	/// Raises the mouse up event.
	/// </summary>
	void OnMouseUp()
	{
		errorObject.SetActive (false);
		if(canMove)
		{

			startAnimating = true;

			if(!reachedCustomer)
				StartCoroutine(MoveToPosition());
			else
			{
				ClickedDestination ();

			}
			canMove = false;
		}
		transform.GetComponent<BoxCollider> ().size = colliderSize;
	}
 	public void Stopa()
	{
		UIManager._instance.achievment_text.SetActive (false);
	}

	public void ClickedDestination()
	{
	
		US_Manager._instance.platesFilledCount--;
		myPlate.available = true;
		if(!otherObject.name.Contains ("dustbin"))
		{
		
			UIManager._instance.n_Hotdogs_served++;
			US_Manager._instance.clickedHotDog = false;
			//Debug.Log("hotdog"+UIManager._instance.n_Hotdogs_served);
			LevelSoundManager._instance.customerEat.Play();

			PlayerPrefs.SetInt ("hotdogServed",UIManager._instance.n_Hotdogs_served);
			if(PlayerPrefs.GetInt("hotdogServed") > 9 && PlayerPrefs.GetInt ("hotdogLevel1")==0)
			{

				PlayerPrefs.SetInt ("hotdogLevel1",1);
				UIManager._instance.achievment_text.SetActive(true);
				AchievementChild.check_claim++;
				PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
				Invoke("Stopa",4.0f);
				//Debug.Log("ACHIEVEMENT UNLOCKED");
//				MenuManager.totalscore+=100;
//				PlayerPrefs.SetString("TotalScore",EncryptionHandler64.Encrypt (MenuManager.totalscore.ToString ()));
			}
			if(PlayerPrefs.GetInt("hotdogServed") > 99 && PlayerPrefs.GetInt ("hotdogLevel2")==0)
			{
				//Debug.Log("ACHIEVEMENT UNLOCKED");
				PlayerPrefs.SetInt ("hotdogLevel2",1);
				UIManager._instance.achievment_text.SetActive(true);
				AchievementChild.check_claim++;
				PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
				Invoke("Stopa",4.0f);
//				MenuManager.golds++;
//				PlayerPrefs.SetString("Golds",EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
//				UIManager._instance.goldText.text = MenuManager.golds.ToString ();
			}
			if(PlayerPrefs.GetInt("hotdogServed") > 999 && PlayerPrefs.GetInt ("hotdogLevel3")==0)
			{
				//Debug.Log("ACHIEVEMENT UNLOCKED");
				PlayerPrefs.SetInt ("hotdogLevel3",1);
				UIManager._instance.achievment_text.SetActive(true);
				AchievementChild.check_claim++;
				PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
				Invoke("Stopa",4.0f);
//				MenuManager.golds += 5;
//				PlayerPrefs.SetString("Golds",EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
//				UIManager._instance.goldText.text = MenuManager.golds.ToString ();
			}
			if(tutorialOn)
			{
				tutorialOn = false;
				US_Manager._instance.firstCoins.tutorialOn = true;
				UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
				UIManager._instance.tutorialPanelBg.OpenPopup ("COLLECT THE COINS.",false,false , 4);
			}
//			if(!wrongOrderGiven)
//			{
//				Debug.LogError(customer.name+" : "+myType);
//				customer.myOrder.Remove(myType);
//				customer.RemoveOrderFromBoard (myType);
//			}
//			else
//			{
//				string myTypeToEatSub = myTypeToEat.ToString().Substring(0,5);
//			//	Debug.LogError(customer.name+" : "+myTypeToEat);
//				//customer.myOrder.Remove(myTypeToEat);
//				for(int count = 0; count < customer.myOrder.Count; count++)
//				{
//					if(customer.myOrder[count].ToString().Contains(myTypeToEatSub.ToString()))
//					{
//						customer.myOrder.RemoveAt(count);
//					}
//					
//				}
//				customer.RemoveOrderFromBoard (myTypeToEat);
//				
//			}



			string myTypeToEatSub = "HOTDOG";
			for(int count = 0; count < customer.myOrder.Count; count++)
			{
				if(customer.myOrder[count].ToString().Contains(myTypeToEatSub.ToString()))
				{
					if(customer.myOrder[count] == myType)
					{
						wrongOrderGiven = false;

					}
					else
					{
						wrongOrderGiven = true;

					}
					customer.myOrder.RemoveAt(count);
				}
				
			}
			customer.RemoveOrderFromBoard (myType);



			customer.iHaveAMultipleTypeOrder = LevelManager.Orders.NONE;
			
			if(perfect)
			{
				//Show perfect
				if(customer.myOrder.Count > 0)
					customer.myWaitingTime-= 30;

				if(!wrongOrderGiven)
				{
					customer.coinsSpent+=US_Manager._instance.perfectHotDog;
					customer.perfect = true;

				}
				else
				{
					customer.coinsSpent+=(US_Manager._instance.perfectHotDog/2);
					
				}
				
			}
			else
			{
				if(customer.myOrder.Count > 0)
					customer.myWaitingTime-= 20;
				
				if(!wrongOrderGiven)
				{
					customer.coinsSpent+=US_Manager._instance.lessBakedHotdog;
				}
				else
				{
					customer.coinsSpent+=(US_Manager._instance.lessBakedHotdog/2);
				}
			}
			
			if(yellowSauce || redSauce)
			{
				customer.coinsSpent+=10;
			}
			
			//Increase timer..
			
			if(customer.myWaitingTime < 0)
			{
				customer.myWaitingTime = 0;
			}
			
			if(customer.myOrder.Count <= 0)
				customer.MoveToEndPosition();
			
		}
		else
		{
			LevelSoundManager._instance.dustbin.Play();
			US_Manager._instance.clickedHotDog = false;
			if(perfect)
				UIManager._instance.totalCoins-=US_Manager._instance.perfectHotDog;
			else
				UIManager._instance.totalCoins-=US_Manager._instance.lessBakedHotdog;
			
			UIManager._instance.coinsText.text = UIManager._instance.totalCoins.ToString ();
			if(UIManager._instance.totalCoins < 0)
			{
				UIManager._instance.totalCoins = 0;
				UIManager._instance.coinsText.text = "0";
			}
		}
		transform.position = myOriginalPos;
		transform.gameObject.SetActive(false);
	}

	/// <summary>
	/// Brings Back the object to its original position
	/// </summary>
	/// <returns>The to position.</returns>
	IEnumerator MoveToPosition()
	{

		float distance = Vector3.Distance (transform.position , myOriginalPos);
		float speed = 15;
		while(distance > 0.1f)
		{
			float step = speed * 0.02f;
			transform.position = Vector3.MoveTowards(transform.position, myOriginalPos, step);
			distance = Vector3.Distance (transform.position , myOriginalPos);
			yield return 0;
		}
		if(iAmSelected)
			mySelection.SetActive (true);
		transform.position = myOriginalPos;
	}


	public GameObject otherObject;
	int a=1 ;
	void OnTriggerStay(Collider other)
	{
//		Debug.Log("entered" + other.name);
		if(other.name.Contains ("customer") )
		{
			otherObject = other.gameObject;
			customer = other.GetComponent<Customer>();
			for(int i = 0 ; i< customer.myOrder.Count ; i++)
			{
				if(myType == customer.myOrder[i])
				{
					wrongOrderGiven = false;
					reachedCustomer = true;

					break;
				}
			}
			if(!reachedCustomer && customer.iHaveAMultipleTypeOrder != LevelManager.Orders.NONE)
			{
			
				wrongOrderGiven = true;
				reachedCustomer = true;
				myTypeToEat = customer.iHaveAMultipleTypeOrder;
			}
		}
		else if(other.name.Contains ("dustbin") && (US_Manager.tutorialEnd == true || Australia_Manager.tutorialEnd== true))
		{
			otherObject = other.gameObject;
			reachedCustomer = true;
		}
//		else 
//		{
//			reachedCustomer = false;
//		}
	}
	
	void OnTriggerExit(Collider other)
	{
//		Debug.Log("exit" + other.name);

		if(other.name.Contains ("customer") || other.name.Contains ("dustbin"))
		{

			wrongOrderGiven = false;
			reachedCustomer = false;
		}
			

	}


}
