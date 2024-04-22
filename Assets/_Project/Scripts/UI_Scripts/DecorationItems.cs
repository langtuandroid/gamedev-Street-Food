using _Project.Scripts.Additional;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI_Scripts
{
	public class DecorationItems : MonoBehaviour 
	{
		[Inject] private MenuManager _menuManager;  
		public GameObject myParent;
		public Image myImage;
		public Sprite []elementsToShow;
		public string []elementsName;
		public int []elementsCoinPrice;
		public int []elementsGoldPrice;
		public GameObject priceButton;
		public Text priceCoinText;
		public Text priceGoldText;
		public Text selectedButtonText;
		public GameObject selectButton;
		public string myName;
		private int clickedItem;

		private void OnEnable()
		{
			if(PlayerPrefs.HasKey (elementsName[0]) && PlayerPrefs.HasKey (elementsName[1]) && PlayerPrefs.HasKey (elementsName[2]) && PlayerPrefs.HasKey (elementsName[3]))
			{
				Debug.Log("a");
				selectButton.SetActive (true);
				if(PlayerPrefs.GetString (myName) == elementsName[0])
				{
					selectedButtonText.text = "SELECTED";
				}
				else
				{
					selectedButtonText.text = "SELECT";
				}
				priceButton.SetActive (false);
			}
			else
			{
				selectButton.SetActive (false);
				priceButton.SetActive (true);
				if(PlayerPrefs.HasKey (elementsName[0]))
				{
					if(!PlayerPrefs.HasKey (elementsName[1]))
					{
						priceCoinText.text = elementsCoinPrice[1].ToString ();
						priceGoldText.text = elementsGoldPrice[1].ToString ();
						myImage.sprite = elementsToShow[1];
					}
					else if(!PlayerPrefs.HasKey (elementsName[2]))
					{
					
						priceCoinText.text = elementsCoinPrice[2].ToString ();
						priceGoldText.text = elementsGoldPrice[2].ToString ();
						myImage.sprite = elementsToShow[2];
					}
					else if(!PlayerPrefs.HasKey (elementsName[3]))
					{
					
						priceCoinText.text = elementsCoinPrice[3].ToString ();
						priceGoldText.text = elementsGoldPrice[3].ToString ();
						myImage.sprite = elementsToShow[3];
					}
				}
				else 
				{
					priceCoinText.text = elementsCoinPrice[0].ToString ();
					priceGoldText.text = elementsGoldPrice[0].ToString ();
					myImage.sprite = elementsToShow[0];
				}
			}
		}

		public void SelectImage(int imageNo)
		{
			clickedItem = imageNo;
			myImage.sprite = elementsToShow[imageNo];
			if(PlayerPrefs.HasKey (elementsName[imageNo]))
			{
				selectButton.SetActive (true);
				if(PlayerPrefs.GetString (myName) == elementsName[imageNo])
				{
					selectedButtonText.text = "SELECTED";
				}
				else
				{
					selectedButtonText.text = "SELECT";
				}
				priceButton.SetActive (false);
			}
			else
			{
				selectButton.SetActive (false);
				priceButton.SetActive (true);
				priceCoinText.text = elementsCoinPrice[imageNo].ToString ();
				priceGoldText.text = elementsGoldPrice[imageNo].ToString ();
			}
		}

		public void PurchaseItem()
		{
			if((MenuManager.totalscore >= elementsCoinPrice[clickedItem]) && (MenuManager.golds >= elementsGoldPrice[clickedItem]))
			{
				MenuManager.golds-=elementsGoldPrice[clickedItem];
				MenuManager.totalscore-=elementsCoinPrice[clickedItem];

				PlayerPrefs.SetString("TotalScore",Encryption.Encrypt (MenuManager.totalscore.ToString ()));
				PlayerPrefs.SetString("Golds",Encryption.Encrypt (MenuManager.golds.ToString ()));

				selectButton.SetActive (true);
				PlayerPrefs.SetString (myName ,elementsName[clickedItem]);
				PlayerPrefs.SetInt (elementsName[clickedItem],1);
				selectedButtonText.text = "SELECTED";
				DecorationPanel._instance.CallDecrementCoin ();
				priceButton.SetActive (false);

			}
			else
			{
				if((MenuManager.totalscore < elementsCoinPrice[clickedItem]) )
				{
					_menuManager.lastPanelName = "DecorationPanel";
					_menuManager.Insufficinetcoin();
				}
				else if((MenuManager.golds < elementsGoldPrice[clickedItem]))
				{
					_menuManager.lastPanelName = "DecorationPanel";
					_menuManager.Insufficinetgold() ;
				}
			}
		}

		public void SelectItem()
		{
			if(PlayerPrefs.GetString (myName) != elementsName[clickedItem])
			{
				selectedButtonText.text = "SELECTED";
				PlayerPrefs.SetString (myName ,elementsName[clickedItem]);
			}
		
		}
	}
}
