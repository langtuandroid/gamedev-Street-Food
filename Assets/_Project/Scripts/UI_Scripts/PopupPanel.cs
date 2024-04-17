using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopupPanel : MonoBehaviour {
	public Text popupText;

	public Button popupYes;

	public Button popupNo;

	public GameObject yesNoContainer;

	public GameObject crossButton;

	public TweenScale myScale;
	// Use this for initialization
	void Start () {
	
	}
	
	public void Cross()
	{
		MenuManager._instance.EnableFadePanel ();
		gameObject.SetActive (false);

	}

	void OnEnable()
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
