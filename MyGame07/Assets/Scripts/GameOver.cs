using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    private Image gameOverScreen;
    private NewBoxController newBoxController;
    private CameraController cameraController;
    private PlayerMovement playerMovement;
    private Text text_GameOver, text_ReStart, text_Quit;
    private Button button_ReStart, button_Stop, button_Quit;
    private Image image_Stop;
    public bool IsOver = false;

	// Use this for initialization
	void Awake () {
        text_GameOver = GameObject.Find("Text_GameOver").GetComponent<Text>();
        text_ReStart = GameObject.Find("Text_ReStart").GetComponent<Text>();
        text_Quit = GameObject.Find("Text_Quit").GetComponent<Text>();
        button_ReStart = GameObject.Find("Button_ReStart").GetComponent<Button>();
        button_Stop = GameObject.Find("Button_Stop").GetComponent<Button>();
        button_Quit = GameObject.Find("Button_Quit").GetComponent<Button>();
        gameOverScreen = GameObject.Find("GameOver").GetComponent<Image>();
        newBoxController = gameObject.GetComponent<NewBoxController>();
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        image_Stop = GameObject.Find("Button_Stop").GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        if (IsOver)
        {
            GameObject[] cube = GameObject.FindGameObjectsWithTag("Cube");
            DestroyCube(cube);
            cube = GameObject.FindGameObjectsWithTag("RedCube");
            DestroyCube(cube);
            cube = GameObject.FindGameObjectsWithTag("BlackCube");
            DestroyCube(cube);
            gameOverScreen.enabled = true;
            text_GameOver.enabled = true;
            text_ReStart.enabled = true;
            text_Quit.enabled = true;
            button_Quit.enabled = true;
            button_ReStart.enabled = true;
            cameraController.transform.position = new Vector3(-1.5f, 9.2f, -3f);
            button_Stop.enabled = false;
            image_Stop.enabled = false;
            newBoxController.enabled = false;
            playerMovement.enabled = false;
            IsOver = false;
        }
	}

    private void DestroyCube(GameObject[] g)
    {
        for (int i = 0; i < g.Length; i++)
        {
            Destroy(g[i]);
        }
    }
}
