using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AchievementChild : MonoBehaviour {
	public string myPlayerPrefVariableAchReached;
	public string myPlayerPrefCountVariable;
	public int myMaxValue;
	public GameObject myTick;
	public GameObject myClaimButton;
	public Text myAch;
	public string myClaimPlayerPref;
	public static int check_claim ;
	void Start()
	{
		check_claim = PlayerPrefs.GetInt("claimvalue");
	}
	void OnEnable()
	{
		int myCount = PlayerPrefs.GetInt (myPlayerPrefCountVariable);
		//Debug.Log ("myCount = "+myCount);
		if(myCount > myMaxValue)
			myAch.text =  myMaxValue+ "/" + myMaxValue;
		else
			myAch.text =  myCount+ "/" + myMaxValue;
		if (PlayerPrefs.GetInt (myPlayerPrefVariableAchReached) == 1 && PlayerPrefs.HasKey (myClaimPlayerPref)) {
			myTick.SetActive (true);
		} else if (PlayerPrefs.GetInt (myPlayerPrefVariableAchReached) == 1 && !PlayerPrefs.HasKey (myClaimPlayerPref)) {
			myClaimButton.SetActive (true);


//			if(myClaimButton.activeInHierarchy)
//			{
//				MenuManager._instance.achievement_bttn.GetComponent<Animator>().enabled = true;
//			}
//			else
//			{
//				MenuManager._instance.achievement_bttn.GetComponent<Animator>().enabled = false;
//			}
		}
	}

	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
	
	}
}
