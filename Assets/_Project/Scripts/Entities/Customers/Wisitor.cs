using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Achivments;
using _Project.Scripts.Additional;
using _Project.Scripts.Food;
using _Project.Scripts.Managers;
using _Project.Scripts.UI_Scripts;
using _Project.Scripts.UI.Tutorial;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Scripts.Entities.Customers
{
	public class Wisitor : MonoBehaviour
	{
		public static bool _isRadioBought;
		[Inject] private WisitorHandler _customerHandler;
		[Inject] private LevelSoundManager _levelSoundManager;
		[Inject] private US_Manager _usManager;
		[Inject] private Italy_Manager _italyManager;
		[Inject] private China_Manager _chinaManager;
		[Inject] private Australia_Manager _australiaManager;
		[Inject] private UIManager _uiManager;
		private Vector3 _orderScale;
		private Vector3 _drinkScale;
		private Vector3 _friesScale;
		private float _timer;
		private bool _full;
		private bool _isWistle;
		private bool _isBell ;
		private bool _isCupCake ;
		private List<int> _noPay = new(){0,1,2};
		private bool _isClick;
		private float _waitingTime = 60;
		private bool _isLeavingNoPay;
		private int _coinsNoPay;
		private bool _orderPlaces;
		
		[FormerlySerializedAs("orderPanel")] [SerializeField] private GameObject _orderPanel;
		[FormerlySerializedAs("orderPanelTween")] [SerializeField] private TweenScale _orderTween;
		[FormerlySerializedAs("drinkableOrderTween")] [SerializeField] private TweenScale _drinkableOrderTween;
		[FormerlySerializedAs("eatableOrderTween")] [SerializeField] private TweenScale _eatableOrderTween;
		[FormerlySerializedAs("friesTween")] [SerializeField] private TweenScale _friesTween;
		[FormerlySerializedAs("angryEffect")] [SerializeField] private ParticleSystem _angryEffect;
		[FormerlySerializedAs("myEyes")] [SerializeField] private SpriteRenderer _eyes;
		[FormerlySerializedAs("myEyesExpressions")] [SerializeField] private Sprite []_eyesExpressions;
		[FormerlySerializedAs("myCollider")] [SerializeField] private BoxCollider _collider;
		[FormerlySerializedAs("myAnimation")] [SerializeField] private Animation _animation;
		[FormerlySerializedAs("eatableOrder")] [SerializeField] private SpriteRenderer _orderEat;
		[FormerlySerializedAs("pizzadot")] [SerializeField] private SpriteRenderer _pizzaDOt;
		[FormerlySerializedAs("plateOfEatableOrder")] [SerializeField] private GameObject _orderPlate;
		[FormerlySerializedAs("fries")] [SerializeField] private GameObject _fricesPrefav;
		[FormerlySerializedAs("myBurger")] [SerializeField] private BurgerFood _burger;
		[FormerlySerializedAs("drinkingOrder")] [SerializeField] private GameObject _drinkableOrder;
		
		[FormerlySerializedAs("myOrder")] public List<LevelManager.Orders> _order = new();
		[FormerlySerializedAs("myFacialExpression")] public SpriteRenderer _facialExpression;
		[FormerlySerializedAs("expressions")] public Sprite []_expressions;
		[FormerlySerializedAs("waitingSlider")] public SpriteRenderer _sliderWait;
		public LevelManager.Orders iHaveAMultipleTypeOrder { get; set; } = LevelManager.Orders.NONE;
		public int coinsSpent { get; set; }
		private int noOfOrders { get; set; }
		public bool perfect { get; set; }
		public bool shouldBePerfectIfServed { get; set; }
		public bool tutorialOn { get; set; }
		public float myWaitingTime { get; set; }
		public int positionTaken { get; set; }

		private void Start () 
		{
			_uiManager.n_Customer_served = PlayerPrefs.GetInt ("CustomerServed");
			_uiManager.n_Perfect_achieved = PlayerPrefs.GetInt ("Perfectachieved");

			_orderScale = _orderPlate.transform.localScale;
			_drinkScale = _drinkableOrder.transform.localScale;
			if(_australiaManager != null)
			{
				_friesScale = _fricesPrefav.transform.localScale;
			}

			if(PlayerPrefs.HasKey ("Radio"))
			{
				_isRadioBought = true;
			}
			if(PlayerPrefs.HasKey ("Whistle"))
			{
				_isWistle = true;
			}
			if (PlayerPrefs.HasKey ("Bell"))
			{
				_isBell = true ;
			}

		}
		
		private void OnMouseDown()
		{
			if(_italyManager != null )
			{
				if(_italyManager.firstOvenPizza.tutorialOn)
				{
					_isClick = true;
				}

			}
			if(!TutorialPanel.popupPanelActive || US_Manager.tutorialEnd || tutorialOn || China_Manager._endTutorial || Italy_Manager.tutorialEnd || _isClick)
			{
				if(LevelManager.levelNo <= 10)
				{
					if(_usManager.clickedCoke)
					{
						_usManager.clickedItemDestinationFunction.customer = this;

						for(int i = 0 ; i< _order.Count ; i++)
						{
							if(_usManager.clickedItemDestinationFunction.myType == _order[i])
							{
								_usManager.ObjectReached();
								break;
							}
						}
						_usManager.AllClickedBoolsReset ();
					}
					else if(_usManager.clickedHotDog)
					{
						_usManager.clickedHotDogDestinationFunction.wisitor = this;
						_usManager.clickedHotDogDestinationFunction._otherObject = this.gameObject;
						bool foundOrder = false;
						for(int i = 0 ; i< _order.Count ; i++)
						{
							if(_usManager.clickedHotDogDestinationFunction.isTape == _order[i])
							{
								_levelSoundManager.customerEat.Play();
								_usManager.HotDogReached();
								foundOrder = true;
								break;
							}
						}
						if(!foundOrder)
						{
							if(iHaveAMultipleTypeOrder != LevelManager.Orders.NONE)
							{	
								_usManager.clickedHotDogDestinationFunction.wrongOrder = true;
								_usManager.HotDogReached();
							}
						}
						_usManager.AllClickedBoolsReset ();
					}
					else
					{
						_usManager.AllClickedBoolsReset ();
					}
				}
				else if(LevelManager.levelNo > 10 && LevelManager.levelNo <= 20)
				{
					if(_chinaManager.IsClickedNoodlePlate)
					{
						_chinaManager.clickedItemDestinationFunction.customer = this;

						for(int i = 0 ; i< _order.Count ; i++)
						{
							if(_chinaManager.clickedItemDestinationFunction.myType == _order[i])
							{
								_chinaManager.ObjectReach();
								break;
							}
						}
						_chinaManager.ResetBowlsCliked ();
					}
					else if(_chinaManager.IsClickSoupBowl)
					{
						Debug.Log("here....");
						_chinaManager.clickedItemDestinationFunction.customer = this;
						for(int i = 0 ; i< _order.Count ; i++)
						{
							if(_chinaManager.clickedItemDestinationFunction.myType == _order[i])
							{
								_chinaManager.ObjectReach();
								break;
							}
						}
						_chinaManager.ResetBowlsCliked ();
					}
					else
					{
						_chinaManager.ResetBowlsCliked ();
					}
				}
				else if(LevelManager.levelNo > 20 && LevelManager.levelNo <= 30)
				{
					if(_italyManager.clickedOvenPizza)
					{
						if(!_italyManager.clickedPizzaDestinationFunction.isBurnt)
						{
							_italyManager.clickedPizzaDestinationFunction.customer = this;
							_italyManager.clickedPizzaDestinationFunction.otherObject = this.gameObject;
						
							bool foundOrder = false;
							for(int i = 0 ; i< _order.Count ; i++)
							{
								if(_italyManager.clickedPizzaDestinationFunction.myType == _order[i])
								{
									_italyManager.PizzaReached();
									foundOrder = true;
									break;
								}
							}
							if(!foundOrder)
							{
								if(iHaveAMultipleTypeOrder != LevelManager.Orders.NONE)
								{
									_italyManager.clickedPizzaDestinationFunction.wrongOrderGiven = true;
									_italyManager.PizzaReached();
								}
							}
							_italyManager.AllClickedBoolsReset ();
						}
					}
					else if(_italyManager.clickedCoke)
					{
						_italyManager.clickedItemDestinationFunction.customer = this;
					
						for(int i = 0 ; i< _order.Count ; i++)
						{
							if(_italyManager.clickedItemDestinationFunction.myType == _order[i])
							{
								_italyManager.ObjectReached();
								break;
							}
						}
						_italyManager.AllClickedBoolsReset ();
					}
					else
					{
						_italyManager.AllClickedBoolsReset ();
					}
				}
				else if(LevelManager.levelNo > 30 && LevelManager.levelNo <= 40)
				{
					if(_australiaManager.IsBurgerClick ) 
					{
						_australiaManager._burgerFood.wisitior = this;
						_australiaManager._burgerFood.otherObject = this.gameObject;
					
						bool foundOrder = false;
						for(int i = 0 ; i< _order.Count ; i++)
						{
							if(_australiaManager._burgerFood.type == _order[i])
							{
								_australiaManager.HotDogDone();
								foundOrder = true;
								break;
							}
						}
						if(!foundOrder)
						{
							if(iHaveAMultipleTypeOrder != LevelManager.Orders.NONE)
							{
								_australiaManager._burgerFood.wrongOrders = true;
								_australiaManager.HotDogDone();
							}
						}
						_australiaManager.AllClickedBoolsReset ();
					}
					else if(_australiaManager.IsClickedCoke)
					{
						_australiaManager._objectMotion.customer = this;
					
						for(int i = 0 ; i< _order.Count ; i++)
						{
							if(_australiaManager._objectMotion.myType == _order[i])
							{
								_australiaManager.ReachObject();
								break;
							}
						}
						_australiaManager.AllClickedBoolsReset ();
					}
					else if(_australiaManager.IsCickedFries)
					{
						_australiaManager._objectMotion.customer = this;
						for(int i = 0 ; i< _order.Count ; i++)
						{
							if(_australiaManager._objectMotion.myType == _order[i])
							{
								_australiaManager.ReachObject();
								break;
							}
						}
						_australiaManager.AllClickedBoolsReset ();
					}
					else
					{
						_australiaManager.AllClickedBoolsReset ();
					}
				}

				if(_isLeavingNoPay && _customerHandler._timeOnGame > 0 )
				{

					_levelSoundManager.caught.Play();
		
					_uiManager.totalCoins+=_coinsNoPay;
					_uiManager.CallIncrementCoint ();
					_isLeavingNoPay = false;
				}
			}
		}
		
		private void Update () 
		{
			if(_orderPlaces)
			{
				if(_isRadioBought)
					myWaitingTime+=Time.deltaTime/2f;

				else
					myWaitingTime+=Time.deltaTime;

				ShowNeededProduct();
				if(_usManager != null)
				{
					if(myWaitingTime >= _waitingTime && US_Manager.tutorialEnd) 
					{
						_orderPlaces = false;
						StartCoroutine (MoveToPositionRoutine (_customerHandler._customerEndPos.position , false));
					
					}
				}
				else if(_chinaManager != null)
				{
					if(myWaitingTime >= _waitingTime && China_Manager._endTutorial) 
					{
						_orderPlaces = false;
						StartCoroutine (MoveToPositionRoutine (_customerHandler._customerEndPos.position , false));
					
					}
				}
				else if(_italyManager != null)
				{
					if(myWaitingTime >= _waitingTime && Italy_Manager.tutorialEnd)
					{
						_orderPlaces = false;
						StartCoroutine (MoveToPositionRoutine (_customerHandler._customerEndPos.position , false));
					
					}
				}
				else if(_australiaManager != null)
				{
					if(myWaitingTime >= _waitingTime && Australia_Manager.tutorialEnd) 
					{
						_orderPlaces = false;
						StartCoroutine (MoveToPositionRoutine (_customerHandler._customerEndPos.position , false));
					
					}
				}
			}
			if(_eyes != null && _orderPlaces)
			{
				_timer+=Time.deltaTime;
				if(_full)
				{
					if(_timer > 2.0f)
					{	
						_eyes.gameObject.SetActive (true);
						if(myWaitingTime < _waitingTime/2f)
							_eyes.sprite = _eyesExpressions[0];
						else
							_eyes.sprite = _eyesExpressions[1];
						_timer = 0;
						_full = false;
					}
				}
				else
				{
					if(_timer > 0.5f)
					{
						_eyes.gameObject.SetActive (false);
						_timer = 0;
						_full = true;
					}
				}
			}
			else if(_eyes != null && _eyes.sprite != _eyesExpressions[0])
			{
				_eyes.sprite = _eyesExpressions[0];
			}
		}


		public IEnumerator AngryRoutine()
		{
			float sliderValue = 0.49f;
			bool right = false;
			while (sliderValue < 0.5f) 
			{

				if(right)
				{
					_animation.transform.localPosition = new Vector3(_animation.transform.localPosition.x+ Time.deltaTime , _animation.transform.localPosition.y , _animation.transform.localPosition.z) ;
					if(_animation.transform.localPosition.x > 0.04f)
					{
						right = false;
					}
				}
				else
				{
					_animation.transform.localPosition = new Vector3(_animation.transform.localPosition.x - Time.deltaTime , _animation.transform.localPosition.y , _animation.transform.localPosition.z) ;
					if(_animation.transform.localPosition.x < -0.04f)
					{
						right = true;
					}
				}
				sliderValue = myWaitingTime/_waitingTime;
				sliderValue = 1f - sliderValue;
				yield return null;
			}
			_animation.transform.localPosition = Vector3.zero;

		}

		private void ShowNeededProduct()
		{
			if(myWaitingTime > _waitingTime)
				myWaitingTime = _waitingTime;
			float sliderValue = myWaitingTime/_waitingTime;
			sliderValue = 1f - sliderValue;
			if(sliderValue < 0.5f && _sliderWait.color == Color.green)
			{
				_sliderWait.color = Color.red;
				_angryEffect.Play ();
				gameObject.GetComponent<AudioSource>().Play();

				StartCoroutine (nameof(AngryRoutine));
				if(_facialExpression != null)
					_facialExpression.sprite = _expressions[1];
			}
			else if(sliderValue >= 0.5f && _sliderWait.color == Color.red)
			{
				StopCoroutine (nameof(AngryRoutine));

				_angryEffect.Stop ();
				_sliderWait.color = Color.green;
				if(_facialExpression != null)
					_facialExpression.sprite = _expressions[0];
			}
			_sliderWait.transform.localScale = new Vector3( _sliderWait.transform.localScale.x , sliderValue , _sliderWait.transform.localScale.z);
		}

		public void MoveToEnd()
		{
			StartCoroutine (MoveToPositionRoutine (_customerHandler._customerEndPos.position , false));
		}
		
		public void Gold_Deactive()
		{
			_uiManager.gold_Collected.SetActive(false);
		}
		public	void Stop()
		{
			_uiManager.achievment_text.SetActive (false);
		}
		public IEnumerator MoveToPositionRoutine(Vector3 finalPos , bool toOrder)
		{
			_angryEffect.Stop();
			_animation.GetComponent<SpriteRenderer>().sortingOrder = -2;
			_collider.enabled = false;
			if(_eyes != null)
			{
				_eyes.sortingOrder = -1;
			}
			if(_facialExpression!= null)
			{
				_facialExpression.sortingOrder = -1;
			}
			_animation.Play ();
			finalPos = new Vector3(finalPos.x , transform.position.y , finalPos.z);
			float distance = Vector3.Distance (transform.position , finalPos);
			float speed = 5;
			if(!toOrder)
			{
				_orderPlaces = false;
				if(_order.Count == 0)
				{
					_uiManager.n_Customer_served++;

					PlayerPrefs.SetInt ("CustomerServed",_uiManager.n_Customer_served);
					if(PlayerPrefs.GetInt("CustomerServed") > 19 && PlayerPrefs.GetInt ("CustomerLevel1") == 0 )
					{
						PlayerPrefs.SetInt ("CustomerLevel1",1);
						_uiManager.achievment_text.SetActive(true);
						AchievementBlock._claimCheck++;
						PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
						Invoke(nameof(Stop),4.0f);

					}
					if(PlayerPrefs.GetInt("CustomerServed") > 99 && PlayerPrefs.GetInt ("CustomerLevel2") == 0  )
					{
						PlayerPrefs.SetInt ("CustomerLevel2",1);
						_uiManager.achievment_text.SetActive(true);
						AchievementBlock._claimCheck++;
						PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
						Invoke(nameof(Stop),4.0f);
						
					}
					if(PlayerPrefs.GetInt("CustomerServed") > 999 && PlayerPrefs.GetInt ("CustomerLevel3") == 0 )
					{
						MenuManager.golds += 5;
						_uiManager.achievment_text.SetActive(true);
						PlayerPrefs.SetInt ("CustomerLevel3",1);
						AchievementBlock._claimCheck++;
						PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
						Invoke(nameof(Stop),4.0f);
					}
					
					if(_facialExpression!= null)
					{
						_facialExpression.sprite = _expressions[0];
					}
				}
				else
				{
					US_Manager.noOfPerfects = 0;
				}

				if(coinsSpent > 0)
				{
					if(myWaitingTime > _waitingTime/2f)
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
					if(_customerHandler.canBeAnUnPayingCustomer && _customerHandler.noOfCustomers >= 4 && _customerHandler.noOfCustomers <= 16 )
					{
						if(_customerHandler._timeOnGame > 0)
						{
							if(_customerHandler.noOfCustomers > 14 )
							{
								shouldNotPay = true;
							}
							else
							{
								int randomDecider = Random.Range (0,_noPay.Count);
								if(randomDecider == 0)
								{
									shouldNotPay = true;

								}
							}
						}
					}

					if(shouldNotPay)
					{
						_collider.enabled = true;
						if(PlayerPrefs.HasKey ("Whistle"))
						{
							_levelSoundManager.whistle.Play();
							_uiManager.whistle.transform.localScale = new Vector3(1.2f,1.2f,0);
							_uiManager.blow.SetActive(true);
							_uiManager.Invoke("WhistleInitialpos",1.2f);
						}
						_customerHandler.noOfUnpayingCustomers++;
						if(_customerHandler.noOfUnpayingCustomers >= _customerHandler.maxNoOfUnpayableCustomers)
						{
							_customerHandler.canBeAnUnPayingCustomer = false;
						}
						_isLeavingNoPay = true;
						_coinsNoPay = coinsSpent;
						_customerHandler._availablePositions.Add (positionTaken);
					}
					else
					{
						int shouldGiveBonus = Random.Range(0,7);
						if(shouldGiveBonus == 0)
						{
							int bonusVal = _uiManager.Bonus_coin;
							if(!perfect)
							{
								bonusVal = Mathf.CeilToInt (bonusVal/2f);
							}
							Debug.Log("bonus Val = "+bonusVal);
							_customerHandler._coins[positionTaken].bonusVal = bonusVal;
							_customerHandler._coins[positionTaken].myAmount = coinsSpent +bonusVal;
						}
						else
						{
							_customerHandler._coins[positionTaken].myAmount = coinsSpent;
						}

						_customerHandler._coins[positionTaken].gameObject.SetActive (true);
						if(perfect)
						{
							_customerHandler._coins[positionTaken].perfectText.SetActive (true);
						}
						else
							_customerHandler._coins[positionTaken].perfectText.SetActive (false);

						if(perfect)
						{
							US_Manager.noOfPerfects++;
							_uiManager.n_Perfect_achieved++;
							PlayerPrefs.SetInt ("Perfectachieved", _uiManager.n_Perfect_achieved);
						
							if(PlayerPrefs.GetInt("Perfectachieved") > 9 && PlayerPrefs.GetInt ("PerfectLevel1")== 0 )
							{
								PlayerPrefs.SetInt ("PerfectLevel1",1)  ;
								_uiManager.achievment_text.SetActive(true);
								AchievementBlock._claimCheck++;
								PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
								Invoke(nameof(Stop),4.0f);
							
							}
							if(PlayerPrefs.GetInt("Perfectachieved") > 99 && PlayerPrefs.GetInt ("PerfectLevel2")== 0 )
							{
								PlayerPrefs.SetInt ("PerfectLevel2",1);
								_uiManager.achievment_text.SetActive(true);
								AchievementBlock._claimCheck++;
								PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
								Invoke(nameof(Stop),4.0f);
							
							}
							if(PlayerPrefs.GetInt("Perfectachieved") > 999 && PlayerPrefs.GetInt ("PerfectLevel3")== 0 )
							{
								PlayerPrefs.SetInt ("PerfectLevel3",1);
								_uiManager.achievment_text.SetActive(true);
								AchievementBlock._claimCheck++;
								PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
								Invoke(nameof(Stop),4.0f);
							
							}
							
							if(US_Manager.noOfPerfects >= 5)
							{
								_uiManager.gold_Collected.SetActive(false);
								_uiManager.gold_Collected.SetActive(true);
							
								Debug.Log ("added gold");
								Invoke(nameof(AddGold),1.5f);
								Invoke("Gold_Deactive ",1.5f);
								US_Manager.noOfPerfects = 0;
							
							}
						}
						else
						{
							Debug.Log ("perfects made 0");
							US_Manager.noOfPerfects = 0;
						}
						
						_customerHandler._coins[positionTaken].coinCollected.Play ();
						_customerHandler._coins[positionTaken].positionTaken = positionTaken;
						_customerHandler._coins[positionTaken].addValue.text = "+"+coinsSpent;
					}
				}
				else
				{
					_customerHandler._availablePositions.Add (positionTaken);
					if(_customerHandler.timerStopped)
					{
						if(_customerHandler._availablePositions.Count == 5)
						{
							_uiManager.OnGameOver ();
						}
					}
				}


				if(_eyes != null)
					_eyes.gameObject.SetActive (false);
				positionTaken = -1;
				_order.Clear ();
				_orderTween.duration = 0.15f;
				_orderTween.PlayReverse ();
				if(!_isLeavingNoPay)
					speed = 7;
				else
					speed = 4;
				shouldBePerfectIfServed = false;
			}
			else
			{
				iHaveAMultipleTypeOrder = LevelManager.Orders.NONE;
				shouldBePerfectIfServed = false;
		
				_angryEffect.Stop ();
				if(_facialExpression!= null)
				{
					_facialExpression.sprite = _expressions[0];
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
				_collider.enabled = true;
				transform.position = finalPos;
				_sliderWait.color = Color.green;
				_orderPanel.SetActive (true);
				_orderTween.duration = 0.5f;
				_orderTween.PlayForward ();
				_orderPlaces = true;
				OrderTake ();

			}
			else
			{
				if(_australiaManager != null)
				{
					_friesTween.enabled = false;
				}
				_isLeavingNoPay = false;
				_eatableOrderTween.enabled = false;
				_drinkableOrderTween.enabled = false;
				_orderPanel.SetActive (false);
				transform.position = new Vector3(_customerHandler._customerStartPos.position.x , transform.position.y , _customerHandler._customerStartPos.position.z);
				_customerHandler._wisitorsPool.Add (this);
			}
			_animation.Stop ();
			_animation.GetComponent<SpriteRenderer>().sortingOrder = 0;
			if(_eyes != null)
			{
				_eyes.sortingOrder = 1;
			}
			if(_facialExpression != null)
			{
				_facialExpression.sortingOrder = 1;
			}
			coinsSpent = 0;

			int rand = Random.Range (0, 8);
			if (rand == 4) {
				_levelSoundManager.come_random.Play();
			}
			perfect = false;
		}

		private void OrderTake()
		{
			_orderPlate.SetActive (false);
			_drinkableOrder.SetActive (false);
			if(LevelManager.levelNo <= 10)
			{
				OrdersUS ();
			}
			else if(LevelManager.levelNo > 10 && LevelManager.levelNo <= 20)
			{
				OrderChina ();
			}
			else if(LevelManager.levelNo > 20 && LevelManager.levelNo <= 30)
			{
				OrderItaly ();
			}
			else
			{
				OrderAustralia();
			}
		}


		private void OrdersUS()
		{
			if(LevelManager.levelNo == 1 ){
				noOfOrders = 1;
				_order.Add ((LevelManager.Orders)1);
				OrderCheck(1);
			}
			else if(LevelManager.levelNo == 2)
			{
				noOfOrders = 1;
				int valueToadd = Random.Range (1,4);  // find any one out of 3
				_order.Add ((LevelManager.Orders)valueToadd);
				OrderCheck(valueToadd);
			}
			else
			{
				noOfOrders = Random.Range(1,3);
				if(noOfOrders == 1)
				{
					int valueToadd = Random.Range (1,5);  // find any one out of 4
					_order.Add ((LevelManager.Orders)valueToadd);
					OrderCheck(valueToadd);
				
				}
				else
				{
					_order.Add ((LevelManager.Orders)4);  // one would de coke for sure
					OrderCheck(4);
					int valueToadd = Random.Range (1,4);
					_order.Add ((LevelManager.Orders)valueToadd);
					OrderCheck(valueToadd);
				}
			}
		}
		public void AddGold()
		{
			MenuManager.golds++;
			PlayerPrefs.SetString("Golds",Encryption.Encrypt (MenuManager.golds.ToString ()));
			_uiManager.goldText.text = MenuManager.golds.ToString ();
			_levelSoundManager.coinAdd.Play ();
		}

		private void OrderChina()
		{
			if(LevelManager.levelNo == 11 || LevelManager.levelNo == 12){
				noOfOrders = 1;
				_order.Add ((LevelManager.Orders)5);
				OrderCheck(5);
			}
			else if(LevelManager.levelNo == 13)
			{
				noOfOrders = 1;
				int valueToadd = 0;
				if(_customerHandler.noOfCustomers == 1)
				{
					valueToadd = 6;
				}
				else
				{
					valueToadd = Random.Range (5,7);  
				}
				_order.Add ((LevelManager.Orders)valueToadd);
				OrderCheck(valueToadd);
			}
			else
			{
				noOfOrders = Random.Range(1,3);
				if(noOfOrders == 1)
				{
					int valueToadd = Random.Range (5,7);  
					_order.Add ((LevelManager.Orders)valueToadd);
					OrderCheck(valueToadd);
				
				}
				else
				{
					_order.Add ((LevelManager.Orders)5);  
					OrderCheck(5);
					_order.Add ((LevelManager.Orders)6); 
					OrderCheck(6);
				}
			}
		}


		private void OrderItaly()
		{
			if(LevelManager.levelNo == 21 ){
				noOfOrders = 1;
				_order.Add ((LevelManager.Orders)7);
				OrderCheck(7);
			}
			else if(LevelManager.levelNo == 22)
			{
				noOfOrders = 1;
				int valueToadd = 0;
				if(_customerHandler.noOfCustomers == 1)
				{
					valueToadd = 8;
				}
				else
				{
					valueToadd = Random.Range (7,9);  
				}
				_order.Add ((LevelManager.Orders)valueToadd);
				OrderCheck(valueToadd);
			}
			else
			{
				noOfOrders = Random.Range(1,3);
				if(noOfOrders == 1)
				{
					int valueToadd = Random.Range (7,10);  
					_order.Add ((LevelManager.Orders)valueToadd);
					OrderCheck(valueToadd);
				
				}
				else
				{
					_order.Add ((LevelManager.Orders)9); 
					OrderCheck(9);
					int valueToadd = Random.Range (7,9);
					_order.Add ((LevelManager.Orders)valueToadd);
					OrderCheck(valueToadd);
				}
			}
		}

		private void OrderAustralia()
		{
			if(LevelManager.levelNo == 31 ){

				if(_customerHandler.noOfCustomers == 1)
				{
					noOfOrders = 1;
					_order.Add ((LevelManager.Orders)10);
					OrderCheck(10);
				}
				else
				{
					noOfOrders = 2;
					_order.Add ((LevelManager.Orders)11); 
					OrderCheck(11);
					_order.Add ((LevelManager.Orders)10);
					OrderCheck(10);
				}
			}
			else if(LevelManager.levelNo == 32)
			{
				int valueToadd = 0;
				if(_customerHandler.noOfCustomers == 1)
				{
					noOfOrders = 1;
					valueToadd = 12;
					_order.Add ((LevelManager.Orders)valueToadd);
					OrderCheck(valueToadd);
					shouldBePerfectIfServed = true;
				}
				else
				{
					noOfOrders = 2;
					List <int>order = new List<int>(){10,11,12};
					valueToadd = order[Random.Range (0,order.Count)];
					_order.Add ((LevelManager.Orders)valueToadd);
					OrderCheck(valueToadd);
					order.Remove (valueToadd);
					valueToadd = order[Random.Range (0,order.Count)];
					_order.Add ((LevelManager.Orders)valueToadd);
					OrderCheck(valueToadd);
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
				_order.Add ((LevelManager.Orders)valueToadd);
				OrderCheck(valueToadd);

				valueToadd = order[Random.Range (0,order.Count)];
				order.Remove (valueToadd);
				if(valueToadd == 10)
				{
					List <int>burgerOrder = new List<int>(){10,13,14};
					valueToadd = burgerOrder[Random.Range (0,burgerOrder.Count)];
				}
				_order.Add ((LevelManager.Orders)valueToadd);
				OrderCheck(valueToadd);

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
				_order.Add ((LevelManager.Orders)valueToadd);
				OrderCheck(valueToadd);

				valueToadd = order[Random.Range (0,order.Count)];
				order.Remove (valueToadd);
				if(valueToadd == 10)
				{
					List <int>burgerOrder = new List<int>(){10,13,14,15};
					valueToadd = burgerOrder[Random.Range (0,order.Count)];
				}
				_order.Add ((LevelManager.Orders)valueToadd);
				OrderCheck(valueToadd);

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
				_order.Add ((LevelManager.Orders)valueToadd);
				OrderCheck(valueToadd);
			
				valueToadd = order[Random.Range (0,order.Count)];
				order.Remove (valueToadd);
				if(valueToadd == 10)
				{
					List <int>burgerOrder = new List<int>(){10,13,14,15,16,17,18};
					valueToadd = burgerOrder[Random.Range (0,burgerOrder.Count)];
				}
				_order.Add ((LevelManager.Orders)valueToadd);
				OrderCheck(valueToadd);

				if(noOfOrders == 3)
				{
					valueToadd = order[0];
					if(valueToadd == 10)
					{
						List <int>burgerOrder = new List<int>(){10,13,14,15,16,17,18};
						valueToadd = burgerOrder[Random.Range (0,burgerOrder.Count)];
					}
					_order.Add ((LevelManager.Orders)valueToadd);
					OrderCheck(valueToadd);
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
				_order.Add ((LevelManager.Orders)valueToadd);
				OrderCheck(valueToadd);
			
				valueToadd = order[Random.Range (0,order.Count)];
				order.Remove (valueToadd);
				if(valueToadd == 10)
				{
					List <int>burgerOrder = new List<int>(){10,13,14,15,16,17,18,19};
					valueToadd = burgerOrder[Random.Range (0,burgerOrder.Count)];
				}
				_order.Add ((LevelManager.Orders)valueToadd);
				OrderCheck(valueToadd);
			
				if(noOfOrders == 3)
				{
					valueToadd = order[0];
					if(valueToadd == 10)
					{
						List <int>burgerOrder = new List<int>(){10,13,14,15,16,17,18,19};
						valueToadd = burgerOrder[Random.Range (0,burgerOrder.Count)];
					}
					_order.Add ((LevelManager.Orders)valueToadd);
					OrderCheck(valueToadd);
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
				_order.Add ((LevelManager.Orders)valueToadd);
				OrderCheck(valueToadd);
			

				_order.Add ((LevelManager.Orders)11);
				OrderCheck(11);
	
				_order.Add ((LevelManager.Orders)12);
				OrderCheck(12);
			}
			else
			{
				noOfOrders = 3;

				_order.Add ((LevelManager.Orders)19);
				OrderCheck(19);

				_order.Add ((LevelManager.Orders)11);
				OrderCheck(11);
			
				_order.Add ((LevelManager.Orders)12);
				OrderCheck(12);
			}
		}

		private void OrderCheck(int orderNo)
		{
			if(orderNo == 4 || orderNo == 6 || orderNo == 9 || orderNo == 11)  //drinkable Orders
			{
				if(noOfOrders == 1)
				{
					shouldBePerfectIfServed = true;
				}
				_drinkableOrderTween.enabled = false;
			
				_drinkableOrder.SetActive (true);
				_drinkableOrder.transform.localScale = _drinkScale;
			}
			else if(orderNo != 12)
			{
				if(orderNo != 5)
					iHaveAMultipleTypeOrder = (LevelManager.Orders)orderNo;

				_eatableOrderTween.enabled = false;
				_orderPlate.SetActive (true);
				_orderPlate.transform.localScale = _orderScale;
				SetEatableSprite(orderNo);
			
			}
			else
			{
				_friesTween.enabled = false;
				_fricesPrefav.SetActive (true);
				_fricesPrefav.transform.localScale = _friesScale;
			}
		}

		private void SetEatableSprite(int orderNo)
		{ 
			if(LevelManager.levelNo <= 10)
			{
				_orderEat.sprite = _usManager.hotDogOrderVariations[orderNo-1];
			}
			else if(LevelManager.levelNo > 20 && LevelManager.levelNo <= 30)
			{
				_orderEat.sprite = _italyManager.pizzaBakedVariations[orderNo-6];
				_pizzaDOt.sprite = _italyManager.pizzaDot[orderNo-6];


			}
			else if(LevelManager.levelNo > 30 && LevelManager.levelNo <= 40)
			{
				switch(orderNo)
				{
					case 10:
						_burger._tomatoPrefab.gameObject.SetActive (false);
						_burger._onionPrefab.gameObject.SetActive (false);
						_burger._cabbagePrefab.gameObject.SetActive (false);
						break;
					case 13:
						_burger._tomatoPrefab.gameObject.SetActive (true);
						_burger._onionPrefab.gameObject.SetActive (false);
						_burger._cabbagePrefab.gameObject.SetActive (false);
						break;
					case 14:
						_burger._tomatoPrefab.gameObject.SetActive (false);
						_burger._onionPrefab.gameObject.SetActive (true);
						_burger._cabbagePrefab.gameObject.SetActive (false);
						break;
					case 15:
						_burger._tomatoPrefab.gameObject.SetActive (false);
						_burger._onionPrefab.gameObject.SetActive (false);
						_burger._cabbagePrefab.gameObject.SetActive (true);
						break;
					case 16:
						_burger._tomatoPrefab.gameObject.SetActive (true);
						_burger._onionPrefab.gameObject.SetActive (true);
						_burger._cabbagePrefab.gameObject.SetActive (false);
						break;
					case 17:
						_burger._tomatoPrefab.gameObject.SetActive (true);
						_burger._onionPrefab.gameObject.SetActive (false);
						_burger._cabbagePrefab.gameObject.SetActive (true);
						break;
					case 18:
						_burger._tomatoPrefab.gameObject.SetActive (false);
						_burger._onionPrefab.gameObject.SetActive (true);
						_burger._cabbagePrefab.gameObject.SetActive (true);
						break;
					case 19:
						_burger._tomatoPrefab.gameObject.SetActive (true);
						_burger._onionPrefab.gameObject.SetActive (true);
						_burger._cabbagePrefab.gameObject.SetActive (true);
						break;
				}

			}
		}

		public void RemoveOrderFromBoard(LevelManager.Orders orderNo)
		{

			if((int)orderNo == 4 || (int)orderNo == 6 || (int)orderNo == 9 || (int)orderNo == 11)  //drinkable Orders
			{
				_drinkableOrderTween.ResetToBeginning ();
				_drinkableOrderTween.PlayForward ();
			}
			else if((int)orderNo != 12)
			{
				_eatableOrderTween.ResetToBeginning ();
				_eatableOrderTween.PlayForward ();
			}
			else
			{
				_friesTween.ResetToBeginning ();
				_friesTween.PlayForward ();
			}
		}
	}
}

