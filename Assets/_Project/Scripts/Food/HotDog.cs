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
	public class HotDog : MonoBehaviour 
	{
		[Inject] private SoundsAll _levelSoundManager;
		[Inject] private USController _usManager;
		[Inject] private UIManager _uiManager;
		private bool _isOnCustomer;
		private bool _isScale;
		private Vector3 _sizeCollider;
		private bool _isCanMove;
		private int _a = 1;
		[FormerlySerializedAs("errorObject")] [SerializeField] private GameObject _errorPrefab;
		[FormerlySerializedAs("myPlate")] [SerializeField] private Availability _plate;
		[FormerlySerializedAs("myOriginalPos")] [SerializeField] private Vector3 _originalPos;
		[FormerlySerializedAs("myTouchPos")] [SerializeField] private Vector3 _touchPos;
		
		[FormerlySerializedAs("myLocalScale")] public Vector3 _localScale;
		[FormerlySerializedAs("mySelection")] public GameObject _selectionObject;
		[FormerlySerializedAs("otherObject")] public GameObject _otherObject;
		public bool isTikki { get; set; }
		public bool isTutorial { get; set; }
		public LevelManager.Orders isTape { get; set; }
		public bool isPerfect { get; set; }
		public bool isSelected { get; set; }
		public bool wrongOrder{ get; set; }
		public Wisitor wisitor { get; set; }
		public bool isSauce { get; set; }
		public bool yellowSauce { get; set; }
		private void Start()
		{
			_uiManager.n_Hotdogs_served = PlayerPrefs.GetInt ("hotdogServed");
			_localScale = transform.localScale;
			_originalPos = transform.position;
			_sizeCollider = transform.GetComponent<BoxCollider> ().size;
		}


		private void OnDisable()
		{
			wrongOrder = false;
			isSauce = false;
	
			yellowSauce  = false;
			isTikki  = false;
			_isOnCustomer = false;
			transform.GetComponent<Availability>().isAvailable = true;
			_usManager.HotDogSaucesOnPlatesSR[transform.GetComponent<Availability>()._arrayPos].gameObject.SetActive (false);
			isTape = LevelManager.Orders.NONE; 
			transform.localScale = _localScale;
			isSelected = false;
			_selectionObject.SetActive (false);
		}


		private void OnMouseDown()
		{
			if(!TutorialPanel.popupPanelActive || USController._isEndTutorial || AustraliaController.tutorialEnd || isTutorial)
			{
				_usManager.HotDog = this;
				_isCanMove = true;
				Vector3 myPos = Camera.main.WorldToScreenPoint (transform.position);
				_touchPos =  Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y , myPos.z));
				if(_usManager.IsClickedTikki && isTape == LevelManager.Orders.NONE)
				{
					if(!_usManager.MakeTiki.isBurnt)
					{
						_usManager.MakeTiki.availableHotDog = this.GetComponent<Availability>();
						_usManager.OnTikkiDestination ();

					}
					_usManager.ResetBools ();
				}
				else if((_usManager.IsClickedYellowSauce || _usManager.IsClickedRedSauce) && (isTape == LevelManager.Orders.NONE || isTape == LevelManager.Orders.HOTDOG))
				{
					_usManager.objectMotion.availableHotDog = this;
					_usManager.OnObjectReach ();
					_usManager.ResetBools ();
				}
				else 
				{
					if(isTikki)
					{
						if(isTutorial)
						{
							_usManager.firstWisitor.tutorialOn = true;
						}
						_usManager.ResetBools ();
						_usManager.IsClickedHotDog = true;

						_isScale = true;
						transform.GetComponent<BoxCollider> ().size = new Vector3(_sizeCollider.x/2f , _sizeCollider.y/2f , _sizeCollider.z);
						isSelected = true;
					}
					else
					{
						_usManager.HotDog = null;
						_isCanMove = false;
						_errorPrefab.SetActive(true);
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
			_errorPrefab.SetActive (false);
			if(_isCanMove)
			{
				if(!_isOnCustomer)
					StartCoroutine(MoveRoutine());
				else
				{
					OnDistinationClick ();

				}
				_isCanMove = false;
			}
			transform.GetComponent<BoxCollider> ().size = _sizeCollider;
		}
		public void Deactivate()
		{
			_uiManager.achievment_text.SetActive (false);
		}

		public void OnDistinationClick()
		{
			_usManager.PlatesFilledCount--;
			_plate.isAvailable = true;
			if(!_otherObject.name.Contains ("dustbin"))
			{
		
				_uiManager.n_Hotdogs_served++;
				_usManager.IsClickedHotDog = false;
				_levelSoundManager.customerEatSound.Play();

				PlayerPrefs.SetInt ("hotdogServed", _uiManager.n_Hotdogs_served);
				if(PlayerPrefs.GetInt("hotdogServed") > 9 && PlayerPrefs.GetInt ("hotdogLevel1")==0)
				{

					PlayerPrefs.SetInt ("hotdogLevel1",1);
					_uiManager.achievment_text.SetActive(true);
					AchievementBlock._claimCheck++;
					PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
					Invoke(nameof(Deactivate),4.0f);
				}
				if(PlayerPrefs.GetInt("hotdogServed") > 99 && PlayerPrefs.GetInt ("hotdogLevel2")==0)
				{
					PlayerPrefs.SetInt ("hotdogLevel2",1);
					_uiManager.achievment_text.SetActive(true);
					AchievementBlock._claimCheck++;
					PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
					Invoke(nameof(Deactivate),4.0f);
				}
				if(PlayerPrefs.GetInt("hotdogServed") > 999 && PlayerPrefs.GetInt ("hotdogLevel3")==0)
				{
					PlayerPrefs.SetInt ("hotdogLevel3",1);
					_uiManager.achievment_text.SetActive(true);
					AchievementBlock._claimCheck++;
					PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
					Invoke(nameof(Deactivate),4.0f);
				}
				if(isTutorial)
				{
					isTutorial = false;
					//_usManager.FirstCoins.IsTutorialOn = true;
					_usManager.IsCoinTutorial = true;
					/*
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopup ("COLLECT THE COINS.",false,false , 4);
					*/
				}
				
				string myTypeToEatSub = "HOTDOG";
				for(int count = 0; count < wisitor._order.Count; count++)
				{
					if(wisitor._order[count].ToString().Contains(myTypeToEatSub.ToString()))
					{
						if(wisitor._order[count] == isTape)
						{
							wrongOrder = false;

						}
						else
						{
							wrongOrder = true;

						}
						wisitor._order.RemoveAt(count);
					}
				
				}
				wisitor.RemoveOrderFromBoard (isTape);



				wisitor.iHaveAMultipleTypeOrder = LevelManager.Orders.NONE;
			
				if(isPerfect)
				{
					if(wisitor._order.Count > 0)
						wisitor.myWaitingTime-= 30;

					if(!wrongOrder)
					{
						wisitor.coinsSpent+=_usManager.perfectHotDogTime;
						wisitor.perfect = true;
					}
					else
					{
						wisitor.coinsSpent+=(_usManager.perfectHotDogTime/2);
					}
				}
				else
				{
					if(wisitor._order.Count > 0)
						wisitor.myWaitingTime-= 20;
				
					if(!wrongOrder)
					{
						wisitor.coinsSpent+=_usManager.HotdogBakeTime;
					}
					else
					{
						wisitor.coinsSpent+=(_usManager.HotdogBakeTime/2);
					}
				}
			
				if(yellowSauce || isSauce)
				{
					wisitor.coinsSpent+=10;
				}
				if(wisitor.myWaitingTime < 0)
				{
					wisitor.myWaitingTime = 0;
				}
			
				if(wisitor._order.Count <= 0)
					wisitor.MoveToEnd();
			
			}
			else
			{
				_levelSoundManager.dustbinSound.Play();
				_usManager.IsClickedHotDog = false;
				if(isPerfect)
					_uiManager.totalCoins-=_usManager.perfectHotDogTime;
				else
					_uiManager.totalCoins-=_usManager.HotdogBakeTime;
			
				_uiManager.coinsText.text = _uiManager.totalCoins.ToString ();
				if(_uiManager.totalCoins < 0)
				{
					_uiManager.totalCoins = 0;
					_uiManager.coinsText.text = "0";
				}
			}
			transform.position = _originalPos;
			transform.gameObject.SetActive(false);
		}

		private IEnumerator MoveRoutine()
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
			if(other.name.Contains ("customer") )
			{
				_otherObject = other.gameObject;
				wisitor = other.GetComponent<Wisitor>();
				for(int i = 0 ; i< wisitor._order.Count ; i++)
				{
					if(isTape == wisitor._order[i])
					{
						wrongOrder = false;
						_isOnCustomer = true;

						break;
					}
				}
				if(!_isOnCustomer && wisitor.iHaveAMultipleTypeOrder != LevelManager.Orders.NONE)
				{
			
					wrongOrder = true;
					_isOnCustomer = true;
				}
			}
			else if(other.name.Contains ("dustbin") && (USController._isEndTutorial == true || AustraliaController.tutorialEnd== true))
			{
				_otherObject = other.gameObject;
				_isOnCustomer = true;
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if(other.name.Contains ("customer") || other.name.Contains ("dustbin"))
			{
				wrongOrder = false;
				_isOnCustomer = false;
			}
		}
	}
}
