using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public float health = 100f;
	public float resetAfterDeathTime = 5f;				// 死亡后等待时间
	public AudioClip deathClip;

	private Animator anim;
	private PlayerMovement playerMovement;
	private HashIDs hash;
	private SceneFadeInOut sceneFadeInOut;
	private LastPlayerSighting lastPlayerSighting;
	private AudioSource audioS;
	private float timer;
	private bool playerDead;

	void Awake() {
		anim = GetComponent<Animator> ();
		playerMovement = GetComponent<PlayerMovement> ();
		hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs> ();
		sceneFadeInOut = GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<SceneFadeInOut> ();
		lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting> ();
		audioS = GetComponent<AudioSource> ();
	}

	void Update() {
		if (health <= 0f) {
			if (!playerDead) {
				PlayerDying ();
			} else {
				PlayerDead ();
				LevelReset ();
			}
		}
	}

	void PlayerDying() {
		playerDead = true;
		anim.SetBool (hash.deadBool, true);
		AudioSource.PlayClipAtPoint (deathClip, transform.position);

	}

	void PlayerDead() {
		if (anim.GetCurrentAnimatorStateInfo(0).fullPathHash == hash.dyingState) {
			anim.SetBool (hash.deadBool, false);
		}

		anim.SetFloat (hash.speedFloat, 0f);
		playerMovement.enabled = false;
		lastPlayerSighting.position = lastPlayerSighting.resetposition;
		audioS.Stop ();
	}

	void LevelReset() {
		timer += Time.deltaTime;

		if (timer >= resetAfterDeathTime) {
			sceneFadeInOut.EndScene ();
		}
	}

	public void TakeDamage(float amount) {
		health -= amount;
	}
}
