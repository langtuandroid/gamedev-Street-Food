using UnityEngine;
using System.Collections;

///We have 3 variations of Tikki ... if the tikki reaches burnt state i.e. state 2, it is non edible.. need to throw in dustbin


public class MakeTikki : MonoBehaviour {

	bool readyToPick;

	bool isPicked;

	public bool isBurnt;
	
	/// <summary>
	/// The hotdog Availability on which I am placed
	/// </summary>
	public Availability availableHotDog;


	/// <summary>
	/// have the tikki reached plate?
	/// </summary>
	bool reachedPlate;

	/// <summary>
	/// The grill on which I am Present
	/// </summary>
	public Availability myGrill;

	/// <summary>
	/// My original position - the position at which the object will return
	/// </summary>
	public Vector3 myOriginalPos, myTouchPos;

	/// <summary>
	/// The heating timer - timer to get burnt
	/// </summary>
	public float heatingTimer = 0;

	float perfectTimer = 6f , burningTimer = 12f;
	
	public SpriteRenderer myRenderer;

	public bool iAmSelected;

	public bool startAnimating;

	public GameObject mySelection;

	public bool tutorialOn;

	bool clickTikki;
	
	public ParticleSystem mySmoke;

	public ParticleSystem tikkiCompletelyBaked;

	public GameObject dustbin_text ;
	bool isUS;
	public static bool maketiiki;
	// Use this for initialization
	void Start () {
		if(US_Manager._instance != null)
		{
			isUS = true;

		}
		myRenderer = transform.GetComponent<SpriteRenderer>();
		myOriginalPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(readyToPick && !isBurnt && !isPicked)
		{
			heatingTimer+=Time.deltaTime;
			if(heatingTimer > perfectTimer && heatingTimer <= burningTimer)
			{
				if(tutorialOn)
				{
					tutorialPick = true;
					UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
				
					if(isUS)
					{
						US_Manager._instance.firstHotDog.tutorialOn = true;
						UIManager._instance.tutorialPanelBg.OpenPopup ("TAP OR DRAG THIS TO \n THE BUN.",false,false , 2);
					}
					else
					{
						Australia_Manager._instance.firstBurger.tutorialOn = true;
						UIManager._instance.tutorialPanelBg.OpenPopupAustralia ("TAP OR DRAG TIKKI TO \n THE BUN.",false,false , 2);
					}
					tutorialOn = false;
				}
				if(isUS)
				{
					if(myRenderer.sprite == US_Manager._instance.hotDogTikkiVariations[1])
					{
						myRenderer.sprite = US_Manager._instance.hotDogTikkiVariations[2];
						tikkiCompletelyBaked.Play ();
					}
				}
				else
				{
					if(myRenderer.sprite == Australia_Manager._instance.burgerTikkiVariations[0])
					{
						myRenderer.sprite = Australia_Manager._instance.burgerTikkiVariations[1];
						tikkiCompletelyBaked.Play ();
					}
				}
			}
			else if(heatingTimer > burningTimer && !isBurnt)
			{
				if(!TutorialPanel.popupPanelActive)
				{
					if(isUS)
					{
						myRenderer.sprite = US_Manager._instance.hotDogTikkiVariations[3];
					}
					else
					{
						myRenderer.sprite = Australia_Manager._instance.burgerTikkiVariations[2];
					}
					isBurnt = true;  // burnt
					mySmoke.gameObject.SetActive (true);
					mySmoke.Play ();
				}
			}
		}
	}

	void OnEnable()
	{
		mySmoke.gameObject.SetActive (false);
//		mySmoke.Stop ();
		StartCoroutine (HeatTikkiCoroutine());
		heatingTimer = 0;
	}

	void OnDisable()
	{
		tikkiCompletelyBaked.Stop ();
		readyToPick = false;
		isPicked = false;
		isBurnt = false;
		canMove = false;
		reachedPlate = false;
		if(LevelManager.levelNo <= 10)
			transform.localScale = Vector3.one;
		startAnimating = false;
		mySelection.SetActive (false);
		iAmSelected = false;
		otherObject = null;
//		heatingTimer = 0;
	}

	IEnumerator HeatTikkiCoroutine()
	{
		readyToPick = true;
		heatingTimer = 0;
		yield return new WaitForSeconds(3);
//		readyToPick = true;
		if(isUS)
		{
			myRenderer.sprite = US_Manager._instance.hotDogTikkiVariations[1];
		}
		else
		{
			myRenderer.sprite = Australia_Manager._instance.burgerTikkiVariations[0];
		}
//		
	}

