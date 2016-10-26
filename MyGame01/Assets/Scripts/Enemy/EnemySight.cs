﻿using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {
	public float fielOfViewAngle = 110f;
	public bool playerInSight;
	public Vector3 personalLastSighting;

	private NavMeshAgent nav;
	private SphereCollider col;
	private Animator anim;
	private HashIDs hash;
	private LastPlayerSighting lastPlayerSighting;
	private GameObject player;
	private Animator playerAnim;
	private PlayerHealth playerHealth;
	private Vector3 previousSighting;

	void Awake() {
		nav = GetComponent<NavMeshAgent> ();
		col = GetComponent<SphereCollider> ();
		anim = GetComponent<Animator> ();
		hash = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<HashIDs> ();
		lastPlayerSighting = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<LastPlayerSighting> ();
		player = GameObject.FindGameObjectWithTag (Tags.player);
		playerAnim = player.GetComponent<Animator> ();
		playerHealth = player.GetComponent<PlayerHealth> ();

		personalLastSighting = lastPlayerSighting.resetposition;
		previousSighting = lastPlayerSighting.resetposition;
	}

	void Update() {
		if (lastPlayerSighting.position != personalLastSighting) {
			personalLastSighting = lastPlayerSighting.position;
		}

		previousSighting = lastPlayerSighting.position;

		if (playerHealth.health > 0f) {
			anim.SetBool (hash.playerInSightBool, playerInSight);
		} else {
			anim.SetBool (hash.playerInSightBool, false);
		}

	}

	void OnTriggerEnter(Collider other){
		print ("OnTriggerEnter");
		if (other.gameObject == player) {
			print ("player");
			playerInSight = false;

			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle (direction, transform.forward);

			if (angle <= fielOfViewAngle * 0.5f) {
				print ("angle");
				RaycastHit hit;
				if (Physics.Raycast(transform.position + transform.up, direction.normalized , out hit, col.radius)) {
					if (hit.collider.gameObject == player) {
						print ("hit player");
						playerInSight = true;
						lastPlayerSighting.position = player.transform.position;
					}
				}
			}

			int playerLayerZeroStateHash = playerAnim.GetCurrentAnimatorStateInfo (0).fullPathHash;
			int playerLayerOneStateHash = playerAnim.GetCurrentAnimatorStateInfo (1).fullPathHash;

			if (playerLayerZeroStateHash == hash.locomotionState || playerLayerOneStateHash == hash.shoutState) {
				print ("locomotionSate");
				if (CalculatePathLength(player.transform.position) <= col.radius) {
					print ("CalculatePathLength");
					personalLastSighting = player.transform.position;
				}

			}
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject == player) {
			playerInSight = false;
		}
	}

	float CalculatePathLength(Vector3 targetPostion) {
		NavMeshPath path = new NavMeshPath ();

		if (nav.enabled) {
			nav.CalculatePath (targetPostion, path);
		}

		Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];
		allWayPoints [0] = transform.position;
		allWayPoints [path.corners.Length - 1] = targetPostion;

		for (int i = 0; i < path.corners.Length; i++) {
			allWayPoints [i + 1] = path.corners [i];
		}

		float pathLength = 0f;

		for (int i = 0; i < allWayPoints.Length - 1; i++) {
			pathLength += Vector3.Distance (allWayPoints[i], allWayPoints[i+1]);
		}

		return pathLength;
	}
}
