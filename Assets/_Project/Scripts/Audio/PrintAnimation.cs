using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Project.Scripts.Audio
{
	public class PrintAnimation : MonoBehaviour 
	{
		private string _message;
		[FormerlySerializedAs("letterPause")] [SerializeField] private float _delayPause = 0.03f;
		[FormerlySerializedAs("myText")] [SerializeField] private Text _text;
		private void Start () {

			_message = _text.text;
			_text.text = "";
			StartCoroutine (Print());
		}

		private IEnumerator Print () {

			foreach (char letter in _message.ToCharArray()) {
				_text.text += letter;
				yield return 0;
				yield return new WaitForSeconds (_delayPause);
			}      

		}
	}
}