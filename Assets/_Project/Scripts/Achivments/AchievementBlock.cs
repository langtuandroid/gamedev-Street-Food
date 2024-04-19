using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Project.Scripts.Achivments
{
	public class AchievementBlock : MonoBehaviour 
	{
		[FormerlySerializedAs("myPlayerPrefVariableAchReached")] [SerializeField] private string _keyAchivmentReach;
		[FormerlySerializedAs("myPlayerPrefCountVariable")] [SerializeField] private string _keyCount;
		[FormerlySerializedAs("myMaxValue")] [SerializeField] private int _maxValue;
		[FormerlySerializedAs("myTick")] public GameObject _tickPrefab;
		[FormerlySerializedAs("myClaimButton")] public GameObject _claimButton;
		[FormerlySerializedAs("myAch")] [SerializeField] private  Text _achivmentText;
		[FormerlySerializedAs("myClaimPlayerPref")] [SerializeField] private  string _claimKey;
		public static int _claimCheck ;

		private void Start()
		{
			_claimCheck = PlayerPrefs.GetInt("claimvalue");
		}

		private void OnEnable()
		{
			int myCount = PlayerPrefs.GetInt (_keyCount);
			if(myCount > _maxValue)
				_achivmentText.text =  _maxValue+ "/" + _maxValue;
			else
				_achivmentText.text =  myCount+ "/" + _maxValue;
			if (PlayerPrefs.GetInt (_keyAchivmentReach) == 1 && PlayerPrefs.HasKey (_claimKey)) 
			{
				_tickPrefab.SetActive (true);
			} 
			else if (PlayerPrefs.GetInt (_keyAchivmentReach) == 1 && !PlayerPrefs.HasKey (_claimKey)) 
			{
				_claimButton.SetActive (true);
			}
		}
	}
}
