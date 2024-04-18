using UnityEngine;

namespace _Project.Scripts.UI
{
	public class DecativeUpgradepopup : MonoBehaviour {

		public GameObject panel ;
		public void Close()
		{
			panel.SetActive (false);
		}
	}
}
