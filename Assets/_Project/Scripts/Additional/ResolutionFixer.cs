using UnityEngine;

namespace _Project.Scripts.Additional
{
	public class ResolutionFixer : MonoBehaviour 
	{
		[SerializeField] private float myScaleX;
		[SerializeField] private float myScaleY;
		[SerializeField] private GameObject left;
		[SerializeField] private GameObject right;
		[SerializeField] private GameObject up;
		[SerializeField] private GameObject down;

		private void Awake() 
		{
			Vector3 leftPos = Camera.main.WorldToViewportPoint (left.transform.position);
			Vector3 rightPos = Camera.main.WorldToViewportPoint (right.transform.position);
			Vector3 upPos = Camera.main.WorldToViewportPoint (up.transform.position);
			Vector3 downPos = Camera.main.WorldToViewportPoint (down.transform.position);
			float widthOfBg =	-(rightPos.x - leftPos.x);
			float heightOfBg = upPos.y - downPos.y;
			float newScaleX = myScaleX/widthOfBg;
			float newScaleY = myScaleY/heightOfBg;
			transform.localScale = new Vector3(newScaleX , newScaleY,1);
		}
	}
}
