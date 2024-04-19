using _Project.Scripts.Additional;
using _Project.Scripts.Entities.Customers;
using _Project.Scripts.Food;
using _Project.Scripts.Other;
using _Project.Scripts.UI_Scripts;
using _Project.Scripts.UI.Tutorial;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Scripts.Managers
{
	public class Italy_Manager : MonoBehaviour 
	{
		public static bool _isEndTutorial;
		[Inject] private UIManager _uiManager;   
		[Inject] private LevelSoundManager _levelSoundManager;
		private int _ovensAvailable;
		private int _platesAvailable;
		private int _cockeAvailable;
		private int _fridgeFilled;
		private bool _hasFridge;
		
		[FormerlySerializedAs("TheifPanel")] [SerializeField] private GameObject _theifPanel;
		[FormerlySerializedAs("pizzaPlates")] [SerializeField] private SpriteRenderer []_platesPizza; //6
		[FormerlySerializedAs("pizzaOnPlates")] [SerializeField] private SpriteRenderer []_pizzaPlates; //6
		[FormerlySerializedAs("plateColliders")] [SerializeField] private BoxCollider []_platesCollers; //6
		[FormerlySerializedAs("ovens")] [SerializeField] private SpriteRenderer []_ovens; //6
		[FormerlySerializedAs("ovenHot")] [SerializeField] private Sprite _hotOvenSprite;
		[FormerlySerializedAs("ovenPlaces")] [SerializeField] private Availability []_ovensPlaces; //6
		[FormerlySerializedAs("cokeBottles")] [SerializeField] private SpriteRenderer []_cokeBottles; //9
		[FormerlySerializedAs("cokePlaces")] [SerializeField] private Availability []_places;  //9
		[FormerlySerializedAs("fridePlaces")] [SerializeField] private bool []_fridgePlaces;  //9
		[FormerlySerializedAs("frideBottles")] [SerializeField] private GameObject []_bottlesFridge;
		[FormerlySerializedAs("dustbin")] [SerializeField] private GameObject _dustbin;
		[FormerlySerializedAs("nonVeg")] [SerializeField] private ObjectMotion _nonVeg;
		[FormerlySerializedAs("vegetables")] [SerializeField] private ObjectMotion _vegetables;
		[FormerlySerializedAs("cupCake")] [SerializeField] private ObjectMotion _cupCake;
		[FormerlySerializedAs("cokeAdd")] [SerializeField] private SpriteRenderer _cokeAdd ;
		[FormerlySerializedAs("nonVegAdd")] [SerializeField] private SpriteRenderer _nonVegAddSprite ;
		[FormerlySerializedAs("tableTop")] [SerializeField] private SpriteRenderer _tableTopSpriteRenderer;
		[FormerlySerializedAs("tableCover")] [SerializeField] private SpriteRenderer _tableCoverSpriteRenderer;
		[FormerlySerializedAs("cokeFridge")] [SerializeField] private GameObject _fridge;
		[FormerlySerializedAs("Radio")] [SerializeField] private GameObject _radio ;
		[FormerlySerializedAs("Whistle")] [SerializeField] private GameObject _whistle ;
		[FormerlySerializedAs("bell")] [SerializeField] private GameObject _bell ;
		[FormerlySerializedAs("handcuff")] [SerializeField] private GameObject _handcuff ;
		[FormerlySerializedAs("nonvegflag")] [SerializeField] private GameObject _nonVegFlag;  
		[FormerlySerializedAs("starting_text")] [SerializeField] private GameObject _startingText ;
		[FormerlySerializedAs("cokeBottlesSprites")] [SerializeField] private Sprite []_cokeBottlesSprites;
		[FormerlySerializedAs("firstPizza")] public Pizza _firstPizza ;
		[FormerlySerializedAs("firstOvenAvailabe")] public Availability _firstOvenAvailabe;
		[FormerlySerializedAs("firstOvenPizza")] public Pizza _firstOvenPizza;
		[FormerlySerializedAs("cheese")] public ObjectMotion _cheese;
		[FormerlySerializedAs("ovenColliders")] public BoxCollider []_ovenColliders; //6
		[FormerlySerializedAs("ovenPizzas")] public Pizza []_ovenPizzas; //6
		[FormerlySerializedAs("ovenPizzaRenderer")] public SpriteRenderer []_ovenPizzaRenderer;  //6
		[FormerlySerializedAs("pizzaBakedVariations")] public Sprite []_pizzaBakedVariations;  //3 -- 0 - pizze base , 1-veg baked ,2- non veg baked  
		[FormerlySerializedAs("pizzaDot")] public Sprite[]_pizzaDot ;
		[FormerlySerializedAs("pizzaToppings")] public Sprite []_pizzaToppings; 
		public int _platesFilledCount { get; set; }
		public int CokesFilled { get; set; }
		public bool IsClickedNonVeg { get; set; }
		public bool IsClickedVeg { get; set; }
		public bool IsClickedCheese { get; set; }
		public bool IsClickedCoke { get; set; }
		public bool IsClickedOvenPizza { get; set; }
		public bool IsClickedPlatePizza { get; set; }
		public ObjectMotion IsClickedItemDestinationFunction { get; set; }
		public Pizza _pizza { get; set; }
		public Wisitor _customerFirst { get; set; }
		public bool IsClickFirstBase { get; set; }
		public int CokePrice => 10;
		public int LessBakedPizza => 30;
		public int PerfectPizza => 60;
		
		
		private void OnEnable()
		{
			if (LevelManager.levelNo == 21) {
				_startingText.SetActive(true);
			}
			if(PlayerPrefs.HasKey ("Radio"))
			{
			
				_radio.SetActive(true);
			}
			if(PlayerPrefs.HasKey ("Whistle"))
			{

				_whistle.SetActive(true);
			}
			if (PlayerPrefs.HasKey ("Bell"))
			{
			
				_bell.SetActive(true);
			}
			if(MenuManager.handcuffNo > 0)
			{
				_handcuff.SetActive(true);
			}
		
		}

		public void OnPizzaReach()
		{
			_pizza.DestinationClick ();
		}

		public void OnObjectReach()
		{
			IsClickedItemDestinationFunction.ClickedDestination ();
		}

		private void Start()
		{
			PlayerPrefs.SetInt ("ItalyOpen",1);
			China_Manager._endTutorial = false;
			US_Manager.tutorialEnd = false;
			Australia_Manager.tutorialEnd = false;
			_isEndTutorial = false;
			if(LevelManager.levelNo == 21)
			{
				_cokeAdd.gameObject.SetActive (false);
				_nonVegAddSprite.gameObject.SetActive (false);
				_nonVegFlag.gameObject.SetActive(false);

			}
			else if(LevelManager.levelNo == 22)
			{
				_cokeAdd.gameObject.SetActive (false);
			}
		
			int platesUpgradeValue =  (int)Encryption.Decrypt (PlayerPrefs.GetString("ItalyPlateUpgrade")); 
			_platesAvailable = 2+(platesUpgradeValue*2);
			for(int i = 0; i < _platesAvailable ; i++)
			{
				_platesPizza[i].color = new Color(1,1,1,1);
			}

			int cokeUpgradeValue =  (int)Encryption.Decrypt (PlayerPrefs.GetString("ItalyCokeUpgrade")); 
			_cockeAvailable = 2+(cokeUpgradeValue*2);
			int ovenToUpgrade =  (int)Encryption.Decrypt (PlayerPrefs.GetString("OvenUpgrade")); 
			_ovensAvailable = 2+(ovenToUpgrade*2);
	
			_ovensPlaces[0].available = true;
			_ovenColliders[0].enabled = true;
			for(int i = 1; i < _ovensAvailable ; i++)
			{
				_ovens[i].sprite = _hotOvenSprite;
				_ovensPlaces[i].available = true;
				_ovenColliders[i].enabled = true;
			}

			_tableCoverSpriteRenderer.sprite = Resources.Load<Sprite> (PlayerPrefs.GetString ("Italy_TableCover"));
			_tableTopSpriteRenderer.sprite = Resources.Load<Sprite> (PlayerPrefs.GetString ("Italy_TableTop")) as Sprite;
			char []coverVal = PlayerPrefs.GetString ("Italy_TableCover").ToCharArray ();
			_uiManager._tabelcover = int.Parse (coverVal[coverVal.Length - 1].ToString ());
			char []coverVal2 = PlayerPrefs.GetString ("Italy_TableTop").ToCharArray ();
			_uiManager._tabeltop = int.Parse (coverVal[coverVal.Length - 1].ToString ());
			if(MenuManager.cupcakeNo <= 0)
			{
				_cupCake.gameObject.SetActive (false);
			}

			_uiManager.ForCoinAdd ();
			if(!PlayerPrefs.HasKey ("Fridge"))
			{
				_fridge.SetActive (false);
				_fridgeFilled = _cockeAvailable;
				_hasFridge = false;
			}
			else
			{
				if(LevelManager.levelNo < 23)
				{
					_fridge.SetActive (false);
					_fridgeFilled = _cockeAvailable;
					_hasFridge = false;
				}
				else
				{
					_hasFridge = true;
				}
			}
		}
	
		public void AddMoreBases()
		{
			if(IsClickFirstBase || (_isEndTutorial && !TutorialPanel.popupPanelActive))
			{
				ResetAllBool();
				if(_platesFilledCount < _platesAvailable)
				{
					for(int i = 0 ; i < _platesAvailable ; i++)
					{
						if(_platesPizza[i].gameObject.GetComponent<Availability>().available)
						{
							_platesCollers[i].enabled = false;
							_pizzaPlates[i].gameObject.SetActive (true);
							_platesFilledCount++;
							_platesPizza[i].gameObject.GetComponent<Availability>().available = false;
							_pizzaPlates[i].transform.GetComponent<Pizza>().perfect = false;
							break;
						}
					}
				}
				if(IsClickFirstBase)
				{
					_vegetables.tutorialOn = true;
					_uiManager.tutorialPanelBg.gameObject.SetActive (true);
					_uiManager.tutorialPanelBg.OpenPopupItaly ("TAP OR DRAG VEGETABELS \n TO PIZZA BASE.",false,false , 1);
				}
				IsClickFirstBase = false;
			}
		}

		private void DeactivateSelections()
		{
			for(int i = 0 ; i < _platesAvailable ; i++)
			{
				if(!_platesPizza[i].gameObject.GetComponent<Availability>().available)
				{
					Pizza myPizza = _pizzaPlates[i].transform.GetComponent<Pizza>();
					myPizza.iAmSelected = false;
					myPizza._selectionObject.SetActive (false);
				}
			}
		}

		private void UnselectionOven()
		{
			for(int i = 0 ; i < _ovensAvailable ; i++)
			{
				if(!_ovensPlaces[i].available)
				{
					_ovenPizzas[i].iAmSelected = false;
					_ovenPizzas[i]._selectionObject.SetActive (false);
				}
			}
		}


		public void AddCokeBottlesFromFridge()
		{
			if(_fridgeFilled > 0)
			{
				if(CokesFilled < _cockeAvailable)
				{
					for(int i = 0 ; i < _cockeAvailable ; i++)
					{
						if(_places[i].available)
						{
							_levelSoundManager.bottle_click.Play();
							_cokeBottles[i].gameObject.SetActive (true);
							_cokeBottles[i].color = new Color(1,1,1,1);
							_cokeBottles[i].sprite = _cokeBottlesSprites[1];
							CokesFilled++;
							_cokeBottles[i].gameObject.GetComponent<ObjectMotion>().isChilled = true;
							for(int j = 0 ; j < _cockeAvailable ; j++)
							{
								if(!_fridgePlaces[j])
								{
									_bottlesFridge[j].SetActive (false);
									_fridgeFilled--;
									_fridgePlaces[j] = true;
									break;
								}
							}
							_places[i].available = false;
							break;
						}
					}
				}
			}
		}
	


		public void AddCokeBottles()
		{
			if(_isEndTutorial && !TutorialPanel.popupPanelActive)
			{
				ResetAllBool();
		
				if(_fridgeFilled < _cockeAvailable && _hasFridge)
				{
					for(int i = 0 ; i < _cockeAvailable ; i++)
					{
						if(_fridgePlaces[i])
						{
							_levelSoundManager.bottle_click.Play();
							_bottlesFridge[i].SetActive (true);
							_fridgeFilled++;
							_fridgePlaces[i] = false;
							break;
						}
					}
				}

				else if(CokesFilled < _cockeAvailable)
				{
					for(int i = 0 ; i < _cockeAvailable ; i++)
					{
						if(_places[i].available)
						{
							_levelSoundManager.bottle_click.Play();
							_cokeBottles[i].gameObject.SetActive (true);
							_cokeBottles[i].color = new Color(1,1,1,1);
							_cokeBottles[i].sprite = _cokeBottlesSprites[0];
							CokesFilled++;
							_places[i].available = false;
							break;
						}
					}
				}
			}
		}

		private void UnselectAllBottles()
		{
			for(int i = 0 ; i < _cockeAvailable ; i++)
			{
				if(_cokeBottles[i].gameObject.activeInHierarchy)
				{
					if(_cokeBottles[i].GetComponent<ObjectMotion>().iAmSelected)
					{
						_cokeBottles[i].GetComponent<ObjectMotion>().iAmSelected = false;
						_cokeBottles[i].GetComponent<ObjectMotion>().mySelection.SetActive (false);
						_cokeBottles[i].gameObject.transform.localScale = Vector3.one;
					}
				}
			}
		}


	

		public void OnClickDustbin()
		{
			if(_isEndTutorial)
			{
				if(IsClickedOvenPizza || IsClickedPlatePizza)
				{
					_pizza.otherObject = _dustbin;
					OnPizzaReach();
				}

				ResetAllBool();
			}
		}


		private void ClickOven(Availability ovenAvailability)
		{

			if(IsClickedPlatePizza)
			{
				_pizza.otherObject = ovenAvailability.gameObject;
				_pizza.pizzaDestinationAvailable = ovenAvailability;
				OnPizzaReach();
			}
			ResetAllBool();
		}
		public void OnClickOven1()
		{
			if(_isEndTutorial || _firstPizza.tutorialOn)
			{
				ClickOven (_ovensPlaces[0]);
			}
		}

		public void OnClickOven2()
		{
			if(_isEndTutorial )
			{
				ClickOven (_ovensPlaces[1]);
			}
		}

		public void OnClickOven3()
		{
			if(_isEndTutorial )
			{
				ClickOven (_ovensPlaces[2]);
			}
		}
		public void OnClickOven4()
		{
			if(_isEndTutorial )
			{
				ClickOven (_ovensPlaces[3]);
			}
		}
		public void OnClickOven5()
		{
			if(_isEndTutorial )
			{
				ClickOven (_ovensPlaces[4]);
			}
		}
		public void OnClickOven6()
		{
			if(_isEndTutorial)
			{
				ClickOven (_ovensPlaces[5]);
			}
		}


		private void PlateClick(Availability plateAvailability)
		{
			if(_isEndTutorial || _firstPizza.tutorialOn)
			{
				if(IsClickedOvenPizza)
				{
					_pizza.otherObject = plateAvailability.gameObject;
					_pizza.pizzaDestinationAvailable = plateAvailability;
					OnPizzaReach();
				}
				ResetAllBool();
			}
		}
		public void OnClickPlate1()
		{
			PlateClick (_platesPizza[0].GetComponent<Availability>());
		}
	
		public void OnClickPlate2()
		{
			PlateClick (_platesPizza[1].GetComponent<Availability>());
		}
	
		public void OnClickPlate3()
		{
			PlateClick (_platesPizza[2].GetComponent<Availability>());
		}
		public void OnClickPlate4()
		{
			PlateClick (_platesPizza[3].GetComponent<Availability>());
		}
		public void OnClickPlate5()
		{
			PlateClick (_platesPizza[4].GetComponent<Availability>());
		}
		public void OnClickPlate6()
		{
			PlateClick (_platesPizza[5].GetComponent<Availability>());
		}

		public void ResetAllBool()
		{
			UnselectionOven();
			DeactivateSelections();
			UnselectAllBottles();
			
			_nonVeg.iAmSelected = false;
			_nonVeg.mySelection.SetActive (false); 
			
			_vegetables.iAmSelected = false;
			_vegetables.mySelection.SetActive (false);
			
			_cheese.iAmSelected = false;
			_cheese.mySelection.SetActive (false);
			
			_cupCake.iAmSelected = false;
			_cupCake.mySelection.SetActive (false);;
	

			IsClickedNonVeg = false;
			IsClickedVeg = false;
			IsClickedCheese = false;
			IsClickedCoke = false;
			IsClickedOvenPizza = false;
			IsClickedPlatePizza = false;
		}
	}
}
