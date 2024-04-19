using System.Collections;
using _Project.Scripts.Managers;
using _Project.Scripts.Other;
using _Project.Scripts.UI_Scripts;
using _Project.Scripts.UI.Tutorial;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Scripts.Game
{
	public class ChineseUtils : MonoBehaviour
	{
		[Inject] private SoundsAll _levelSoundManager;
		[Inject] private China_Manager _chinaManager;
		[Inject] private UIManager _uiManager;   
		private bool _isReachedDestination;
		private bool _isScale;
		private bool _isPicked;
		private float _heatingTimer;
		private float _perfectTimer = 6f;
		private float _burningTimer = 18f;
		private bool _isCanMove;
		private bool _isTutorialPick;
		
		[FormerlySerializedAs("isSoupContainer")] public bool _isSoup;
		[FormerlySerializedAs("isPan")] public bool _isPan;
		[FormerlySerializedAs("noodlesAdded")] public bool _isNoodlesAdded;
		[FormerlySerializedAs("noOfServingsAvailable")] public int _servingsAvailable;
		[FormerlySerializedAs("myImage")] public SpriteRenderer _imageSpriteRenderer;
		[FormerlySerializedAs("ladleVariations")] public Sprite []_laddlesSprites;
		[FormerlySerializedAs("myFood")] public SpriteRenderer _foodSpriteRenderer;
		[FormerlySerializedAs("myReadyFood")] public GameObject _readyFood;
		[FormerlySerializedAs("myOriginalPos")] public Vector3 _originalPos;
		[FormerlySerializedAs("myTouchPos")] public Vector3 _touchPos;
		[FormerlySerializedAs("mySelection")] public GameObject _selection;
		[FormerlySerializedAs("fullCollider")] public BoxCollider []_colliders ;
		[FormerlySerializedAs("tipCollider")] public BoxCollider _tipCollider;
		[FormerlySerializedAs("mySmoke")] public ParticleSystem _smokeParticle;
		[FormerlySerializedAs("myScale")] public TweenScale _tweenScale;
		[FormerlySerializedAs("myAlpha")] public TweenAlpha _alphaTween;
		[FormerlySerializedAs("scaledImage")] public TweenScale _scaledImage;
		[FormerlySerializedAs("Pan_error")] public GameObject _panError;
		[FormerlySerializedAs("soup_error")] public GameObject _soupError;
		public bool _isVegAdded { get; set; }
		public bool _isSelected { get; set; }
		public bool _isBurnt { get; set; }
		public bool _isTutorialOn { get; set; }

		private void Start () 
		{
			_originalPos = transform.position;
		}


		private void OnDisable()
		{
			transform.localScale = Vector3.one;
			_isReachedDestination = false;
			_isSelected = false;
			_selection.SetActive (false);
		}

		private void Update () 
		{
			if(_servingsAvailable > 0 && !_isBurnt && !_isPicked && _isPan)
			{
				_heatingTimer+=Time.deltaTime;
				if(_heatingTimer > _perfectTimer && _heatingTimer <= _burningTimer)
				{
					if(_isTutorialOn && !_isTutorialPick)
					{
						_isTutorialPick = true;
						_uiManager.tutorialPanelBg.gameObject.SetActive (true);
						_chinaManager.MotionNoodlesPlates[0].tutorialOn = true;
						if(_isPan)
							_uiManager.tutorialPanelBg.OpenPopupChina ("TAP OR DRAG NOODLES ON \n THE PLATE.",false,false , 3);
					}
					if(!_readyFood.activeInHierarchy)
					{
						_alphaTween.ResetToBeginning ();
						_alphaTween.PlayForward ();
						_readyFood.gameObject.SetActive (true);
						_readyFood.GetComponent<SpriteRenderer>().sprite = _chinaManager.noodlesPan[3];
					}
				}
				else if(_heatingTimer > _burningTimer && !_isBurnt)
				{
					if(!TutorialPanel.popupPanelActive)
					{
						_isBurnt = true;  // burnt
						_readyFood.GetComponent<SpriteRenderer>().sprite = _chinaManager.noodlesPan[4];
						_smokeParticle.gameObject.SetActive (true);
						_smokeParticle.Play ();
					}
				}
			}
		}


		private void OnMouseDown()
		{
			if ((!TutorialPanel.popupPanelActive || China_Manager._endTutorial || _isTutorialPick ||
			     (_isSoup && _isTutorialOn)) && (_servingsAvailable > 0) && (_isNoodlesAdded && _isVegAdded))
			{
				_isPicked = true;
		
				_isScale = true;
				_isCanMove = true;
				_chinaManager.ResetBowlsCliked();
				_chinaManager.clickedUtensilsDestinationFunction = this;
				_isSelected = true;
				if (_isSoup)
				{
					_chinaManager.IsClickedSoupContainer = true;
					if (_isTutorialOn)
						_chinaManager.SoupBowlFirst.tutorialOn = true;
				}
				else
				{
					_chinaManager.IsPanClick = true;
				}

				if (!_isBurnt || _isSoup)
					_imageSpriteRenderer.sprite = _laddlesSprites[1];
				else
					_imageSpriteRenderer.sprite = _laddlesSprites[2];
				for (int i = 0; i < _colliders.Length; i++)
				{
					_colliders[i].enabled = false;
				}

				_tipCollider.enabled = true;

				Vector3 myPos = Camera.main.WorldToScreenPoint(transform.position);
				_touchPos =
					Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, myPos.z));

			}
			else if ((!TutorialPanel.popupPanelActive || !China_Manager._endTutorial) &&
			         _servingsAvailable <= 0) 
			{
				if (_isPan)
				{
					if (_chinaManager.NoodlesVeg && !_isVegAdded)
					{
						_chinaManager.clickedItemDestinationFunction.utensil = this;
						_chinaManager.ObjectReach();
						_chinaManager.ResetBowlsCliked();
					}
					else if (_chinaManager.ClikedNoodles && !_isNoodlesAdded)
					{
						_chinaManager.clickedItemDestinationFunction.utensil = this;
						_chinaManager.ObjectReach();
						_chinaManager.ResetBowlsCliked();
					}
					else
					{
						_panError.SetActive(true);
					}
				}
				else if (_isSoup)
				{
					if (_chinaManager.IsClikedSoupVeg && !_isVegAdded)
					{
						_chinaManager.clickedItemDestinationFunction.utensil = this;
						_chinaManager.ObjectReach();
						_chinaManager.ResetBowlsCliked();
					}
					else
					{
						_soupError.SetActive(true);
					}
				}
			}
		}

		private void OnMouseDrag()
		{
			if( _isCanMove)
			{
				_panError.SetActive(false);
				_soupError.SetActive(false);
				Vector3 myPos = Camera.main.WorldToScreenPoint (transform.position);
				Vector3 newPos = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y , myPos.z));
				if(Vector3.Distance (newPos,_touchPos) > 0.2f)
				{
					transform.position =  newPos;
				}
			
			}
		}


		private void OnMouseUp()
		{
			_panError.SetActive (false);
			_soupError.SetActive (false);
			if(_isCanMove)
			{
				if(!_isReachedDestination)
					StartCoroutine(MoveRoutine());
				else
				{
					if(_isSoup)
					{
						OnDestinationClick();
						StartCoroutine(MoveRoutine(false));
					}
					else if(_isPan)
					{
						OnDestinationClick();
						StartCoroutine(MoveRoutine(false));

					
					}
				}
				_isCanMove = false;
				_isPicked = false;
			}
			_tipCollider.enabled = false;
			for(int i = 0 ; i < _colliders.Length ; i++)
			{
				_colliders[i].enabled = true;
			}
		}
		public void DeactivateDustbin()
		{
			_uiManager.dustbin_textparent.SetActive (false);
			_uiManager.dustbin_textparent.transform.position = _uiManager.dustbintextintialposition;
		}

		public void OnDestinationClick()
		{
			if(otherObject.name.Contains ("dustbin"))
			{
				if(_isSoup)
				{
					_isBurnt = false;
					_heatingTimer = 0;
					_servingsAvailable= 0;
					_alphaTween.gameObject.SetActive (false);
					_foodSpriteRenderer.gameObject.SetActive (false);
					_readyFood.GetComponent<SpriteRenderer>().sprite = _chinaManager.SoupContainer[0];
					_imageSpriteRenderer.sprite = _laddlesSprites[0];
					_isVegAdded = false;
					_chinaManager.IsClickedSoupContainer = false;
					_uiManager.totalCoins -= _chinaManager.SoupPrice;
					_uiManager.coinsText.text = _uiManager.totalCoins.ToString ();
			
				}
				else if(_isPan)
				{
					_isBurnt = false;
					_heatingTimer = 0;
					_smokeParticle.gameObject.SetActive (false);
					_servingsAvailable = 0;
					_scaledImage.gameObject.SetActive (false);
					_foodSpriteRenderer.gameObject.SetActive (false);
					_readyFood.gameObject.SetActive (false);
					_imageSpriteRenderer.sprite = _laddlesSprites[0];
					_isNoodlesAdded = false;
					_isVegAdded = false;
					_chinaManager.IsPanClick = false;
					_uiManager.totalCoins-=_chinaManager.perfectNoodlesPrice;
					_uiManager.coinsText.text = _uiManager.totalCoins.ToString ();
					_levelSoundManager.dustbinSound.Play();
					if(_uiManager.totalCoins > 0) 
					{
						_uiManager.dustbin_textparent.SetActive(true);
						_uiManager.dustbin_text.text = "-"+_chinaManager.perfectNoodlesPrice.ToString(); 
						Invoke(nameof(DeactivateDustbin),1.0f);
					}
					if(_uiManager.totalCoins < 0)
					{
						_uiManager.totalCoins = 0;
						_uiManager.coinsText.text = "0";
					}
				}
				_isSelected = false;
			}
			else
			{
				if(_isSoup)
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
							_servingsAvailable--;
							if(_servingsAvailable <= 0)
							{
								_isBurnt = false;
								_heatingTimer = 0;
								_alphaTween.gameObject.SetActive (false);
								_foodSpriteRenderer.gameObject.SetActive (false);
								_readyFood.GetComponent<SpriteRenderer>().sprite = _chinaManager.SoupContainer[0];
								_imageSpriteRenderer.sprite = _laddlesSprites[0];
								_isVegAdded = false;
							}
							if(_isTutorialOn)
							{
								_isTutorialPick = false;
								_chinaManager.SoupBowlFirst.tutorialOn = true;
								_uiManager.tutorialPanelBg.OpenPopupChina ("DRAG TO THE \n CUSTOMER.",false,false , 6);
								_isTutorialOn = false;
							}
							_isSelected = false;
					
							_chinaManager.IsClickedSoupContainer = false;
						}
					}
				}
				else if(_isPan)
				{
					if(otherObject != null)
					{
						ObjectMotion otherObjectMotion = otherObject.GetComponent<ObjectMotion>();
					
						if(!otherObjectMotion.myNoodles.activeInHierarchy)
						{
							if(_isTutorialOn)
							{
								_isTutorialPick = false;
								_chinaManager.CustomerFirst.tutorialOn = true;
								_chinaManager.MotionNoodlesPlates[0].tutorialOn = true;
								_uiManager.tutorialPanelBg.OpenPopupChina ("DRAG TO THE \n CUSTOMER.",false,false , 6);
								_isTutorialOn = false;
							}
							otherObjectMotion.myNoodles.SetActive (true);
							otherObjectMotion.myType = LevelManager.Orders.NOODLES;
							if(_heatingTimer >= _perfectTimer && !_isBurnt)
							{
								otherObjectMotion.perfect = true;
								otherObjectMotion.myNoodles.GetComponent<SpriteRenderer>().sprite = _chinaManager.PlateVariations[1];
							
							}
							else if(!_isBurnt)
							{
								otherObjectMotion.myNoodles.GetComponent<SpriteRenderer>().sprite = _chinaManager.PlateVariations[0];
							}
							_servingsAvailable--;
							if(_servingsAvailable <= 0)
							{
								_smokeParticle.gameObject.SetActive (false);
								_isBurnt = false;
								_scaledImage.gameObject.SetActive (false);
								_heatingTimer = 0;
								_foodSpriteRenderer.gameObject.SetActive (false);
								_readyFood.gameObject.SetActive (false);
								_imageSpriteRenderer.sprite = _laddlesSprites[0];
								_isNoodlesAdded = false;
								_isVegAdded = false;
							}
							_chinaManager.IsPanClick = false;
							_isSelected = false;
						}
					}
				}
			}
		}

		private IEnumerator MoveRoutine(bool showSelection = true)
		{
			transform.localEulerAngles = Vector3.zero;
			_isReachedDestination = false;
			float distance = Vector3.Distance (transform.position , _originalPos);
			float speed = 15;
			while(distance > 0.1f)
			{
				float step = speed * Time.deltaTime;
				transform.position = Vector3.MoveTowards(transform.position, _originalPos, step);
				distance = Vector3.Distance (transform.position , _originalPos);
				yield return 0;
			}
			if(_isSelected && showSelection)
				_selection.SetActive (true);
			transform.position = _originalPos;
		}
	
		public GameObject otherObject;


		private void OnTriggerStay(Collider other)
		{
			if(_servingsAvailable > 0)
			{
				if(_isSoup)
				{
					if(other.name.Contains ("soupBowl"))
					{
						otherObject = other.gameObject;
						ObjectMotion otherObjectMotion = otherObject.GetComponent<ObjectMotion>();
					
						if(!otherObjectMotion.mySoup.activeInHierarchy)
						{
							_levelSoundManager.buttnClickSound.Play();
							_isReachedDestination = true;
						}
					}
				}
				else if(_isPan)
				{
					if(!_isBurnt)
					{
						if(other.name.Contains ("plate"))
						{
							otherObject = other.gameObject;

							ObjectMotion otherObjectMotion = otherObject.GetComponent<ObjectMotion>();
						
							if(!otherObjectMotion.myNoodles.activeInHierarchy)
							{
								_levelSoundManager.buttnClickSound.Play();
								_isReachedDestination = true;
							}
						}
					}
				}
				if(China_Manager._endTutorial)
				{
					if(other.name.Contains("dustbin"))
					{
						otherObject = other.gameObject;
						_isReachedDestination = true;
					}
				}
			}
		}

		private void OnTriggerExit(Collider other)
		{

			if(_isSoup)
			{
				if(other.name.Contains ("soupBowl"))
				{
					if(otherObject != null)
					{
						if(otherObject == other.gameObject)
						{
							otherObject = null;
							_isReachedDestination = false;
						}
					}
					else
					{
						otherObject = null;
						_isReachedDestination = false;
					}
				}
			}
			else if(_isPan)
			{
				if(!_isBurnt)
				{
					if(other.name.Contains ("plate"))
					{
						if(otherObject != null)
						{
							if(otherObject == other.gameObject)
							{
								otherObject = null;
								_isReachedDestination = false;
							}
						}
						else
						{
							otherObject = null;
							_isReachedDestination = false;
						}
					}
				}
			}
			if(China_Manager._endTutorial)
			{
				if(other.name.Contains("dustbin"))
				{
					otherObject = null;
					_isReachedDestination = false;
				}
			}
		}

	}
}
