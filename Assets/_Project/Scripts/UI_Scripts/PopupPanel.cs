using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI_Scripts
{
	public class PopupPanel : MonoBehaviour 
	{
		[Inject] private MenuManager _menuManager;  
		public Text popupText;
		public GameObject yesNoContainer;
		public GameObject crossButton;
		public TweenScale myScale;
	
		public void Cross()
		{
			_menuManager.EnableFadePanel ();
			gameObject.SetActive (false);
		}

		private void OnEnable()
		{
			myScale.ResetToBeginning ();
			myScale.PlayForward ();
			transform.SetAsLastSibling ();
		}

		public void EnablePopup(string messagePopup , bool yesNo)
		{
		
			popupText.text = messagePopup.ToUpper ();
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
