using UnityEngine;
using System.Collections;

public class LaserSwitchDeactivation : MonoBehaviour {
	private GameObject player;
	private AudioSource laserAudio;

	public GameObject laser;
	public Material unlockedMat;

	void Awake () {
		player = GameObject.FindGameObjectWithTag (Tags.player);
		laserAudio = gameObject.GetComponent<AudioSource> ();
	}

	void OnTriggerStay (Collider other) {
		if (other.gameObject == player) {
			if (Input.GetButtonDown("Switch")) {
				LaserDeactivation ();
			}
		}
	}

	void LaserDeactivation () {
		laser.SetActive (false);

		Renderer screen = transform.Find ("prop_switchUnit_screen").GetComponent<Renderer> ();
		screen.material = unlockedMat;

		laserAudio.Play ();
	}
}
