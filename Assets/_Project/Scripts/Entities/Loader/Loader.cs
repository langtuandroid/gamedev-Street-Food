using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Project.Scripts.Entities.Loader
{
	public class Loader : MonoBehaviour
	{
		private int _randNum;
		private bool _isA;
		private bool _started = true;
		private AsyncOperation _async;
		
		public static string levelToLoad;
		[FormerlySerializedAs("loader_text")] [SerializeField] private Text _textLoader ;
		[FormerlySerializedAs("loader")] [SerializeField] private Slider _loaderSlider;

		private void Awake()
		{
			DontDestroyOnLoad (gameObject);
		}
		
		private void OnEnable()
		{
			_started = true;
			_loaderSlider.value = 0.01f;
			_randNum = Random.Range (0, 8);
			if (_randNum == 0) {
				_textLoader.text = "Use handcuffs to catch thieves";
			}
			else if (_randNum == 1) {
				_textLoader.text = "Use the bell to call more customers.";
			}
			else if (_randNum == 2) {
				_textLoader.text = "The customer waits longer when radio is switched on";
			}
			else if (_randNum == 3) {
				_textLoader.text = "The whistle blows when the customer leaves without paying.";
			}
			else if (_randNum == 4) {
				_textLoader.text = "Buy upgrades to increase the equipments capacity.";
			}
			else if (_randNum == 5) {
				_textLoader.text = "The cupcake refills the customer waiting bar.";
			}
			else if (_randNum == 6) {
				_textLoader.text = "Purchase gold to buy more upgrades.";
			}
			else if (_randNum == 7) {
				_textLoader.text = "Better decorated stall fetches higher bonus.";
			}
		}

		private void Update () 
		{
			_loaderSlider.value += 0.009f ;
			if (_loaderSlider.value >= 0.9f) {
				_isA= true;
				Application.LoadLevel(levelToLoad);
			}
		}
	}
}
