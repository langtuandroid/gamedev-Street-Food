﻿using System.Collections;
using _Project.Scripts.UI.Tutorial;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI_Scripts
{
	public class DecorationPanel : MonoBehaviour 
	{
		[Inject] private DiContainer _diContainer;
		[Inject] private MenuManager _menuManager;  
		[Inject] private UIManager _uiManager;   
		private int selectedPanel;
		
		public GameObject leftArrow , rightArrow;
		public GameObject []panels;
		public Text totalCoinsText;
		public Text totalGoldText;
		public static DecorationPanel _instance;
		public GameObject China_tabletop_lock ;
		public GameObject China_tablecover_lock ;
		public GameObject Italy_tabletop_lock ;
		public GameObject Italy_tablecover_lock ;
		public GameObject Aus_tabletop_lock ;
		public GameObject Aus_tablecover_lock ;

		private void Start () 
		{
			_instance = this;
			totalGoldText.text = MenuManager.golds.ToString ();
			totalCoinsText.text = MenuManager.totalscore.ToString ();
		}

		private void OnEnable()
		{
			if(PlayerPrefs.HasKey ("ChinaOpen")) {
				China_tabletop_lock.SetActive(false); 
				China_tablecover_lock.SetActive(false); 
			}
		
			if(PlayerPrefs.HasKey ("ItalyOpen")) {
				Italy_tabletop_lock.SetActive(false); 
				Italy_tablecover_lock.SetActive(false);
			}
			if(PlayerPrefs.HasKey ("AusOpen")) {
				Aus_tabletop_lock.SetActive(false); 
				Aus_tablecover_lock.SetActive(false);

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
			Destroy (gameObject);
			if(_menuManager != null)
				_menuManager.EnableFadePanel ();
			else
				_uiManager.EnableFadePanel ();
		}

		public void MoveRight()
		{
			if(selectedPanel < panels.Length - 1)
			{
				_menuManager.EnableFadePanel();
				panels[selectedPanel].SetActive (false);
				selectedPanel++;
				panels[selectedPanel].SetActive (true);
				if(selectedPanel == panels.Length - 1)
				{
					rightArrow.SetActive (false);
				}
				leftArrow.SetActive (true);
			}
		}

		public void MoveLeft()
		{
			if(selectedPanel > 0)
			{
				_menuManager.EnableFadePanel();
				panels[selectedPanel].SetActive (false);
				selectedPanel--;
				panels[selectedPanel].SetActive (true);
				if(selectedPanel == 0)
				{
					leftArrow.SetActive (false);
				}
	
				rightArrow.SetActive (true);
			}
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
