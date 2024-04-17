using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
	public enum Orders{NONE = 0,HOTDOG = 1,HOTDOG_YELLOW = 2,HOTDOG_RED = 3,COKE =  4 , NOODLES = 5 ,SOUP = 6 , VEG_PIZZA = 7 , NON_VEG_PIZZA = 8 , ITALY_COKE = 9 , BURGER = 10 ,AUS_COKE = 11, FRIES = 12, BURGER_TOMATO = 13, BURGER_ONION = 14, BURGER_CABBAGE = 15, BURGER_TOMATO_ONION = 16, BURGER_TOMATO_CABBAGE = 17, BURGER_ONION_CABBAGE = 18 , BURGER_COMPLETE = 19 };
	
	public List<int> targetScore = new List<int>();
	public List <int> expertScore = new List<int>();


	public static int levelNo = 11;

	public static LevelManager _instance;
	// Use this for initialization
	void Awake () {
		if(_instance == null)
		{
			_instance = this;
			DontDestroyOnLoad (this.gameObject);
		}
		else
		{
			Destroy (gameObject);
		}

	}
	void Start()
	{
		Application.targetFrameRate = 120;
	}
	// Update is called once per frame
	void Update () {
	

	}
}
