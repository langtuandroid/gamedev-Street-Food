using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Achivments;
using _Project.Scripts.Additional;
using _Project.Scripts.Food;
using _Project.Scripts.Managers;
using _Project.Scripts.UI_Scripts;
using _Project.Scripts.UI.Tutorial;
using UnityEngine;

namespace _Project.Scripts.Entities.Customers
{
	public class Customer : MonoBehaviour 
	{
		Vector3 scaleOfEatableOrder;
		Vector3 scaleOfDrinkableOrder;
		Vector3 scaleOfFries;
		float eyesTimer;
		bool eyesFull;
		bool hasWhistle;
		bool hasbell ;
		bool hascupcake ;
		List<int> randomListToDecideNoPay = new(){0,1,2};
		bool allowClick;
		
		public static bool radioPurchased;
		public LevelManager.Orders iHaveAMultipleTypeOrder;
		public List<LevelManager.Orders> myOrder = new();
		public float customerWaitingTime;  // it would vary depending on the level 
		public float myWaitingTime; // how much time I have waited..
		public int positionTaken;
		public bool orderPlaced;
		public GameObject orderPanel;
		public TweenScale orderPanelTween;
		public TweenScale drinkableOrderTween;
		public TweenScale eatableOrderTween;
		public TweenScale friesTween;
		public SpriteRenderer waitingSlider;
		public Animation myAnimation;
		public SpriteRenderer eatableOrder;
		public SpriteRenderer pizzadot ;
		public GameObject plateOfEatableOrder;
		public GameObject fries;
		public Burger myBurger;
		public GameObject drinkingOrder;
		public int coinsSpent;
		public BoxCollider myCollider;
		public SpriteRenderer myFacialExpression;
		public Sprite []expressions;
		public SpriteRenderer myEyes;
		public Sprite []myEyesExpressions;
		public int noOfOrders ;
		public bool perfect;
		public bool shouldBePerfectIfServed;
		public bool tutorialOn;
		public ParticleSystem angryEffect;
		public bool leavingWithoutPaying;
		public int coinsWithoutPaying;

		private void Start () 
		{
			UIManager._instance.n_Customer_served = PlayerPrefs.GetInt ("CustomerServed");
			UIManager._instance.n_Perfect_achieved = PlayerPrefs.GetInt ("Perfectachieved");

			scaleOfEatableOrder = plateOfEatableOrder.transform.localScale;
			scaleOfDrinkableOrder = drinkingOrder.transform.localScale;
			if(Australia_Manager._instance != null)
			{
				scaleOfFries = fries.transform.localScale;
			}

			if(PlayerPrefs.HasKey ("Radio"))
			{
				radioPurchased = true;
			}
			if(PlayerPrefs.HasKey ("Whistle"))
			{
				hasWhistle = true;
			}
			if (PlayerPrefs.HasKey ("Bell"))
			{
				hasbell = true ;
			}

		}
		
