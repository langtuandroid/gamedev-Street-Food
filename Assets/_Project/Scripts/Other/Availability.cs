using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Other
{
	public class Availability : MonoBehaviour 
	{
		[FormerlySerializedAs("available")] public bool isAvailable;
		[FormerlySerializedAs("myPositionInArray")] public int _arrayPos;
	}
}
