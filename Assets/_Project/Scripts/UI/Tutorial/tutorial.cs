using _Project.Scripts.UI_Scripts;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI.Tutorial
{
	public class tutorial : MonoBehaviour 
	{
		[Inject] private MenuManager _menuManager;  
		public GameObject tutorialpanel;
		public GameObject image1;
		public GameObject image2 ;
		public GameObject tutorial_hand;
		public GameObject price_text;
		public GameObject select_bttn_text ;
	
	
		public void Tutorial_panel()
		{
			PlayerPrefs.SetInt ("Stop", 2);
			_menuManager.EnableFadePanel ();
			tutorialpanel.SetActive (true);
			tutorial_hand.GetComponent<Animator> ().enabled = true;
			Invoke ("Ontutorialstart",1.3f);
			Invoke ("ONtext",2.5f);
		}

		private void Ontutorialstart()
		{
			image1.SetActive (false);
			image2.SetActive (true);
		}

		private void ONtext()
		{
			price_text.SetActive (false);
			select_bttn_text.SetActive (true);
			Invoke ("Ontutorialend",0.6f);
			Invoke ("ONtextend",1.9f);
		}

		private void Ontutorialend()
		{
			image1.SetActive (true);
			image2.SetActive (false);
		}

		private void ONtextend()
		{
			price_text.SetActive (true);
			select_bttn_text.SetActive (false);
			Invoke ("Ontutorialstart",1.3f);
			Invoke ("ONtext",2.5f);
		}
	}
}
