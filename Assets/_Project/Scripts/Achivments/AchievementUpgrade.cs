using UnityEngine;
using System.Collections;
using _Project.Scripts.UI_Scripts;
using UnityEngine.UI;

public class AchievementUpgrade : MonoBehaviour {




	public AchievementChild []myChild; 



	public Text totalCoinsText;
	public Text totalGoldText ;
	void Awake()
	{

	}
	void Start()
	{

	}



	void OnEnable()
	{

		
		totalGoldText.text = MenuManager.golds.ToString ();
		totalCoinsText.text = MenuManager.totalscore.ToString ();

//		customerf ();
//		perfectf ();
//		Hotdogf ();
//		Pizzaf ();
//		Cokef ();
//		thieff ();
//		burgerf ();
//		Frenchfriesf ();
//		Noodlesf ();

	}


	bool once;
	
	public void CallIncrementCoin()
	{
		StopCoroutine ("IncrementCoins");

		StartCoroutine ("IncrementCoins");

	}
	public void CallIncrementgold()
	{
		StopCoroutine ("IncrementGold");
		
		StartCoroutine ("IncrementGold");
	}

	IEnumerator IncrementCoins()
	{
		int textCoins = int.Parse(totalCoinsText.text);
		
		while (textCoins < MenuManager.totalscore)
		{
			textCoins+=1;
			totalCoinsText.text = textCoins.ToString ();
			yield return 0;
		}
		totalCoinsText.text = MenuManager.totalscore.ToString ();
		
	}
	
	IEnumerator IncrementGold()
	{
		int textCoins = int.Parse(totalGoldText.text);
		while (textCoins < MenuManager.golds)
		{
			textCoins+=1;
			totalGoldText.text = textCoins.ToString ();
			yield return 0;
		}
		totalGoldText.text = MenuManager.golds.ToString ();
		
	}

