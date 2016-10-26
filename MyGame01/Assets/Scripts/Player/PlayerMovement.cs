using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public AudioClip shoutingClip;					// 喊叫声
	public float turnSmoothing = 15f;				// 人物转向速度
	public float speedDampTime = 0.1f;				// 速度缓冲时间

	private Animator anim;
	private HashIDs hash;
	private Rigidbody rd;
	private AudioSource audioS;

	void Awake () {
		anim = gameObject.GetComponent<Animator> ();
		hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs> ();
		rd = gameObject.GetComponent<Rigidbody> ();
		audioS = gameObject.GetComponent<AudioSource> ();
		anim.SetLayerWeight (1, 1f);
	}

	void FixedUpdate (){
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		bool sneak = Input.GetButton ("Sneak");
		MovementManagement (h, v, sneak);
	}

	void Update () {
		bool shout = Input.GetButtonDown ("Attract");
		anim.SetBool (hash.shoutingBool, shout);
		AudioManagement (shout);
	}

	void MovementManagement (float horizontal, float vertical, bool sneaking){
		anim.SetBool (hash.sneakingBool, sneaking);

		if (horizontal != 0f || vertical != 0f) {
			Rotating (horizontal, vertical);
			anim.SetFloat (hash.speedFloat, 5.5f, speedDampTime, Time.deltaTime);
		} else {
			anim.SetFloat (hash.speedFloat, 0f, speedDampTime, Time.deltaTime);
		}
	}

	void Rotating (float horizontal, float vertical) {
		Vector3 targetDirection = new Vector3 (horizontal, 0f, vertical);
		Quaternion targetRotation = Quaternion.LookRotation (targetDirection, Vector3.up);
		Quaternion newRotation = Quaternion.Lerp (rd.rotation, targetRotation, turnSmoothing * Time.deltaTime);
		rd.MoveRotation (newRotation);
	}

	void AudioManagement (bool shout) {
		if (anim.GetCurrentAnimatorStateInfo(0).fullPathHash == hash.locomotionState) {
			if (!audioS.isPlaying) {
				audioS.Play ();
			}
		} else {
			audioS.Stop ();
		}

		if (shout) {
			AudioSource.PlayClipAtPoint (shoutingClip, transform.position);
		}
	}
}
