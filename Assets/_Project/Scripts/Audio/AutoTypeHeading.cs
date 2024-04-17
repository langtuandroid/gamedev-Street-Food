using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AutoTypeHeading : MonoBehaviour {

	public	float letterPause = 0.03f;
	string message;
	public Text myText;
	
	void Start () {

		message = myText.text;
		myText.text = "";
		StartCoroutine (TypeText());
	}
	
	public IEnumerator TypeText () {

		foreach (char letter in message.ToCharArray()) {
			myText.text += letter;
			yield return 0;
			yield return new WaitForSeconds (letterPause);
		}      

	}
}