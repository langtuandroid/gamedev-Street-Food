using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Managers
{
	public class SoundsAll : MonoBehaviour {
		
		[FormerlySerializedAs("bell")] public AudioSource bellSound;
		[FormerlySerializedAs("whistle")] public AudioSource whistleSound ;
		[FormerlySerializedAs("bttn_click")] public AudioSource buttnClickSound ;
		[FormerlySerializedAs("bottle_click")] public AudioSource bottleClickSound ;
		[FormerlySerializedAs("customerEat")] public AudioSource customerEatSound ;
		[FormerlySerializedAs("bowl_click")] public AudioSource bowlClickSound ;
		[FormerlySerializedAs("successful_level")] public AudioSource successfulLevelSound ;
		[FormerlySerializedAs("unsuccessful_level")] public AudioSource unsuccessfulLevelSound ;
		[FormerlySerializedAs("eat_random")] public AudioSource eatRandomSound ;
		[FormerlySerializedAs("drink")] public AudioSource drinkSound;
		[FormerlySerializedAs("grunt")] public AudioSource gruntSound;
		[FormerlySerializedAs("coinAdd")] public AudioSource coinAddSound;
		[FormerlySerializedAs("come_random")] public AudioSource comeRandomSound;
		[FormerlySerializedAs("drink2")] public AudioSource drink2Sound;
		[FormerlySerializedAs("dustbin")] public AudioSource dustbinSound;
		[FormerlySerializedAs("Gameoverpanel")] public AudioSource GameoverpanelSound;
		[FormerlySerializedAs("coin_click")] public AudioSource coinClickSound ;
		[FormerlySerializedAs("caught")] public AudioSource caughtSound;
	}
}
