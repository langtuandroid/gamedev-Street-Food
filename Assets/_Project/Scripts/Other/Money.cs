using System;
using System.Collections;
using _Project.Scripts.Entities.Customers;
using _Project.Scripts.Managers;
using _Project.Scripts.UI_Scripts;
using _Project.Scripts.UI.Tutorial;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Scripts.Other
{
	public class Money : MonoBehaviour 
	{
		public static int _visible;
		
		[Inject] private WisitorHandler _customerHandler;
		[Inject] private SoundsAll _levelSoundManager;
		[Inject] private USController _usManager;
		[Inject] private ChinaController _chinaManager;
		[Inject] private UIManager _uiManager;
		
		[FormerlySerializedAs("perfectText")] public GameObject _textObject;
		[FormerlySerializedAs("addValue")] public TextMesh _textMashAdd;
		[FormerlySerializedAs("coinCollected")] public ParticleSystem _collectedParticle;
		[FormerlySerializedAs("highlight")] public ParticleSystem _highLightParticle;
		[FormerlySerializedAs("Bonus_value")] public TextMesh _bonusText ;
		
		private Vector3 CoinsMovePos;
		public int posTaken { get; set; }
		public int Amount { get; set; }
		public bool IsTutorialOn { get; set; }
		public int Bonus { get; set; }
		private void Start () 
		{
			CoinsMovePos = transform.position;
		}

		private void OnEnable()
		{
			if (_usManager.IsCoinTutorial)
			{
				_usManager.IsCoinTutorial = false;
				IsTutorialOn = true;
				_uiManager.tutorialPanelBg.positionPickCoins = transform.parent.localPosition + Vector3.up;
				_uiManager.tutorialPanelBg.gameObject.SetActive (true);
				_uiManager.tutorialPanelBg.OpenPopup ("COLLECT THE COINS.",false,false , 4);
			}
			
			_visible++;
			if (_customerHandler._timeOnGame <= 15 ) {

				{
					_highLightParticle.Play();
				}
			}

			if (CoinsMovePos != Vector3.zero)
			{
				transform.position = CoinsMovePos;
			}
		
			transform.localScale = new Vector3(1,1,1);
			StartCoroutine (ScaleRoutine());
			_textMashAdd.gameObject.SetActive (true);
			_textMashAdd.text = "+"+Amount;
			if (Bonus > 0) {
	
				_bonusText.gameObject.SetActive (true);
				_bonusText.text = "Bonus +" + Bonus;
				Invoke(nameof(DeactivateObject),1.3f);
			} 
			else 
			{
				_bonusText.gameObject.SetActive (false);
			}
		}

		private void DeactivateObject()
		{
			_bonusText.gameObject.SetActive (false);
		}

		private void OnDisable()
		{
			_highLightParticle.Stop ();
			_visible--;
			StopCoroutine (ScaleRoutine());
		}

		private IEnumerator ScaleRoutine()
		{
			while(transform.localScale.x < 3)
			{
				transform.localScale = new Vector3(transform.localScale.x+0.12f,transform.localScale.y+0.12f,1);
				yield return 0;
			}
		}

		private IEnumerator MoveCoinRoutine()
		{
			Vector3 finalPos = _uiManager.coinsText.transform.position;
			finalPos = new Vector3(finalPos.x , finalPos.y,CoinsMovePos.z);
			transform.position = CoinsMovePos;
			while(Vector3.Distance(transform.position, finalPos) > 1f)
			{
				transform.position = Vector3.MoveTowards(transform.position, finalPos, 0.25f);
				yield return 0;
			}
			
			_uiManager.CallIncrementCoint ();
			_uiManager.coincollect.Play();
			gameObject.SetActive (false);
		}

		private void OnMouseDown()
		{
			_levelSoundManager.coinClickSound.Play ();
			if(IsTutorialOn)
			{
				IsTutorialOn = false;
				_uiManager.tutorialPanelBg.gameObject.SetActive (false);
				_uiManager.tutorialPanelCanvas.gameObject.SetActive (true);
				_uiManager.tutorialPanelCanvas.OpenPopup ("YOUR EARNINGS HELP TO BUY UPGRADES.",true,false , 5 , 1);
			}

			if(_usManager != null)
			{
				_usManager.IsClickedCoke = false;
				_usManager.IsClickedHotDog = false;
				_usManager.IsClickedTikki = false;
				_usManager.IsClickedRedSauce = false;
				_usManager.IsClickedYellowSauce = false;
			}
			else if(_chinaManager != null)
			{
				_chinaManager.ResetBowlsCliked();
			}

			_uiManager.totalCoins+=_uiManager.Bonus_coin;
			_uiManager.totalCoins+=Amount;

			StartCoroutine (MoveCoinRoutine());
			_customerHandler._availablePositions.Add (posTaken);

			if(_customerHandler.timerStopped ||  _customerHandler._timeOnGame <= 0)
			{
				if(_customerHandler._availablePositions.Count == 5)
				{
					_uiManager.OnGameOver ();
				}
			}
		}

		public void StolenMoney()
		{
			_customerHandler._availablePositions.Add (posTaken);
		
			if(_customerHandler.timerStopped || _customerHandler._timeOnGame <= 0)
			{
				if(_customerHandler._availablePositions.Count == 5)
				{
					_uiManager.OnGameOver ();
				}
			}
			gameObject.SetActive (false);
		}
	}
}
