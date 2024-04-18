using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Managers;
using _Project.Scripts.Other;
using _Project.Scripts.UI_Scripts;
using _Project.Scripts.UI.Tutorial;
using UnityEngine;

namespace _Project.Scripts.Entities.Customers
{
	public class CustomerHandler : MonoBehaviour 
	{
		bool wait=true;
		public int noOfCustomers;
		public float gameTimer = 120.0f;
		public Transform []customerPositions;
		public Transform customerStartPosition;
		public Transform customerEndPosition;
		public List <int> availablePositions = new List<int>();
		public List<Customer> customerPool;
		public Coins []coinImages;
		public TextMesh timerText;
		public bool timerStopped;
		public bool canBeAnUnPayingCustomer;
		public int noOfUnpayingCustomers;
		public int maxNoOfUnpayableCustomers;
		public static CustomerHandler _instance;
		public  bool stop;

		private void Start () 
		{
			_instance = this;
			noOfUnpayingCustomers = 0;
			StartCoroutine (BringCustomers());
			timerText.text = "02:00";
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

		private void TimerValue()
		{
			int timer = Mathf.FloorToInt (gameTimer);
			if(timer < 60)
			{
				if(timer > 9)
					timerText.text = "00:"+timer;
				else
					timerText.text = "00:0"+timer;
			}
			else
			{
				if(timer == 120)
				{
					timerText.text = "02:00";
				}
				else
				{
					int remainder = timer%60;
					if(remainder > 9)
						timerText.text = "01:"+remainder;
					else
						timerText.text = "01:0"+remainder;
				}
			}
		}
		
		private IEnumerator BringCustomers()
		{
			yield return new WaitForSeconds(5f);
			if(LevelManager.levelNo != 2 && LevelManager.levelNo != 3 && LevelManager.levelNo != 22 && LevelManager.levelNo != 23 && LevelManager.levelNo != 33 && LevelManager.levelNo != 34)
			{
				if(!stop)
				{
					InitializeCustomer();
				}
			}
			
			float timerSinceLastChar = 0;
			while(gameTimer > 0 )
			{
				if(!TutorialPanel.popupPanelActive || gameTimer < 100f)
				{
					TimerValue();
					gameTimer-=Time.deltaTime;
					timerSinceLastChar+=Time.deltaTime;
					if(timerSinceLastChar > 5 && wait)
					{
						if(LevelManager.levelNo > 1)
						{
							if(availablePositions.Count > 0)
							{
								if(!stop)
								{
									InitializeCustomer();
								}
							}
							timerSinceLastChar = 0;
						}
						else
						{
							if(gameTimer < 60f)
							{
								if(availablePositions.Count > 0)
								{
									if(!stop)
									{
										InitializeCustomer();
									}
								}
								timerSinceLastChar = 0;
							}
							else
							{
								if(availablePositions.Count > 2)
								{
									if(!stop)
									{
										InitializeCustomer();
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
			if(availablePositions.Count == 5)
			{
				UIManager._instance.OnGameOver ();
			}
		}

		public void BellPanelCheck()
		{
			UIManager._instance.BellPanelTry();
		}

		public void BellPanelSeven()
		{
			UIManager._instance.BellPanelBuy ();
		}

		public void InitializeCustomer()
		{
			if (LevelManager.levelNo == 5 && gameTimer < 90f && !stop)
			{
				{
					if (!PlayerPrefs.HasKey("Bell"))
					{
						if (PlayerPrefs.GetInt("belltrydone") != 2)
						{
							PlayerPrefs.SetInt("belltrydone", 2);
							Invoke(nameof(BellPanelCheck), 10.0f);
							stop = true;
						}
					}
				}
			}
			else if (LevelManager.levelNo == 7 && gameTimer < 90f && !stop)
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

			int customerNo = Random.Range(0, customerPool.Count);
			int availablePosForCustomer = Random.Range(0, availablePositions.Count);
			if ((LevelManager.levelNo == 1 || LevelManager.levelNo == 11 || LevelManager.levelNo == 13 ||
			     LevelManager.levelNo == 21 || LevelManager.levelNo == 31 || LevelManager.levelNo == 32) &&
			    noOfCustomers == 0)
			{
				availablePosForCustomer = 1;
				if (LevelManager.levelNo == 1)
					US_Manager._instance.firstCustomer = customerPool[customerNo];
				else if (LevelManager.levelNo == 31 || LevelManager.levelNo == 32)
				{
					Australia_Manager._instance.firstCustomer = customerPool[customerNo];
					if (LevelManager.levelNo == 32)
					{
						Australia_Manager._instance.firstCustomer.tutorialOn = true;
					}
				}
				else if (LevelManager.levelNo == 11 || LevelManager.levelNo == 13)
				{
					China_Manager._instance.firstCustomer = customerPool[customerNo];
					China_Manager._instance.firstCustomer.tutorialOn = true;
				}
				else if (LevelManager.levelNo == 21 || LevelManager.levelNo == 22)
				{
					Italy_Manager._instance.firstCustomer = customerPool[customerNo];
				}
			}

			noOfCustomers++;
			StartCoroutine(customerPool[customerNo]
				.MoveToPosition(customerPositions[availablePositions[availablePosForCustomer]].position, true));
			customerPool[customerNo].positionTaken = availablePositions[availablePosForCustomer];
			availablePositions.Remove(availablePositions[availablePosForCustomer]);
			customerPool.Remove(customerPool[customerNo]);
		}

		public void PressBell()
		{
			if(availablePositions.Count > 0 && gameTimer > 0 && wait && (China_Manager.tutorialEnd || US_Manager.tutorialEnd || Australia_Manager.tutorialEnd || Italy_Manager.tutorialEnd ) )
			{
				wait = false ;
				InitializeCustomer();
				Invoke("Waitf",0.4f);
			}
		}

		private void Waitf()
		{
			wait = true;
		}
	 
	}
}
