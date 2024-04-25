using System.Collections;
using _Project.Scripts.Food;
using _Project.Scripts.Managers;
using _Project.Scripts.UI_Scripts;
using _Project.Scripts.UI.Tutorial;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Other
{
	public class MakeTikki : MonoBehaviour 
	{
		[Inject] private SoundsAll _levelSoundManager;
		[Inject] private USController _usManager;
		[Inject] private AustraliaController _australiaManager;
		[Inject] private UIManager _uiManager;   
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
		
		public bool isBurnt { get; private set; }
		public Availability availableHotDog { get; set; }
		public Availability myGrill;
		public Vector3 myOriginalPos, myTouchPos;
		public float heatingTimer { get; set; }
		public SpriteRenderer myRenderer { get; set; }
		public bool iAmSelected { get; set; }
		public GameObject mySelection;
		public bool tutorialOn { get; set; }
		public ParticleSystem mySmoke;
		public ParticleSystem tikkiCompletelyBaked;

		private void Start () 
		{
			if(_usManager != null)
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
						_uiManager.tutorialPanelBg.gameObject.SetActive (true);
				
						if(isUS)
						{
							_usManager.FirstHotDog.isTutorial = true;
							_uiManager.tutorialPanelBg.OpenPopup ("TAP OR DRAG THIS TO \n THE BUN.",false,false , 2);
						}
						else
						{
							_australiaManager._firstBurger.isTutorialOn = true;
							_uiManager.tutorialPanelBg.OpenPopupAustralia ("TAP OR DRAG TIKKI TO \n THE BUN.",false,false , 2);
						}
						tutorialOn = false;
					}
					if(isUS)
					{
						if(myRenderer.sprite == _usManager.HotDogVariants[1])
						{
							myRenderer.sprite = _usManager.HotDogVariants[2];
							tikkiCompletelyBaked.Play ();
						}
					}
					else
					{
						if(myRenderer.sprite == _australiaManager._burgerVariations[0])
						{
							myRenderer.sprite = _australiaManager._burgerVariations[1];
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
							myRenderer.sprite = _usManager.HotDogVariants[3];
						}
						else
						{
							myRenderer.sprite = _australiaManager._burgerVariations[2];
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
				myRenderer.sprite = _usManager.HotDogVariants[1];
			}
			else
			{
				myRenderer.sprite = _australiaManager._burgerVariations[0];
			}
		}


		private void OnMouseDown()
		{
			if((readyToPick && (!TutorialPanel.popupPanelActive || (USController._isEndTutorial && AustraliaController.tutorialEnd))) || (tutorialPick)) 
			{
				if(isUS)
				{
					_usManager.ResetBools ();
					_usManager.MakeTiki = this;
					_usManager.IsClickedTikki = true;
				}
				else
				{
					_australiaManager.AllClickedBoolsReset ();
					_australiaManager.ClickedFunktion = this;
					_australiaManager.IsClickedTikki = true;
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


	

		public void ClickedDestination()
		{
			if(!isBurnt)
			{
				if(availableHotDog.isAvailable)
				{
					if(tutorialPick)
					{
						if(isUS)
						{
							_usManager.FirstHotDog.isTutorial = true;
							_uiManager.tutorialPanelBg.OpenPopup ("TAP OR DRAG THIS TO \n THE CUSTOMER.",false,false , 3);
						}
						else
						{
							_australiaManager._firstBurger.isTutorialOn = true;
							_uiManager.tutorialPanelBg.OpenPopupAustralia ("TAP OR DRAG BURGER TO \n THE CUSTOMER.",false,false , 3);
						}
						tutorialPick = false;
					}
					readyToPick = false;
					isBurnt = false;
					myGrill.isAvailable = true;
					availableHotDog.isAvailable = false;

				
					transform.position = myOriginalPos;
					transform.gameObject.SetActive(false);

					if(isUS)
					{
						HotDog myHotDog = availableHotDog.transform.GetComponent<HotDog>();
						myHotDog.isTikki = true;
						if(myHotDog.yellowSauce)
							myHotDog.isTape = LevelManager.Orders.HOTDOG_YELLOW;
						else if(myHotDog.isSauce)
							myHotDog.isTape = LevelManager.Orders.HOTDOG_RED;
						else
							myHotDog.isTape = LevelManager.Orders.HOTDOG;
					}
					else
					{
						BurgerFood myBurger = availableHotDog.transform.GetComponent<BurgerFood>();
						myBurger._tikkiSpriteRenderer.gameObject.SetActive (true);
						myBurger._tikkiSpriteRenderer.sprite = myRenderer.sprite;
					
						myBurger.isTikki = true;
						if(myBurger.IsTomato && myBurger.IsOnion && myBurger.IsCabbage)
							myBurger.type = LevelManager.Orders.BURGER_COMPLETE;
						else if(myBurger.IsTomato && myBurger.IsOnion)
							myBurger.type = LevelManager.Orders.BURGER_TOMATO_ONION;
						else if(myBurger.IsCabbage && myBurger.IsOnion)
							myBurger.type = LevelManager.Orders.BURGER_ONION_CABBAGE;
						else if(myBurger.IsTomato && myBurger.IsCabbage)
							myBurger.type = LevelManager.Orders.BURGER_TOMATO_CABBAGE;
						else if(myBurger.IsTomato)
							myBurger.type = LevelManager.Orders.BURGER_TOMATO;
						else if(myBurger.IsOnion)
							myBurger.type = LevelManager.Orders.BURGER_ONION;
						else if(myBurger.IsCabbage)
							myBurger.type = LevelManager.Orders.BURGER_CABBAGE;
						else
							myBurger.type = LevelManager.Orders.BURGER;

					}
					if(heatingTimer > perfectTimer)
					{
						_levelSoundManager.buttnClickSound.Play();
						if(isUS)
						{
							availableHotDog.transform.GetComponent<HotDog>().isPerfect = true;
							_usManager.HotdogOnPlatesSR[availableHotDog._arrayPos].sprite = _usManager.HotDogVariations[2];

						}
						else
						{
							availableHotDog.transform.GetComponent<BurgerFood>().isPrefavet = true;
							_australiaManager._tikkiPlates[availableHotDog._arrayPos].sprite = _australiaManager._burgerVariations[1];
						}
					}
					else
					{
						_levelSoundManager.buttnClickSound.Play();
						if(isUS)
							_usManager.HotdogOnPlatesSR[availableHotDog._arrayPos].sprite = _usManager.HotDogVariations[1];
						else
							_australiaManager._tikkiPlates[availableHotDog._arrayPos].sprite = _australiaManager._burgerVariations[0];
					}
					if(isUS)
					{
						_usManager.GrillsFilledCount--;
						_usManager.IsClickedTikki = false;
					}
					else
					{
						_australiaManager.GrillsFilled--;
						_australiaManager.IsClickedTikki = false;
					}
				}
				else
					StartCoroutine(MoveToPosition());
			}
			else
			{
				readyToPick = false;
				isBurnt = false;
				myGrill.isAvailable = true;
				transform.position = myOriginalPos;
				transform.gameObject.SetActive(false);
				if(isUS)
				{
					_usManager.GrillsFilledCount--;
					_usManager.IsClickedTikki = false;
				}
				else
				{
					_australiaManager.GrillsFilled--;
					_australiaManager.IsClickedTikki = false;
				}
				_uiManager.totalCoins-=10;
				_levelSoundManager.dustbinSound.Play();
				if(_uiManager.totalCoins > 0){
					_uiManager.dustbin_textparent.SetActive(true);
					_uiManager.dustbin_text.text = "-10" ; 
					Invoke(nameof(Deactivedustbin),1.0f);
				}
				_uiManager.coinsText.text = _uiManager.totalCoins.ToString ();
				if(_uiManager.totalCoins < 0)
				{
					_uiManager.totalCoins = 0;
					_uiManager.coinsText.text = "0";
				}
			}
		}
		public void Deactivedustbin()
		{
			_uiManager.dustbin_textparent.SetActive (false);
			_uiManager.dustbin_textparent.transform.position = _uiManager.dustbintextintialposition;
		}
		
		private void OnTriggerStay(Collider other)
		{
			if(!isBurnt)
			{
				if(other.name.Contains ("hotdog") && otherObject == null )
				{

					availableHotDog = other.GetComponent<Availability>();
					if(availableHotDog.isAvailable)
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
					if(availableHotDog.isAvailable)
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
				if(other.name.Contains ("dustbin") && (USController._isEndTutorial == true || AustraliaController.tutorialEnd== true))
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
						if(otherObject == other.gameObject && availableHotDog.isAvailable)
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
