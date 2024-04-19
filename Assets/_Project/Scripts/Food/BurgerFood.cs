using System.Collections;
using _Project.Scripts.Achivments;
using _Project.Scripts.Entities.Customers;
using _Project.Scripts.Managers;
using _Project.Scripts.Other;
using _Project.Scripts.UI_Scripts;
using _Project.Scripts.UI.Tutorial;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Scripts.Food
{
	public class BurgerFood : MonoBehaviour 
	{
		[Inject] private LevelSoundManager _levelSoundManager;
		[Inject] private Australia_Manager _australiaManager;
		[Inject] private UIManager _uiManager;   
		private bool _isScale;
		private bool _isOnCustomer;
		private Vector3 _collisionSize;
		private bool _isCanMove;
		
		[FormerlySerializedAs("myTikki")] public SpriteRenderer _tikkiSpriteRenderer;
		[FormerlySerializedAs("myTomato")] public GameObject _tomatoPrefab;
		[FormerlySerializedAs("myOnion")] public GameObject _onionPrefab;
		[FormerlySerializedAs("myCabbage")] public GameObject _cabbagePrefab;
		[FormerlySerializedAs("mySelection")] public GameObject _selectionObject;
		
		[FormerlySerializedAs("myPlate")] [SerializeField] private Availability _plate;
		[FormerlySerializedAs("myOriginalPos")] [SerializeField] private Vector3 _originalPos;
		[FormerlySerializedAs("myTouchPos")] [SerializeField] private Vector3 _touchPos;
		[FormerlySerializedAs("myLocalScale")] [SerializeField] private Vector3 _scale;
		[FormerlySerializedAs("orderBurger")] [SerializeField] private bool _isOrder;
		[FormerlySerializedAs("error")] [SerializeField] private GameObject _errorObject;
		public bool IsTomato { get; set; }
		public bool IsOnion { get; set; }
		public bool IsCabbage { get; set; }
		public bool isTikki { get; set; }
		public LevelManager.Orders type { get; set; }
		public bool isPrefavet { get; set; }
		public bool isSelected { get; set; }
		public bool isTutorialOn { get; set; }
		public GameObject otherObject { get; set; }
		public bool wrongOrders { get; set; }
		public Wisitor wisitior { get; set; }
		private void Start()
		{
			_uiManager.n_Burger_served=PlayerPrefs.GetInt ("BurgerServed");
			_scale = transform.localScale;
			_originalPos = transform.position;
			if(!_isOrder)
				_collisionSize = transform.GetComponent<BoxCollider> ().size;
		}

		private void OnDisable()
		{
			wrongOrders = false;
			IsTomato = false;
			IsOnion  = false;
			IsCabbage  = false;
			isTikki  = false;
			_isOnCustomer = false;


			_tomatoPrefab.SetActive (false);
			_cabbagePrefab.SetActive (false);
			_onionPrefab.SetActive (false);

			if(transform.GetComponent<Availability>())
			{
				_tikkiSpriteRenderer.gameObject.SetActive (false);
				transform.GetComponent<Availability>().available = true;
			}
			type = LevelManager.Orders.NONE; 
			transform.localScale = _scale;

			isSelected = false;
			if(_selectionObject != null)
				_selectionObject.SetActive (false);
			
		}
		
		private void OnMouseDown()
		{
			if(!TutorialPanel.popupPanelActive || Australia_Manager.tutorialEnd || Australia_Manager.tutorialEnd || isTutorialOn)
			{
				_australiaManager._burgerFood = this;
				_isCanMove = true;
				Vector3 myPos = Camera.main.WorldToScreenPoint (transform.position);
				_touchPos =  Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y , myPos.z));
				if(_australiaManager.IsClickedTikki && !isTikki)
				{
					if(!_australiaManager.ClickedFunktion.isBurnt)
					{
						_australiaManager.ClickedFunktion.availableHotDog = this.GetComponent<Availability>();
						_australiaManager.TikkiDone ();

					}
					_australiaManager.AllClickedBoolsReset ();
				}
				else if(_australiaManager.IsClickedTomato && !IsTomato)
				{
					_australiaManager._objectMotion.availableBurger = this;
					_australiaManager.ReachObject ();
					_australiaManager.AllClickedBoolsReset ();
				}
				else if(_australiaManager.IsClickedOnion && !IsOnion)
				{
					_australiaManager._objectMotion.availableBurger = this;
					_australiaManager.ReachObject ();
					_australiaManager.AllClickedBoolsReset ();
				}
				else if(_australiaManager.IsClickedCabbage && !IsCabbage)
				{
					_australiaManager._objectMotion.availableBurger = this;
					_australiaManager.ReachObject ();
					_australiaManager.AllClickedBoolsReset ();
				}
				else 
				{
					if(isTikki)
					{
						if(isTutorialOn)
						{
							_australiaManager.FirstCustomer.tutorialOn = true;
						}
						_australiaManager.AllClickedBoolsReset ();
						_australiaManager.IsBurgerClick = true;
						
						_isScale = true;
						if(!_isOrder)
							transform.GetComponent<BoxCollider> ().size = new Vector3(_collisionSize.x/2f , _collisionSize.y/2f , _collisionSize.z);
						isSelected = true;
					}
					else
					{
						_australiaManager._burgerFood = null;
						_isCanMove = false;
						_errorObject.SetActive(true);
					}
				}
			}
		}

		private void OnMouseDrag()
		{
			if( _isCanMove)
			{
				Vector3 myPos = Camera.main.WorldToScreenPoint (transform.position);
				Vector3 newPos = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y , myPos.z));
				if(Vector3.Distance (newPos,_touchPos) > 0.2f)
				{
					transform.position =  Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y , myPos.z));
				}
			}
		}

		private void OnMouseUp()
		{
			_errorObject.SetActive (false);
			if(_isCanMove)
			{
				if(!_isOnCustomer)
					StartCoroutine(MoveToPositionRoutine());
				else
				{
					OnDestinationClick ();
				}
				_isCanMove = false;
			}
			if(!_isOrder)
				transform.GetComponent<BoxCollider> ().size = _collisionSize;
		}
		
		public void Deactivate()
		{
			_uiManager.achievment_text.SetActive (false);
		}
		
		public void OnDestinationClick()
		{
			_australiaManager.PlatesFilled--;
			_plate.available = true;
			if(!otherObject.name.Contains ("dustbin"))
			{
				_uiManager.n_Burger_served++ ;
				_levelSoundManager.customerEat.Play();
				PlayerPrefs.SetInt ("BurgerServed", _uiManager.n_Burger_served);
				if(PlayerPrefs.GetInt("BurgerServed") > 9 && PlayerPrefs.GetInt ("BurgerLevel1")==0 )
				{
					PlayerPrefs.SetInt ("BurgerLevel1",1);
					_uiManager.achievment_text.SetActive(true);
					AchievementBlock._claimCheck++;
					PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
					Invoke(nameof(Deactivate),4.0f);
				}
				if(PlayerPrefs.GetInt("BurgerServed") > 99 && PlayerPrefs.GetInt ("BurgerLevel2")==1)
				{
					PlayerPrefs.SetInt ("BurgerLevel2",1);
					_uiManager.achievment_text.SetActive(true);
					AchievementBlock._claimCheck++;
					PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
					Invoke(nameof(Deactivate),4.0f);
				}
				if(PlayerPrefs.GetInt("BurgerServed") > 999 && PlayerPrefs.GetInt ("BurgerLevel3")==2)
				{
					PlayerPrefs.SetInt ("BurgerLevel3",1);
					_uiManager.achievment_text.SetActive(true);
					AchievementBlock._claimCheck++;
					PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
					Invoke(nameof(Deactivate),4.0f);
				}
				if(isTutorialOn)
				{
					isTutorialOn = false;
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopupAustralia ("PUT BURNT TIKKI \n INTO THE DUSTBIN!",false,false ,7 , 1);
				}
				
				string myTypeToEatSub = "BURGER";
				
				for(int count = 0; count < wisitior._order.Count; count++)
				{
					if(wisitior._order[count].ToString().Contains(myTypeToEatSub.ToString()))
					{
						if(wisitior._order[count] == type)
						{
							wrongOrders = false;
						
						}
						else
						{
							wrongOrders = true;
						
						}
						wisitior._order.RemoveAt(count);
					}
				
				}
				wisitior.RemoveOrderFromBoard (type);


				wisitior.iHaveAMultipleTypeOrder = LevelManager.Orders.NONE;
			
				if(isPrefavet)
				{
					if(wisitior._order.Count > 0)
						wisitior.myWaitingTime-= 30;

					if(!wrongOrders)
					{
						wisitior.coinsSpent+=_australiaManager.PerfectBurger;
						wisitior.perfect = true;
					}
					else
					{
						wisitior.coinsSpent+=(_australiaManager.PerfectBurger/2);
					
					}
				
				}
				else
				{
					if(wisitior._order.Count > 0)
						wisitior.myWaitingTime-= 20;
				
					if(!wrongOrders)
					{
						wisitior.coinsSpent+=_australiaManager.LessBaked;
					}
					else
					{
						wisitior.coinsSpent+=(_australiaManager.LessBaked/2);
					}
				}
			
				if(IsCabbage)
				{
					wisitior.coinsSpent+=10;
				}
				if(IsTomato)
				{
					wisitior.coinsSpent+=10;
				}
				if(IsOnion)
				{
					wisitior.coinsSpent+=10;
				}
				
				if(wisitior.myWaitingTime < 0)
				{
					wisitior.myWaitingTime = 0;
				}
			
				if(wisitior._order.Count <= 0)
					wisitior.MoveToEnd();
			
			}
			else
			{
				if(isPrefavet){
					_uiManager.totalCoins-=_australiaManager.PerfectBurger;
					_levelSoundManager.dustbin.Play();
					if(_uiManager.totalCoins > 0){

						_uiManager.dustbin_textparent.SetActive(true);
						_uiManager.dustbin_text.text = "-"+_australiaManager.PerfectBurger.ToString(); 
						Invoke(nameof(DeactivateDustbin),1.0f);
					}}
				else
				{
					_uiManager.totalCoins-=_australiaManager.LessBaked;
					_levelSoundManager.dustbin.Play();
					_uiManager.coinsText.text = _uiManager.totalCoins.ToString ();
					if(_uiManager.totalCoins > 0){

						_uiManager.dustbin_textparent.SetActive(true);
						_uiManager.dustbin_text.text = "-"+_australiaManager.LessBaked.ToString(); 
						Invoke(nameof(DeactivateDustbin),1.0f);
					}}
				if(_uiManager.totalCoins < 0)
				{
					_uiManager.totalCoins = 0;
					_uiManager.coinsText.text = "0";
				}
			}
			transform.position = _originalPos;
			transform.gameObject.SetActive(false);
		}

		public void DeactivateDustbin()
		{
			_uiManager.dustbin_textparent.SetActive (false);
		}

		private IEnumerator MoveToPositionRoutine()
		{
			float distance = Vector3.Distance (transform.position , _originalPos);
			float speed = 15;
			while(distance > 0.1f)
			{
				float step = speed * 0.02f;
				transform.position = Vector3.MoveTowards(transform.position, _originalPos, step);
				distance = Vector3.Distance (transform.position , _originalPos);
				yield return 0;
			}
			if(isSelected)
				_selectionObject.SetActive (true);
			transform.position = _originalPos;
		}


		

		private void OnTriggerStay(Collider other)
		{
			if(other.name.Contains ("customer"))
			{

				otherObject = other.gameObject;
				wisitior = other.GetComponent<Wisitor>();
				for(int i = 0 ; i< wisitior._order.Count ; i++)
				{
					if(type == wisitior._order[i])
					{
						wrongOrders = false;
						_isOnCustomer = true;
						wrongOrders = false;
						break;
					}
				}
				if(!_isOnCustomer && wisitior.iHaveAMultipleTypeOrder != LevelManager.Orders.NONE)
				{
					wrongOrders = true;
					_isOnCustomer = true;
				}
			}
			else if(other.name.Contains ("dustbin") && Australia_Manager.tutorialEnd== true )
			{
				wrongOrders = false;
				otherObject = other.gameObject;
				_isOnCustomer = true;
			}

		}

		private void OnTriggerExit(Collider other)
		{
			if(other.name.Contains ("customer") || other.name.Contains ("dustbin"))
			{
				wrongOrders = false;
				_isOnCustomer = false;
			}
		}
	}
}
