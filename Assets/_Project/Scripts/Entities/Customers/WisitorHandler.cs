using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Managers;
using _Project.Scripts.Other;
using _Project.Scripts.UI_Scripts;
using _Project.Scripts.UI.Tutorial;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Scripts.Entities.Customers
{
	public class WisitorHandler : MonoBehaviour 
	{
		[Inject] private UIManager _uiManager;
		[Inject] private Australia_Manager _australiaManager;
		[Inject] private China_Manager _chinaManager;
		[Inject] private US_Manager _usManager;
		[Inject] private Italy_Manager _italyManager;
		private bool _isWait = true;
		
		[FormerlySerializedAs("gameTimer")] public float _timeOnGame = 120.0f;
		[FormerlySerializedAs("customerPositions")] [SerializeField] private Transform []_customerPositions;
		[FormerlySerializedAs("customerStartPosition")] public Transform _customerStartPos;
		[FormerlySerializedAs("customerEndPosition")] public Transform _customerEndPos;
		[FormerlySerializedAs("availablePositions")] public List <int> _availablePositions = new List<int>();
		[FormerlySerializedAs("customerPool")] public List<Wisitor> _wisitorsPool;
		[FormerlySerializedAs("timerText")] [SerializeField] private TextMesh _timerText;
		[FormerlySerializedAs("coinImages")] public Coins[]_coins;
		public bool timerStopped { get; set; }
		public bool canBeAnUnPayingCustomer { get; set; }
		public int noOfUnpayingCustomers { get; set; }
		public int maxNoOfUnpayableCustomers { get; set; }
		public bool stop { get; set; }
		public int noOfCustomers { get; set; }
		private void Start () 
		{
			noOfUnpayingCustomers = 0;
			StartCoroutine (CustomersRoutine());
			_timerText.text = "02:00";
			int remainderVal = LevelManager.levelNo%10;
			if(remainderVal > 6 || remainderVal == 0)
			{
				canBeAnUnPayingCustomer = true;
				if(remainderVal == 7)
				{
					maxNoOfUnpayableCustomers = 1;
				}
				else if(remainderVal == 8)
				{
					maxNoOfUnpayableCustomers = Random.Range (1,3);
				}
				else if(remainderVal == 9)
				{
					maxNoOfUnpayableCustomers = Random.Range (1,4);
				}
				else
				{
					maxNoOfUnpayableCustomers = Random.Range (3,5);
				}
			}
		}

		private void TimerSetValue()
		{
			int timer = Mathf.FloorToInt (_timeOnGame);
			if(timer < 60)
			{
				if(timer > 9)
					_timerText.text = "00:"+timer;
				else
					_timerText.text = "00:0"+timer;
			}
			else
			{
				if(timer == 120)
				{
					_timerText.text = "02:00";
				}
				else
				{
					int remainder = timer%60;
					if(remainder > 9)
						_timerText.text = "01:"+remainder;
					else
						_timerText.text = "01:0"+remainder;
				}
			}
		}
		
		private IEnumerator CustomersRoutine()
		{
			yield return new WaitForSeconds(5f);
			if(LevelManager.levelNo != 2 && LevelManager.levelNo != 3 && LevelManager.levelNo != 22 && LevelManager.levelNo != 23 && LevelManager.levelNo != 33 && LevelManager.levelNo != 34)
			{
				if(!stop)
				{
					ConfigureCustomer();
				}
			}
			
			float timerSinceLastChar = 0;
			while(_timeOnGame > 0 )
			{
				if(!TutorialPanel.popupPanelActive || _timeOnGame < 100f)
				{
					TimerSetValue();
					_timeOnGame-=Time.deltaTime;
					timerSinceLastChar+=Time.deltaTime;
					if(timerSinceLastChar > 5 && _isWait)
					{
						if(LevelManager.levelNo > 1)
						{
							if(_availablePositions.Count > 0)
							{
								if(!stop)
								{
									ConfigureCustomer();
								}
							}
							timerSinceLastChar = 0;
						}
						else
						{
							if(_timeOnGame < 60f)
							{
								if(_availablePositions.Count > 0)
								{
									if(!stop)
									{
										ConfigureCustomer();
									}
								}
								timerSinceLastChar = 0;
							}
							else
							{
								if(_availablePositions.Count > 2)
								{
									if(!stop)
									{
										ConfigureCustomer();
									}
									timerSinceLastChar = 0;
								}

							}
						}
					}
				}
				yield return 0;
			}
			timerStopped = true;
			if(_availablePositions.Count == 5)
			{
				_uiManager.OnGameOver ();
			}
		}

		public void PanelCheck()
		{
			_uiManager.BellPanelTry();
		}

		public void BellPanelSeven()
		{
			_uiManager.BellPanelBuy ();
		}

		public void ConfigureCustomer()
		{
			if (LevelManager.levelNo == 5 && _timeOnGame < 90f && !stop)
			{
				{
					if (!PlayerPrefs.HasKey("Bell"))
					{
						if (PlayerPrefs.GetInt("belltrydone") != 2)
						{
							PlayerPrefs.SetInt("belltrydone", 2);
							Invoke(nameof(PanelCheck), 10.0f);
							stop = true;
						}
					}
				}
			}
			else if (LevelManager.levelNo == 7 && _timeOnGame < 90f && !stop)
			{
				if (!PlayerPrefs.HasKey("Bell"))
				{
					if (PlayerPrefs.GetInt("belltrydone2") != 2)
					{
						PlayerPrefs.SetInt("belltrydone2", 2);
						Invoke(nameof(BellPanelSeven), 20.0f);
						stop = true;
					}
				}
			}

			int customerNo = Random.Range(0, _wisitorsPool.Count);
			int availablePosForCustomer = Random.Range(0, _availablePositions.Count);
			if ((LevelManager.levelNo == 1 || LevelManager.levelNo == 11 || LevelManager.levelNo == 13 ||
			     LevelManager.levelNo == 21 || LevelManager.levelNo == 31 || LevelManager.levelNo == 32) &&
			    noOfCustomers == 0)
			{
				availablePosForCustomer = 1;
				if (LevelManager.levelNo == 1)
					_usManager.firstCustomer = _wisitorsPool[customerNo];
				else if (LevelManager.levelNo == 31 || LevelManager.levelNo == 32)
				{
					_australiaManager.FirstCustomer = _wisitorsPool[customerNo];
					if (LevelManager.levelNo == 32)
					{
						_australiaManager.FirstCustomer.tutorialOn = true;
					}
				}
				else if (LevelManager.levelNo == 11 || LevelManager.levelNo == 13)
				{
					_chinaManager.CustomerFirst = _wisitorsPool[customerNo];
					_chinaManager.CustomerFirst.tutorialOn = true;
				}
				else if (LevelManager.levelNo == 21 || LevelManager.levelNo == 22)
				{
					_italyManager.firstCustomer = _wisitorsPool[customerNo];
				}
			}

			noOfCustomers++;
			StartCoroutine(_wisitorsPool[customerNo]
				.MoveToPositionRoutine(_customerPositions[_availablePositions[availablePosForCustomer]].position, true));
			_wisitorsPool[customerNo].positionTaken = _availablePositions[availablePosForCustomer];
			_availablePositions.Remove(_availablePositions[availablePosForCustomer]);
			_wisitorsPool.Remove(_wisitorsPool[customerNo]);
		}

		public void BellPress()
		{
			if(_availablePositions.Count > 0 && _timeOnGame > 0 && _isWait && (China_Manager._endTutorial || US_Manager.tutorialEnd || Australia_Manager.tutorialEnd || Italy_Manager.tutorialEnd ) )
			{
				_isWait = false ;
				ConfigureCustomer();
				Invoke(nameof(Wait),0.4f);
			}
		}

		private void Wait()
		{
			_isWait = true;
		}
	 
	}
}
