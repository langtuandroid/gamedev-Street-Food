using UnityEngine;
using System.Collections;

public class Pizza : MonoBehaviour {

	bool reachedDestination;
	
	public bool wrongOrderGiven;

	bool tutorialPick;
	
	bool isPicked;

	/// <summary>
	/// The heating timer - timer to get burnt
	/// </summary>
	public float heatingTimer = 0;
	
	float perfectTimer = 6f , burningTimer = 12f;

	public Customer customer;
	
	public bool cheese;
	public bool vegetable;

	public SpriteRenderer myToppings;

	public GameObject myCheese;

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

	public GameObject mySelection;
	
	public bool tutorialOn;

	public bool isInOven , isBurnt;

	public ParticleSystem mySmoke , pizzaCompletelyBaked;

	public SpriteRenderer myRenderer;

	public BoxCollider myContainerCollider;

	public SpriteRenderer pizzadot;

	public bool customer_Check=false ;
	public GameObject error;
	Vector3 colliderSize;

	void Start()
	{
		if (LevelManager.levelNo == 21) {
			customer_Check = true ;
		}
		UIManager._instance.n_Pizzas_served = PlayerPrefs.GetInt ("PizzaServed");
		myLocalScale = transform.localScale;
		myOriginalPos = transform.position;
		colliderSize = transform.GetComponent<BoxCollider> ().size;
		
	}
	
	void OnDisable()
	{
		perfect =false;
		wrongOrderGiven = false;
		if(!isInOven)
		{
			cheese = false;
			vegetable  = false;
		}
		reachedDestination = false;
		myToppings.gameObject.SetActive (false);
		myCheese.gameObject.SetActive (false);
		transform.GetComponent<SpriteRenderer>().sprite = Italy_Manager._instance.pizzaBakedVariations[0];
		myType = LevelManager.Orders.NONE; 
		myTypeToEat = LevelManager.Orders.NONE; 
		transform.localScale = myLocalScale;
		startAnimating = false;
		iAmSelected = false;
		mySelection.SetActive (false);
		heatingTimer = 0;
		isPicked = false;
		if(mySmoke != null)
		{
			mySmoke.gameObject.SetActive (false);
			mySmoke.Stop ();
		}
		isBurnt = false;
	}

	void Update () {

		if(!isBurnt && !isPicked && isInOven)
		{
			heatingTimer+=Time.deltaTime;
			if(heatingTimer > perfectTimer && heatingTimer <= burningTimer)
			{
				if(tutorialOn)
				{
					tutorialPick = true;
					UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
					UIManager._instance.tutorialPanelBg.OpenPopupItaly ("TAP OR DRAG THIS TO \n THE CUSTOMER.",false,false , 4);
					tutorialOn = false;
				}
				if(myRenderer.sprite == Italy_Manager._instance.pizzaBakedVariations[0])
				{
					perfect = true;
					pizzaCompletelyBaked.gameObject.SetActive(true);
					pizzaCompletelyBaked.Play ();
					myCheese.gameObject.SetActive (false);
					myToppings.gameObject.SetActive (false);
					if(myType == LevelManager.Orders.VEG_PIZZA)
					{
						myRenderer.sprite = Italy_Manager._instance.pizzaBakedVariations[1];
				
					}
					else
					{
						myRenderer.sprite = Italy_Manager._instance.pizzaBakedVariations[2];

					}
				}
			}
			else if(heatingTimer > burningTimer && !isBurnt)
			{

				if(!TutorialPanel.popupPanelActive)
				{
					perfect =false;
					myRenderer.sprite = Italy_Manager._instance.pizzaBakedVariations[3];
					isBurnt = true;  // burnt
					pizzaCompletelyBaked.Stop();
					pizzaCompletelyBaked.gameObject.SetActive(false);
					mySmoke.gameObject.SetActive (true);
					mySmoke.Play ();

				}
			}
		
		}
	}