	bool tutorialPick;
	bool canMove;
	/// <summary>
	/// Raises the mouse down event.
	/// </summary>
	void OnMouseDown()
	{
	     
		if((readyToPick && (!TutorialPanel.popupPanelActive || (US_Manager.tutorialEnd && Australia_Manager.tutorialEnd))) || (tutorialPick)) 
		{

		    startAnimating = false;
			if(isUS)
			{
				US_Manager._instance.AllClickedBoolsReset ();
				US_Manager._instance.clickedTikkiDestinationFunction = this;
				US_Manager._instance.clickedTikki = true;
			}
			else
			{
				Australia_Manager._instance.AllClickedBoolsReset ();
				Australia_Manager._instance.clickedTikkiDestinationFunction = this;
				Australia_Manager._instance.clickedTikki = true;
			}
			iAmSelected = true;

			isPicked = true;
			canMove = true;
			Vector3 myPos = Camera.main.WorldToScreenPoint (transform.position);
			myTouchPos = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y , myPos.z));
		}
	}
	
		/// <summary>
	/// Raises the mouse drag event.
	/// </summary>
	void OnMouseDrag()
	{
		if(canMove)
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
		if(canMove)
		{
			startAnimating = true;
			if(!reachedPlate)
				StartCoroutine(MoveToPosition());
			else
			{
				ClickedDestination ();
			}
			isPicked = false;
			canMove = false;
		}
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
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, myOriginalPos, step);
			distance = Vector3.Distance (transform.position , myOriginalPos);
			yield return 0;
		}
		if(iAmSelected)
			mySelection.SetActive (true);
		transform.position = myOriginalPos;
	}


	//TODO: availableHotDog

	public void ClickedDestination()
	{
		if(!isBurnt)
		{
			if(availableHotDog.available)
			{
				maketiiki=true;
				if(tutorialPick)
				{
					if(isUS)
					{
						US_Manager._instance.firstHotDog.tutorialOn = true;
						UIManager._instance.tutorialPanelBg.OpenPopup ("TAP OR DRAG THIS TO \n THE CUSTOMER.",false,false , 3);
					}
					else
					{
						Australia_Manager._instance.firstBurger.tutorialOn = true;
						UIManager._instance.tutorialPanelBg.OpenPopupAustralia ("TAP OR DRAG BURGER TO \n THE CUSTOMER.",false,false , 3);
					}
					tutorialPick = false;
				}
				readyToPick = false;
				isBurnt = false;
				myGrill.available = true;
				availableHotDog.available = false;

				
				transform.position = myOriginalPos;
				transform.gameObject.SetActive(false);

				if(isUS)
				{
					HotDog myHotDog = availableHotDog.transform.GetComponent<HotDog>();
					myHotDog.tikki = true;
					if(myHotDog.yellowSauce)
						myHotDog.myType = LevelManager.Orders.HOTDOG_YELLOW;
					else if(myHotDog.redSauce)
						myHotDog.myType = LevelManager.Orders.HOTDOG_RED;
					else
						myHotDog.myType = LevelManager.Orders.HOTDOG;
				}
				else
				{
					Burger myBurger = availableHotDog.transform.GetComponent<Burger>();
					myBurger.myTikki.gameObject.SetActive (true);
					myBurger.myTikki.sprite = myRenderer.sprite;
					
					myBurger.tikki = true;
					if(myBurger.tomato && myBurger.onion && myBurger.cabbage)
						myBurger.myType = LevelManager.Orders.BURGER_COMPLETE;
					else if(myBurger.tomato && myBurger.onion)
						myBurger.myType = LevelManager.Orders.BURGER_TOMATO_ONION;
					else if(myBurger.cabbage && myBurger.onion)
						myBurger.myType = LevelManager.Orders.BURGER_ONION_CABBAGE;
					else if(myBurger.tomato && myBurger.cabbage)
						myBurger.myType = LevelManager.Orders.BURGER_TOMATO_CABBAGE;
					else if(myBurger.tomato)
						myBurger.myType = LevelManager.Orders.BURGER_TOMATO;
					else if(myBurger.onion)
						myBurger.myType = LevelManager.Orders.BURGER_ONION;
					else if(myBurger.cabbage)
						myBurger.myType = LevelManager.Orders.BURGER_CABBAGE;
					else
						myBurger.myType = LevelManager.Orders.BURGER;

				}
				if(heatingTimer > perfectTimer)
				{
					//Debug.Log("in perfect");
					LevelSoundManager._instance.bttn_click.Play();
					if(isUS)
					{
						availableHotDog.transform.GetComponent<HotDog>().perfect = true;
						US_Manager._instance.hotdogOnPlates[availableHotDog.myPositionInArray].sprite = US_Manager._instance.hotDogVariations[2];

					}
					else
					{
						availableHotDog.transform.GetComponent<Burger>().perfect = true;
						Australia_Manager._instance.burgerTikkiOnPlates[availableHotDog.myPositionInArray].sprite = Australia_Manager._instance.burgerTikkiVariations[1];
					}
				}
				else
				{
					//Debug.Log("not perfect");
					LevelSoundManager._instance.bttn_click.Play();
					if(isUS)
						US_Manager._instance.hotdogOnPlates[availableHotDog.myPositionInArray].sprite = US_Manager._instance.hotDogVariations[1];
					else
						Australia_Manager._instance.burgerTikkiOnPlates[availableHotDog.myPositionInArray].sprite = Australia_Manager._instance.burgerTikkiVariations[0];
				}
				if(isUS)
				{
					US_Manager._instance.grillsFilledCount--;
					US_Manager._instance.clickedTikki = false;
				}
				else
				{
					Australia_Manager._instance.grillsFilledCount--;
					Australia_Manager._instance.clickedTikki = false;
				}
			}
			else
				StartCoroutine(MoveToPosition());
		}
		else
		{
			readyToPick = false;
			isBurnt = false;
			myGrill.available = true;
			transform.position = myOriginalPos;
			transform.gameObject.SetActive(false);
			if(isUS)
			{
				US_Manager._instance.grillsFilledCount--;
				US_Manager._instance.clickedTikki = false;
			}
			else
			{
				Australia_Manager._instance.grillsFilledCount--;
				Australia_Manager._instance.clickedTikki = false;
			}
			UIManager._instance.totalCoins-=10;
			LevelSoundManager._instance.dustbin.Play();
			if(UIManager._instance.totalCoins > 0){
			UIManager._instance.dustbin_textparent.SetActive(true);
			UIManager._instance.dustbin_text.text = "-10" ; 
			Invoke("Deactivedustbin",1.0f);
			}
			UIManager._instance.coinsText.text = UIManager._instance.totalCoins.ToString ();
			if(UIManager._instance.totalCoins < 0)
			{
				UIManager._instance.totalCoins = 0;
				UIManager._instance.coinsText.text = "0";
			}
			//-----------------------------------------------
			//Inside dustbin
		}
	}
	public void Deactivedustbin()
	{
	UIManager._instance.dustbin_textparent.SetActive (false);
		UIManager._instance.dustbin_textparent.transform.position = UIManager._instance.dustbintextintialposition;
		
		
		
	}

	GameObject otherObject;

	void OnTriggerStay(Collider other)
	{
//		Debug.Log("entered" + other.name);
		if(!isBurnt)
		{
			if(other.name.Contains ("hotdog") && otherObject == null )
			{

				availableHotDog = other.GetComponent<Availability>();
				if(availableHotDog.available)
				{
					otherObject = other.gameObject;
					reachedPlate = true;
//					Debug.Log("rechedin hot dog");
				}
				else
				{
					availableHotDog = null;
				}
//				Debug.Log("entered reached hotdog" + transform.name);
			}
			if(other.name.Contains ("burger") && otherObject == null)
			{
				availableHotDog = other.GetComponent<Availability>();
				if(availableHotDog.available)
				{
					otherObject = other.gameObject;
					reachedPlate = true;
				}
				else
				{
					availableHotDog = null;
				}
//				Debug.Log("entered reached hotdog" + transform.name);
			}
		}
		else
		{
			if(other.name.Contains ("dustbin") && (US_Manager.tutorialEnd == true || Australia_Manager.tutorialEnd== true))
			{
				reachedPlate = true;
			}
		}

	}
	
	void OnTriggerExit(Collider other)
	{
//		Debug.Log("exit" + other.name);
		if(!isBurnt)
		{
			if(other.name.Contains ("hotdog") || other.name.Contains ("burger"))
			{
				if(otherObject != null)
				{
					if(otherObject == other.gameObject && availableHotDog.available)
					{
						otherObject = null;
						reachedPlate = false;

//						Debug.Log("exit reached hotdog" + transform.name);
					}
				}
				else
				{
					reachedPlate = false;
				}

			}

		}
		else
		{
			if(other.name.Contains ("dustbin"))
			{
				reachedPlate = false;
			}
		}

	}




}
