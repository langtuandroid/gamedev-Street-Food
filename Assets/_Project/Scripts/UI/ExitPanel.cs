using UnityEngine;

namespace _Project.Scripts.UI
{
	public class ExitPanel : MonoBehaviour 
	{
		public void EXIT()
		{
			Application.Quit ();
		}
		public void Not_Exit()
		{
			Destroy (gameObject);
		}
	}
}
