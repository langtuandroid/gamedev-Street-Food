using UnityEngine;
using System.Collections;

public class LevelAnimation : MonoBehaviour {


	public GameObject level1 ;
	public GameObject level2 ;
	public GameObject level3 ;
	public GameObject level4 ;
	public GameObject level5 ;
	public GameObject level6 ;
	public GameObject level7 ;
	public GameObject level8 ;
	public GameObject level9 ;
	public GameObject level10 ;
	float timervalue = 0.1f ;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}
	void OnEnable()
	{
		StartCoroutine ("level_animation");
	
	}

	IEnumerator level_animation()
	{
	
		level1.GetComponent<Animator>().enabled = true ;
		yield return new WaitForSeconds(timervalue);
	
		level2.GetComponent<Animator>().enabled = true ;
		yield return new WaitForSeconds(timervalue);
	;
		level3.GetComponent<Animator>().enabled = true ;
		yield return new WaitForSeconds(timervalue);
	
		level4.GetComponent<Animator>().enabled = true ;
		yield return new WaitForSeconds(timervalue);

		level5.GetComponent<Animator>().enabled = true ;
		yield return new WaitForSeconds(timervalue);

		level6.GetComponent<Animator>().enabled = true ;
		yield return new WaitForSeconds(timervalue);
	
		level7.GetComponent<Animator>().enabled = true ;
		yield return new WaitForSeconds(timervalue);

		level8.GetComponent<Animator>().enabled = true ;
		yield return new WaitForSeconds(timervalue);

		level9.GetComponent<Animator>().enabled = true ;
		yield return new WaitForSeconds(timervalue);

		level10.GetComponent<Animator>().enabled = true ;
		yield return new WaitForSeconds(timervalue);


	}
	void OnDisable()
	{
		level1.GetComponent<Animator>().enabled = false ;

		level2.GetComponent<Animator>().enabled =  false ;

		level3.GetComponent<Animator>().enabled =  false ;

		level4.GetComponent<Animator>().enabled =  false;

		level5.GetComponent<Animator>().enabled =  false ;

		level6.GetComponent<Animator>().enabled =  false;

		level7.GetComponent<Animator>().enabled =  false;
	
		level8.GetComponent<Animator>().enabled =  false ;

		level9.GetComponent<Animator>().enabled =  false;

		level10.GetComponent<Animator>().enabled =  false ;

	}
}
