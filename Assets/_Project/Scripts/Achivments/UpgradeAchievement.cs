using System.Collections;
using _Project.Scripts.Additional;
using _Project.Scripts.UI_Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Project.Scripts.Achivments
{
	public class UpgradeAchievement : MonoBehaviour 
	{
		private bool _isOnce;
		[FormerlySerializedAs("myChild")] [SerializeField] private AchievementBlock[] _blocksAchivents; 
		[FormerlySerializedAs("totalCoinsText")] [SerializeField] private  Text _coinsText;
		[FormerlySerializedAs("totalGoldText")] [SerializeField] private  Text _goldText ;

		private void OnEnable()
		{
			_goldText.text = MenuManager.golds.ToString ();
			_coinsText.text = MenuManager.totalscore.ToString ();
		}

		private void GoldIncrement()
		{
			StopCoroutine (nameof(CoinsIncrement));
			StartCoroutine (nameof(CoinsIncrement));
		}

		private void GoldIncrement1()
		{
			StopCoroutine (nameof(IncrementGold));
			StartCoroutine (nameof(IncrementGold));
		}

		private IEnumerator CoinsIncrement()
		{
			int textCoins = int.Parse(_coinsText.text);
		
			while (textCoins < MenuManager.totalscore)
			{
				textCoins+=1;
				_coinsText.text = textCoins.ToString ();
				yield return 0;
			}
			_coinsText.text = MenuManager.totalscore.ToString ();
		
		}

		private IEnumerator IncrementGold()
		{
			int textCoins = int.Parse(_goldText.text);
			while (textCoins < MenuManager.golds)
			{
				textCoins+=1;
				_goldText.text = textCoins.ToString ();
				yield return 0;
			}
			_goldText.text = MenuManager.golds.ToString ();
		
		}

		public void ClaimRevard()
		{
			if (PlayerPrefs.GetInt ("CustomerLevel1") == 1 && !PlayerPrefs.HasKey ("Customer1Claimed")) {
				MenuManager.totalscore+=100;
				PlayerPrefs.SetString("TotalScore",Encryption.Encrypt (MenuManager.totalscore.ToString ()));
				PlayerPrefs.SetInt ("Customer1Claimed" , 1);
				GoldIncrement();
				_blocksAchivents[0]._tickPrefab.SetActive (true);
				_blocksAchivents[0]._claimButton.SetActive (false);
				AchievementBlock._claimCheck--;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
			}
		}
		public void ClaimReward()
		{
			if (PlayerPrefs.GetInt ("CustomerLevel2") == 1 && !PlayerPrefs.HasKey ("Customer2Claimed")) 
			{
				MenuManager.golds ++;
				PlayerPrefs.SetString ("Golds", Encryption.Encrypt (MenuManager.golds.ToString ()));
				PlayerPrefs.SetInt ("Customer2Claimed" , 1);
				GoldIncrement1();
				_blocksAchivents[1]._tickPrefab.SetActive (true);
				_blocksAchivents[1]._claimButton.SetActive (false);
				AchievementBlock._claimCheck--;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
			}
		}
		public void ClaimReward3()
		{
			if (PlayerPrefs.GetInt ("CustomerLevel3") == 1 && !PlayerPrefs.HasKey ("Customer3Claimed")) 
			{
				MenuManager.golds += 5;
				PlayerPrefs.SetString ("Golds", Encryption.Encrypt (MenuManager.golds.ToString ()));
				PlayerPrefs.SetInt ("Customer3Claimed" , 1);
				GoldIncrement1();
				_blocksAchivents[2]._tickPrefab.SetActive (true);
				_blocksAchivents[2]._claimButton.SetActive (false);
				AchievementBlock._claimCheck--;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
			}
		}


		public void ClaimMaxReward()
		{
			if (PlayerPrefs.GetInt ("PerfectLevel1") == 1 && !PlayerPrefs.HasKey ("Perfect1Claimed")) 
			{
				MenuManager.totalscore+=100;
				PlayerPrefs.SetString("TotalScore",Encryption.Encrypt (MenuManager.totalscore.ToString ()));
				PlayerPrefs.SetInt ("Perfect1Claimed" , 1);
				GoldIncrement();
				_blocksAchivents[3]._tickPrefab.SetActive (true);
				_blocksAchivents[3]._claimButton.SetActive (false);
				AchievementBlock._claimCheck--;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
			}
		}
		public void ClaimMaxReward2()
		{
			if (PlayerPrefs.GetInt ("PerfectLevel2") == 1 && !PlayerPrefs.HasKey ("Perfect2Claimed"))
			{
				MenuManager.golds ++;
				PlayerPrefs.SetString ("Golds", Encryption.Encrypt (MenuManager.golds.ToString ()));
				PlayerPrefs.SetInt ("Perfect2Claimed" , 1);
				GoldIncrement1();
				_blocksAchivents[4]._tickPrefab.SetActive (true);
				_blocksAchivents[4]._claimButton.SetActive (false);
				AchievementBlock._claimCheck--;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
			}
		}
		public void ClaimMaxReward3()
		{
			if (PlayerPrefs.GetInt ("PerfectLevel3") == 1 && !PlayerPrefs.HasKey ("Perfect3Claimed")) 
			{
				MenuManager.golds += 5;
				PlayerPrefs.SetString ("Golds", Encryption.Encrypt (MenuManager.golds.ToString ()));
				PlayerPrefs.SetInt ("Perfect3Claimed" , 1);
				GoldIncrement1();
				_blocksAchivents[5]._tickPrefab.SetActive (true);
				_blocksAchivents[5]._claimButton.SetActive (false);
				AchievementBlock._claimCheck--;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
			}
		}
		
		public void ClaimBurgerReward1Lvl()
		{
			if (PlayerPrefs.GetInt ("BurgerLevel1") == 1 && !PlayerPrefs.HasKey ("Burger1Claimed")) 
			{
				PlayerPrefs.SetInt ("Burger1Claimed" , 1);
				MenuManager.totalscore+=100;
				PlayerPrefs.SetString("TotalScore",Encryption.Encrypt (MenuManager.totalscore.ToString ()));
				GoldIncrement();
				_blocksAchivents[6]._tickPrefab.SetActive (true);
				_blocksAchivents[6]._claimButton.SetActive (false);
				AchievementBlock._claimCheck--;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
			}
		}
	
		public void ClaimBurgerReward2Lvl()
		{
			if (PlayerPrefs.GetInt ("BurgerLevel2") == 1 && !PlayerPrefs.HasKey ("Burger2Claimed")) 
			{
				MenuManager.golds++;
				PlayerPrefs.SetString ("Golds", Encryption.Encrypt (MenuManager.golds.ToString ()));
				PlayerPrefs.SetInt ("Burger2Claimed" , 1);
				GoldIncrement1();
				_blocksAchivents[7]._tickPrefab.SetActive (true);
				_blocksAchivents[7]._claimButton.SetActive (false);
				AchievementBlock._claimCheck--;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
			}
		}
	
		public void ClaimBurgerReward3Lvl()
		{
			if (PlayerPrefs.GetInt ("BurgerLevel3") == 1 && !PlayerPrefs.HasKey ("Burger3Claimed")) 
			{
				MenuManager.golds += 5;
				PlayerPrefs.SetString ("Golds", Encryption.Encrypt (MenuManager.golds.ToString ()));
				PlayerPrefs.SetInt ("Burger3Claimed" , 1);
				GoldIncrement1();
				_blocksAchivents[8]._tickPrefab.SetActive (true);
				_blocksAchivents[8]._claimButton.SetActive (false);
				AchievementBlock._claimCheck--;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
			}
		}
		public void ClaimFriesReward1Lvl()
		{
			if (PlayerPrefs.GetInt ("FrenchfriesLevel1") == 1 && !PlayerPrefs.HasKey ("Fries1Claimed")) 
			{
				PlayerPrefs.SetInt ("Fries1Claimed" , 1);
				MenuManager.totalscore+=100;
				PlayerPrefs.SetString("TotalScore",Encryption.Encrypt (MenuManager.totalscore.ToString ()));
				GoldIncrement();
				_blocksAchivents[9]._tickPrefab.SetActive (true);
				_blocksAchivents[9]._claimButton.SetActive (false);
				AchievementBlock._claimCheck--;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
			}
		}
	
		public void ClaimFriesReward2Lvl()
		{
			if (PlayerPrefs.GetInt ("FrenchfriesLevel2") == 1 && !PlayerPrefs.HasKey ("Fries2Claimed"))
			{
				MenuManager.golds++;
				PlayerPrefs.SetString ("Golds", Encryption.Encrypt (MenuManager.golds.ToString ()));
				PlayerPrefs.SetInt ("Fries2Claimed" , 1);
				GoldIncrement1();
				_blocksAchivents[10]._tickPrefab.SetActive (true);
				_blocksAchivents[10]._claimButton.SetActive (false);
				AchievementBlock._claimCheck--;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
			}
		}
	
		public void ClaimFriesReward3Lvl()
		{
			if (PlayerPrefs.GetInt ("FrenchfriesLevel3") == 1 && !PlayerPrefs.HasKey ("Fries3Claimed")) 
			{
				MenuManager.golds += 5;
				PlayerPrefs.SetString ("Golds", Encryption.Encrypt (MenuManager.golds.ToString ()));
				PlayerPrefs.SetInt ("Fries3Claimed" , 1);
				GoldIncrement1();
				_blocksAchivents[11]._tickPrefab.SetActive (true);
				_blocksAchivents[11]._claimButton.SetActive (false);
				AchievementBlock._claimCheck--;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
			}
		}
		
		public void ClaimCokeReward1Lvl()
		{
			if (PlayerPrefs.GetInt ("CokeLevel1") == 1 && !PlayerPrefs.HasKey ("Coke1Claimed")) 
			{
				PlayerPrefs.SetInt ("Coke1Claimed" , 1);
				MenuManager.totalscore+=100;
				PlayerPrefs.SetString("TotalScore",Encryption.Encrypt (MenuManager.totalscore.ToString ()));
				GoldIncrement();
				_blocksAchivents[12]._tickPrefab.SetActive (true);
				_blocksAchivents[12]._claimButton.SetActive (false);
				AchievementBlock._claimCheck--;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
		
			}
		}
	
		public void ClaimCokeReward2Lvl()
		{
			if (PlayerPrefs.GetInt ("CokeLevel2") == 1 && !PlayerPrefs.HasKey ("Coke2Claimed")) 
			{
				MenuManager.golds++;
				PlayerPrefs.SetString ("Golds", Encryption.Encrypt (MenuManager.golds.ToString ()));
				PlayerPrefs.SetInt ("Coke2Claimed" , 1);
				GoldIncrement1();
				_blocksAchivents[13]._tickPrefab.SetActive (true);
				_blocksAchivents[13]._claimButton.SetActive (false);
				AchievementBlock._claimCheck--;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
			}
		}
	
		public void ClaimCokeReward3Lvl()
		{
			if (PlayerPrefs.GetInt ("CokeLevel3") == 1 && !PlayerPrefs.HasKey ("Coke3Claimed")) 
			{
				MenuManager.golds += 5;
				PlayerPrefs.SetString ("Golds", Encryption.Encrypt (MenuManager.golds.ToString ()));
				PlayerPrefs.SetInt ("Coke3Claimed" , 1);
				GoldIncrement1();
				_blocksAchivents[14]._tickPrefab.SetActive (true);
				_blocksAchivents[14]._claimButton.SetActive (false);
				AchievementBlock._claimCheck--;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
			}
		}




		public void ClaimThiefRewardLvl1()
		{
			if (PlayerPrefs.GetInt ("ThiefLevel1") == 1 && !PlayerPrefs.HasKey ("Thief1Claimed")) 
			{
				PlayerPrefs.SetInt ("Thief1Claimed" , 1);
				MenuManager.totalscore+=100;
				PlayerPrefs.SetString("TotalScore",Encryption.Encrypt (MenuManager.totalscore.ToString ()));
				GoldIncrement();
				_blocksAchivents[15]._tickPrefab.SetActive (true);
				_blocksAchivents[15]._claimButton.SetActive (false);
				AchievementBlock._claimCheck--;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
			}
		}
	
		public void ClaimThiefRewardLvl2()
		{
			if (PlayerPrefs.GetInt ("ThiefLevel2") == 1 && !PlayerPrefs.HasKey ("Thief2Claimed")) 
			{
				MenuManager.golds++;
				PlayerPrefs.SetString ("Golds", Encryption.Encrypt (MenuManager.golds.ToString ()));
				PlayerPrefs.SetInt ("Thief2Claimed" , 1);
				GoldIncrement1();
				_blocksAchivents[16]._tickPrefab.SetActive (true);
				_blocksAchivents[16]._claimButton.SetActive (false);
				AchievementBlock._claimCheck--;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
			}
		}
	
		public void ClaimThiefRewardLvl3()
		{
			if (PlayerPrefs.GetInt ("ThiefLevel3") == 1 && !PlayerPrefs.HasKey ("Thief3Claimed")) 
			{
				MenuManager.golds += 5;
				PlayerPrefs.SetString ("Golds", Encryption.Encrypt (MenuManager.golds.ToString ()));
				PlayerPrefs.SetInt ("Thief3Claimed" , 1);
				GoldIncrement1();
				_blocksAchivents[17]._tickPrefab.SetActive (true);
				_blocksAchivents[17]._claimButton.SetActive (false);
				AchievementBlock._claimCheck--;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
			}
		}

		public void ClaimNoodlesRewardLvl1()
		{
			if (PlayerPrefs.GetInt ("NoodlesLevel1") == 1 && !PlayerPrefs.HasKey ("Noodles1Claimed")) 
			{
				PlayerPrefs.SetInt ("Noodles1Claimed" , 1);
				MenuManager.totalscore+=100;
				PlayerPrefs.SetString("TotalScore",Encryption.Encrypt (MenuManager.totalscore.ToString ()));
				GoldIncrement();
				_blocksAchivents[18]._tickPrefab.SetActive (true);
				_blocksAchivents[18]._claimButton.SetActive (false);
				AchievementBlock._claimCheck--;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
			}
		}
	
		public void ClaimNoodlesRewardLvl2()
		{
			if (PlayerPrefs.GetInt ("NoodlesLevel2") == 1 && !PlayerPrefs.HasKey ("Noodles2Claimed")) 
			{
				MenuManager.golds++;
				PlayerPrefs.SetString ("Golds", Encryption.Encrypt (MenuManager.golds.ToString ()));
				PlayerPrefs.SetInt ("Noodles2Claimed" , 1);
				GoldIncrement1();
				_blocksAchivents[19]._tickPrefab.SetActive (true);
				_blocksAchivents[19]._claimButton.SetActive (false);
				AchievementBlock._claimCheck--;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
			}
		}
	
		public void ClaimNoodlesRewardLvl3()
		{
			if (PlayerPrefs.GetInt ("NoodlesLevel3") == 1 && !PlayerPrefs.HasKey ("Noodles3Claimed")) {
				MenuManager.golds += 5;
				PlayerPrefs.SetString ("Golds", Encryption.Encrypt (MenuManager.golds.ToString ()));
				PlayerPrefs.SetInt ("Noodles3Claimed" , 1);
				GoldIncrement1();
				_blocksAchivents[20]._tickPrefab.SetActive (true);
				_blocksAchivents[20]._claimButton.SetActive (false);
				AchievementBlock._claimCheck--;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
			}
		}
		

		public void ClaimPizzaRewardLvl1()
		{
			if (PlayerPrefs.GetInt ("PizzaLevel1") == 1 && !PlayerPrefs.HasKey ("Pizza1Claimed")) 
			{
				PlayerPrefs.SetInt ("Pizza1Claimed" , 1);
				MenuManager.totalscore+=100;
				PlayerPrefs.SetString("TotalScore",Encryption.Encrypt (MenuManager.totalscore.ToString ()));
				GoldIncrement();
				_blocksAchivents[21]._tickPrefab.SetActive (true);
				_blocksAchivents[21]._claimButton.SetActive (false);
				AchievementBlock._claimCheck--;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
			}
		}
	
		public void ClaimPizzaRewardLvl2()
		{
			if (PlayerPrefs.GetInt ("PizzaLevel2") == 1 && !PlayerPrefs.HasKey ("Pizza2Claimed")) 
			{
				MenuManager.golds++;
				PlayerPrefs.SetString("Golds",Encryption.Encrypt (MenuManager.golds.ToString ()));
				PlayerPrefs.SetInt ("Pizza2Claimed" , 1);
				GoldIncrement1();
				_blocksAchivents[22]._tickPrefab.SetActive (true);
				_blocksAchivents[22]._claimButton.SetActive (false);
				AchievementBlock._claimCheck--;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
			}
		}
	
		public void ClaimPizzaRewardLvl3()
		{
			if (PlayerPrefs.GetInt ("PizzaLevel3") == 1 && !PlayerPrefs.HasKey ("Pizza3Claimed")) 
			{
				MenuManager.golds += 5;
				PlayerPrefs.SetString("Golds",Encryption.Encrypt (MenuManager.golds.ToString ()));
				PlayerPrefs.SetInt ("Pizza3Claimed" , 1);
				GoldIncrement1();
				_blocksAchivents[23]._tickPrefab.SetActive (true);
				_blocksAchivents[23]._claimButton.SetActive (false);
				AchievementBlock._claimCheck--;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
			}
		}
		
		public void ClaimHotdogRewardLvl1()
		{
			if (PlayerPrefs.GetInt ("hotdogLevel1") == 1 && !PlayerPrefs.HasKey ("hotdod1Claimed")) 
			{
				PlayerPrefs.SetInt ("hotdod1Claimed" , 1);
				MenuManager.totalscore+=100;
				PlayerPrefs.SetString("TotalScore",Encryption.Encrypt (MenuManager.totalscore.ToString ()));
				GoldIncrement();
				_blocksAchivents[24]._tickPrefab.SetActive (true);
				_blocksAchivents[24]._claimButton.SetActive (false);
				AchievementBlock._claimCheck--;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
			}
		}

		public void ClaimHotdogRewardLvl2()
		{
			if (PlayerPrefs.GetInt ("hotdogLevel2") == 1 && !PlayerPrefs.HasKey ("hotdod2Claimed")) 
			{
				MenuManager.golds++;
				PlayerPrefs.SetString ("Golds", Encryption.Encrypt (MenuManager.golds.ToString ()));
				PlayerPrefs.SetInt ("hotdod2Claimed" , 1);
				GoldIncrement1();
				_blocksAchivents[25]._tickPrefab.SetActive (true);
				_blocksAchivents[25]._claimButton.SetActive (false);
				AchievementBlock._claimCheck--;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
			}
		}

		public void ClaimHotdogRewardLvl3()
		{
			if (PlayerPrefs.GetInt ("hotdogLevel3") == 1 && !PlayerPrefs.HasKey ("hotdod3Claimed"))
			{
				MenuManager.golds += 5;
				PlayerPrefs.SetString ("Golds", Encryption.Encrypt (MenuManager.golds.ToString ()));
				PlayerPrefs.SetInt ("hotdod3Claimed" , 1);
				GoldIncrement1();
				_blocksAchivents[26]._tickPrefab.SetActive (true);
				_blocksAchivents[26]._claimButton.SetActive (false);
				AchievementBlock._claimCheck--;
				PlayerPrefs.SetInt("claimvalue",AchievementBlock._claimCheck);
			}
		}
	}
}
