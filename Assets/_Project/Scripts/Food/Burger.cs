using UnityEngine;
using System.Collections;

public class Burger : MonoBehaviour {

	bool reachedCustomer;

	public bool wrongOrderGiven;

	public Customer customer;

	public SpriteRenderer myTikki;
	public GameObject myTomato;
	public GameObject myOnion;
	public GameObject myCabbage;
	public bool tomato;
	public bool onion;
	public bool cabbage;
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

	Vector3 colliderSize;
	public bool orderBurger;
	public GameObject error;
	void Start()
	{
		UIManager._instance.n_Burger_served=PlayerPrefs.GetInt ("BurgerServed");
		myLocalScale = transform.localScale;
		myOriginalPos = transform.position;
		if(!orderBurger)
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
		tomato = false;
		onion  = false;
		cabbage  = false;
		tikki  = false;
		reachedCustomer = false;


		myTomato.SetActive (false);
		myCabbage.SetActive (false);
		myOnion.SetActive (false);

		if(transform.GetComponent<Availability>())
		{
			myTikki.gameObject.SetActive (false);
			transform.GetComponent<Availability>().available = true;
		}
//		Australia_Manager._instance.hotDogSaucesOnPlates[transform.GetComponent<Availability>().myPositionInArray].gameObject.SetActive (false);
		myType = LevelManager.Orders.NONE; 
		transform.localScale = myLocalScale;
		startAnimating = false;
		iAmSelected = false;
		if(mySelection != null)
			mySelection.SetActive (false);

		//myTypeToEat = LevelManager.Orders.BURGER;
	}
	void OnEnable()
	{
		myTypeToEat = LevelManager.Orders.BURGER;
	}

