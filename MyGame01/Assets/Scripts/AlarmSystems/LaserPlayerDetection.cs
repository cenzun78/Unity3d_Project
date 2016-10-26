using UnityEngine;
using System.Collections;

public class LaserPlayerDetection : MonoBehaviour {

	private GameObject player;
	private LastPlayerSighting lastPlayerSighting;
	private Renderer rd;

	void Awake() {
		player = GameObject.FindGameObjectWithTag (Tags.player);
		lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting> ();
		rd = gameObject.GetComponent<Renderer> ();
	}

	void OnTriggerStay (Collider other) {
		if (rd.enabled && other.gameObject == player) {
			lastPlayerSighting.position = other.transform.position;
		}
	}
}