	public void ClaimCustomerAchievement1()
	{
		if (PlayerPrefs.GetInt ("CustomerLevel1") == 1 && !PlayerPrefs.HasKey ("Customer1Claimed")) {
			MenuManager.totalscore+=100;
			PlayerPrefs.SetString("TotalScore",EncryptionHandler64.Encrypt (MenuManager.totalscore.ToString ()));
			PlayerPrefs.SetInt ("Customer1Claimed" , 1);
			//			UIManager._instance.goldText.text = MenuManager.golds.ToString ();
			CallIncrementCoin();
			// Display incremented gol
			// Effect
			// Show Tick
			myChild[0].myTick.SetActive (true);
			myChild[0].myClaimButton.SetActive (false);
			AchievementChild.check_claim--;
			PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
		}
	}
	public void ClaimCustomerAchievement2()
	{
		if (PlayerPrefs.GetInt ("CustomerLevel2") == 1 && !PlayerPrefs.HasKey ("Customer2Claimed")) {
			MenuManager.golds ++;
			PlayerPrefs.SetString ("Golds", EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
			PlayerPrefs.SetInt ("Customer2Claimed" , 1);
			//			UIManager._instance.goldText.text = MenuManager.golds.ToString ();
			// Display incremented gold
			// Effect
			CallIncrementgold();
			myChild[1].myTick.SetActive (true);
			myChild[1].myClaimButton.SetActive (false);
			AchievementChild.check_claim--;
			PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);

		}
	}
	public void ClaimCustomerAchievement3()
	{
		if (PlayerPrefs.GetInt ("CustomerLevel3") == 1 && !PlayerPrefs.HasKey ("Customer3Claimed")) {
			MenuManager.golds += 5;
			PlayerPrefs.SetString ("Golds", EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
			PlayerPrefs.SetInt ("Customer3Claimed" , 1);
			//			UIManager._instance.goldText.text = MenuManager.golds.ToString ();
			// Display incremented gold
			// Effect
			// Show Tick
			CallIncrementgold();
			myChild[2].myTick.SetActive (true);
			myChild[2].myClaimButton.SetActive (false);
			AchievementChild.check_claim--;
			PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
		}
	}


	public void ClaimPerfectAchievement1()
	{
		if (PlayerPrefs.GetInt ("PerfectLevel1") == 1 && !PlayerPrefs.HasKey ("Perfect1Claimed")) {
			MenuManager.totalscore+=100;
			PlayerPrefs.SetString("TotalScore",EncryptionHandler64.Encrypt (MenuManager.totalscore.ToString ()));
			PlayerPrefs.SetInt ("Perfect1Claimed" , 1);
			//			UIManager._instance.goldText.text = MenuManager.golds.ToString ();
			// Display incremented gold
			// Effect
			// Show Tick
			CallIncrementCoin();
			myChild[3].myTick.SetActive (true);
			myChild[3].myClaimButton.SetActive (false);
			AchievementChild.check_claim--;
			PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
		}
	}
	public void ClaimPerfectAchievement2()
	{
		if (PlayerPrefs.GetInt ("PerfectLevel2") == 1 && !PlayerPrefs.HasKey ("Perfect2Claimed")) {
			MenuManager.golds ++;
			PlayerPrefs.SetString ("Golds", EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
			PlayerPrefs.SetInt ("Perfect2Claimed" , 1);
			//			UIManager._instance.goldText.text = MenuManager.golds.ToString ();
			// Display incremented gold
			// Effect
			// Show Tick
			CallIncrementgold();
			myChild[4].myTick.SetActive (true);
			myChild[4].myClaimButton.SetActive (false);
			AchievementChild.check_claim--;
			PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
		}
	}
	public void ClaimPerfectAchievement3()
	{
		if (PlayerPrefs.GetInt ("PerfectLevel3") == 1 && !PlayerPrefs.HasKey ("Perfect3Claimed")) {
			MenuManager.golds += 5;
			PlayerPrefs.SetString ("Golds", EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
			PlayerPrefs.SetInt ("Perfect3Claimed" , 1);
			//			UIManager._instance.goldText.text = MenuManager.golds.ToString ();
			// Display incremented gold
			// Effect
			// Show Tick
			CallIncrementgold();
			myChild[5].myTick.SetActive (true);
			myChild[5].myClaimButton.SetActive (false);
			AchievementChild.check_claim--;
			PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
		}
	}



	public void ClaimBurgerAchievement1()
	{
		if (PlayerPrefs.GetInt ("BurgerLevel1") == 1 && !PlayerPrefs.HasKey ("Burger1Claimed")) {
			// Show Tick
			// Effect
			PlayerPrefs.SetInt ("Burger1Claimed" , 1);
			MenuManager.totalscore+=100;
			PlayerPrefs.SetString("TotalScore",EncryptionHandler64.Encrypt (MenuManager.totalscore.ToString ()));
			//Display score
			CallIncrementCoin();
			myChild[6].myTick.SetActive (true);
			myChild[6].myClaimButton.SetActive (false);
			AchievementChild.check_claim--;
			PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
		}
	}
	
	public void ClaimBurgerAchievement2()
	{
		if (PlayerPrefs.GetInt ("BurgerLevel2") == 1 && !PlayerPrefs.HasKey ("Burger2Claimed")) {
			MenuManager.golds++;
			PlayerPrefs.SetString ("Golds", EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
			PlayerPrefs.SetInt ("Burger2Claimed" , 1);
			//			UIManager._instance.goldText.text = MenuManager.golds.ToString ();
			//Display incremented gold
			// Effect
			// Show Tick
			CallIncrementgold();
			myChild[7].myTick.SetActive (true);
			myChild[7].myClaimButton.SetActive (false);
			AchievementChild.check_claim--;
			PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
		}
	}
	
	public void ClaimBurgerAchievement3()
	{
		if (PlayerPrefs.GetInt ("BurgerLevel3") == 1 && !PlayerPrefs.HasKey ("Burger3Claimed")) {
			MenuManager.golds += 5;
			PlayerPrefs.SetString ("Golds", EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
			PlayerPrefs.SetInt ("Burger3Claimed" , 1);
			//			UIManager._instance.goldText.text = MenuManager.golds.ToString ();
			// Display incremented gold
			// Effect
			// Show Tick
			CallIncrementgold();
			myChild[8].myTick.SetActive (true);
			myChild[8].myClaimButton.SetActive (false);
			AchievementChild.check_claim--;
			PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
		}
	}




	public void ClaimFriesAchievement1()
	{
		if (PlayerPrefs.GetInt ("FrenchfriesLevel1") == 1 && !PlayerPrefs.HasKey ("Fries1Claimed")) {
			// Show Tick
			// Effect
			PlayerPrefs.SetInt ("Fries1Claimed" , 1);
			MenuManager.totalscore+=100;
			PlayerPrefs.SetString("TotalScore",EncryptionHandler64.Encrypt (MenuManager.totalscore.ToString ()));
			//Display score
			CallIncrementCoin();
			myChild[9].myTick.SetActive (true);
			myChild[9].myClaimButton.SetActive (false);
			AchievementChild.check_claim--;
			PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
		}
	}
	
	public void ClaimFriesAchievement2()
	{
		if (PlayerPrefs.GetInt ("FrenchfriesLevel2") == 1 && !PlayerPrefs.HasKey ("Fries2Claimed")) {
			MenuManager.golds++;
			PlayerPrefs.SetString ("Golds", EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
			PlayerPrefs.SetInt ("Fries2Claimed" , 1);
			//			UIManager._instance.goldText.text = MenuManager.golds.ToString ();
			//Display incremented gold
			// Effect
			// Show Tick
			CallIncrementgold();
			myChild[10].myTick.SetActive (true);
			myChild[10].myClaimButton.SetActive (false);
			AchievementChild.check_claim--;
			PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
		}
	}
	
	public void ClaimFrieseAchievement3()
	{
		if (PlayerPrefs.GetInt ("FrenchfriesLevel3") == 1 && !PlayerPrefs.HasKey ("Fries3Claimed")) {
			MenuManager.golds += 5;
			PlayerPrefs.SetString ("Golds", EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
			PlayerPrefs.SetInt ("Fries3Claimed" , 1);
			//			UIManager._instance.goldText.text = MenuManager.golds.ToString ();
			// Display incremented gold
			// Effect
			// Show Tick
			CallIncrementgold();
			myChild[11].myTick.SetActive (true);
			myChild[11].myClaimButton.SetActive (false);
			AchievementChild.check_claim--;
			PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
		}
	}




	public void ClaimCokeAchievement1()
	{
		if (PlayerPrefs.GetInt ("CokeLevel1") == 1 && !PlayerPrefs.HasKey ("Coke1Claimed")) {
			// Show Tick
			// Effect
			PlayerPrefs.SetInt ("Coke1Claimed" , 1);
			MenuManager.totalscore+=100;
			PlayerPrefs.SetString("TotalScore",EncryptionHandler64.Encrypt (MenuManager.totalscore.ToString ()));
			//Display score
			CallIncrementCoin();
			myChild[12].myTick.SetActive (true);
			myChild[12].myClaimButton.SetActive (false);
			AchievementChild.check_claim--;
			PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
		
		}
	}
	
	public void ClaimCokeAchievement2()
	{
		if (PlayerPrefs.GetInt ("CokeLevel2") == 1 && !PlayerPrefs.HasKey ("Coke2Claimed")) {
			MenuManager.golds++;
			PlayerPrefs.SetString ("Golds", EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
			PlayerPrefs.SetInt ("Coke2Claimed" , 1);
			//			UIManager._instance.goldText.text = MenuManager.golds.ToString ();
			//Display incremented gold
			// Effect
			// Show Tick
			CallIncrementgold();
			myChild[13].myTick.SetActive (true);
			myChild[13].myClaimButton.SetActive (false);
			AchievementChild.check_claim--;
			PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
		}
	}
	
	public void ClaimCokeAchievement3()
	{
		if (PlayerPrefs.GetInt ("CokeLevel3") == 1 && !PlayerPrefs.HasKey ("Coke3Claimed")) {
			MenuManager.golds += 5;
			PlayerPrefs.SetString ("Golds", EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
			PlayerPrefs.SetInt ("Coke3Claimed" , 1);
			//			UIManager._instance.goldText.text = MenuManager.golds.ToString ();
			// Display incremented gold
			// Effect
			// Show Tick
			CallIncrementgold();
			myChild[14].myTick.SetActive (true);
			myChild[14].myClaimButton.SetActive (false);
			AchievementChild.check_claim--;
			PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
		}
	}




	public void ClaimThiefAchievement1()
	{
		if (PlayerPrefs.GetInt ("ThiefLevel1") == 1 && !PlayerPrefs.HasKey ("Thief1Claimed")) {
			// Show Tick
			// Effect
			PlayerPrefs.SetInt ("Thief1Claimed" , 1);
			MenuManager.totalscore+=100;
			PlayerPrefs.SetString("TotalScore",EncryptionHandler64.Encrypt (MenuManager.totalscore.ToString ()));
			//Display score
			CallIncrementCoin();
			myChild[15].myTick.SetActive (true);
			myChild[15].myClaimButton.SetActive (false);
			AchievementChild.check_claim--;
			PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
		}
	}
	
	public void ClaimThiefAchievement2()
	{
		if (PlayerPrefs.GetInt ("ThiefLevel2") == 1 && !PlayerPrefs.HasKey ("Thief2Claimed")) {
			MenuManager.golds++;
			PlayerPrefs.SetString ("Golds", EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
			PlayerPrefs.SetInt ("Thief2Claimed" , 1);
			//			UIManager._instance.goldText.text = MenuManager.golds.ToString ();
			//Display incremented gold
			// Effect
			// Show Tick
			CallIncrementgold();
			myChild[16].myTick.SetActive (true);
			myChild[16].myClaimButton.SetActive (false);
			AchievementChild.check_claim--;
			PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
		}
	}
	
	public void ClaimThiefAchievement3()
	{
		if (PlayerPrefs.GetInt ("ThiefLevel3") == 1 && !PlayerPrefs.HasKey ("Thief3Claimed")) {
			MenuManager.golds += 5;
			PlayerPrefs.SetString ("Golds", EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
			PlayerPrefs.SetInt ("Thief3Claimed" , 1);
			//			UIManager._instance.goldText.text = MenuManager.golds.ToString ();
			// Display incremented gold
			// Effect
			// Show Tick
			CallIncrementgold();
			myChild[17].myTick.SetActive (true);
			myChild[17].myClaimButton.SetActive (false);
			AchievementChild.check_claim--;
			PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
		}
	}





	public void ClaimNoodlesAchievement1()
	{
		if (PlayerPrefs.GetInt ("NoodlesLevel1") == 1 && !PlayerPrefs.HasKey ("Noodles1Claimed")) {
			// Show Tick
			// Effect
			PlayerPrefs.SetInt ("Noodles1Claimed" , 1);
			MenuManager.totalscore+=100;
			PlayerPrefs.SetString("TotalScore",EncryptionHandler64.Encrypt (MenuManager.totalscore.ToString ()));
			//Display score
			CallIncrementCoin();
			myChild[18].myTick.SetActive (true);
			myChild[18].myClaimButton.SetActive (false);
			AchievementChild.check_claim--;
			PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
		}
	}
	
	public void ClaimNoodlesAchievement2()
	{
		if (PlayerPrefs.GetInt ("NoodlesLevel2") == 1 && !PlayerPrefs.HasKey ("Noodles2Claimed")) {
			MenuManager.golds++;
			PlayerPrefs.SetString ("Golds", EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
			PlayerPrefs.SetInt ("Noodles2Claimed" , 1);
			//			UIManager._instance.goldText.text = MenuManager.golds.ToString ();
			//Display incremented gold
			// Effect
			// Show Tick
			CallIncrementgold();
			myChild[19].myTick.SetActive (true);
			myChild[19].myClaimButton.SetActive (false);
			AchievementChild.check_claim--;
			PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
		}
	}
	
	public void ClaimNoodlesAchievement3()
	{
		if (PlayerPrefs.GetInt ("NoodlesLevel3") == 1 && !PlayerPrefs.HasKey ("Noodles3Claimed")) {
			MenuManager.golds += 5;
			PlayerPrefs.SetString ("Golds", EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
			PlayerPrefs.SetInt ("Noodles3Claimed" , 1);
			//			UIManager._instance.goldText.text = MenuManager.golds.ToString ();
			// Display incremented gold
			// Effect
			// Show Tick
			CallIncrementgold();
			myChild[20].myTick.SetActive (true);
			myChild[20].myClaimButton.SetActive (false);
			AchievementChild.check_claim--;
			PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
		}
	}





	public void ClaimPizzaAchievement1()
	{
		if (PlayerPrefs.GetInt ("PizzaLevel1") == 1 && !PlayerPrefs.HasKey ("Pizza1Claimed")) {
			// Show Tick
			// Effect
			PlayerPrefs.SetInt ("Pizza1Claimed" , 1);
			MenuManager.totalscore+=100;
			PlayerPrefs.SetString("TotalScore",EncryptionHandler64.Encrypt (MenuManager.totalscore.ToString ()));
			//Display score
			CallIncrementCoin();
			myChild[21].myTick.SetActive (true);
			myChild[21].myClaimButton.SetActive (false);
			AchievementChild.check_claim--;
			PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
		}
	}
	
	public void ClaimPizzaAchievement2()
	{
		if (PlayerPrefs.GetInt ("PizzaLevel2") == 1 && !PlayerPrefs.HasKey ("Pizza2Claimed")) {
			MenuManager.golds++;
			PlayerPrefs.SetString("Golds",EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
//			UIManager._instance.goldText.text = MenuManager.golds.ToString ();
			PlayerPrefs.SetInt ("Pizza2Claimed" , 1);
			//Display incremented gold
			// Effect
			// Show Tick
			CallIncrementgold();
			myChild[22].myTick.SetActive (true);
			myChild[22].myClaimButton.SetActive (false);
			AchievementChild.check_claim--;
			PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
		}
	}
	
	public void ClaimPizzaAchievement3()
	{
		if (PlayerPrefs.GetInt ("PizzaLevel3") == 1 && !PlayerPrefs.HasKey ("Pizza3Claimed")) {
			MenuManager.golds += 5;
			PlayerPrefs.SetString("Golds",EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
//			UIManager._instance.goldText.text = MenuManager.golds.ToString ();
			PlayerPrefs.SetInt ("Pizza3Claimed" , 1);
			// Display incremented gold
			// Effect
			// Show Tick
			CallIncrementgold();
			myChild[23].myTick.SetActive (true);
			myChild[23].myClaimButton.SetActive (false);
			AchievementChild.check_claim--;
			PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
		}
	}



	public void ClaimHotdogAchievement1()
	{
		if (PlayerPrefs.GetInt ("hotdogLevel1") == 1 && !PlayerPrefs.HasKey ("hotdod1Claimed")) {
			// Show Tick
			// Effect
			PlayerPrefs.SetInt ("hotdod1Claimed" , 1);
			MenuManager.totalscore+=100;
			PlayerPrefs.SetString("TotalScore",EncryptionHandler64.Encrypt (MenuManager.totalscore.ToString ()));
			//Display score
			CallIncrementCoin();
			myChild[24].myTick.SetActive (true);
			myChild[24].myClaimButton.SetActive (false);
			AchievementChild.check_claim--;
			PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
		}
	}

	public void ClaimHotdogAchievement2()
	{
		if (PlayerPrefs.GetInt ("hotdogLevel2") == 1 && !PlayerPrefs.HasKey ("hotdod2Claimed")) {
			MenuManager.golds++;
			PlayerPrefs.SetString ("Golds", EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
			PlayerPrefs.SetInt ("hotdod2Claimed" , 1);
//			UIManager._instance.goldText.text = MenuManager.golds.ToString ();
			//Display incremented gold
			// Effect
			// Show Tick
			CallIncrementgold();
			myChild[25].myTick.SetActive (true);
			myChild[25].myClaimButton.SetActive (false);
			AchievementChild.check_claim--;
			PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
		}
	}

	public void ClaimHotdogAchievement3()
	{
		if (PlayerPrefs.GetInt ("hotdogLevel3") == 1 && !PlayerPrefs.HasKey ("hotdod3Claimed")) {
			MenuManager.golds += 5;
			PlayerPrefs.SetString ("Golds", EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
			PlayerPrefs.SetInt ("hotdod3Claimed" , 1);
//			UIManager._instance.goldText.text = MenuManager.golds.ToString ();
			// Display incremented gold
			// Effect
			// Show Tick

			CallIncrementgold();
			myChild[26].myTick.SetActive (true);
			myChild[26].myClaimButton.SetActive (false);
			AchievementChild.check_claim--;
			PlayerPrefs.SetInt("claimvalue",AchievementChild.check_claim);
		}
	}

	// Update is called once per frame
	void Update () {

	
	}


}
