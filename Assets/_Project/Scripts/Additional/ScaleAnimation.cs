using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Additional
{
	public class ScaleAnimation : MonoBehaviour 
	{
		private Transform _parentTransform;
		[FormerlySerializedAs("myMaxScale")] [SerializeField] private float _maxScale;
		[FormerlySerializedAs("myMinScale")] [SerializeField] private float _minScale;

		private void Start () 
		{
			_parentTransform = transform.parent;
			transform.parent = null;
			float myXScale = transform.localScale.x;
			if(myXScale > _maxScale)
			{
				myXScale = _maxScale;
			}
			else if(myXScale < _minScale)
			{
				myXScale = _minScale;
			}
			transform.localScale = new Vector3(myXScale,myXScale,1);
			transform.parent = _parentTransform;
		}

	}
}
