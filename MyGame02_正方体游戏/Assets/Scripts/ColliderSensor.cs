using UnityEngine;
using System.Collections;

public class ColliderSensor : MonoBehaviour {

    private GameObject player;
    private GameOver gameOver;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameOver = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameOver>();
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject == player)
        {
            gameOver.IsOver = true;
        }
    }
}