	bool canMove;
	/// <summary>
	/// Raises the mouse down event.
	/// </summary>
	void OnMouseDown()
	{
		if((!TutorialPanel.popupPanelActive || Italy_Manager.tutorialEnd || tutorialPick || (tutorialOn && !isInOven)) && (vegetable && cheese) )
		{
			isPicked = true;
			startAnimating = false;
			canMove = true;

			Italy_Manager._instance.AllClickedBoolsReset ();
			Italy_Manager._instance.clickedPizzaDestinationFunction = this;
			iAmSelected = true;
			if(isInOven)
			{
				transform.GetComponent<BoxCollider> ().size = new Vector3(colliderSize.x/2f , colliderSize.y/2f , colliderSize.z);
				Italy_Manager._instance.clickedOvenPizza = true;
			}
			else
				Italy_Manager._instance.clickedPlatePizza = true;
				
			if(tutorialPick)
			{
				Italy_Manager._instance.firstCustomer.tutorialOn = true;
			}
			Vector3 myPos = Camera.main.WorldToScreenPoint (transform.position);
			myTouchPos =  Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y , myPos.z));
		}
		else if((!TutorialPanel.popupPanelActive || !Italy_Manager.tutorialEnd) && (!vegetable || !cheese)) // Click on it to place ingredients
		{

			if(Italy_Manager._instance.clickedVeg || Italy_Manager._instance.clickedNonVeg && !vegetable)
			{
				Italy_Manager._instance.clickedItemDestinationFunction.availablePizza = this;
				Italy_Manager._instance.ObjectReached ();
				Italy_Manager._instance.AllClickedBoolsReset ();
			}
			else if(Italy_Manager._instance.clickedCheese && !cheese)
			{
				Italy_Manager._instance.clickedItemDestinationFunction.availablePizza = this;
				Italy_Manager._instance.ObjectReached ();
				Italy_Manager._instance.AllClickedBoolsReset ();
			}
			else
			{
				error.SetActive(true);
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
			error.SetActive(false);
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
		error.SetActive(false);
		if(canMove)
		{

			startAnimating = true;
			if(!reachedDestination)
				StartCoroutine(MoveToPosition());
			else
			{

				ClickedDestination ();
				transform.position = myOriginalPos;
				transform.gameObject.SetActive(false);
			}
			canMove = false;
		}
		transform.GetComponent<BoxCollider> ().size = colliderSize;
	}
	public void Stopa()
	{
		UIManager._instance.achievment_text.SetActive (false);
	}
	///TODO : otherObject , customer , 
	public void ClickedDestination()
	{
		myContainerCollider.enabled = true;
		myPlate.available = true;
		if(otherObject.name.Contains ("customer"))
		{
			UIManager._instance.n_Pizzas_served++;
			LevelSoundManager._instance.customerEat.Play();
			PlayerPrefs.SetInt ("PizzaServed",UIManager._instance.n_Pizzas_served);
			
			
			if(PlayerPrefs.GetInt("PizzaServed") > 9 && PlayerPrefs.GetInt ("PizzaLevel1")==0)
			{

			
				PlayerPrefs.SetInt ("PizzaLevel1",1);

				
				UIManager._instance.achievment_text.SetActive(true);
				AchievementChild.check_claim++;
				PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
				Invoke("Stopa",4.0f);
			}
			if(PlayerPrefs.GetInt("PizzaServed") > 99 && PlayerPrefs.GetInt ("PizzaLevel2")==0)
			{
//				MenuManager.golds++;
//				PlayerPrefs.SetString("Golds",EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
//				UIManager._instance.goldText.text = MenuManager.golds.ToString ();
				PlayerPrefs.SetInt ("PizzaLevel2",1);

				AchievementChild.check_claim++;
				PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
				UIManager._instance.achievment_text.SetActive(true);
				Invoke("Stopa",4.0f);
			}
			if(PlayerPrefs.GetInt("PizzaServed") > 999 && PlayerPrefs.GetInt ("PizzaLevel3")==0)
			{
				PlayerPrefs.SetInt ("PizzaLevel3",1);
				UIManager._instance.achievment_text.SetActive(true);
				AchievementChild.check_claim++;
				PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
				Invoke("Stopa",4.0f);
//				MenuManager.golds += 5;
//				PlayerPrefs.SetString("Golds",EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
//				UIManager._instance.goldText.text = MenuManager.golds.ToString ();
			}
			Italy_Manager._instance.platesFilledCount--;
//			if(!wrongOrderGiven)
//			{
//				Debug.LogError(customer.name+" : "+myType);
////				customer.myOrder.Remove(myType);
//				if(myTypeToEat != LevelManager.Orders.NONE)
//				{
//					string myTypeToEatSub = myTypeToEat.ToString().Substring(myTypeToEat.ToString().Length-5);
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
//					customer.myOrder.Remove(myTypeToEat);
//				}
//				customer.RemoveOrderFromBoard (myType);
//				
//				
//			}
//			else
//			{
//				string myTypeToEatSub = myTypeToEat.ToString().Substring(myTypeToEat.ToString().Length-5);
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



		
				string myTypeToEatSub = "PIZZA";
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

			if(tutorialPick)
			{
				UIManager._instance.tutorialPanelBg.OpenPopupItaly ("PUT BURNT PIZZA \n IN THE DUSTBIN!",false,false , 7 , 1);
				tutorialPick = false;
			}
		//	Debug.Log("wrongOrderGiven = "+wrongOrderGiven);
			customer.iHaveAMultipleTypeOrder = LevelManager.Orders.NONE;
			if(perfect)
			{
				//Show perfect
				if(customer.myOrder.Count > 0)
					customer.myWaitingTime-= 30;
				
				if(!wrongOrderGiven)
				{
					customer.coinsSpent+=Italy_Manager._instance.perfectPizza;
					customer.perfect = true;
				}
				else
				{
					customer.coinsSpent+=(Italy_Manager._instance.perfectPizza/2);
				}
			}
			else
			{
				if(customer.myOrder.Count > 0)
					customer.myWaitingTime-= 20;
				if(!wrongOrderGiven)
				{
					customer.coinsSpent+=Italy_Manager._instance.lessBakedPizza;
				}
				else
				{
					customer.coinsSpent+=(Italy_Manager._instance.lessBakedPizza/2);
					
				}
			}

			
			//Increase timer..
			
			if(customer.myWaitingTime < 0)
			{
				customer.myWaitingTime = 0;
			}
			
			if(customer.myOrder.Count <= 0)
				customer.MoveToEndPosition();
			
		}
		else if(otherObject.name.Contains ("ovenPlace"))
		{
			//Debug.Log("reached oven");

			if(tutorialOn)
			{
				pizzaDestinationAvailable = Italy_Manager._instance.firstOvenAvailabe;
				UIManager._instance.tutorialPanelBg.OpenPopupItaly ("WAIT FOR PIZZA \n TO BAKE.",false,false , 4);
				Italy_Manager._instance.ovenPizzas[pizzaDestinationAvailable.myPositionInArray].tutorialOn = true;
				tutorialOn = false;
			}

			Italy_Manager._instance.platesFilledCount--;
			pizzaDestinationAvailable.available = false;
			Italy_Manager._instance.ovenPizzaRenderer[pizzaDestinationAvailable.myPositionInArray].gameObject.SetActive (true);


			Italy_Manager._instance.ovenPizzas[pizzaDestinationAvailable.myPositionInArray].myType = myType;
			Italy_Manager._instance.ovenPizzas[pizzaDestinationAvailable.myPositionInArray].myCheese.SetActive (true);
			Italy_Manager._instance.ovenPizzas[pizzaDestinationAvailable.myPositionInArray].myToppings.gameObject.SetActive (true);
			if(myType == LevelManager.Orders.VEG_PIZZA)
				Italy_Manager._instance.ovenPizzas[pizzaDestinationAvailable.myPositionInArray].myToppings.sprite = Italy_Manager._instance.pizzaToppings[0];
			else
				Italy_Manager._instance.ovenPizzas[pizzaDestinationAvailable.myPositionInArray].myToppings.sprite = Italy_Manager._instance.pizzaToppings[1];
				
			Italy_Manager._instance.ovenColliders[pizzaDestinationAvailable.myPositionInArray].enabled = false;

		}
//		else if(otherObject.name.Contains ("plate"))
//		{
//			Pizza myPizzaToShow = Italy_Manager._instance.pizzaOnPlates[pizzaDestinationAvailable.myPositionInArray].transform.GetComponent<Pizza>();
//			if(heatingTimer > perfectTimer)
//			{
//				myPizzaToShow.myCheese.gameObject.SetActive (false);
//				myPizzaToShow.myToppings.gameObject.SetActive (false);
//				myPizzaToShow.myRenderer.sprite = myRenderer.sprite;
//			}
//			else
//			{
//				myPizzaToShow.myCheese.SetActive (true);
//				myPizzaToShow.myToppings.gameObject.SetActive (true);
//				if(myType == LevelManager.Orders.VEG_PIZZA)
//					myPizzaToShow.myToppings.sprite = Italy_Manager._instance.pizzaToppings[0];
//				else
//					myPizzaToShow.myToppings.sprite = Italy_Manager._instance.pizzaToppings[1];
//			}
//
//			Debug.Log("reached plate");
//			pizzaDestinationAvailable.available = false;
//			Italy_Manager._instance.pizzaOnPlates[pizzaDestinationAvailable.myPositionInArray].gameObject.SetActive (true);
//
//
//			myPizzaToShow.myType = myType;
//			myPizzaToShow.perfect = perfect;
//
//			myPizzaToShow.vegetable = true;
//			myPizzaToShow.cheese = true;
//			Italy_Manager._instance.plateColliders[pizzaDestinationAvailable.myPositionInArray].enabled = false;
//		}
		else  // dustbin..
		{
			if(!isInOven)
				Italy_Manager._instance.platesFilledCount--;
			if(perfect)
			{
				UIManager._instance.totalCoins-=Italy_Manager._instance.perfectPizza;
				UIManager._instance.coinsText.text = UIManager._instance.totalCoins.ToString ();
				LevelSoundManager._instance.dustbin.Play();
				if(UIManager._instance.totalCoins > 0){
				UIManager._instance.dustbin_textparent.SetActive(true);
				UIManager._instance.dustbin_text.text = "-"+Italy_Manager._instance.perfectPizza.ToString(); 
				Invoke("Deactivedustbin",1.0f);
				}
				if(UIManager._instance.totalCoins < 0)
				{
					UIManager._instance.totalCoins = 0;
					UIManager._instance.coinsText.text = "0";
				}
			}
			else
			{
				UIManager._instance.totalCoins-=Italy_Manager._instance.lessBakedPizza;
				UIManager._instance.coinsText.text = UIManager._instance.totalCoins.ToString ();
				LevelSoundManager._instance.dustbin.Play();
				if(UIManager._instance.totalCoins > 0){
				UIManager._instance.dustbin_textparent.SetActive(true);
				UIManager._instance.dustbin_text.text = "-"+Italy_Manager._instance.lessBakedPizza.ToString(); 
				Invoke("Deactivedustbin",1.0f);
				}
				if(UIManager._instance.totalCoins < 0)
				{
					UIManager._instance.totalCoins = 0;
					UIManager._instance.coinsText.text = "0";
				}
			}
			
		}
		transform.position = myOriginalPos;
		transform.gameObject.SetActive(false);
	}
	public void Deactivedustbin()
	{
		UIManager._instance.dustbin_textparent.SetActive (false);
		UIManager._instance.dustbin_textparent.transform.position = UIManager._instance.dustbintextintialposition;
		
		
		
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
		isPicked = false;
		if(iAmSelected)
			mySelection.SetActive (true);
		transform.position = myOriginalPos;
	}
	
	
	public GameObject otherObject;
	public Availability pizzaDestinationAvailable;
	void OnTriggerStay(Collider other)
	{
//		Debug.Log("entered" + other.name);
		if(!isInOven)
		{
//			if(other.name.Contains ("customer") && (customer_Check == false || Italy_Manager.tutorialEnd == true) )
//			{
//
//
//				otherObject = other.gameObject;
//				customer = other.GetComponent<Customer>();
//				for(int i = 0 ; i< customer.myOrder.Count ; i++)
//				{
//					if(myType == customer.myOrder[i])
//					{
//						wrongOrderGiven = false;
//						reachedDestination = true;
//						break;
//					}
//				}
//				if(!reachedDestination && customer.iHaveAMultipleTypeOrder != LevelManager.Orders.NONE)
//				{
//					wrongOrderGiven = true;
//					reachedDestination = true;
//					myTypeToEat = customer.iHaveAMultipleTypeOrder;
//				}
//			}

			if(other.name.Contains ("ovenPlace"))
			{
				customer_Check = false ;
				otherObject = other.gameObject;
				pizzaDestinationAvailable = other.GetComponent<Availability>();
				if(pizzaDestinationAvailable.available)
				{
					reachedDestination = true;
				}
			}
		}
		else
		{
			if(other.name.Contains ("customer") && !isBurnt)
			{
				otherObject = other.gameObject;
				customer = other.GetComponent<Customer>();
				for(int i = 0 ; i< customer.myOrder.Count ; i++)
				{
					if(myType == customer.myOrder[i])
					{
						wrongOrderGiven = false;
						reachedDestination = true;
						myTypeToEat = LevelManager.Orders.NONE;
						break;
					}
				}
				if(!reachedDestination && customer.iHaveAMultipleTypeOrder != LevelManager.Orders.NONE)
				{
					wrongOrderGiven = true;
					reachedDestination = true;
					myTypeToEat = customer.iHaveAMultipleTypeOrder;
				}
			}
//			else if(other.name.Contains ("plate"))
//			{
//				if( !isBurnt && Italy_Manager.tutorialEnd)
//				{
//					otherObject = other.gameObject;
//					pizzaDestinationAvailable = other.GetComponent<Availability>();
//					if(pizzaDestinationAvailable.available)
//					{
//						reachedDestination = true; 
//					}
//				}
//			}
		}
		if(other.name.Contains ("dustbin") && Italy_Manager.tutorialEnd == true)
		{
			myTypeToEat = LevelManager.Orders.NONE;
			otherObject = other.gameObject;
			reachedDestination = true;
		}
		
	}
	
	void OnTriggerExit(Collider other)
	{
		//		Debug.Log("exit" + other.name);
		
		if(other.name.Contains ("customer") || other.name.Contains ("dustbin"))
		{
			otherObject = null;
			wrongOrderGiven = false;
			reachedDestination = false;
		}
		else if(other.name.Contains ("ovenPlace") )//|| other.name.Contains ("plate") )
		{
			if(otherObject != null)
			{
				if(otherObject == other.gameObject)
				{
					otherObject = null;
					wrongOrderGiven = false;
					reachedDestination = false;
				}
			}
			else
			{
				wrongOrderGiven = false;
				reachedDestination = false;
			}
		}
		
	}
}
