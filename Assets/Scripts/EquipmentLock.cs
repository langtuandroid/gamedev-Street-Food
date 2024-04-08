//using UnityEngine;
//using System.Collections;
//
//public class EquipmentLock : MonoBehaviour {
//
//
//	public GameObject US_1;
//	public GameObject US_2 ;
//	public GameObject US_3 ;
//
//	public GameObject China_1 ;
//	public GameObject China_2;
//	public GameObject China_3 ;
//
//	public GameObject Italy_1 ;
//	public GameObject Italy_2 ;
//	public GameObject Italy_3 ;
//
//	public GameObject Aus_1 ;
//    public GameObject Aus_2 ;
//	public GameObject Aus_3 ;
//	// Use this for initialization
//	void Start () {
//	
//	}
//	void OnEnable()
//	{
//		if(	PlayerPrefs.HasKey ("AusOpen"))
//		{
//		//	Aus_1.SetActive(true);
//		//	Aus_2.SetActive(true);
//		//	Aus_3.SetActive(true);
//
//			Italy_1.SetActive(false);
//			Italy_2.SetActive(false);
//			Italy_3.SetActive(false);
//			China_1.SetActive(false);
//			China_2.SetActive(false);
//			China_3.SetActive(false);
//
//		}
//		if(	PlayerPrefs.HasKey ("ItalyOpen"))
//		{
//			Italy_1.SetActive(true);
//			Italy_2.SetActive(true);
//			Italy_3.SetActive(true);
//
//		//	Aus_1.SetActive(false);
//		//	Aus_2.SetActive(false);
//		//	Aus_3.SetActive(false);
//
//			China_1.SetActive(false);
//			China_2.SetActive(false);
//			China_3.SetActive(false);
//		}
//
//		if(	PlayerPrefs.HasKey ("ChinaOpen"))
//		{
//			China_1.SetActive(true);
//			China_2.SetActive(true);
//			China_3.SetActive(true);
//
//			Italy_1.SetActive(false);
//			Italy_2.SetActive(false);
//			Italy_3.SetActive(false);
//			
//		//	Aus_1.SetActive(false);
//		//	Aus_2.SetActive(false);
//		//	Aus_3.SetActive(false);
//		}
//
//	}
//	// Update is called once per frame
//	void Update () {
//
//
//
//
//}
//}