		private void OnMouseDown()
		{
			if(Italy_Manager._instance != null )
			{
				if(Italy_Manager._instance.firstOvenPizza.tutorialOn)
				{
					allowClick = true;
				}

			}
			if(!TutorialPanel.popupPanelActive || US_Manager.tutorialEnd || tutorialOn || China_Manager.tutorialEnd || Italy_Manager.tutorialEnd || allowClick)
			{
				if(LevelManager.levelNo <= 10)
				{
					if(US_Manager._instance.clickedCoke)
					{
						US_Manager._instance.clickedItemDestinationFunction.customer = this;

						for(int i = 0 ; i< myOrder.Count ; i++)
						{
							if(US_Manager._instance.clickedItemDestinationFunction.myType == myOrder[i])
							{
								US_Manager._instance.ObjectReached();
								break;
							}
						}
						US_Manager._instance.AllClickedBoolsReset ();
					}
					else if(US_Manager._instance.clickedHotDog)
					{
						US_Manager._instance.clickedHotDogDestinationFunction.customer = this;
						US_Manager._instance.clickedHotDogDestinationFunction.otherObject = this.gameObject;
						bool foundOrder = false;
						for(int i = 0 ; i< myOrder.Count ; i++)
						{
							if(US_Manager._instance.clickedHotDogDestinationFunction.myType == myOrder[i])
							{
								LevelSoundManager._instance.customerEat.Play();
								US_Manager._instance.HotDogReached();
								foundOrder = true;
								break;
							}
						}
						if(!foundOrder)
						{
							if(iHaveAMultipleTypeOrder != LevelManager.Orders.NONE)
							{	


								US_Manager._instance.clickedHotDogDestinationFunction.myTypeToEat = iHaveAMultipleTypeOrder;
								US_Manager._instance.clickedHotDogDestinationFunction.wrongOrderGiven = true;
								US_Manager._instance.HotDogReached();
							}
						}
						US_Manager._instance.AllClickedBoolsReset ();
					}
					else
					{
						US_Manager._instance.AllClickedBoolsReset ();
					}
				}
				else if(LevelManager.levelNo > 10 && LevelManager.levelNo <= 20)
				{
					if(China_Manager._instance.clickedNoodlePlate)
					{
						China_Manager._instance.clickedItemDestinationFunction.customer = this;

						for(int i = 0 ; i< myOrder.Count ; i++)
						{
							if(China_Manager._instance.clickedItemDestinationFunction.myType == myOrder[i])
							{
								China_Manager._instance.ObjectReached();
								break;
							}
						}
						China_Manager._instance.AllClickedBoolsReset ();
					}
					else if(China_Manager._instance.clickSoupBowl)
					{
						Debug.Log("here....");
						China_Manager._instance.clickedItemDestinationFunction.customer = this;
						for(int i = 0 ; i< myOrder.Count ; i++)
						{
							if(China_Manager._instance.clickedItemDestinationFunction.myType == myOrder[i])
							{
								China_Manager._instance.ObjectReached();
								break;
							}
						}
						China_Manager._instance.AllClickedBoolsReset ();
					}
					else
					{
						China_Manager._instance.AllClickedBoolsReset ();
					}
				}
				else if(LevelManager.levelNo > 20 && LevelManager.levelNo <= 30)
				{
					if(Italy_Manager._instance.clickedOvenPizza)
					{
						if(!Italy_Manager._instance.clickedPizzaDestinationFunction.isBurnt)
						{
							Italy_Manager._instance.clickedPizzaDestinationFunction.customer = this;
							Italy_Manager._instance.clickedPizzaDestinationFunction.otherObject = this.gameObject;
						
							bool foundOrder = false;
							for(int i = 0 ; i< myOrder.Count ; i++)
							{
								if(Italy_Manager._instance.clickedPizzaDestinationFunction.myType == myOrder[i])
								{
									Italy_Manager._instance.PizzaReached();
									foundOrder = true;
									break;
								}
							}
							if(!foundOrder)
							{
								if(iHaveAMultipleTypeOrder != LevelManager.Orders.NONE)
								{
									Italy_Manager._instance.clickedPizzaDestinationFunction.wrongOrderGiven = true;
									Italy_Manager._instance.PizzaReached();
								}
							}
							Italy_Manager._instance.AllClickedBoolsReset ();
						}
					}
					else if(Italy_Manager._instance.clickedCoke)
					{
						Italy_Manager._instance.clickedItemDestinationFunction.customer = this;
					
						for(int i = 0 ; i< myOrder.Count ; i++)
						{
							if(Italy_Manager._instance.clickedItemDestinationFunction.myType == myOrder[i])
							{
								Italy_Manager._instance.ObjectReached();
								break;
							}
						}
						Italy_Manager._instance.AllClickedBoolsReset ();
					}
					else
					{
						Italy_Manager._instance.AllClickedBoolsReset ();
					}
				}
				else if(LevelManager.levelNo > 30 && LevelManager.levelNo <= 40)
				{
					if(Australia_Manager._instance.clickedBurger ) 
					{
						Australia_Manager._instance.clickedHotDogDestinationFunction.customer = this;
						Australia_Manager._instance.clickedHotDogDestinationFunction.otherObject = this.gameObject;
					
						bool foundOrder = false;
						for(int i = 0 ; i< myOrder.Count ; i++)
						{
							if(Australia_Manager._instance.clickedHotDogDestinationFunction.myType == myOrder[i])
							{
								Australia_Manager._instance.HotDogReached();
								foundOrder = true;
								break;
							}
						}
						if(!foundOrder)
						{
							if(iHaveAMultipleTypeOrder != LevelManager.Orders.NONE)
							{
								Australia_Manager._instance.clickedHotDogDestinationFunction.wrongOrderGiven = true;
								Australia_Manager._instance.HotDogReached();
							}
						}
						Australia_Manager._instance.AllClickedBoolsReset ();
					}
					else if(Australia_Manager._instance.clickedCoke)
					{
						Australia_Manager._instance.clickedItemDestinationFunction.customer = this;
					
						for(int i = 0 ; i< myOrder.Count ; i++)
						{
							if(Australia_Manager._instance.clickedItemDestinationFunction.myType == myOrder[i])
							{
								Australia_Manager._instance.ObjectReached();
								break;
							}
						}
						Australia_Manager._instance.AllClickedBoolsReset ();
					}
					else if(Australia_Manager._instance.cickedFries)
					{
						Australia_Manager._instance.clickedItemDestinationFunction.customer = this;
						for(int i = 0 ; i< myOrder.Count ; i++)
						{
							if(Australia_Manager._instance.clickedItemDestinationFunction.myType == myOrder[i])
							{
								Australia_Manager._instance.ObjectReached();
								break;
							}
						}
						Australia_Manager._instance.AllClickedBoolsReset ();
					}
					else
					{
						Australia_Manager._instance.AllClickedBoolsReset ();
					}
				}

				if(leavingWithoutPaying && CustomerHandler._instance.gameTimer > 0 )
				{

					LevelSoundManager._instance.caught.Play();
		
					UIManager._instance.totalCoins+=coinsWithoutPaying;
					UIManager._instance.CallIncrementCoint ();
					leavingWithoutPaying = false;
				}
			}
		}
		
