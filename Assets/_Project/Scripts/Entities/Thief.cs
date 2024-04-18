using UnityEngine;
using System.Collections;
using _Project.Scripts.Managers;
using _Project.Scripts.Other;
using _Project.Scripts.UI_Scripts;

public class Thief : MonoBehaviour {

	public static Thief _instance ;
	public Transform []coinsPos;
	int reachedPos = 4;
	public int coinsStolen;
	public bool isCaught;

	public bool bringThief;
	// Use this for initialization
	void Awake()
	{
		_instance = this ;
	}
	void Start () {
		UIManager._instance.n_Thieves_caught=PlayerPrefs.GetInt ("ThiefCaught");
		InvokeRepeating("Condition", 1f,1f);
	}
	void Condition()
	{
		if((LevelManager.levelNo == 8 || LevelManager.levelNo == 9 || LevelManager.levelNo == 10 || LevelManager.levelNo == 17 || LevelManager.levelNo == 18 || LevelManager.levelNo == 19 || LevelManager.levelNo == 20 || LevelManager.levelNo == 27 || LevelManager.levelNo == 28 || LevelManager.levelNo == 29 || LevelManager.levelNo == 30 || LevelManager.levelNo == 37 || LevelManager.levelNo == 38 || LevelManager.levelNo == 39 || LevelManager.levelNo == 40)&& Coins.visible > 3) {
		//	Invoke ("bringThiefIn", 60f);
			bringThiefIn();
		}
	}
	void bringThiefIn()
	{
		if(PlayerPrefs.GetInt ("HandCuffTut") == 1)
		{
			if(LevelManager.levelNo > 0 && LevelManager.levelNo <= 10)
			{
				US_Manager._instance.TheifPanel.SetActive(true);
			}
			if(LevelManager.levelNo > 10 && LevelManager.levelNo <= 20)
			{
				China_Manager._instance.TheifPanel.SetActive(true);
			}
			if(LevelManager.levelNo > 20 && LevelManager.levelNo <= 30)
			{
				Italy_Manager._instance.TheifPanel.SetActive(true);
			}
			if(LevelManager.levelNo > 30 && LevelManager.levelNo <= 40)
			{
				Australia_Manager._instance.TheifPanel.SetActive(true);
			}
			PlayerPrefs.SetInt ("HandCuffTut",2);
		}
		bringThief = true;
		reachedPos = 4;
		isCaught = false;
		//bringThief = false;
		StartCoroutine (MoveToPosition(CustomerHandler._instance.customerEndPosition.position ));
		//&& (PlayerPrefs.GetInt("handcuffPanel")!=2)
		if ((MenuManager.handcuffNo <= 0) && PlayerPrefs.GetInt("handcuffPanel")!=2 && coinsStolen > 0 && (LevelManager.levelNo == 8 || LevelManager.levelNo==9 ||LevelManager.levelNo == 10) ) {
		
			Invoke ("bringThiefAgain", 9.0f);
			PlayerPrefs.SetInt("handcuffPanel",2);
		}
	}
	void bringThiefAgain()
	{
		{
			UIManager._instance.Handcuff ();
		}
	}
	// Update is called once per frame
	//void Update () {

	//}


	/// <summary>
	/// Brings Back the object to its final position
	/// </summary>
	/// <returns>The to position.</returns>
	public IEnumerator MoveToPosition(Vector3 finalPos )
	{
		float distance = Vector3.Distance (transform.position , finalPos);
		speed = 2f;
		while(distance > 0.1f)
		{
			if(reachedPos >= 0 && !isCaught)
			{
				if(transform.position.x < coinsPos[reachedPos].position.x )
				{
					//Collect the coin
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
		UIManager._instance.achievment_text.SetActive (false);
	}
	float speed = 1.5f;
	void OnMouseDown()
	{
		if(MenuManager.handcuffNo > 0)
		{
			MenuManager.handcuffNo--;
			UIManager._instance.n_Thieves_caught++;
			PlayerPrefs.SetInt ("ThiefCaught",UIManager._instance.n_Thieves_caught);

			if(PlayerPrefs.GetInt("ThiefCaught") > 9 && PlayerPrefs.GetInt ("ThiefLevel1") == 0)
			{
				PlayerPrefs.SetInt ("ThiefLevel1",1);
				UIManager._instance.achievment_text.SetActive(true);
				AchievementChild.check_claim++;
				PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
				Invoke("Stopa",4.0f);
//				MenuManager.totalscore+=100;
//				PlayerPrefs.SetString("TotalScore",EncryptionHandler64.Encrypt (MenuManager.totalscore.ToString ()));
			}
			if(PlayerPrefs.GetInt("ThiefCaught") > 99 && PlayerPrefs.GetInt ("ThiefLevel2") == 0)
			{
				PlayerPrefs.SetInt ("ThiefLevel2",1);
				UIManager._instance.achievment_text.SetActive(true);
				AchievementChild.check_claim++;
				PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
				Invoke("Stopa",4.0f);
//				MenuManager.golds++;
//				PlayerPrefs.SetString("Golds",EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
//				UIManager._instance.goldText.text = MenuManager.golds.ToString ();
			}
			if(PlayerPrefs.GetInt("ThiefCaught") > 999 && PlayerPrefs.GetInt ("ThiefLevel3") == 0)
			{
				PlayerPrefs.SetInt ("ThiefLevel3",1);
				UIManager._instance.achievment_text.SetActive(true);
				AchievementChild.check_claim++;
				PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
				Invoke("Stopa",4.0f);
//				MenuManager.golds += 5;
//				PlayerPrefs.SetString("Golds",EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
//				UIManager._instance.goldText.text = MenuManager.golds.ToString ();
			}

			transform.position = CustomerHandler._instance.customerEndPosition.position;
			speed = 35;
			isCaught = true;
			UIManager._instance.totalCoins+=coinsStolen;
			UIManager._instance.CallIncrementCoint ();
			UIManager._instance.coincollect.Play();

			PlayerPrefs.SetString ("Handcuff",EncryptionHandler64.Encrypt (MenuManager.handcuffNo.ToString ()));
			if(MenuManager.handcuffNo <=0 )
			{
				US_Manager._instance.handcuff.SetActive(false);
			}
		}
	}




}
