using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class LevelAnimation : MonoBehaviour 
{
	[FormerlySerializedAs("level1")] public GameObject[] levelButtons ;

	readonly float timervalue = 0.1f ;

	private void OnEnable()
	{
		StartCoroutine(nameof(level_animation));
	}

	private IEnumerator level_animation()
	{
		foreach (var button in levelButtons)
		{
			button.GetComponent<Animator>().enabled = true ;
			yield return new WaitForSeconds(timervalue);
		}
	}

	private void OnDisable()
	{
		foreach (var button in levelButtons)
		{
			button.GetComponent<Animator>().enabled = false ;
		}
	}
}
