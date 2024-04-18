using UnityEngine;

namespace _Project.Scripts.Managers
{
	public class LevelSoundManager : MonoBehaviour {

		public static LevelSoundManager _instance ;
		public AudioSource bell;
		public AudioSource whistle ;
		public AudioSource bttn_click ;
		public AudioSource bottle_click ;
		public AudioSource customerEat ;
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
	}
}