	bool canMove;
	/// <summary>
	/// Raises the mouse down event.
	/// </summary>
	void OnMouseDown()
	{
		if(!TutorialPanel.popupPanelActive || Australia_Manager.tutorialEnd || Australia_Manager.tutorialEnd || tutorialOn)
		{
			Australia_Manager._instance.clickedHotDogDestinationFunction = this;
			canMove = true;
			Vector3 myPos = Camera.main.WorldToScreenPoint (transform.position);
			myTouchPos =  Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y , myPos.z));
			if(Australia_Manager._instance.clickedTikki && !tikki)
			{
				if(!Australia_Manager._instance.clickedTikkiDestinationFunction.isBurnt)
				{
					Australia_Manager._instance.clickedTikkiDestinationFunction.availableHotDog = this.GetComponent<Availability>();
					Australia_Manager._instance.TikkiReached ();

				}
				Australia_Manager._instance.AllClickedBoolsReset ();
			}
			else if(Australia_Manager._instance.clickedTomato && !tomato)
			{
				Australia_Manager._instance.clickedItemDestinationFunction.availableBurger = this;
				Australia_Manager._instance.ObjectReached ();
				Australia_Manager._instance.AllClickedBoolsReset ();
			}
			else if(Australia_Manager._instance.clickedOnion && !onion)
			{
				Australia_Manager._instance.clickedItemDestinationFunction.availableBurger = this;
				Australia_Manager._instance.ObjectReached ();
				Australia_Manager._instance.AllClickedBoolsReset ();
			}
			else if(Australia_Manager._instance.clickedCabbage && !cabbage)
			{
				Australia_Manager._instance.clickedItemDestinationFunction.availableBurger = this;
				Australia_Manager._instance.ObjectReached ();
				Australia_Manager._instance.AllClickedBoolsReset ();
			}
			else 
			{
				if(tikki)
				{
					if(tutorialOn)
					{
						Australia_Manager._instance.firstCustomer.tutorialOn = true;
					}
					Australia_Manager._instance.AllClickedBoolsReset ();
					Australia_Manager._instance.clickedBurger = true;

					startAnimating = false;
					scaleUp = true;
					if(!orderBurger)
						transform.GetComponent<BoxCollider> ().size = new Vector3(colliderSize.x/2f , colliderSize.y/2f , colliderSize.z);
					iAmSelected = true;
				}
				else
				{
					Australia_Manager._instance.clickedHotDogDestinationFunction = null;
					canMove = false;
					error.SetActive(true);
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
		error.SetActive (false);
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
		if(!orderBurger)
			transform.GetComponent<BoxCollider> ().size = colliderSize;
	}
	public void Stopa()
	{
		UIManager._instance.achievment_text.SetActive (false);
	}
	public void ClickedDestination()
	{
		Australia_Manager._instance.platesFilledCount--;
		myPlate.available = true;
		if(!otherObject.name.Contains ("dustbin"))
		{
			UIManager._instance.n_Burger_served++ ;
			LevelSoundManager._instance.customerEat.Play();
			//Debug.Log("Burgerserved"+UIManager._instance.n_Burger_served); 
			PlayerPrefs.SetInt ("BurgerServed",UIManager._instance.n_Burger_served);
			if(PlayerPrefs.GetInt("BurgerServed") > 9 && PlayerPrefs.GetInt ("BurgerLevel1")==0 )
			{
				PlayerPrefs.SetInt ("BurgerLevel1",1);
				UIManager._instance.achievment_text.SetActive(true);
				AchievementChild.check_claim++;
			    PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
				Invoke("Stopa",4.0f);
//				MenuManager.totalscore+=100;
//				PlayerPrefs.SetString("TotalScore",EncryptionHandler64.Encrypt (MenuManager.totalscore.ToString ()));
				
			}
			if(PlayerPrefs.GetInt("BurgerServed") > 99 && PlayerPrefs.GetInt ("BurgerLevel2")==1)
			{
				PlayerPrefs.SetInt ("BurgerLevel2",1);
				UIManager._instance.achievment_text.SetActive(true);
				AchievementChild.check_claim++;
				PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
				Invoke("Stopa",4.0f);
//				MenuManager.golds++;
//				PlayerPrefs.SetString("Golds",EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
//				UIManager._instance.goldText.text = MenuManager.golds.ToString ();
			}
			if(PlayerPrefs.GetInt("BurgerServed") > 999 && PlayerPrefs.GetInt ("BurgerLevel3")==2)
			{
				PlayerPrefs.SetInt ("BurgerLevel3",1);
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
				UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
				UIManager._instance.tutorialPanelBg.OpenPopupAustralia ("PUT BURNT TIKKI \n INTO THE DUSTBIN!",false,false ,7 , 1);
			}
//			if(!wrongOrderGiven)
//			{
//				Debug.LogError(customer.name+" : "+myType);
//
//				if(myTypeToEat != LevelManager.Orders.NONE)
//				{
//	//				customer.myOrder.Remove(myType);
//					string myTypeToEatSub = myTypeToEat.ToString().Substring(0,5);
//					Debug.LogError(customer.name+" : "+myTypeToEat);
//					//customer.myOrder.Remove(myTypeToEat);
//					for(int count = 0; count < customer.myOrder.Count; count++)
//					{
//						if(customer.myOrder[count].ToString().Contains(myTypeToEatSub.ToString()))
//						{
//							if(myType == myTypeToEat)
//							{
//								wrongOrderGiven = false;
//
//							}
//							else
//							{
//								wrongOrderGiven = true;
//								
//							}
//							customer.myOrder.RemoveAt(count);
//						}
//						
//					}
//				}
//				else
//				{
//					customer.myOrder.Remove(myType);
//				}
//				customer.RemoveOrderFromBoard (myType);
//				
//				
//			}
//			else
//			{
//				string myTypeToEatSub = myTypeToEat.ToString().Substring(0,5);
//				Debug.LogError(customer.name+" : "+myTypeToEat);
//				//customer.myOrder.Remove(myTypeToEat);
//				for(int count = 0; count < customer.myOrder.Count; count++)
//				{
//					if(customer.myOrder[count].ToString().Contains(myTypeToEatSub.ToString()))
//					{
//						if(myType == myTypeToEat)
//						{
//							wrongOrderGiven = false;
//							
//						}
//						else
//						{
//							wrongOrderGiven = true;
//							
//						}
//						customer.myOrder.RemoveAt(count);
//					}
//					
//				}
//				customer.RemoveOrderFromBoard (myTypeToEat);
//				
//			}


			string myTypeToEatSub = "BURGER";
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
					customer.coinsSpent+=Australia_Manager._instance.perfectBurger;
					customer.perfect = true;
				}
				else
				{
					customer.coinsSpent+=(Australia_Manager._instance.perfectBurger/2);
					
				}
				
			}
			else
			{
				if(customer.myOrder.Count > 0)
					customer.myWaitingTime-= 20;
				
				if(!wrongOrderGiven)
				{
					customer.coinsSpent+=Australia_Manager._instance.lessBakedBurger;
				}
				else
				{
					customer.coinsSpent+=(Australia_Manager._instance.lessBakedBurger/2);
				}
			}
			
			if(cabbage)
			{
				customer.coinsSpent+=10;
			}
			if(tomato)
			{
				customer.coinsSpent+=10;
			}
			if(onion)
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
			if(perfect){
				UIManager._instance.totalCoins-=Australia_Manager._instance.perfectBurger;
				LevelSoundManager._instance.dustbin.Play();
			if(UIManager._instance.totalCoins > 0){

				UIManager._instance.dustbin_textparent.SetActive(true);

				UIManager._instance.dustbin_text.text = "-"+Australia_Manager._instance.perfectBurger.ToString(); 
				Invoke("Deactivedustbin",1.0f);
				}}
			else
			{
				UIManager._instance.totalCoins-=Australia_Manager._instance.lessBakedBurger;
				LevelSoundManager._instance.dustbin.Play();
			UIManager._instance.coinsText.text = UIManager._instance.totalCoins.ToString ();
			if(UIManager._instance.totalCoins > 0){

				UIManager._instance.dustbin_textparent.SetActive(true);
				UIManager._instance.dustbin_text.text = "-"+Australia_Manager._instance.lessBakedBurger.ToString(); 
				Invoke("Deactivedustbin",1.0f);
				}}
			if(UIManager._instance.totalCoins < 0)
			{
				UIManager._instance.totalCoins = 0;
				UIManager._instance.coinsText.text = "0";
			}
		}
		transform.position = myOriginalPos;
		transform.gameObject.SetActive(false);
	}

	public void Deactivedustbin()
	{
		UIManager._instance.dustbin_textparent.SetActive (false);
		//UIManager._instance.dustbin_textparent.transform.position = UIManager._instance.dustbintextintialposition;
		
		
		
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

	void OnTriggerStay(Collider other)
	{
//		Debug.Log("entered" + other.name);
		if(other.name.Contains ("customer"))
		{

			otherObject = other.gameObject;
			customer = other.GetComponent<Customer>();
			for(int i = 0 ; i< customer.myOrder.Count ; i++)
			{
				if(myType == customer.myOrder[i])
				{
					wrongOrderGiven = false;
					reachedCustomer = true;
					myTypeToEat = LevelManager.Orders.NONE;
					wrongOrderGiven = false;
					break;
				}
			}
			if(!reachedCustomer && customer.iHaveAMultipleTypeOrder != LevelManager.Orders.NONE)
			{
				wrongOrderGiven = true;
				reachedCustomer = true;
				myTypeToEat = customer.iHaveAMultipleTypeOrder;
				//Debug.Log("Inside wrongOrder true");
			}
		}
		else if(other.name.Contains ("dustbin") && Australia_Manager.tutorialEnd== true )
		{
			wrongOrderGiven = false;
			otherObject = other.gameObject;
			reachedCustomer = true;
			myTypeToEat = LevelManager.Orders.NONE;
		}

	}
	
	void OnTriggerExit(Collider other)
	{
//		Debug.Log("exit" + other.name);

		if(other.name.Contains ("customer") || other.name.Contains ("dustbin"))
		{
			myTypeToEat = LevelManager.Orders.NONE;
			wrongOrderGiven = false;
			reachedCustomer = false;
		}
			

	}


}
