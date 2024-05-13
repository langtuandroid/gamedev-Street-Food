using _Project.Scripts.Additional;
using _Project.Scripts.Game;
using Integration;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI_Scripts
{
	public class EquipmentShopItems : MonoBehaviour
	{
		[Inject] private RewardedAdController _rewardedAdController;
		[Inject] private MenuManager _menuManager;
		[SerializeField] private bool _isVideo;
		public GameObject []upgradeNoImages;
		public Sprite []upgradeImages;
		public int []coinsToUpgradeLevel;
		public int []goldToUpgradeLevel;
		public string []upgradeValues;
		public EquipmentPanel equipmentPanel;
		public string myName;
		public int equ_number;
		
		private int upgradeValue;
		public bool IsVideo => _isVideo;
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
				
				Button purchaseButton = equipmentPanel.purchaseButton.GetComponent<Button>();
				
				(_isVideo ? equipmentPanel.VideoButton : purchaseButton).onClick.RemoveAllListeners();
				(_isVideo ? equipmentPanel.VideoButton : purchaseButton).onClick.AddListener(OnPurchase);
				purchaseButton.gameObject.SetActive(!_isVideo);
				equipmentPanel.VideoButton.gameObject.SetActive(_isVideo);
			}
			else
			{
				equipmentPanel.upgradeImage.sprite = upgradeImages[1];
				equipmentPanel.upgradeValueText.text = upgradeValues[2];
				equipmentPanel.purchaseButton.SetActive (false);
				equipmentPanel.VideoButton.gameObject.SetActive (false);
			}
			EquimentData._instance.itemNumber = equ_number;
		}

		private void OnPurchase()
		{
			if (_isVideo)
			{
				RequestVideo();
			}
			else
			{
				BuyWithCoins();
			}
		}

		private void BuyWithCoins()
		{
			if(MenuManager.totalscore >= coinsToUpgradeLevel[upgradeValue] && MenuManager.golds >= goldToUpgradeLevel[upgradeValue])
			{
				MenuManager.golds-=goldToUpgradeLevel[upgradeValue];
				MenuManager.totalscore-=coinsToUpgradeLevel[upgradeValue];
				PlayerPrefs.SetString("TotalScore",Encryption.Encrypt (MenuManager.totalscore.ToString ()));
				PlayerPrefs.SetString("Golds",Encryption.Encrypt (MenuManager.golds.ToString ()));
				equipmentPanel.CallDecrementCoin();
					
				UpgradeItem();
			}
			else
			{
				if((MenuManager.totalscore < coinsToUpgradeLevel[upgradeValue]) )
				{
					_menuManager.lastPanelName = "EquipmentUpdrade";
					_menuManager.Insufficinetcoin();	

				}
				else if((MenuManager.golds < goldToUpgradeLevel[upgradeValue]))
				{
					_menuManager.Insufficinetgold();
					_menuManager.lastPanelName = "EquipmentUpdrade";

				}
			}
		}

		private void RequestVideo()
		{
			_rewardedAdController.ShowAd();
			_rewardedAdController.OnVideoClosed += CancelVideo;
			_rewardedAdController.GetRewarded += UpgradeItem;
		}

		private void CancelVideo()
		{
			_rewardedAdController.OnVideoClosed -= CancelVideo;
			_rewardedAdController.GetRewarded -= UpgradeItem;
		}

		private void UpgradeItem()
		{
			upgradeValue++;
			upgradeNoImages[upgradeValue].SetActive (true);
			PlayerPrefs.SetString(myName+"Upgrade",Encryption.Encrypt (upgradeValue.ToString ()));
			OnClickToShow();
			if(equ_number == 10)
			{
				PlayerPrefs.SetInt("Fridge" , 1);
				equipmentPanel.purchaseButton.SetActive (false);
			}
		}
	}
}
