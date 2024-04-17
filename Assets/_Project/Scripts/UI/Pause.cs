using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour 
{
	
	public void MenuPanel()
	{
		GameObject upgradePanel = (GameObject)Instantiate (Resources.Load ("EnvPanel"));
		upgradePanel.transform.SetParent (transform, false);
		upgradePanel.transform.localScale = Vector3.one;
		upgradePanel.transform.localPosition = Vector3.zero;
	}
	public void Restart()
	{
		Time.timeScale = 1;
		Application.LoadLevel(Application.loadedLevel);
	}

	public void Resume()
	{
		Time.timeScale = 1;
		Invoke ("ForDestroy", 0.1f);
	}
	public void ForDestroy()
	{
		Destroy (gameObject);
	}

}
