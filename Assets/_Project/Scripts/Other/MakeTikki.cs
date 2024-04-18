using System.Collections;
using _Project.Scripts.Food;
using _Project.Scripts.Managers;
using _Project.Scripts.UI_Scripts;
using _Project.Scripts.UI.Tutorial;
using UnityEngine;

namespace _Project.Scripts.Other
{
	public class MakeTikki : MonoBehaviour 
	{
		private bool readyToPick;
		private bool isPicked;
		private bool reachedPlate;
		private float perfectTimer = 6f;
		private float burningTimer = 12f;
		private bool clickTikki;
		private bool isUS;
		private bool tutorialPick;
		private bool canMove;
		private GameObject otherObject;
		
		public bool isBurnt;
		public Availability availableHotDog;
		public Availability myGrill;
		public Vector3 myOriginalPos, myTouchPos;
		public float heatingTimer = 0;
		public SpriteRenderer myRenderer;
		public bool iAmSelected;
		public GameObject mySelection;
		public bool tutorialOn;
		public ParticleSystem mySmoke;
		public ParticleSystem tikkiCompletelyBaked;

		private void Start () 
		{
			if(US_Manager._instance != null)
			{
				isUS = true;
			}
			myRenderer = transform.GetComponent<SpriteRenderer>();
			myOriginalPos = transform.position;
		}


		private void Update () 
		{
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
						isBurnt = true; 
						mySmoke.gameObject.SetActive (true);
						mySmoke.Play ();
					}
				}
			}
		}

		private void OnEnable()
		{
			mySmoke.gameObject.SetActive (false);
			StartCoroutine (HeatTikkiCoroutine());
			heatingTimer = 0;
		}

		private void OnDisable()
		{
			tikkiCompletelyBaked.Stop ();
			readyToPick = false;
			isPicked = false;
			isBurnt = false;
			canMove = false;
			reachedPlate = false;
			if(LevelManager.levelNo <= 10)
				transform.localScale = Vector3.one;
			
			mySelection.SetActive (false);
			iAmSelected = false;
			otherObject = null;
		}

		private IEnumerator HeatTikkiCoroutine()
		{
			readyToPick = true;
			heatingTimer = 0;
			yield return new WaitForSeconds(3);

			if(isUS)
			{
				myRenderer.sprite = US_Manager._instance.hotDogTikkiVariations[1];
			}
			else
			{
				myRenderer.sprite = Australia_Manager._instance.burgerTikkiVariations[0];
			}
		}


		private void OnMouseDown()
		{
			if((readyToPick && (!TutorialPanel.popupPanelActive || (US_Manager.tutorialEnd && Australia_Manager.tutorialEnd))) || (tutorialPick)) 
			{
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

		private void OnMouseDrag()
		{
			if (!canMove) return;
			
			Vector3 myPos = Camera.main.WorldToScreenPoint (transform.position);
			Vector3 newPos = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y , myPos.z));
			if(Vector3.Distance (newPos,myTouchPos) > 0.2f)
			{
				transform.position =  Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y , myPos.z));
			}
		}


		private void OnMouseUp()
		{
			if (!canMove) return;
			
			if(!reachedPlate)
				StartCoroutine(MoveToPosition());
			else
			{
				ClickedDestination ();
			}
			isPicked = false;
			canMove = false;
		}

		private IEnumerator MoveToPosition()
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
					Invoke(nameof(Deactivedustbin),1.0f);
				}
				UIManager._instance.coinsText.text = UIManager._instance.totalCoins.ToString ();
				if(UIManager._instance.totalCoins < 0)
				{
					UIManager._instance.totalCoins = 0;
					UIManager._instance.coinsText.text = "0";
				}
			}
		}
		public void Deactivedustbin()
		{
			UIManager._instance.dustbin_textparent.SetActive (false);
			UIManager._instance.dustbin_textparent.transform.position = UIManager._instance.dustbintextintialposition;
		}
		
		private void OnTriggerStay(Collider other)
		{
			if(!isBurnt)
			{
				if(other.name.Contains ("hotdog") && otherObject == null )
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

		private void OnTriggerExit(Collider other)
		{
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
}
