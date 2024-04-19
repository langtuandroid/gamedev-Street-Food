using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Additional
{
	public class ResolutionFixer : MonoBehaviour 
	{
		private float _xScale = 1.330215f;
		private float _yScale = 1.330215f;
		[FormerlySerializedAs("left")] [SerializeField] private GameObject _leftSide;
		[FormerlySerializedAs("right")] [SerializeField] private GameObject _rightSide;
		[FormerlySerializedAs("up")] [SerializeField] private GameObject _topSide;
		[FormerlySerializedAs("down")] [SerializeField] private GameObject _downSide;

		private void Awake() 
		{
			Vector3 leftPos = Camera.main.WorldToViewportPoint (_leftSide.transform.position);
			Vector3 rightPos = Camera.main.WorldToViewportPoint (_rightSide.transform.position);
			Vector3 upPos = Camera.main.WorldToViewportPoint (_topSide.transform.position);
			Vector3 downPos = Camera.main.WorldToViewportPoint (_downSide.transform.position);
			float widthOfBg =	-(rightPos.x - leftPos.x);
			float heightOfBg = upPos.y - downPos.y;
			float newScaleX = _xScale/widthOfBg;
			float newScaleY = _yScale/heightOfBg;
			transform.localScale = new Vector3(newScaleX , newScaleY,1);
		}
	}
}
