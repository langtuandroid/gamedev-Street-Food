﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
	public class PopupPanel2 : MonoBehaviour {

		public TMP_Text popupText;
		public Text popupText2 ;
		public Text popupText3 ;
		public Text popupText4 ;
		public Text popupText5 ;
		public GameObject yesNoContainer;
		public GameObject crossButton;
		public TweenScale myScale;
	
		public void Cross()
		{
			gameObject.SetActive (false);
		}

		private void OnEnable()
		{
			myScale.ResetToBeginning ();
			myScale.PlayForward ();
			transform.SetAsLastSibling ();
		}
	
		public void EnablePopup(string messagePopup ,string messagePopup2,string messagePopup3,string messagePopup4,string messagePopup5, bool yesNo)
		{
			popupText.text = messagePopup;
			popupText2.text = messagePopup2;
			popupText3.text = messagePopup3;
			popupText4.text = messagePopup4;
			popupText5.text = messagePopup5;

			if(yesNo)
			{
				yesNoContainer.SetActive (true);
				crossButton.SetActive (false);
			}
			else
			{
				yesNoContainer.SetActive (false);
				crossButton.SetActive (true);
			}
		}

	}
}
