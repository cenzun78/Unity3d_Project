using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    private Transform player;
    private Vector3 gap;
    private float speed = 2f;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gap = transform.position - player.position;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + gap, speed * Time.deltaTime);
    }
}
