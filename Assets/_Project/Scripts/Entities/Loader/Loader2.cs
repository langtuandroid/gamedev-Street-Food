using _Project.Scripts.UI_Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Entities.Loader
{
	public class Loader2 : MonoBehaviour 
	{
		[Inject] private MenuManager _menuManager;   
		private int _rand;
		private bool _isStarted;
		private int _a = 0;
		private int _b = 0;
		[FormerlySerializedAs("loader_text")] [SerializeField] private Text _loaderText ;
		[FormerlySerializedAs("loader")] [SerializeField] private Slider loaderSlider;
		
		private void OnEnable() {
		
			_isStarted = true;
			loaderSlider.value = 0.01f;
			_rand = Random.Range (0, 8);
			if (_rand == 0) {
				_loaderText.text = "Use handcuffs to catch thieves";
			}
			else if (_rand == 1) {
				_loaderText.text = "Use the bell to call more customers.";
			}
			else if (_rand == 2) {
				_loaderText.text = "The customer waits longer when radio is switched on";
			}
			else if (_rand == 3) {
				_loaderText.text = "The whistle blows when the customer leaves without paying.";
			}
			else if (_rand == 4) {
				_loaderText.text = "Buy upgrades to increase the equipments capacity.";
			}
			else if (_rand == 5) {
				_loaderText.text = "The cupcake refills the customer waiting bar.";
			}
			else if (_rand == 6) {
				_loaderText.text = "Purchase gold to buy more upgrades.";
			}
			else if (_rand == 7) {
				_loaderText.text = "Better decorated stall fetches higher bonus.";
			}
		}


		private void Update () {

			loaderSlider.value += 0.015f ;
			if (loaderSlider.value >= 0.9f) 
			{
				_menuManager.Achievments();
			}
		}
	}
}
