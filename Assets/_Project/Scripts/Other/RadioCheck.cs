using _Project.Scripts.Additional;
using _Project.Scripts.UI_Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Scripts.Other
{
	public class RadioCheck : MonoBehaviour 
	{
		[Inject] private UIManager _uiManager;   
		 
		[FormerlySerializedAs("radio_bttn")] [SerializeField] private GameObject _radioButton ;
		[FormerlySerializedAs("cup_bttn")] [SerializeField] private GameObject _cupButton ;
		[FormerlySerializedAs("radio_text")] [SerializeField] private GameObject _radioText ;
		[FormerlySerializedAs("cupcake_text")] [SerializeField] private GameObject _cupCakeText ;

		private void OnEnable()
		{
			if (MenuManager.cupcakeNo <= 0) {
				_cupButton.SetActive (true);
				_cupCakeText.SetActive(false);
			} else {
				_cupCakeText.SetActive(true);
				_cupButton.SetActive (false);
			}
			if (!PlayerPrefs.HasKey ("Radio")) {
				_radioButton.SetActive (true);
				_radioText.SetActive(false);
			} else {
				_radioText.SetActive(true);
				_radioButton.SetActive (false);
			}
		}
		public void Buy()
		{
			if (MenuManager.golds >= 20)
			{
				MenuManager.golds -= 20;
				_uiManager.goldText.text = MenuManager.golds.ToString ();
				PlayerPrefs.SetString ("Golds", Encryption.Encrypt (MenuManager.golds.ToString ()));
				PlayerPrefs.SetInt ("Radio", 1);
			} 
			else
			{
				_uiManager.EarnGold();
			}
			gameObject.SetActive (false);
		}
		public void BuyCupcake()
		{
			if (MenuManager.totalscore >= 250) 
			{
				MenuManager.totalscore -= 250;
				PlayerPrefs.SetString ("TotalScore", Encryption.Encrypt (MenuManager.totalscore.ToString ()));

				MenuManager.cupcakeNo = 3;
				PlayerPrefs.SetString ("Cupcake", Encryption.Encrypt (MenuManager.cupcakeNo.ToString ()));
			} 
			else 
			{
				_uiManager.EarnCoin() ;
			}
			gameObject.SetActive (false);
		}
	}
}