		private void Update () 
		{
			if(orderPlaced)
			{
				if(radioPurchased)
					myWaitingTime+=Time.deltaTime/2f;

				else
					myWaitingTime+=Time.deltaTime;

				Expressions();
				if(US_Manager._instance != null)
				{
					if(myWaitingTime >= customerWaitingTime && US_Manager.tutorialEnd) 
					{
						orderPlaced = false;
						StartCoroutine (MoveToPosition (CustomerHandler._instance.customerEndPosition.position , false));
					
					}
				}
				else if(China_Manager._instance != null)
				{
					if(myWaitingTime >= customerWaitingTime && China_Manager.tutorialEnd) 
					{
						orderPlaced = false;
						StartCoroutine (MoveToPosition (CustomerHandler._instance.customerEndPosition.position , false));
					
					}
				}
				else if(Italy_Manager._instance != null)
				{
					if(myWaitingTime >= customerWaitingTime && Italy_Manager.tutorialEnd)
					{
						orderPlaced = false;
						StartCoroutine (MoveToPosition (CustomerHandler._instance.customerEndPosition.position , false));
					
					}
				}
				else if(Australia_Manager._instance != null)
				{
					if(myWaitingTime >= customerWaitingTime && Australia_Manager.tutorialEnd) 
					{
						orderPlaced = false;
						StartCoroutine (MoveToPosition (CustomerHandler._instance.customerEndPosition.position , false));
					
					}
				}
			}
			if(myEyes != null && orderPlaced)
			{
				eyesTimer+=Time.deltaTime;
				if(eyesFull)
				{
					if(eyesTimer > 2.0f)
					{	
						myEyes.gameObject.SetActive (true);
						if(myWaitingTime < customerWaitingTime/2f)
							myEyes.sprite = myEyesExpressions[0];
						else
							myEyes.sprite = myEyesExpressions[1];
						eyesTimer = 0;
						eyesFull = false;
					}
				}
				else
				{
					if(eyesTimer > 0.5f)
					{
						myEyes.gameObject.SetActive (false);
						eyesTimer = 0;
						eyesFull = true;
					}
				}
			}
			else if(myEyes != null && myEyes.sprite != myEyesExpressions[0])
			{
				myEyes.sprite = myEyesExpressions[0];
			}
		}


		public IEnumerator Angry_effect()
		{
			float sliderValue = 0.49f;
			bool right = false;
			while (sliderValue < 0.5f) 
			{

				if(right)
				{
					myAnimation.transform.localPosition = new Vector3(myAnimation.transform.localPosition.x+ Time.deltaTime , myAnimation.transform.localPosition.y , myAnimation.transform.localPosition.z) ;
					if(myAnimation.transform.localPosition.x > 0.04f)
					{
						right = false;
					}
				}
				else
				{
					myAnimation.transform.localPosition = new Vector3(myAnimation.transform.localPosition.x - Time.deltaTime , myAnimation.transform.localPosition.y , myAnimation.transform.localPosition.z) ;
					if(myAnimation.transform.localPosition.x < -0.04f)
					{
						right = true;
					}
				}
				sliderValue = myWaitingTime/customerWaitingTime;
				sliderValue = 1f - sliderValue;
				yield return null;
			}
			myAnimation.transform.localPosition = Vector3.zero;

		}

		private void Expressions()
		{
			if(myWaitingTime > customerWaitingTime)
				myWaitingTime = customerWaitingTime;
			float sliderValue = myWaitingTime/customerWaitingTime;
			sliderValue = 1f - sliderValue;
			if(sliderValue < 0.5f && waitingSlider.color == Color.green)
			{
				waitingSlider.color = Color.red;
				angryEffect.Play ();
				gameObject.GetComponent<AudioSource>().Play();

				StartCoroutine ("Angry_effect");
				if(myFacialExpression != null)
					myFacialExpression.sprite = expressions[1];
			}
			else if(sliderValue >= 0.5f && waitingSlider.color == Color.red)
			{
				StopCoroutine ("Angry_effect");

				angryEffect.Stop ();
				waitingSlider.color = Color.green;
				if(myFacialExpression != null)
					myFacialExpression.sprite = expressions[0];
			}
			waitingSlider.transform.localScale = new Vector3( waitingSlider.transform.localScale.x , sliderValue , waitingSlider.transform.localScale.z);
		}

		public void MoveToEndPosition()
		{
			StartCoroutine (MoveToPosition (CustomerHandler._instance.customerEndPosition.position , false));
		}
		
