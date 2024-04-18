﻿using UnityEngine;

namespace _Project.Scripts.Additional
{
	public class RotateSelection : MonoBehaviour {
		private void Update() 
		{
			transform.localEulerAngles = new Vector3(0,0,transform.localEulerAngles.z+Time.deltaTime*40f);
		}
	}
}
