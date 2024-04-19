using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Game
{
	public class ObjectClick : MonoBehaviour 
	{
		[FormerlySerializedAs("onClick")] public List<EventDelegate> clickMade = new();

		private void OnMouseDown()
		{
			EventDelegate.Execute(clickMade);
		}
	}
}
