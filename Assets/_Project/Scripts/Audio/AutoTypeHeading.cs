using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Audio
{
	public class AutoTypeHeading : MonoBehaviour 
	{
		private string _message;
		[SerializeField] private float letterPause = 0.03f;
		[SerializeField] private Text myText;
		private void Start () {

			_message = myText.text;
			myText.text = "";
			StartCoroutine (TypeText());
		}

		private IEnumerator TypeText () {

			foreach (char letter in _message.ToCharArray()) {
				myText.text += letter;
				yield return 0;
				yield return new WaitForSeconds (letterPause);
			}      

		}
	}
}