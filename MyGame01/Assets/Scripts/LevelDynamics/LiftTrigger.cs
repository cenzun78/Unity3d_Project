using UnityEngine;
using System.Collections;

public class LiftTrigger : MonoBehaviour {
	public float timeDoorsClose = 2f;
	public float timeToLiftStart = 3f;
	public float timeToEndLevel = 6f;
	public float liftSpeed = 3f;

	private GameObject player;
	private Animator playerAnim;
	private HashIDs hash;
	private CameraMovement cameraMovement;
	private SceneFadeInOut sceneFadeInOut;
	private LiftDoorsTracking liftDoorsTraking;
	private AudioSource audioS;
	private bool playerInlift;
	private float timer;

	void Awake() {
		player = GameObject.FindGameObjectWithTag (Tags.player);
		playerAnim = player.GetComponent<Animator> ();
		hash = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<HashIDs> ();
		cameraMovement = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraMovement> ();
		sceneFadeInOut = GameObject.FindGameObjectWithTag (Tags.fader).GetComponent<SceneFadeInOut> ();
		liftDoorsTraking = GetComponent<LiftDoorsTracking> ();
		audioS = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject == player) {
			playerInlift = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject == player) {
			playerInlift = false;
			timer = 0f;
		}
	}

	void Update() {
		if (playerInlift) {
			LiftActivation ();
		}

		if (timer < timeDoorsClose) {
			liftDoorsTraking.DoorFollowing ();
		} else {
			liftDoorsTraking.CloseDoors ();
		}
	}

	void LiftActivation() {
		timer += Time.deltaTime;
		if (timer >= timeToLiftStart) {
			playerAnim.SetFloat (hash.speedFloat, 0f);
			cameraMovement.enabled = false;
			player.transform.parent = transform;
			
			transform.Translate (Vector3.up * liftSpeed * Time.deltaTime);
			if (!audioS.isPlaying) {
				audioS.Play ();
			}
			
			if (timer >= timeToEndLevel) {
				sceneFadeInOut.EndScene ();
			}
		}
	}
}