		public void Gold_Deactive()
		{
			UIManager._instance.gold_Collected.SetActive(false);
		}
		public	void Stopa()
		{
			UIManager._instance.achievment_text.SetActive (false);
		}
		public IEnumerator MoveToPosition(Vector3 finalPos , bool toOrder)
		{
			angryEffect.Stop();
			myAnimation.GetComponent<SpriteRenderer>().sortingOrder = -2;
			myCollider.enabled = false;
			if(myEyes != null)
			{
				myEyes.sortingOrder = -1;
			}
			if(myFacialExpression!= null)
			{
				myFacialExpression.sortingOrder = -1;
			}
			myAnimation.Play ();
			finalPos = new Vector3(finalPos.x , transform.position.y , finalPos.z);
			float distance = Vector3.Distance (transform.position , finalPos);
			float speed = 5;
			if(!toOrder)
			{
				orderPlaced = false;
				if(myOrder.Count == 0)
				{
					UIManager._instance.n_Customer_served++;

					PlayerPrefs.SetInt ("CustomerServed",UIManager._instance.n_Customer_served);
					if(PlayerPrefs.GetInt("CustomerServed") > 19 && PlayerPrefs.GetInt ("CustomerLevel1") == 0 )
					{
						PlayerPrefs.SetInt ("CustomerLevel1",1);
						UIManager._instance.achievment_text.SetActive(true);
						AchievementChild.check_claim++;
						PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
						Invoke("Stopa",4.0f);

					}
					if(PlayerPrefs.GetInt("CustomerServed") > 99 && PlayerPrefs.GetInt ("CustomerLevel2") == 0  )
					{
						PlayerPrefs.SetInt ("CustomerLevel2",1);
						UIManager._instance.achievment_text.SetActive(true);
						AchievementChild.check_claim++;
						PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
						Invoke("Stopa",4.0f);
						
					}
					if(PlayerPrefs.GetInt("CustomerServed") > 999 && PlayerPrefs.GetInt ("CustomerLevel3") == 0 )
					{
						MenuManager.golds += 5;
						UIManager._instance.achievment_text.SetActive(true);
						PlayerPrefs.SetInt ("CustomerLevel3",1);
						AchievementChild.check_claim++;
						PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
						Invoke("Stopa",4.0f);
					}
					
					if(myFacialExpression!= null)
					{
						myFacialExpression.sprite = expressions[0];
					}
				}
				else
				{
					US_Manager.noOfPerfects = 0;
				}

				if(coinsSpent > 0)
				{
					if(myWaitingTime > customerWaitingTime/2f)
					{
						if(!perfect)
						{
							if(shouldBePerfectIfServed)
								coinsSpent-=5;
							else
							{
								if(coinsSpent > 10)
									coinsSpent-=10;
								else
									coinsSpent-=5;
							}
						}
						perfect = false;
					}
					bool shouldNotPay = false;
					if(CustomerHandler._instance.canBeAnUnPayingCustomer && CustomerHandler._instance.noOfCustomers >= 4 && CustomerHandler._instance.noOfCustomers <= 16 )
					{
						if(CustomerHandler._instance.gameTimer > 0)
						{
							if(CustomerHandler._instance.noOfCustomers > 14 )
							{
								shouldNotPay = true;
							}
							else
							{
								int randomDecider = Random.Range (0,randomListToDecideNoPay.Count);
								if(randomDecider == 0)
								{
									shouldNotPay = true;

								}
							}
						}
					}

					if(shouldNotPay)
					{
						myCollider.enabled = true;
						if(PlayerPrefs.HasKey ("Whistle"))
						{
							LevelSoundManager._instance.whistle.Play();
							UIManager._instance.whistle.transform.localScale = new Vector3(1.2f,1.2f,0);
							UIManager._instance.blow.SetActive(true);
							UIManager._instance.Invoke("WhistleInitialpos",1.2f);
						}
						CustomerHandler._instance.noOfUnpayingCustomers++;
						if(CustomerHandler._instance.noOfUnpayingCustomers >= CustomerHandler._instance.maxNoOfUnpayableCustomers)
						{
							CustomerHandler._instance.canBeAnUnPayingCustomer = false;
						}
						leavingWithoutPaying = true;
						coinsWithoutPaying = coinsSpent;
						CustomerHandler._instance.availablePositions.Add (positionTaken);
					}
					else
					{
						int shouldGiveBonus = Random.Range(0,7);
						if(shouldGiveBonus == 0)
						{
							int bonusVal = UIManager._instance.Bonus_coin;
							if(!perfect)
							{
								bonusVal = Mathf.CeilToInt (bonusVal/2f);
							}
							Debug.Log("bonus Val = "+bonusVal);
							CustomerHandler._instance.coinImages[positionTaken].bonusVal = bonusVal;
							CustomerHandler._instance.coinImages[positionTaken].myAmount = coinsSpent +bonusVal;
						}
						else
						{
							CustomerHandler._instance.coinImages[positionTaken].myAmount = coinsSpent;
						}

						CustomerHandler._instance.coinImages[positionTaken].gameObject.SetActive (true);
						if(perfect)
						{
							CustomerHandler._instance.coinImages[positionTaken].perfectText.SetActive (true);
						}
						else
							CustomerHandler._instance.coinImages[positionTaken].perfectText.SetActive (false);

						if(perfect)
						{
							US_Manager.noOfPerfects++;
							UIManager._instance.n_Perfect_achieved++;
							PlayerPrefs.SetInt ("Perfectachieved", UIManager._instance.n_Perfect_achieved);
						
							if(PlayerPrefs.GetInt("Perfectachieved") > 9 && PlayerPrefs.GetInt ("PerfectLevel1")== 0 )
							{
								PlayerPrefs.SetInt ("PerfectLevel1",1)  ;
								UIManager._instance.achievment_text.SetActive(true);
								AchievementChild.check_claim++;
								PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
								Invoke(nameof(Stopa),4.0f);
							
							}
							if(PlayerPrefs.GetInt("Perfectachieved") > 99 && PlayerPrefs.GetInt ("PerfectLevel2")== 0 )
							{
								PlayerPrefs.SetInt ("PerfectLevel2",1);
								UIManager._instance.achievment_text.SetActive(true);
								AchievementChild.check_claim++;
								PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
								Invoke(nameof(Stopa),4.0f);
							
							}
							if(PlayerPrefs.GetInt("Perfectachieved") > 999 && PlayerPrefs.GetInt ("PerfectLevel3")== 0 )
							{
								PlayerPrefs.SetInt ("PerfectLevel3",1);
								UIManager._instance.achievment_text.SetActive(true);
								AchievementChild.check_claim++;
								PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
								Invoke(nameof(Stopa),4.0f);
							
							}
							
							if(US_Manager.noOfPerfects >= 5)
							{
								UIManager._instance.gold_Collected.SetActive(false);
								UIManager._instance.gold_Collected.SetActive(true);
							
								Debug.Log ("added gold");
								Invoke(nameof(GoldAdd),1.5f);
								Invoke("Gold_Deactive ",1.5f);
								US_Manager.noOfPerfects = 0;
							
							}
						}
						else
						{
							Debug.Log ("perfects made 0");
							US_Manager.noOfPerfects = 0;
						}
						
						CustomerHandler._instance.coinImages[positionTaken].coinCollected.Play ();
						CustomerHandler._instance.coinImages[positionTaken].positionTaken = positionTaken;
						CustomerHandler._instance.coinImages[positionTaken].addValue.text = "+"+coinsSpent;
					}
				}
				else
				{
					CustomerHandler._instance.availablePositions.Add (positionTaken);
					if(CustomerHandler._instance.timerStopped)
					{
						if(CustomerHandler._instance.availablePositions.Count == 5)
						{
							UIManager._instance.OnGameOver ();
						}
					}
				}


				if(myEyes != null)
					myEyes.gameObject.SetActive (false);
				positionTaken = -1;
				myOrder.Clear ();
				orderPanelTween.duration = 0.15f;
				orderPanelTween.PlayReverse ();
				if(!leavingWithoutPaying)
					speed = 7;
				else
					speed = 4;
				shouldBePerfectIfServed = false;
			}
			else
			{
				iHaveAMultipleTypeOrder = LevelManager.Orders.NONE;
				shouldBePerfectIfServed = false;
		
				angryEffect.Stop ();
				if(myFacialExpression!= null)
				{
					myFacialExpression.sprite = expressions[0];
				}
			}
			coinsSpent = 0;
			myWaitingTime = 0;
			while(distance > 0.1f)
			{
				float step = speed * Time.deltaTime;
				transform.position = Vector3.MoveTowards(transform.position, finalPos, step);
				distance = Vector3.Distance (transform.position , finalPos);
				yield return 0;
			}

			if(toOrder)
			{
				myCollider.enabled = true;
				transform.position = finalPos;
				waitingSlider.color = Color.green;
				orderPanel.SetActive (true);
				orderPanelTween.duration = 0.5f;
				orderPanelTween.PlayForward ();
				orderPlaced = true;
				GetOrder ();

			}
			else
			{
				if(Australia_Manager._instance != null)
				{
					friesTween.enabled = false;
				}
				leavingWithoutPaying = false;
				eatableOrderTween.enabled = false;
				drinkableOrderTween.enabled = false;
				orderPanel.SetActive (false);
				transform.position = new Vector3(CustomerHandler._instance.customerStartPosition.position.x , transform.position.y , CustomerHandler._instance.customerStartPosition.position.z);
				CustomerHandler._instance.customerPool.Add (this);
			}
			myAnimation.Stop ();
			myAnimation.GetComponent<SpriteRenderer>().sortingOrder = 0;
			if(myEyes != null)
			{
				myEyes.sortingOrder = 1;
			}
			if(myFacialExpression != null)
			{
				myFacialExpression.sortingOrder = 1;
			}
			coinsSpent = 0;

			int rand = Random.Range (0, 8);
			if (rand == 4) {
				LevelSoundManager._instance.come_random.Play();
			}
			perfect = false;
		}

