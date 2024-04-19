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
	public class Pizza : MonoBehaviour
	{
		[Inject] private LevelSoundManager _levelSoundManager;
		[Inject] private Italy_Manager _italyManager;
		[Inject] private UIManager _uiManager;   
		private Vector3 _colliderSize;
		private bool _isCanMove;
		private bool _isOnCustomer;
		private bool _tutorialPick;
		private bool _isPick;
		private float _isPerfect = 6f;
		private float _burningTimer = 12f;
		
		[FormerlySerializedAs("cheese")] public bool _isCheese;
		[FormerlySerializedAs("vegetable")] public bool _isVegetable;
		[FormerlySerializedAs("myToppings")] public SpriteRenderer _topping;
		[FormerlySerializedAs("myCheese")] public GameObject _cheesePrefav;
		[FormerlySerializedAs("myPlate")] public Availability _plate;
		[FormerlySerializedAs("myOriginalPos")] public Vector3 _originalPos;
		[FormerlySerializedAs("myTouchPos")] public Vector3 _touchPos;
		[FormerlySerializedAs("myLocalScale")] public Vector3 _localScale;
		[FormerlySerializedAs("mySelection")] public GameObject _selectionObject;
		[FormerlySerializedAs("isInOven")] public bool _isOnOven;
		[FormerlySerializedAs("mySmoke")] public ParticleSystem _smokeParticle;
		[FormerlySerializedAs("pizzaCompletelyBaked")] public ParticleSystem _pizzaBakedParticle;
		[FormerlySerializedAs("myRenderer")] public SpriteRenderer _renderer;
		[FormerlySerializedAs("myContainerCollider")] public BoxCollider _colliderConteiner;
		[FormerlySerializedAs("error")] public GameObject _error;
		public bool isBurnt { get; private set; }
		public bool wrongOrderGiven { get; set; }
		public float heatingTimer { get; set; }
		public Wisitor customer{ get; set; }
		public bool tutorialOn{ get; set; }
		public LevelManager.Orders myType { get; set; }
		public bool perfect{ get; set; }
		public bool iAmSelected{ get; set; }
		private void Start()
		{
			_uiManager.n_Pizzas_served = PlayerPrefs.GetInt ("PizzaServed");
			_localScale = transform.localScale;
			_originalPos = transform.position;
			_colliderSize = transform.GetComponent<BoxCollider> ().size;
		}

		private void OnDisable()
		{
			perfect =false;
			wrongOrderGiven = false;
			if(!_isOnOven)
			{
				_isCheese = false;
				_isVegetable  = false;
			}
			_isOnCustomer = false;
			_topping.gameObject.SetActive (false);
			_cheesePrefav.gameObject.SetActive (false);
			transform.GetComponent<SpriteRenderer>().sprite = _italyManager.pizzaBakedVariations[0];
			myType = LevelManager.Orders.NONE; 
			transform.localScale = _localScale;
			iAmSelected = false;
			_selectionObject.SetActive (false);
			heatingTimer = 0;
			_isPick = false;
			if(_smokeParticle != null)
			{
				_smokeParticle.gameObject.SetActive (false);
				_smokeParticle.Stop ();
			}
			isBurnt = false;
		}

		private void Update () 
		{
			if(!isBurnt && !_isPick && _isOnOven)
			{
				heatingTimer+=Time.deltaTime;
				if(heatingTimer > _isPerfect && heatingTimer <= _burningTimer)
				{
					if(tutorialOn)
					{
						_tutorialPick = true;
						_uiManager.tutorialPanelBg.gameObject.SetActive (true);
						_uiManager.tutorialPanelBg.OpenPopupItaly ("TAP OR DRAG THIS TO \n THE CUSTOMER.",false,false , 4);
						tutorialOn = false;
					}
					if(_renderer.sprite == _italyManager.pizzaBakedVariations[0])
					{
						perfect = true;
						_pizzaBakedParticle.gameObject.SetActive(true);
						_pizzaBakedParticle.Play ();
						_cheesePrefav.gameObject.SetActive (false);
						_topping.gameObject.SetActive (false);
						if(myType == LevelManager.Orders.VEG_PIZZA)
						{
							_renderer.sprite = _italyManager.pizzaBakedVariations[1];
				
						}
						else
						{
							_renderer.sprite = _italyManager.pizzaBakedVariations[2];

						}
					}
				}
				else if(heatingTimer > _burningTimer && !isBurnt)
				{

					if(!TutorialPanel.popupPanelActive)
					{
						perfect =false;
						_renderer.sprite = _italyManager.pizzaBakedVariations[3];
						isBurnt = true;  // burnt
						_pizzaBakedParticle.Stop();
						_pizzaBakedParticle.gameObject.SetActive(false);
						_smokeParticle.gameObject.SetActive (true);
						_smokeParticle.Play ();
					}
				}
			}
		}

		private void OnMouseDown()
		{
			if((!TutorialPanel.popupPanelActive || Italy_Manager.tutorialEnd || _tutorialPick || (tutorialOn && !_isOnOven)) && (_isVegetable && _isCheese) )
			{
				_isPick = true;
				_isCanMove = true;
				_italyManager.AllClickedBoolsReset ();
				_italyManager.clickedPizzaDestinationFunction = this;
				iAmSelected = true;
				if(_isOnOven)
				{
					transform.GetComponent<BoxCollider> ().size = new Vector3(_colliderSize.x/2f , _colliderSize.y/2f , _colliderSize.z);
					_italyManager.clickedOvenPizza = true;
				}
				else
					_italyManager.clickedPlatePizza = true;
				
				if(_tutorialPick)
				{
					_italyManager.firstCustomer.tutorialOn = true;
				}
				Vector3 myPos = Camera.main.WorldToScreenPoint (transform.position);
				_touchPos =  Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y , myPos.z));
			}
			else if((!TutorialPanel.popupPanelActive || !Italy_Manager.tutorialEnd) && (!_isVegetable || !_isCheese)) // Click on it to place ingredients
			{

				if(_italyManager.clickedVeg || _italyManager.clickedNonVeg && !_isVegetable)
				{
					_italyManager.clickedItemDestinationFunction.availablePizza = this;
					_italyManager.ObjectReached ();
					_italyManager.AllClickedBoolsReset ();
				}
				else if(_italyManager.clickedCheese && !_isCheese)
				{
					_italyManager.clickedItemDestinationFunction.availablePizza = this;
					_italyManager.ObjectReached ();
					_italyManager.AllClickedBoolsReset ();
				}
				else
				{
					_error.SetActive(true);
				}
			}

		}

		private void OnMouseDrag()
		{
			if( _isCanMove)
			{
				_error.SetActive(false);
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
			_error.SetActive(false);
			if(_isCanMove)
			{
				if(!_isOnCustomer)
					StartCoroutine(MoveRoutine());
				else
				{

					DestinationClick ();
					transform.position = _originalPos;
					transform.gameObject.SetActive(false);
				}
				_isCanMove = false;
			}
			transform.GetComponent<BoxCollider> ().size = _colliderSize;
		}
		public void Deactivate()
		{
			_uiManager.achievment_text.SetActive (false);
		}
		public void DestinationClick()
		{
			_colliderConteiner.enabled = true;
			_plate.available = true;
			if(otherObject.name.Contains ("customer"))
			{
				_uiManager.n_Pizzas_served++;
				_levelSoundManager.customerEat.Play();
				PlayerPrefs.SetInt ("PizzaServed", _uiManager.n_Pizzas_served);
			
			
				if(PlayerPrefs.GetInt("PizzaServed") > 9 && PlayerPrefs.GetInt ("PizzaLevel1")==0)
				{
					PlayerPrefs.SetInt ("PizzaLevel1",1);
					_uiManager.achievment_text.SetActive(true);
					AchievementBlock._claimCheck++;
					PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
					Invoke(nameof(Deactivate),4.0f);
				}
				if(PlayerPrefs.GetInt("PizzaServed") > 99 && PlayerPrefs.GetInt ("PizzaLevel2")==0)
				{
					PlayerPrefs.SetInt ("PizzaLevel2",1);

					AchievementBlock._claimCheck++;
					PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
					_uiManager.achievment_text.SetActive(true);
					Invoke(nameof(Deactivate),4.0f);
				}
				if(PlayerPrefs.GetInt("PizzaServed") > 999 && PlayerPrefs.GetInt ("PizzaLevel3")==0)
				{
					PlayerPrefs.SetInt ("PizzaLevel3",1);
					_uiManager.achievment_text.SetActive(true);
					AchievementBlock._claimCheck++;
					PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
					Invoke(nameof(Deactivate),4.0f);
				}
				_italyManager.platesFilledCount--;
				string myTypeToEatSub = "PIZZA";
				for(int count = 0; count < customer._order.Count; count++)
				{
					if(customer._order[count].ToString().Contains(myTypeToEatSub.ToString()))
					{
						if(customer._order[count] == myType)
						{
							wrongOrderGiven = false;
							
						}
						else
						{
							wrongOrderGiven = true;
							
						}
						customer._order.RemoveAt(count);
					}
					
				}
				customer.RemoveOrderFromBoard (myType);

				if(_tutorialPick)
				{
					_uiManager.tutorialPanelBg.OpenPopupItaly ("PUT BURNT PIZZA \n IN THE DUSTBIN!",false,false , 7 , 1);
					_tutorialPick = false;
				}
			
				customer.iHaveAMultipleTypeOrder = LevelManager.Orders.NONE;
				if(perfect)
				{
					if(customer._order.Count > 0)
						customer.myWaitingTime-= 30;
				
					if(!wrongOrderGiven)
					{
						customer.coinsSpent+=_italyManager.perfectPizza;
						customer.perfect = true;
					}
					else
					{
						customer.coinsSpent+=(_italyManager.perfectPizza/2);
					}
				}
				else
				{
					if(customer._order.Count > 0)
						customer.myWaitingTime-= 20;
					if(!wrongOrderGiven)
					{
						customer.coinsSpent+=_italyManager.lessBakedPizza;
					}
					else
					{
						customer.coinsSpent+=(_italyManager.lessBakedPizza/2);
					
					}
				}
				if(customer.myWaitingTime < 0)
				{
					customer.myWaitingTime = 0;
				}
			
				if(customer._order.Count <= 0)
					customer.MoveToEnd();
			
			}
			else if(otherObject.name.Contains ("ovenPlace"))
			{
				if(tutorialOn)
				{
					pizzaDestinationAvailable = _italyManager.firstOvenAvailabe;
					_uiManager.tutorialPanelBg.OpenPopupItaly ("WAIT FOR PIZZA \n TO BAKE.",false,false , 4);
					_italyManager.ovenPizzas[pizzaDestinationAvailable.myPositionInArray].tutorialOn = true;
					tutorialOn = false;
				}

				_italyManager.platesFilledCount--;
				pizzaDestinationAvailable.available = false;
				_italyManager.ovenPizzaRenderer[pizzaDestinationAvailable.myPositionInArray].gameObject.SetActive (true);


				_italyManager.ovenPizzas[pizzaDestinationAvailable.myPositionInArray].myType = myType;
				_italyManager.ovenPizzas[pizzaDestinationAvailable.myPositionInArray]._cheesePrefav.SetActive (true);
				_italyManager.ovenPizzas[pizzaDestinationAvailable.myPositionInArray]._topping.gameObject.SetActive (true);
				if(myType == LevelManager.Orders.VEG_PIZZA)
					_italyManager.ovenPizzas[pizzaDestinationAvailable.myPositionInArray]._topping.sprite = _italyManager.pizzaToppings[0];
				else
					_italyManager.ovenPizzas[pizzaDestinationAvailable.myPositionInArray]._topping.sprite = _italyManager.pizzaToppings[1];
				
				_italyManager.ovenColliders[pizzaDestinationAvailable.myPositionInArray].enabled = false;

			}
			else  
			{
				if(!_isOnOven)
					_italyManager.platesFilledCount--;
				if(perfect)
				{
					_uiManager.totalCoins-= _italyManager.perfectPizza;
					_uiManager.coinsText.text = _uiManager.totalCoins.ToString ();
					_levelSoundManager.dustbin.Play();
					if(_uiManager.totalCoins > 0){
						_uiManager.dustbin_textparent.SetActive(true);
						_uiManager.dustbin_text.text = "-"+_italyManager.perfectPizza.ToString(); 
						Invoke(nameof(DeactivateDubin),1.0f);
					}
					if(_uiManager.totalCoins < 0)
					{
						_uiManager.totalCoins = 0;
						_uiManager.coinsText.text = "0";
					}
				}
				else
				{
					_uiManager.totalCoins-=_italyManager.lessBakedPizza;
					_uiManager.coinsText.text = _uiManager.totalCoins.ToString ();
					_levelSoundManager.dustbin.Play();
					if(_uiManager.totalCoins > 0){
						_uiManager.dustbin_textparent.SetActive(true);
						_uiManager.dustbin_text.text = "-"+_italyManager.lessBakedPizza.ToString(); 
						Invoke(nameof(DeactivateDubin),1.0f);
					}
					if(_uiManager.totalCoins < 0)
					{
						_uiManager.totalCoins = 0;
						_uiManager.coinsText.text = "0";
					}
				}
			
			}
			transform.position = _originalPos;
			transform.gameObject.SetActive(false);
		}
		public void DeactivateDubin()
		{
			_uiManager.dustbin_textparent.SetActive (false);
			_uiManager.dustbin_textparent.transform.position = _uiManager.dustbintextintialposition;
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
			_isPick = false;
			if(iAmSelected)
				_selectionObject.SetActive (true);
			transform.position = _originalPos;
		}
	
	
		public GameObject otherObject;
		public Availability pizzaDestinationAvailable;

		private void OnTriggerStay(Collider other)
		{
			if(!_isOnOven)
			{
				if(other.name.Contains ("ovenPlace"))
				{
					otherObject = other.gameObject;
					pizzaDestinationAvailable = other.GetComponent<Availability>();
					if(pizzaDestinationAvailable.available)
					{
						_isOnCustomer = true;
					}
				}
			}
			else
			{
				if(other.name.Contains ("customer") && !isBurnt)
				{
					otherObject = other.gameObject;
					customer = other.GetComponent<Wisitor>();
					for(int i = 0 ; i< customer._order.Count ; i++)
					{
						if(myType == customer._order[i])
						{
							wrongOrderGiven = false;
							_isOnCustomer = true;
							break;
						}
					}
					if(!_isOnCustomer && customer.iHaveAMultipleTypeOrder != LevelManager.Orders.NONE)
					{
						wrongOrderGiven = true;
						_isOnCustomer = true;
					}
				}
			}
			if(other.name.Contains ("dustbin") && Italy_Manager.tutorialEnd == true)
			{
				otherObject = other.gameObject;
				_isOnCustomer = true;
			}
		
		}

		private void OnTriggerExit(Collider other)
		{
			if(other.name.Contains ("customer") || other.name.Contains ("dustbin"))
			{
				otherObject = null;
				wrongOrderGiven = false;
				_isOnCustomer = false;
			}
			else if(other.name.Contains ("ovenPlace") )
			{
				if(otherObject != null)
				{
					if(otherObject == other.gameObject)
					{
						otherObject = null;
						wrongOrderGiven = false;
						_isOnCustomer = false;
					}
				}
				else
				{
					wrongOrderGiven = false;
					_isOnCustomer = false;
				}
			}
		
		}
	}
}
