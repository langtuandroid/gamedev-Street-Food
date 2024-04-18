using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Game
{
	public class OnClickObject : MonoBehaviour 
	{
		public List<EventDelegate> onClick = new();

		private void OnMouseDown()
		{
			EventDelegate.Execute(onClick);
		}
	}
}
