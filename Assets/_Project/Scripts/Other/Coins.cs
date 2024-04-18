﻿using System.Collections;
using _Project.Scripts.Managers;
using _Project.Scripts.UI_Scripts;
using UnityEngine;

namespace _Project.Scripts.Other
{
	public class Coins : MonoBehaviour 
	{
		public int positionTaken;
		public int myAmount;
		public GameObject perfectText;
		public TextMesh addValue;
		public bool tutorialOn;
		public ParticleSystem coinCollected;
		public ParticleSystem highlight ;
		public GameObject uiCoins;
		public TextMesh Bonus_value ;
		public int bonusVal;
		public static int visible ;
		
		private Vector3 coinsToMoveInitialPosition;
		

		private void Start () 
		{
			coinsToMoveInitialPosition = transform.position;
		}

		private void OnEnable()
		{
			visible++;
			if (CustomerHandler._instance.gameTimer <= 15 ) {

				{
					highlight.Play();
				}
			}

			if (coinsToMoveInitialPosition != Vector3.zero)
			{
				transform.position = coinsToMoveInitialPosition;
			}
		
			transform.localScale = new Vector3(1,1,1);
			StartCoroutine (ScaleObject());
			addValue.gameObject.SetActive (true);
			addValue.text = "+"+myAmount;
			if (bonusVal > 0) {
	
				Bonus_value.gameObject.SetActive (true);
				Bonus_value.text = "Bonus +" + bonusVal;
				Invoke(nameof(deactive),1.3f);
			} 
			else 
			{
				Bonus_value.gameObject.SetActive (false);
			}
		}

		private void deactive()
		{
			Bonus_value.gameObject.SetActive (false);
		}

		private void OnDisable()
		{
			highlight.Stop ();
			visible--;
			StopCoroutine (ScaleObject());
		}

		private IEnumerator ScaleObject()
		{
			while(transform.localScale.x < 3)
			{
				transform.localScale = new Vector3(transform.localScale.x+0.12f,transform.localScale.y+0.12f,1);
				yield return 0;
			}
		}

		private IEnumerator MoveCoins()
		{
			Vector3 finalPos = uiCoins.transform.position;
			finalPos = new Vector3(finalPos.x , finalPos.y,coinsToMoveInitialPosition.z);
			transform.position = coinsToMoveInitialPosition;
			while(Vector3.Distance(transform.position, finalPos) > 1f)
			{
				transform.position = Vector3.MoveTowards(transform.position, finalPos, 0.25f);
				yield return 0;
			}
			UIManager._instance.CallIncrementCoint ();
			UIManager._instance.coincollect.Play();
			gameObject.SetActive (false);
		}

		private void OnMouseDown()
		{
			LevelSoundManager._instance.coin_click.Play ();
			if(tutorialOn)
			{
				tutorialOn = false;
				UIManager._instance.tutorialPanelBg.gameObject.SetActive (false);
				UIManager._instance.tutorialPanelCanvas.gameObject.SetActive (true);
				UIManager._instance.tutorialPanelCanvas.OpenPopup ("YOUR EARNINGS HELP TO BUY UPGRADES.",true,false , 5 , 1);
			}

			if(US_Manager._instance != null)
			{
				US_Manager._instance.clickedCoke = false;
				US_Manager._instance.clickedHotDog = false;
				US_Manager._instance.clickedTikki = false;
				US_Manager._instance.clickedRedSauce = false;
				US_Manager._instance.clickedYellowSauce = false;
			}
			else if(China_Manager._instance != null)
			{
				China_Manager._instance.AllClickedBoolsReset();
			}

			UIManager._instance.totalCoins+=UIManager._instance.Bonus_coin;
			UIManager._instance.totalCoins+=myAmount;

			StartCoroutine (MoveCoins());
			CustomerHandler._instance.availablePositions.Add (positionTaken);

			if(CustomerHandler._instance.timerStopped ||  CustomerHandler._instance.gameTimer <= 0)
			{
				if(CustomerHandler._instance.availablePositions.Count == 5)
				{
					UIManager._instance.OnGameOver ();
				}
			}
		}

		public void CoinsStolen()
		{
			CustomerHandler._instance.availablePositions.Add (positionTaken);
		
			if(CustomerHandler._instance.timerStopped ||  CustomerHandler._instance.gameTimer <= 0)
			{
				if(CustomerHandler._instance.availablePositions.Count == 5)
				{
					UIManager._instance.OnGameOver ();
				}
			}
			gameObject.SetActive (false);
		}
	}
}