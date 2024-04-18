using System.Collections;
using _Project.Scripts.Managers;
using _Project.Scripts.Other;
using _Project.Scripts.UI_Scripts;
using _Project.Scripts.UI.Tutorial;
using UnityEngine;

namespace _Project.Scripts.Game
{
	public class ChineseUtensils : MonoBehaviour
	{
		private bool reachedDestination;
		private bool scaleUp;
		private bool isPicked;
		private float heatingTimer;
		private float perfectTimer = 6f;
		private float burningTimer = 18f;
		public bool isSoupContainer , isPan;
		public LevelManager.Orders myType;
		public bool vegAdded;
		public bool noodlesAdded;
		public bool initialIngredientsAdded;
		public int noOfServingsAvailable;
		public SpriteRenderer myImage;
		public Sprite []ladleVariations;
		public SpriteRenderer myFood;
		public GameObject myReadyFood;
		public Vector3 myOriginalPos , myTouchPos;
		public bool iAmSelected;
		public Vector3 myLocalScale;
		public GameObject mySelection;
		public BoxCollider []fullCollider ;
		public BoxCollider tipCollider;
		public bool isBurnt;
		public bool tutorialOn;
		public ParticleSystem foodCooked;
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
			myLocalScale = transform.localScale;
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
						UIManager._instance.tutorialPanelBg.gameObject.SetActive (true);
						China_Manager._instance.noodlesPlatesMotion[0].tutorialOn = true;
						if(isPan)
							UIManager._instance.tutorialPanelBg.OpenPopupChina ("TAP OR DRAG NOODLES ON \n THE PLATE.",false,false , 3);
					}
					if(!myReadyFood.activeInHierarchy)
					{
//					myFood.gameObject.SetActive (false);
						myAlpha.ResetToBeginning ();
						myAlpha.PlayForward ();
						myReadyFood.gameObject.SetActive (true);
						myReadyFood.GetComponent<SpriteRenderer>().sprite = China_Manager._instance.noodlesInPanVariations[3];
//					foodCooked.Play ();
					}
				}
				else if(heatingTimer > burningTimer && !isBurnt)
				{
					if(!TutorialPanel.popupPanelActive)
					{
						isBurnt = true;  // burnt
						myReadyFood.GetComponent<SpriteRenderer>().sprite = China_Manager._instance.noodlesInPanVariations[4];
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
				China_Manager._instance.AllClickedBoolsReset();
				China_Manager._instance.clickedUtensilsDestinationFunction = this;
				iAmSelected = true;
				if (isSoupContainer)
				{
					China_Manager._instance.clickedSoupContainer = true;
					if (tutorialOn)
						China_Manager._instance.firstSoupBowl.tutorialOn = true;
				}
				else
				{
					China_Manager._instance.clickedPan = true;
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
//			myImage.enabled = true;
				Vector3 myPos = Camera.main.WorldToScreenPoint(transform.position);
				myTouchPos =
					Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, myPos.z));

			}
			else if ((!TutorialPanel.popupPanelActive || !China_Manager.tutorialEnd) &&
			         noOfServingsAvailable <= 0) // Click on it to place ingredients
			{
				if (isPan)
				{
//				China_Manager._instance.clickedUtensilsDestinationFunction = this;
					if (China_Manager._instance.clickedNoodlesVeg && !vegAdded)
					{
						China_Manager._instance.clickedItemDestinationFunction.utensil = this;
						China_Manager._instance.ObjectReached();
						China_Manager._instance.AllClickedBoolsReset();
					}
					else if (China_Manager._instance.clickedNoodlesToCook && !noodlesAdded)
					{
						China_Manager._instance.clickedItemDestinationFunction.utensil = this;
						China_Manager._instance.ObjectReached();
						China_Manager._instance.AllClickedBoolsReset();
					}
					else
					{
						Pan_error.SetActive(true);
					}
				}
				else if (isSoupContainer)
				{
					if (China_Manager._instance.clickedSoupVeg && !vegAdded)
					{
						China_Manager._instance.clickedItemDestinationFunction.utensil = this;
						China_Manager._instance.ObjectReached();
						China_Manager._instance.AllClickedBoolsReset();
					}
					else
					{
						soup_error.SetActive(true);
					}
				}
			}
		}

		void OnMouseDrag()
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
	
	

		void OnMouseUp()
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
			UIManager._instance.dustbin_textparent.SetActive (false);
			UIManager._instance.dustbin_textparent.transform.position = UIManager._instance.dustbintextintialposition;
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
					myReadyFood.GetComponent<SpriteRenderer>().sprite = China_Manager._instance.soupContainerVariations[0];
					myImage.sprite = ladleVariations[0];
					vegAdded = false;
					China_Manager._instance.clickedSoupContainer = false;
					UIManager._instance.totalCoins-=China_Manager._instance.soupPrice;
					UIManager._instance.coinsText.text = UIManager._instance.totalCoins.ToString ();
			
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
					China_Manager._instance.clickedPan = false;
					UIManager._instance.totalCoins-=China_Manager._instance.perfectNoodlesPrice;
					UIManager._instance.coinsText.text = UIManager._instance.totalCoins.ToString ();
					LevelSoundManager._instance.dustbin.Play();
					if(UIManager._instance.totalCoins > 0) 
					{
						UIManager._instance.dustbin_textparent.SetActive(true);
						UIManager._instance.dustbin_text.text = "-"+China_Manager._instance.perfectNoodlesPrice.ToString(); 
						Invoke("Deactivedustbin",1.0f);
					}
					if(UIManager._instance.totalCoins < 0)
					{
						UIManager._instance.totalCoins = 0;
						UIManager._instance.coinsText.text = "0";
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
								myReadyFood.GetComponent<SpriteRenderer>().sprite = China_Manager._instance.soupContainerVariations[0];
								myImage.sprite = ladleVariations[0];
								vegAdded = false;
							}
							if(tutorialOn)
							{
								tutorialPick = false;
								China_Manager._instance.firstSoupBowl.tutorialOn = true;
								UIManager._instance.tutorialPanelBg.OpenPopupChina ("DRAG TO THE \n CUSTOMER.",false,false , 6);
								tutorialOn = false;
							}
							iAmSelected = false;
					
							China_Manager._instance.clickedSoupContainer = false;
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
								China_Manager._instance.firstCustomer.tutorialOn = true;
								China_Manager._instance.noodlesPlatesMotion[0].tutorialOn = true;
								UIManager._instance.tutorialPanelBg.OpenPopupChina ("DRAG TO THE \n CUSTOMER.",false,false , 6);
								tutorialOn = false;
							}
							otherObjectMotion.myNoodles.SetActive (true);
							otherObjectMotion.myType = LevelManager.Orders.NOODLES;
							if(heatingTimer >= perfectTimer && !isBurnt)
							{
								otherObjectMotion.perfect = true;
								otherObjectMotion.myNoodles.GetComponent<SpriteRenderer>().sprite = China_Manager._instance.noodlesInPlateVariations[1];
							
							}
							else if(!isBurnt)
							{
								otherObjectMotion.myNoodles.GetComponent<SpriteRenderer>().sprite = China_Manager._instance.noodlesInPlateVariations[0];
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
							China_Manager._instance.clickedPan = false;
							iAmSelected = false;
						}
					}
				}
			}
		}
		
		IEnumerator MoveToPosition(bool showSelection = true)
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
							LevelSoundManager._instance.bttn_click.Play();
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
								LevelSoundManager._instance.bttn_click.Play();
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
