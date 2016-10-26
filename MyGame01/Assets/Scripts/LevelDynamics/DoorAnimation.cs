using UnityEngine;
using System.Collections;

public class DoorAnimation : MonoBehaviour {

	public bool requireKey;
	public AudioClip doorSwishClip;
	public AudioClip accessDeniedClip;

	private Animator anim;
	private HashIDs hash;
	private GameObject player;
	private PlayerInventory playerInventory;
	private AudioSource audioS;
	private int count;

	void Awake() {
		anim = GetComponent<Animator> ();
		hash = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<HashIDs> ();
		player = GameObject.FindGameObjectWithTag (Tags.player);
		playerInventory = player.GetComponent<PlayerInventory> ();
		audioS = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject == player) {
			if (requireKey) {
				if (playerInventory.hasKey) {
					count++;
				} else {
					audioS.clip = accessDeniedClip;
					audioS.Play ();
				}
			} else {
				count++;
			}
		} else if (other.gameObject.tag == Tags.enemy) {
			if (other is CapsuleCollider) {
				count++;
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject == player || (other.gameObject.tag == Tags.enemy && other is CapsuleCollider)) {
			if (count > 0) {
				count--;
			}
		}
	}

	void Update() {
		anim.SetBool (hash.openBool, count > 0);

		if (anim.IsInTransition(0) && !audioS.isPlaying) {
			audioS.clip = doorSwishClip;
			audioS.Play ();
		}
	}
}
