using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public float patrolSpeed = 2f;					// 巡逻时的速度
	public float chaseSpeed = 5f;					// 跟踪时的速度
	public float patrolWaitTime = 1f;				// 巡逻等待时间
	public float chaseWaitTime = 5f;				// 从跟踪转换巡逻时中间停留时间
	public Transform[] patrolWayPoints;				// 巡逻的路径点坐标

	private EnemySight enemySight;
	private NavMeshAgent nav;
	private Transform player;
	private PlayerHealth playerHealth;
	private LastPlayerSighting lastPlayerSighting;
	private float chaseTimer;
	private float patrolTimer;
	private int wayPointIndex;

	void Awake() {
		enemySight = GetComponent<EnemySight> ();
		nav = GetComponent<NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag (Tags.player).transform;
		playerHealth = player.GetComponent<PlayerHealth> ();
		lastPlayerSighting = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<LastPlayerSighting> ();
	}

	void Update() {
		if (enemySight.playerInSight && playerHealth.health > 0f) {
			print ("shooting");
			Shooting ();
		} else if (enemySight.personalLastSighting != lastPlayerSighting.resetposition && playerHealth.health > 0f) {
			print ("chasing");
			Chasing ();
		} else {
			Patrolling ();
		}
	}

	void Shooting() {
		nav.Stop ();
	}

	void Chasing() {
		Vector3 sightingDeltaPos = enemySight.personalLastSighting - transform.position;
		if (sightingDeltaPos.sqrMagnitude > 4f) {
			nav.destination = enemySight.personalLastSighting;
		}

		nav.speed = chaseSpeed;

		if (nav.remainingDistance < nav.stoppingDistance) {
			chaseTimer += Time.deltaTime;

			if (chaseTimer > chaseWaitTime) {
				lastPlayerSighting.position = lastPlayerSighting.resetposition;
				enemySight.personalLastSighting = lastPlayerSighting.resetposition;
				chaseSpeed = 0f;
			}
		} else {
			chaseTimer = 0f;
		}
	}

	void Patrolling() {
		nav.speed = patrolSpeed;

		if (nav.destination == lastPlayerSighting.resetposition || nav.remainingDistance < nav.stoppingDistance) {
			patrolTimer += Time.deltaTime;

			if (patrolTimer > patrolWaitTime) {
				if (wayPointIndex == patrolWayPoints.Length - 1) {
					wayPointIndex = 0;
				} else {
					wayPointIndex++;
				}
				patrolTimer = 0f;
			}
		} else {
			patrolTimer = 0f;
		}

		nav.destination = patrolWayPoints [wayPointIndex].position;
	}

}
