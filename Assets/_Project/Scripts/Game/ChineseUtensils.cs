using System.Collections;
using _Project.Scripts.Managers;
using _Project.Scripts.Other;
using _Project.Scripts.UI_Scripts;
using _Project.Scripts.UI.Tutorial;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Game
{
	public class ChineseUtensils : MonoBehaviour
	{
		[Inject] private LevelSoundManager _levelSoundManager;
		[Inject] private China_Manager _chinaManager;
		[Inject] private UIManager _uiManager;   
		private bool reachedDestination;
		private bool scaleUp;
		private bool isPicked;
		private float heatingTimer;
		private float perfectTimer = 6f;
		private float burningTimer = 18f;
		public bool isSoupContainer;
		public bool isPan;
		public LevelManager.Orders myType;
		public bool vegAdded { get; set; }
		public bool noodlesAdded;
		public int noOfServingsAvailable;
		public SpriteRenderer myImage;
		public Sprite []ladleVariations;
		public SpriteRenderer myFood;
		public GameObject myReadyFood;
		public Vector3 myOriginalPos , myTouchPos;
		public bool iAmSelected { get; set; }
		public GameObject mySelection;
		public BoxCollider []fullCollider ;
		public BoxCollider tipCollider;
		public bool isBurnt { get; set; }
		public bool tutorialOn { get; set; }
		public ParticleSystem mySmoke;
		public TweenScale myScale;
		public TweenAlpha myAlpha;
		public TweenScale scaledImage;
		public GameObject Pan_error;
		public GameObject soup_error;

		void Start () 
		{
			if(isSoupContainer)
			{
				myType = LevelManager.Orders.SOUP;
			}
			else
			{
				myType = LevelManager.Orders.NOODLES;
			}

			myOriginalPos = transform.position;
		}


		void OnDisable()
		{
			transform.localScale = Vector3.one;
			reachedDestination = false;
			iAmSelected = false;
			mySelection.SetActive (false);
		}

		void ResetUtensil()
		{
			vegAdded = false;

		}

		void Update () {
			if(noOfServingsAvailable > 0 && !isBurnt && !isPicked && isPan)
			{
				heatingTimer+=Time.deltaTime;
				if(heatingTimer > perfectTimer && heatingTimer <= burningTimer)
				{
					if(tutorialOn && !tutorialPick)
					{
						tutorialPick = true;
						_uiManager.tutorialPanelBg.gameObject.SetActive (true);
						_chinaManager.noodlesPlatesMotion[0].tutorialOn = true;
						if(isPan)
							_uiManager.tutorialPanelBg.OpenPopupChina ("TAP OR DRAG NOODLES ON \n THE PLATE.",false,false , 3);
					}
					if(!myReadyFood.activeInHierarchy)
					{
						myAlpha.ResetToBeginning ();
						myAlpha.PlayForward ();
						myReadyFood.gameObject.SetActive (true);
						myReadyFood.GetComponent<SpriteRenderer>().sprite = _chinaManager.noodlesInPanVariations[3];
					}
				}
				else if(heatingTimer > burningTimer && !isBurnt)
				{
					if(!TutorialPanel.popupPanelActive)
					{
						isBurnt = true;  // burnt
						myReadyFood.GetComponent<SpriteRenderer>().sprite = _chinaManager.noodlesInPanVariations[4];
						mySmoke.gameObject.SetActive (true);
						mySmoke.Play ();
					}
				}
			}
		}


	
		bool canMove;
		bool tutorialPick;

		/// <summary>
		/// Raises the mouse down event.
		/// </summary>
		void OnMouseDown()
		{
			if ((!TutorialPanel.popupPanelActive || China_Manager.tutorialEnd || tutorialPick ||
			     (isSoupContainer && tutorialOn)) && (noOfServingsAvailable > 0) && (noodlesAdded && vegAdded))
			{
				isPicked = true;
		
				scaleUp = true;
				canMove = true;
				_chinaManager.AllClickedBoolsReset();
				_chinaManager.clickedUtensilsDestinationFunction = this;
				iAmSelected = true;
				if (isSoupContainer)
				{
					_chinaManager.clickedSoupContainer = true;
					if (tutorialOn)
						_chinaManager.firstSoupBowl.tutorialOn = true;
				}
				else
				{
					_chinaManager.clickedPan = true;
				}

				if (!isBurnt || isSoupContainer)
					myImage.sprite = ladleVariations[1];
				else
					myImage.sprite = ladleVariations[2];
				for (int i = 0; i < fullCollider.Length; i++)
				{
					fullCollider[i].enabled = false;
				}

				tipCollider.enabled = true;

				Vector3 myPos = Camera.main.WorldToScreenPoint(transform.position);
				myTouchPos =
					Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, myPos.z));

			}
			else if ((!TutorialPanel.popupPanelActive || !China_Manager.tutorialEnd) &&
			         noOfServingsAvailable <= 0) 
			{
				if (isPan)
				{
					if (_chinaManager.clickedNoodlesVeg && !vegAdded)
					{
						_chinaManager.clickedItemDestinationFunction.utensil = this;
						_chinaManager.ObjectReached();
						_chinaManager.AllClickedBoolsReset();
					}
					else if (_chinaManager.clickedNoodlesToCook && !noodlesAdded)
					{
						_chinaManager.clickedItemDestinationFunction.utensil = this;
						_chinaManager.ObjectReached();
						_chinaManager.AllClickedBoolsReset();
					}
					else
					{
						Pan_error.SetActive(true);
					}
				}
				else if (isSoupContainer)
				{
					if (_chinaManager.clickedSoupVeg && !vegAdded)
					{
						_chinaManager.clickedItemDestinationFunction.utensil = this;
						_chinaManager.ObjectReached();
						_chinaManager.AllClickedBoolsReset();
					}
					else
					{
						soup_error.SetActive(true);
					}
				}
			}
		}

		private void OnMouseDrag()
		{
			if( canMove)
			{
				Pan_error.SetActive(false);
				soup_error.SetActive(false);
				Vector3 myPos = Camera.main.WorldToScreenPoint (transform.position);
				Vector3 newPos = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y , myPos.z));
				if(Vector3.Distance (newPos,myTouchPos) > 0.2f)
				{
					transform.position =  newPos;
				}
			
			}
		}


		private void OnMouseUp()
		{
			Pan_error.SetActive (false);
			soup_error.SetActive (false);
			if(canMove)
			{
				if(!reachedDestination)
					StartCoroutine(MoveToPosition());
				else
				{
					if(isSoupContainer)
					{
						ClickedDestination();
						StartCoroutine(MoveToPosition(false));
					}
					else if(isPan)
					{
						ClickedDestination();
						StartCoroutine(MoveToPosition(false));

					
					}
				}
				canMove = false;
				isPicked = false;
			}
			tipCollider.enabled = false;
			for(int i = 0 ; i < fullCollider.Length ; i++)
			{
				fullCollider[i].enabled = true;
			}
		}
		public void Deactivedustbin()
		{
			_uiManager.dustbin_textparent.SetActive (false);
			_uiManager.dustbin_textparent.transform.position = _uiManager.dustbintextintialposition;
		}

		public void ClickedDestination()
		{
			if(otherObject.name.Contains ("dustbin"))
			{
				if(isSoupContainer)
				{
					isBurnt = false;
					heatingTimer = 0;
					noOfServingsAvailable= 0;
					myAlpha.gameObject.SetActive (false);
					myFood.gameObject.SetActive (false);
					myReadyFood.GetComponent<SpriteRenderer>().sprite = _chinaManager.soupContainerVariations[0];
					myImage.sprite = ladleVariations[0];
					vegAdded = false;
					_chinaManager.clickedSoupContainer = false;
					_uiManager.totalCoins -= _chinaManager.soupPrice;
					_uiManager.coinsText.text = _uiManager.totalCoins.ToString ();
			
				}
				else if(isPan)
				{
					isBurnt = false;
					heatingTimer = 0;
					mySmoke.gameObject.SetActive (false);
					noOfServingsAvailable = 0;
					scaledImage.gameObject.SetActive (false);
					myFood.gameObject.SetActive (false);
					myReadyFood.gameObject.SetActive (false);
					myImage.sprite = ladleVariations[0];
					noodlesAdded = false;
					vegAdded = false;
					_chinaManager.clickedPan = false;
					_uiManager.totalCoins-=_chinaManager.perfectNoodlesPrice;
					_uiManager.coinsText.text = _uiManager.totalCoins.ToString ();
					_levelSoundManager.dustbin.Play();
					if(_uiManager.totalCoins > 0) 
					{
						_uiManager.dustbin_textparent.SetActive(true);
						_uiManager.dustbin_text.text = "-"+_chinaManager.perfectNoodlesPrice.ToString(); 
						Invoke("Deactivedustbin",1.0f);
					}
					if(_uiManager.totalCoins < 0)
					{
						_uiManager.totalCoins = 0;
						_uiManager.coinsText.text = "0";
					}
				}
				iAmSelected = false;
			}
			else
			{
				if(isSoupContainer)
				{
					if(otherObject != null)
					{
						ObjectMotion otherObjectMotion = otherObject.GetComponent<ObjectMotion>();
						if(!otherObjectMotion.mySoup.activeInHierarchy)
						{
							otherObjectMotion.mySoup.SetActive (true);
							otherObjectMotion.soupScale.ResetToBeginning ();                             
							otherObjectMotion.soupScale.PlayForward ();
							otherObjectMotion.myType = LevelManager.Orders.SOUP;
							noOfServingsAvailable--;
							if(noOfServingsAvailable <= 0)
							{
								isBurnt = false;
								heatingTimer = 0;
								myAlpha.gameObject.SetActive (false);
								myFood.gameObject.SetActive (false);
								myReadyFood.GetComponent<SpriteRenderer>().sprite = _chinaManager.soupContainerVariations[0];
								myImage.sprite = ladleVariations[0];
								vegAdded = false;
							}
							if(tutorialOn)
							{
								tutorialPick = false;
								_chinaManager.firstSoupBowl.tutorialOn = true;
								_uiManager.tutorialPanelBg.OpenPopupChina ("DRAG TO THE \n CUSTOMER.",false,false , 6);
								tutorialOn = false;
							}
							iAmSelected = false;
					
							_chinaManager.clickedSoupContainer = false;
						}
					}
				}
				else if(isPan)
				{
					if(otherObject != null)
					{
						ObjectMotion otherObjectMotion = otherObject.GetComponent<ObjectMotion>();
					
						if(!otherObjectMotion.myNoodles.activeInHierarchy)
						{
							if(tutorialOn)
							{
								tutorialPick = false;
								_chinaManager.firstCustomer.tutorialOn = true;
								_chinaManager.noodlesPlatesMotion[0].tutorialOn = true;
								_uiManager.tutorialPanelBg.OpenPopupChina ("DRAG TO THE \n CUSTOMER.",false,false , 6);
								tutorialOn = false;
							}
							otherObjectMotion.myNoodles.SetActive (true);
							otherObjectMotion.myType = LevelManager.Orders.NOODLES;
							if(heatingTimer >= perfectTimer && !isBurnt)
							{
								otherObjectMotion.perfect = true;
								otherObjectMotion.myNoodles.GetComponent<SpriteRenderer>().sprite = _chinaManager.noodlesInPlateVariations[1];
							
							}
							else if(!isBurnt)
							{
								otherObjectMotion.myNoodles.GetComponent<SpriteRenderer>().sprite = _chinaManager.noodlesInPlateVariations[0];
							}
							noOfServingsAvailable--;
							if(noOfServingsAvailable <= 0)
							{
								mySmoke.gameObject.SetActive (false);
								isBurnt = false;
								scaledImage.gameObject.SetActive (false);
								heatingTimer = 0;
								myFood.gameObject.SetActive (false);
								myReadyFood.gameObject.SetActive (false);
								myImage.sprite = ladleVariations[0];
								noodlesAdded = false;
								vegAdded = false;
							}
							_chinaManager.clickedPan = false;
							iAmSelected = false;
						}
					}
				}
			}
		}

		private IEnumerator MoveToPosition(bool showSelection = true)
		{
			transform.localEulerAngles = Vector3.zero;
			reachedDestination = false;
			float distance = Vector3.Distance (transform.position , myOriginalPos);
			float speed = 15;
			while(distance > 0.1f)
			{
				float step = speed * Time.deltaTime;
				transform.position = Vector3.MoveTowards(transform.position, myOriginalPos, step);
				distance = Vector3.Distance (transform.position , myOriginalPos);
				yield return 0;
			}
			if(iAmSelected && showSelection)
				mySelection.SetActive (true);
			transform.position = myOriginalPos;
		}
	
		public GameObject otherObject;


		private void OnTriggerStay(Collider other)
		{
			if(noOfServingsAvailable > 0)
			{
				if(isSoupContainer)
				{
					if(other.name.Contains ("soupBowl"))
					{
						otherObject = other.gameObject;
						ObjectMotion otherObjectMotion = otherObject.GetComponent<ObjectMotion>();
					
						if(!otherObjectMotion.mySoup.activeInHierarchy)
						{
							_levelSoundManager.bttn_click.Play();
							reachedDestination = true;
						}
					}
				}
				else if(isPan)
				{
					if(!isBurnt)
					{
						if(other.name.Contains ("plate"))
						{
							otherObject = other.gameObject;

							ObjectMotion otherObjectMotion = otherObject.GetComponent<ObjectMotion>();
						
							if(!otherObjectMotion.myNoodles.activeInHierarchy)
							{
								_levelSoundManager.bttn_click.Play();
								reachedDestination = true;
							}
						}
					}
				}
				if(China_Manager.tutorialEnd)
				{
					if(other.name.Contains("dustbin"))
					{
						otherObject = other.gameObject;
						reachedDestination = true;
					}
				}
			}
		}

		private void OnTriggerExit(Collider other)
		{

			if(isSoupContainer)
			{
				if(other.name.Contains ("soupBowl"))
				{
					if(otherObject != null)
					{
						if(otherObject == other.gameObject)
						{
							otherObject = null;
							reachedDestination = false;
						}
					}
					else
					{
						otherObject = null;
						reachedDestination = false;
					}
				}
			}
			else if(isPan)
			{
				if(!isBurnt)
				{
					if(other.name.Contains ("plate"))
					{
						if(otherObject != null)
						{
							if(otherObject == other.gameObject)
							{
								otherObject = null;
								reachedDestination = false;
							}
						}
						else
						{
							otherObject = null;
							reachedDestination = false;
						}
					}
				}
			}
			if(China_Manager.tutorialEnd)
			{
				if(other.name.Contains("dustbin"))
				{
					otherObject = null;
					reachedDestination = false;
				}
			}
		}

	}
}
