using UnityEngine;
using System.Collections;

public class LevelSoundManager : MonoBehaviour {

	public static LevelSoundManager _instance ;
	public AudioSource tikkiburn ;

	public AudioSource bell;
	public AudioSource whistle ;
	public AudioSource radio ;

	public AudioSource noodlesBurn ;
	public AudioSource customerPay_Coin;
	public AudioSource cooking_Noodles ;
	public AudioSource cooking_Tikki ;
	public AudioSource cooking_Burger ;
	public AudioSource cooking_soup ;

	public AudioSource sausage_burn ;
	public AudioSource bttn_click ;
	public AudioSource bottle_click ;
	public AudioSource customerEat ;

	public AudioSource plate_click ;
	public AudioSource bowl_click ;
	public AudioSource successful_level ;
	public AudioSource unsuccessful_level ;
	public AudioSource eat_random ;
	public AudioSource drink ;
	public AudioSource grunt ;
	public AudioSource coinAdd ;
	public AudioSource come_random ;

	public AudioSource drink2 ;
	public AudioSource dustbin;
	public AudioSource Gameoverpanel;

	public AudioSource coin_click ;
	public AudioSource caught;
	void Awake()
	{
		_instance = this;
	}
	//public AudioSource 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
