using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Achivments
{
	public class AchievementChild : MonoBehaviour 
	{
		public string myPlayerPrefVariableAchReached;
		public string myPlayerPrefCountVariable;
		public int myMaxValue;
		public GameObject myTick;
		public GameObject myClaimButton;
		public Text myAch;
		public string myClaimPlayerPref;
		public static int check_claim ;

		private void Start()
		{
			check_claim = PlayerPrefs.GetInt("claimvalue");
		}

		private void OnEnable()
		{
			int myCount = PlayerPrefs.GetInt (myPlayerPrefCountVariable);
			if(myCount > myMaxValue)
				myAch.text =  myMaxValue+ "/" + myMaxValue;
			else
				myAch.text =  myCount+ "/" + myMaxValue;
			if (PlayerPrefs.GetInt (myPlayerPrefVariableAchReached) == 1 && PlayerPrefs.HasKey (myClaimPlayerPref)) 
			{
				myTick.SetActive (true);
			} 
			else if (PlayerPrefs.GetInt (myPlayerPrefVariableAchReached) == 1 && !PlayerPrefs.HasKey (myClaimPlayerPref)) 
			{
				myClaimButton.SetActive (true);
			}
		}
	}
}
