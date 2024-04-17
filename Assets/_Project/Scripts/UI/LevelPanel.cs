using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelPanel : MonoBehaviour {


	public Text totalCoinsText;
	
	public Text totalGoldText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnEnable()
	{
		totalGoldText.text = MenuManager.golds.ToString ();
		totalCoinsText.text = MenuManager.totalscore.ToString ();
		TutorialPanel.popupPanelActive = true;
	}
}
