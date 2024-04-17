using UnityEngine;
using System.Collections;

public class InsuffcintCoin : MonoBehaviour {
	
	void OnEnable()
	{
		transform.SetAsLastSibling ();
	}
	public void Gold()
	{
		GameObject specialPanel = ( GameObject )Instantiate(Resources.Load ("GoldPanel"));
		specialPanel.transform.SetParent(transform.parent,false);
		specialPanel.transform.localScale = Vector3.one;
		specialPanel.transform.localPosition = Vector3.zero;
		if(MenuManager._instance != null)
			MenuManager._instance.EnableFadePanel ();
		else
			UIManager._instance.EnableFadePanel ();
		Destroy (gameObject);

		Destroy (MenuManager._instance.lastPanel);
	    
	}

	public void Close()
	{
		MenuManager._instance.lastPanel = null;
		MenuManager._instance.lastPanelName = "";
		Destroy (gameObject);
	}
}
