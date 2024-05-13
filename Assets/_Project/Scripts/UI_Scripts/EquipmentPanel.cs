using System.Collections;
using _Project.Scripts.UI.Tutorial;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI_Scripts
{
	public class EquipmentPanel : MonoBehaviour
	{
		[Inject] private DiContainer _diContainer;
		[Inject] private MenuManager _menuManager;  
		[Inject] private UIManager _uiManager;   
		public Image upgradeImage;
		public GameObject goldObject;
		public Text coinsText;
		public Text goldText;
		public Text totalCoinsText;
		public Text totalGoldText;
		public TMP_Text upgradeValueText;
		public GameObject purchaseButton;
		[SerializeField] private Button _videoButton;
	
		public GameObject China_1 ;
		public GameObject China_2;
		public GameObject China_3 ;
		public GameObject China_4 ;
	
		public GameObject Italy_1 ;
		public GameObject Italy_2 ;
		public GameObject Italy_3 ;
		public GameObject Italy_4 ;

		public GameObject Aus_1 ;
		public GameObject Aus_2 ;
		public GameObject Aus_3 ;
		public GameObject Aus_4 ;

		public Button VideoButton => _videoButton;
		private void OnEnable()
		{
			if(PlayerPrefs.HasKey ("ChinaOpen")) {
				China_1.SetActive (false); 
				China_2.SetActive (false);
				China_3.SetActive(false) ;
				China_4.SetActive(false) ;
			}
		
			if(PlayerPrefs.HasKey ("ItalyOpen")) {
				Italy_1.SetActive(false) ;
				Italy_2.SetActive(false);
				Italy_3.SetActive(false);
				Italy_4.SetActive(false);
			}
			if(PlayerPrefs.HasKey ("AusOpen")) {
				Aus_1.SetActive(false) ;
				Aus_2.SetActive(false) ;
				Aus_3.SetActive(false) ;
				Aus_4.SetActive(false) ;
			}
		
			totalGoldText.text = MenuManager.golds.ToString ();
			totalCoinsText.text = MenuManager.totalscore.ToString ();
			TutorialPanel.popupPanelActive = true;
		}

		public void Close()
		{
			GameObject upgradePanel = _diContainer.InstantiatePrefab(Resources.Load ("UpgradePanel"));
			upgradePanel.transform.SetParent(transform.parent,false);
			upgradePanel.transform.localScale = Vector3.one;
			upgradePanel.transform.localPosition = Vector3.zero;
			if(_menuManager != null)
				_menuManager.EnableFadePanel ();
			else
				_uiManager.EnableFadePanel ();
			Destroy (gameObject);
		}

		public void CallDecrementCoin()
		{
			StopCoroutine (nameof(DecrementCoins));
			StopCoroutine (nameof(DecrementGold));
			StartCoroutine (nameof(DecrementCoins));
			StartCoroutine (nameof(DecrementGold));
		}

		private IEnumerator DecrementCoins()
		{
			int textCoins = int.Parse(totalCoinsText.text);
			while (textCoins > MenuManager.totalscore)
			{
				textCoins-=20;
				totalCoinsText.text = textCoins.ToString ();
				yield return 0;
			}
			totalCoinsText.text = MenuManager.totalscore.ToString ();
		
		}

		private IEnumerator DecrementGold()
		{
			int textCoins = int.Parse(totalGoldText.text);
			while (textCoins > MenuManager.golds)
			{
				textCoins-=1;
				totalGoldText.text = textCoins.ToString ();
				yield return 0;
			}
			totalGoldText.text = MenuManager.golds.ToString ();
		
		}

	}
}
