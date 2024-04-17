using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EquipmentShopItems : MonoBehaviour {

	public static EquipmentShopItems _instance;
	public GameObject []upgradeNoImages;

	public Sprite []upgradeImages;

	public int []coinsToUpgradeLevel;

	public int []goldToUpgradeLevel;

	public string []upgradeValues;

	public EquipmentPanel equipmentPanel;

	public string myName;

	int upgradeValue;
	public int equ_value ;
	public int equ_number ;
	// Use this for initialization
	void Awake () {
		_instance = this;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable()
	{

		if (!myName.Contains ("ItalyFridge"))
			upgradeValue = (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString (myName + "Upgrade"));
		else {
			if(PlayerPrefs.HasKey (myName + "Upgrade"))
			{
				upgradeValue = (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString (myName + "Upgrade"));
				
			}
			else
				upgradeValue = 0;
		}
	//	Debug.Log("myname = "+ myName+"Upgrade");
	//	Debug.Log("upgradeValue = "+upgradeValue);
		for(int i= 0 ; i <= upgradeValue ; i++)
		{
			upgradeNoImages[i].SetActive (true);
		}

		if(myName == "Grills")
		{
			OnClickToShow();

		}
//			PlayerPrefs.SetInt (myName+"Upgrade",PlayerPrefs.GetInt (myName+"Upgrade")+1);

	}




	public void OnClickToShow()
	{

		EquipmentPanel._instance.help_bttn.GetComponent<Animator> ().enabled = true;
//		Debug.Log (equ_number);
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

//		int golds = (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("Golds"));
//		int totalscore = (int)EncryptionHandler64.Decrypt (PlayerPrefs.GetString("TotalScore"));

		if(MenuManager.totalscore >= coinsToUpgradeLevel[upgradeValue] && MenuManager.golds >= goldToUpgradeLevel[upgradeValue])
		{

			MenuManager.golds-=goldToUpgradeLevel[upgradeValue];
			MenuManager.totalscore-=coinsToUpgradeLevel[upgradeValue];
			upgradeNoImages[upgradeValue+1].SetActive (true);
			PlayerPrefs.SetString("TotalScore",EncryptionHandler64.Encrypt (MenuManager.totalscore.ToString ()));
			PlayerPrefs.SetString("Golds",EncryptionHandler64.Encrypt (MenuManager.golds.ToString ()));
			upgradeValue++;
			PlayerPrefs.SetString(myName+"Upgrade",EncryptionHandler64.Encrypt (upgradeValue.ToString ()));
//			equipmentPanel.totalGoldText.text = golds.ToString ();
//			equipmentPanel.totalCoinsText.text = totalscore.ToString ();
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
				MenuManager._instance.lastPanel = equipmentPanel.gameObject;
				MenuManager._instance.lastPanelName = "EquipmentUpdrade";
				MenuManager._instance.Insufficinetcoin();	

			}
			else if((MenuManager.golds < goldToUpgradeLevel[upgradeValue]))
			{
				MenuManager._instance.Insufficinetgold();
				MenuManager._instance.lastPanel = equipmentPanel.gameObject;
				MenuManager._instance.lastPanelName = "EquipmentUpdrade";

			}
			//Debug.Log("insufficient funds");
		}
//		PlayerPrefs.GetInt ("Golds");
//		PlayerPrefs.SetInt ("TotalScore",PlayerPrefs.GetInt ("TotalScore"));
	}





}
