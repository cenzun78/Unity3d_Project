using UnityEngine;
using System.Collections;

public class CCTVPlayerDetection : MonoBehaviour {

	private GameObject player;
	private LastPlayerSighting lastPlayerSighting;					// 更新玩家位置信息

	void Awake () {
		player = GameObject.FindGameObjectWithTag (Tags.player);
		lastPlayerSighting = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<LastPlayerSighting> ();

	}

	void OnTriggerStay (Collider other) {
		if (other.gameObject == player) {
			Vector3 relPlayerPos = player.transform.position - gameObject.transform.position;
			RaycastHit hit;

			if (Physics.Raycast(transform.position, relPlayerPos, out hit)) {
				if (hit.transform.tag == Tags.player) {
					lastPlayerSighting.position = player.transform.position;
				}
			}
		}
	}
}
