using System.Collections;
using _Project.Scripts.Achivments;
using _Project.Scripts.Additional;
using _Project.Scripts.Entities.Customers;
using _Project.Scripts.Managers;
using _Project.Scripts.Other;
using _Project.Scripts.UI_Scripts;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Entities
{
	public class Thief : MonoBehaviour
	{
		[Inject] private US_Manager _usManager;
		[Inject] private Italy_Manager _italyManager;
		[Inject] private China_Manager _chinaManager;
		[Inject] private Australia_Manager _australiaManager;
		[Inject] private UIManager _uiManager;   
		float speed = 1.5f;
		int reachedPos = 4;
		
		public static Thief _instance ;
		public Transform []coinsPos;
		public int coinsStolen;
		public bool isCaught;

		private void Awake()
		{
			_instance = this ;
		}
		private void Start () 
		{
			_uiManager.n_Thieves_caught=PlayerPrefs.GetInt ("ThiefCaught");
			InvokeRepeating("Condition", 1f,1f);
		}

		private void Condition()
		{
			if ((LevelManager.levelNo == 8 || LevelManager.levelNo == 9 || LevelManager.levelNo == 10 ||
			     LevelManager.levelNo == 17 || LevelManager.levelNo == 18 || LevelManager.levelNo == 19 ||
			     LevelManager.levelNo == 20 || LevelManager.levelNo == 27 || LevelManager.levelNo == 28 ||
			     LevelManager.levelNo == 29 || LevelManager.levelNo == 30 || LevelManager.levelNo == 37 ||
			     LevelManager.levelNo == 38 || LevelManager.levelNo == 39 || LevelManager.levelNo == 40) &&
			    Coins.visible > 3)
			{
				bringThiefIn();
			}
		}

		private void bringThiefIn()
		{
			if(PlayerPrefs.GetInt ("HandCuffTut") == 1)
			{
				if(LevelManager.levelNo > 0 && LevelManager.levelNo <= 10)
				{
					_usManager.TheifPanel.SetActive(true);
				}
				if(LevelManager.levelNo > 10 && LevelManager.levelNo <= 20)
				{
					_chinaManager.TheifPanel.SetActive(true);
				}
				if(LevelManager.levelNo > 20 && LevelManager.levelNo <= 30)
				{
					_italyManager.TheifPanel.SetActive(true);
				}
				if(LevelManager.levelNo > 30 && LevelManager.levelNo <= 40)
				{
					_australiaManager.TheifPanel.SetActive(true);
				}
				PlayerPrefs.SetInt ("HandCuffTut",2);
			}
			reachedPos = 4;
			isCaught = false;

			StartCoroutine (MoveToPosition(CustomerHandler._instance.customerEndPosition.position ));
			if ((MenuManager.handcuffNo <= 0) && PlayerPrefs.GetInt("handcuffPanel")!=2 && coinsStolen > 0 && (LevelManager.levelNo == 8 || LevelManager.levelNo==9 ||LevelManager.levelNo == 10) ) {
		
				Invoke (nameof(bringThiefAgain), 9.0f);
				PlayerPrefs.SetInt("handcuffPanel",2);
			}
		}

		private void bringThiefAgain()
		{
			_uiManager.Handcuff ();
		}

		private IEnumerator MoveToPosition(Vector3 finalPos )
		{
			float distance = Vector3.Distance (transform.position , finalPos);
			speed = 2f;
			while(distance > 0.1f)
			{
				if(reachedPos >= 0 && !isCaught)
				{
					if(transform.position.x < coinsPos[reachedPos].position.x )
					{
						if(CustomerHandler._instance.coinImages[reachedPos].gameObject.activeInHierarchy)
						{
							coinsStolen+=CustomerHandler._instance.coinImages[reachedPos].myAmount;
							CustomerHandler._instance.coinImages[reachedPos].CoinsStolen ();
						}
						reachedPos--;
					}
				}
				float step = speed * Time.deltaTime;
				transform.position = Vector3.MoveTowards(transform.position, finalPos, step);
				distance = Vector3.Distance (transform.position , finalPos);
				yield return 0;
			}
		}

		public void Stopa()
		{
			_uiManager.achievment_text.SetActive (false);
		}

		private void OnMouseDown()
		{
			if(MenuManager.handcuffNo > 0)
			{
				MenuManager.handcuffNo--;
				_uiManager.n_Thieves_caught++;
				PlayerPrefs.SetInt ("ThiefCaught", _uiManager.n_Thieves_caught);

				if(PlayerPrefs.GetInt("ThiefCaught") > 9 && PlayerPrefs.GetInt ("ThiefLevel1") == 0)
				{
					PlayerPrefs.SetInt ("ThiefLevel1",1);
					_uiManager.achievment_text.SetActive(true);
					AchievementChild.check_claim++;
					PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
					Invoke(nameof(Stopa),4.0f);
				}
				if(PlayerPrefs.GetInt("ThiefCaught") > 99 && PlayerPrefs.GetInt ("ThiefLevel2") == 0)
				{
					PlayerPrefs.SetInt ("ThiefLevel2",1);
					_uiManager.achievment_text.SetActive(true);
					AchievementChild.check_claim++;
					PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
					Invoke(nameof(Stopa),4.0f);
				}
				if(PlayerPrefs.GetInt("ThiefCaught") > 999 && PlayerPrefs.GetInt ("ThiefLevel3") == 0)
				{
					PlayerPrefs.SetInt ("ThiefLevel3",1);
					_uiManager.achievment_text.SetActive(true);
					AchievementChild.check_claim++;
					PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
					Invoke(nameof(Stopa),4.0f);
				}

				transform.position = CustomerHandler._instance.customerEndPosition.position;
				speed = 35;
				isCaught = true;
				_uiManager.totalCoins+=coinsStolen;
				_uiManager.CallIncrementCoint ();
				_uiManager.coincollect.Play();

				PlayerPrefs.SetString ("Handcuff",EncryptionHandler64.Encrypt (MenuManager.handcuffNo.ToString ()));
				if(MenuManager.handcuffNo <=0 )
				{
					_usManager.handcuff.SetActive(false);
				}
			}
		}
	}
}
