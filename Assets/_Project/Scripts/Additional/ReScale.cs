using UnityEngine;

namespace _Project.Scripts.Additional
{
	public class ReScale : MonoBehaviour 
	{
		private Transform _myParent;
		[SerializeField] private float myMaxScale;
		[SerializeField] private float myMinScale;

		private void Start () 
		{
			_myParent = transform.parent;
			transform.parent = null;
			float myXScale = transform.localScale.x;
			if(myXScale > myMaxScale)
			{
				myXScale = myMaxScale;
			}
			else if(myXScale < myMinScale)
			{
				myXScale = myMinScale;
			}
			transform.localScale = new Vector3(myXScale,myXScale,1);
			transform.parent = _myParent;
		}

	}
}
