using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Cartoon FX - (c) 2014 - Jean Moreno
//
// Script for the Demo scene

public class CFX_Demo_GTToggle : MonoBehaviour
{
	public Sprite Normal, Hover;
	public Color NormalColor = new Color32(128,128,128,128), DisabledColor = new Color32(128,128,128,48);
	public bool State = true;
	
	public string Callback;
	public GameObject Receiver;
	
	private Rect CollisionRect;
	private bool Over;
	private Text Label;
	
	//-------------------------------------------------------------
	
	void Awake()
	{
		Label = this.GetComponentInChildren<Text>();
		
		UpdateTexture();
	}
	
	void Update ()
	{
		if(CollisionRect.Contains(Input.mousePosition))
		{
			Over = true;
			if(Input.GetMouseButtonDown(0))
			{
				OnClick();
			}
		}
		else
		{
			Over = false;
			this.GetComponent<Image>().color = NormalColor;
		}
		
		UpdateTexture();
	}
	
	//-------------------------------------------------------------
	
	private void OnClick()
	{
		State = !State;
		
		Receiver.SendMessage(Callback);
	}
	
	private void UpdateTexture()
	{
		Color col = State ? NormalColor : DisabledColor;
		if(Over)
		{
			this.GetComponent<Image>().sprite = Hover;
		}
		else
			this.GetComponent<Image>().sprite = Normal;
		
		this.GetComponent<Image>().color = col;
		
		if(Label != null)
			Label.color = col * 1.75f;
	}
}