		private void GetOrder()
		{
			plateOfEatableOrder.SetActive (false);
			drinkingOrder.SetActive (false);
			if(LevelManager.levelNo <= 10)
			{
				USOrders ();
			}
			else if(LevelManager.levelNo > 10 && LevelManager.levelNo <= 20)
			{
				ChinaOrders ();
			}
			else if(LevelManager.levelNo > 20 && LevelManager.levelNo <= 30)
			{
				ItalyOrders ();
			}
			else
			{
				AustraliaOrder();
			}
		}


		private void USOrders()
		{
			if(LevelManager.levelNo == 1 ){
				noOfOrders = 1;
				myOrder.Add ((LevelManager.Orders)1);
				CheckForOrder(1);
			}
			else if(LevelManager.levelNo == 2)
			{
				noOfOrders = 1;
				int valueToadd = Random.Range (1,4);  // find any one out of 3
				myOrder.Add ((LevelManager.Orders)valueToadd);
				CheckForOrder(valueToadd);
			}
			else
			{
				noOfOrders = Random.Range(1,3);
				if(noOfOrders == 1)
				{
					int valueToadd = Random.Range (1,5);  // find any one out of 4
					myOrder.Add ((LevelManager.Orders)valueToadd);
					CheckForOrder(valueToadd);
				
				}
				else
				{
					myOrder.Add ((LevelManager.Orders)4);  // one would de coke for sure
					CheckForOrder(4);
					int valueToadd = Random.Range (1,4);
					myOrder.Add ((LevelManager.Orders)valueToadd);
					CheckForOrder(valueToadd);
				}
			}
		}
		public void GoldAdd()
		{
			MenuManager.golds++;
			PlayerPrefs.SetString("Golds",EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
			UIManager._instance.goldText.text = MenuManager.golds.ToString ();
			LevelSoundManager._instance.coinAdd.Play ();
		}

		private void ChinaOrders()
		{
			if(LevelManager.levelNo == 11 || LevelManager.levelNo == 12){
				noOfOrders = 1;
				myOrder.Add ((LevelManager.Orders)5);
				CheckForOrder(5);
			}
			else if(LevelManager.levelNo == 13)
			{
				noOfOrders = 1;
				int valueToadd = 0;
				if(CustomerHandler._instance.noOfCustomers == 1)
				{
					valueToadd = 6;
				}
				else
				{
					valueToadd = Random.Range (5,7);  
				}
				myOrder.Add ((LevelManager.Orders)valueToadd);
				CheckForOrder(valueToadd);
			}
			else
			{
				noOfOrders = Random.Range(1,3);
				if(noOfOrders == 1)
				{
					int valueToadd = Random.Range (5,7);  
					myOrder.Add ((LevelManager.Orders)valueToadd);
					CheckForOrder(valueToadd);
				
				}
				else
				{
					myOrder.Add ((LevelManager.Orders)5);  
					CheckForOrder(5);
					myOrder.Add ((LevelManager.Orders)6); 
					CheckForOrder(6);
				}
			}
		}


		private void ItalyOrders()
		{
			if(LevelManager.levelNo == 21 ){
				noOfOrders = 1;
				myOrder.Add ((LevelManager.Orders)7);
				CheckForOrder(7);
			}
			else if(LevelManager.levelNo == 22)
			{
				noOfOrders = 1;
				int valueToadd = 0;
				if(CustomerHandler._instance.noOfCustomers == 1)
				{
					valueToadd = 8;
				}
				else
				{
					valueToadd = Random.Range (7,9);  
				}
				myOrder.Add ((LevelManager.Orders)valueToadd);
				CheckForOrder(valueToadd);
			}
			else
			{
				noOfOrders = Random.Range(1,3);
				if(noOfOrders == 1)
				{
					int valueToadd = Random.Range (7,10);  
					myOrder.Add ((LevelManager.Orders)valueToadd);
					CheckForOrder(valueToadd);
				
				}
				else
				{
					myOrder.Add ((LevelManager.Orders)9); 
					CheckForOrder(9);
					int valueToadd = Random.Range (7,9);
					myOrder.Add ((LevelManager.Orders)valueToadd);
					CheckForOrder(valueToadd);
				}
			}
		}

		private void AustraliaOrder()
		{
			if(LevelManager.levelNo == 31 ){

				if(CustomerHandler._instance.noOfCustomers == 1)
				{
					noOfOrders = 1;
					myOrder.Add ((LevelManager.Orders)10);
					CheckForOrder(10);
				}
				else
				{
					noOfOrders = 2;
					myOrder.Add ((LevelManager.Orders)11); 
					CheckForOrder(11);
					myOrder.Add ((LevelManager.Orders)10);
					CheckForOrder(10);
				}
			}
			else if(LevelManager.levelNo == 32)
			{
				int valueToadd = 0;
				if(CustomerHandler._instance.noOfCustomers == 1)
				{
					noOfOrders = 1;
					valueToadd = 12;
					myOrder.Add ((LevelManager.Orders)valueToadd);
					CheckForOrder(valueToadd);
					shouldBePerfectIfServed = true;
				}
				else
				{
					noOfOrders = 2;
					List <int>order = new List<int>(){10,11,12};
					valueToadd = order[Random.Range (0,order.Count)];
					myOrder.Add ((LevelManager.Orders)valueToadd);
					CheckForOrder(valueToadd);
					order.Remove (valueToadd);
					valueToadd = order[Random.Range (0,order.Count)];
					myOrder.Add ((LevelManager.Orders)valueToadd);
					CheckForOrder(valueToadd);
					order.Remove (valueToadd);
					if(order[0] == 10)
					{
						shouldBePerfectIfServed = true;
					}
				}

			}
			else if(LevelManager.levelNo == 33)
			{
				int valueToadd = 0;
				noOfOrders = 2;
				List <int>order = new List<int>(){10,11,12};
				valueToadd = order[Random.Range (0,order.Count)];
				order.Remove (valueToadd);
				if(valueToadd == 10)
				{
					List <int>burgerOrder = new List<int>(){10,13,14};
					valueToadd = burgerOrder[Random.Range (0,burgerOrder.Count)];
				}
				myOrder.Add ((LevelManager.Orders)valueToadd);
				CheckForOrder(valueToadd);

				valueToadd = order[Random.Range (0,order.Count)];
				order.Remove (valueToadd);
				if(valueToadd == 10)
				{
					List <int>burgerOrder = new List<int>(){10,13,14};
					valueToadd = burgerOrder[Random.Range (0,burgerOrder.Count)];
				}
				myOrder.Add ((LevelManager.Orders)valueToadd);
				CheckForOrder(valueToadd);

				if(order[0] == 10)
				{
					shouldBePerfectIfServed = true;
				}
			}
			else if(LevelManager.levelNo == 34)
			{
				int valueToadd = 0;
				noOfOrders = 2;
				List <int>order = new List<int>(){10,11,12};
				valueToadd = order[Random.Range (0,order.Count)];
				order.Remove (valueToadd);
				if(valueToadd == 10)
				{
					List <int>burgerOrder = new List<int>(){10,13,14,15};
					valueToadd = burgerOrder[Random.Range (0,order.Count)];
				}
				myOrder.Add ((LevelManager.Orders)valueToadd);
				CheckForOrder(valueToadd);

				valueToadd = order[Random.Range (0,order.Count)];
				order.Remove (valueToadd);
				if(valueToadd == 10)
				{
					List <int>burgerOrder = new List<int>(){10,13,14,15};
					valueToadd = burgerOrder[Random.Range (0,order.Count)];
				}
				myOrder.Add ((LevelManager.Orders)valueToadd);
				CheckForOrder(valueToadd);

				if(order[0] == 10)
				{
					shouldBePerfectIfServed = true;
				}
			}
			else if(LevelManager.levelNo == 35)
			{
				int valueToadd = 0;
				noOfOrders = Random.Range (2,4);
				List <int>order = new List<int>(){10,11,12};
				valueToadd = order[Random.Range (0,order.Count)];
				order.Remove (valueToadd);
				if(valueToadd == 10)
				{
					List <int>burgerOrder = new List<int>(){10,13,14,15,16,17,18};
					valueToadd = burgerOrder[Random.Range (0,burgerOrder.Count)];
				}
				myOrder.Add ((LevelManager.Orders)valueToadd);
				CheckForOrder(valueToadd);
			
				valueToadd = order[Random.Range (0,order.Count)];
				order.Remove (valueToadd);
				if(valueToadd == 10)
				{
					List <int>burgerOrder = new List<int>(){10,13,14,15,16,17,18};
					valueToadd = burgerOrder[Random.Range (0,burgerOrder.Count)];
				}
				myOrder.Add ((LevelManager.Orders)valueToadd);
				CheckForOrder(valueToadd);

				if(noOfOrders == 3)
				{
					valueToadd = order[0];
					if(valueToadd == 10)
					{
						List <int>burgerOrder = new List<int>(){10,13,14,15,16,17,18};
						valueToadd = burgerOrder[Random.Range (0,burgerOrder.Count)];
					}
					myOrder.Add ((LevelManager.Orders)valueToadd);
					CheckForOrder(valueToadd);
				}
				else if(order[0] == 10)
				{
					shouldBePerfectIfServed = true;
				}
			}
			else if(LevelManager.levelNo >= 36 && LevelManager.levelNo <= 38)
			{
				int valueToadd = 0;
				noOfOrders = Random.Range (2,4);
				List <int>order = new List<int>(){10,11,12};
				valueToadd = order[Random.Range (0,order.Count)];
				order.Remove (valueToadd);
				if(valueToadd == 10)
				{
					List <int>burgerOrder = new List<int>(){10,13,14,15,16,17,18,19};
					valueToadd = burgerOrder[Random.Range (0,burgerOrder.Count)];
				}
				myOrder.Add ((LevelManager.Orders)valueToadd);
				CheckForOrder(valueToadd);
			
				valueToadd = order[Random.Range (0,order.Count)];
				order.Remove (valueToadd);
				if(valueToadd == 10)
				{
					List <int>burgerOrder = new List<int>(){10,13,14,15,16,17,18,19};
					valueToadd = burgerOrder[Random.Range (0,burgerOrder.Count)];
				}
				myOrder.Add ((LevelManager.Orders)valueToadd);
				CheckForOrder(valueToadd);
			
				if(noOfOrders == 3)
				{
					valueToadd = order[0];
					if(valueToadd == 10)
					{
						List <int>burgerOrder = new List<int>(){10,13,14,15,16,17,18,19};
						valueToadd = burgerOrder[Random.Range (0,burgerOrder.Count)];
					}
					myOrder.Add ((LevelManager.Orders)valueToadd);
					CheckForOrder(valueToadd);
				}
				else if(order[0] == 10)
				{
					shouldBePerfectIfServed = true;
				}
			}
			else if(LevelManager.levelNo == 39)
			{
				int valueToadd = 0;
				noOfOrders = 3;

				List <int>burgerOrder = new List<int>(){16,17,18,19};
				valueToadd = burgerOrder[Random.Range (0,burgerOrder.Count)];
				myOrder.Add ((LevelManager.Orders)valueToadd);
				CheckForOrder(valueToadd);
			

				myOrder.Add ((LevelManager.Orders)11);
				CheckForOrder(11);
	
				myOrder.Add ((LevelManager.Orders)12);
				CheckForOrder(12);
			}
			else
			{
				noOfOrders = 3;

				myOrder.Add ((LevelManager.Orders)19);
				CheckForOrder(19);

				myOrder.Add ((LevelManager.Orders)11);
				CheckForOrder(11);
			
				myOrder.Add ((LevelManager.Orders)12);
				CheckForOrder(12);
			}
		}

		private void CheckForOrder(int orderNo)
		{
			if(orderNo == 4 || orderNo == 6 || orderNo == 9 || orderNo == 11)  //drinkable Orders
			{
				if(noOfOrders == 1)
				{
					shouldBePerfectIfServed = true;
				}
				drinkableOrderTween.enabled = false;
			
				drinkingOrder.SetActive (true);
				drinkingOrder.transform.localScale = scaleOfDrinkableOrder;
			}
			else if(orderNo != 12)
			{
				if(orderNo != 5)
					iHaveAMultipleTypeOrder = (LevelManager.Orders)orderNo;

				eatableOrderTween.enabled = false;
				plateOfEatableOrder.SetActive (true);
				plateOfEatableOrder.transform.localScale = scaleOfEatableOrder;
				SetEatableSprite(orderNo);
			
			}
			else
			{
				friesTween.enabled = false;
				fries.SetActive (true);
				fries.transform.localScale = scaleOfFries;
			}


		}

		private void SetEatableSprite(int orderNo)
		{ 
			if(LevelManager.levelNo <= 10)
			{
				eatableOrder.sprite = US_Manager._instance.hotDogOrderVariations[orderNo-1];
			}
			else if(LevelManager.levelNo > 20 && LevelManager.levelNo <= 30)
			{
				eatableOrder.sprite = Italy_Manager._instance.pizzaBakedVariations[orderNo-6];
				pizzadot.sprite = Italy_Manager._instance.pizzaDot[orderNo-6];


			}
			else if(LevelManager.levelNo > 30 && LevelManager.levelNo <= 40)
			{
				switch(orderNo)
				{
					case 10:
						myBurger.myTomato.gameObject.SetActive (false);
						myBurger.myOnion.gameObject.SetActive (false);
						myBurger.myCabbage.gameObject.SetActive (false);
						break;
					case 13:
						myBurger.myTomato.gameObject.SetActive (true);
						myBurger.myOnion.gameObject.SetActive (false);
						myBurger.myCabbage.gameObject.SetActive (false);
						break;
					case 14:
						myBurger.myTomato.gameObject.SetActive (false);
						myBurger.myOnion.gameObject.SetActive (true);
						myBurger.myCabbage.gameObject.SetActive (false);
						break;
					case 15:
						myBurger.myTomato.gameObject.SetActive (false);
						myBurger.myOnion.gameObject.SetActive (false);
						myBurger.myCabbage.gameObject.SetActive (true);
						break;
					case 16:
						myBurger.myTomato.gameObject.SetActive (true);
						myBurger.myOnion.gameObject.SetActive (true);
						myBurger.myCabbage.gameObject.SetActive (false);
						break;
					case 17:
						myBurger.myTomato.gameObject.SetActive (true);
						myBurger.myOnion.gameObject.SetActive (false);
						myBurger.myCabbage.gameObject.SetActive (true);
						break;
					case 18:
						myBurger.myTomato.gameObject.SetActive (false);
						myBurger.myOnion.gameObject.SetActive (true);
						myBurger.myCabbage.gameObject.SetActive (true);
						break;
					case 19:
						myBurger.myTomato.gameObject.SetActive (true);
						myBurger.myOnion.gameObject.SetActive (true);
						myBurger.myCabbage.gameObject.SetActive (true);
						break;
				}

			}
		}

		public void RemoveOrderFromBoard(LevelManager.Orders orderNo)
		{

			if((int)orderNo == 4 || (int)orderNo == 6 || (int)orderNo == 9 || (int)orderNo == 11)  //drinkable Orders
			{
				drinkableOrderTween.ResetToBeginning ();
				drinkableOrderTween.PlayForward ();
			}
			else if((int)orderNo != 12)
			{
				eatableOrderTween.ResetToBeginning ();
				eatableOrderTween.PlayForward ();
			}
			else
			{
				friesTween.ResetToBeginning ();
				friesTween.PlayForward ();
			}
		}
	}
}

