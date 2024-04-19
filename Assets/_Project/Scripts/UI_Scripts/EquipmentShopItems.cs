﻿using _Project.Scripts.Additional;
using _Project.Scripts.Game;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI_Scripts
{
	public class EquipmentShopItems : MonoBehaviour 
	{
		[Inject] private MenuManager _menuManager;  
		public GameObject []upgradeNoImages;
		public Sprite []upgradeImages;
		public int []coinsToUpgradeLevel;
		public int []goldToUpgradeLevel;
		public string []upgradeValues;
		public EquipmentPanel equipmentPanel;
		public string myName;
		public int equ_number;
		
		private int upgradeValue;
		private void OnEnable()
		{

			if (!myName.Contains ("ItalyFridge"))
				upgradeValue = (int)Encryption.Decrypt (PlayerPrefs.GetString (myName + "Upgrade"));
			else {
				if(PlayerPrefs.HasKey (myName + "Upgrade"))
				{
					upgradeValue = (int)Encryption.Decrypt (PlayerPrefs.GetString (myName + "Upgrade"));
				
				}
				else
					upgradeValue = 0;
			}

			for(int i= 0 ; i <= upgradeValue ; i++)
			{
				upgradeNoImages[i].SetActive (true);
			}

			if(myName == "Grills")
			{
				OnClickToShow();
			}
		}
	
		public void OnClickToShow()
		{
			EquipmentPanel._instance.help_bttn.GetComponent<Animator> ().enabled = true;
			if(upgradeValue < 2)
			{
				equipmentPanel.upgradeImage.sprite = upgradeImages[upgradeValue];
				equipmentPanel.upgradeValueText.text = upgradeValues[upgradeValue].ToString ();
				equipmentPanel.purchaseButton.SetActive (true);
				if(equ_number == 10  && PlayerPrefs.GetInt("Fridge")==1 )
				{

					equipmentPanel.purchaseButton.SetActive (false);
				
				}
				if(goldToUpgradeLevel[upgradeValue] > 0)
				{
					equipmentPanel.goldObject.SetActive (true);
					equipmentPanel.goldText.text = goldToUpgradeLevel[upgradeValue].ToString ();
				}
				else
				{
					equipmentPanel.goldObject.SetActive (false);
				}
			
				equipmentPanel.coinsText.text = coinsToUpgradeLevel[upgradeValue].ToString ();
				equipmentPanel.purchaseButton.GetComponent<Button>().onClick.RemoveAllListeners ();
				equipmentPanel.purchaseButton.GetComponent<Button>().onClick.AddListener (()=>OnPurchase());
			
			}
			else
			{
				equipmentPanel.upgradeImage.sprite = upgradeImages[1];
				equipmentPanel.upgradeValueText.text = upgradeValues[2];
				equipmentPanel.purchaseButton.SetActive (false);
			
			}
			EquimentShowInfo._instance.itemNo = equ_number;

		}

		public void OnPurchase()
		{
			if(MenuManager.totalscore >= coinsToUpgradeLevel[upgradeValue] && MenuManager.golds >= goldToUpgradeLevel[upgradeValue])
			{
				MenuManager.golds-=goldToUpgradeLevel[upgradeValue];
				MenuManager.totalscore-=coinsToUpgradeLevel[upgradeValue];
				upgradeNoImages[upgradeValue+1].SetActive (true);
				PlayerPrefs.SetString("TotalScore",Encryption.Encrypt (MenuManager.totalscore.ToString ()));
				PlayerPrefs.SetString("Golds",Encryption.Encrypt (MenuManager.golds.ToString ()));
				upgradeValue++;
				PlayerPrefs.SetString(myName+"Upgrade",Encryption.Encrypt (upgradeValue.ToString ()));
				equipmentPanel.CallDecrementCoin();
				OnClickToShow();
				if(equ_number == 10)
				{
					PlayerPrefs.SetInt("Fridge" , 1);
					equipmentPanel.purchaseButton.SetActive (false);
				}
			}
			else
			{
				if((MenuManager.totalscore < coinsToUpgradeLevel[upgradeValue]) )
				{
					_menuManager.lastPanel = equipmentPanel.gameObject;
					_menuManager.lastPanelName = "EquipmentUpdrade";
					_menuManager.Insufficinetcoin();	

				}
				else if((MenuManager.golds < goldToUpgradeLevel[upgradeValue]))
				{
					_menuManager.Insufficinetgold();
					_menuManager.lastPanel = equipmentPanel.gameObject;
					_menuManager.lastPanelName = "EquipmentUpdrade";

				}
			}
		}
	}
}
