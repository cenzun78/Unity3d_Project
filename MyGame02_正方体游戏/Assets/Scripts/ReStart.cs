using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReStart : MonoBehaviour {

    private Button restart;
    private Image gameOverScreen;
    private NewBoxController newBoxController;
    private CameraController cameraController;
    private PlayerMovement playerMovement;
    private Text text_GameOver, text_ReStart, text_Quit;
    private Button button_Stop, button_Quit;
    private Image image_Stop;

    void Awake()
    {
        text_GameOver = GameObject.Find("Text_GameOver").GetComponent<Text>();
        text_ReStart = GameObject.Find("Text_ReStart").GetComponent<Text>();
        text_Quit = GameObject.Find("Text_Quit").GetComponent<Text>();
        gameOverScreen = GameObject.Find("GameOver").GetComponent<Image>();
        newBoxController = GameObject.FindGameObjectWithTag("GameController").GetComponent<NewBoxController>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        button_Stop = GameObject.Find("Button_Stop").GetComponent<Button>();
        image_Stop = GameObject.Find("Button_Stop").GetComponent<Image>();
        button_Quit = GameObject.Find("Button_Quit").GetComponent<Button>();

        restart = gameObject.GetComponent<Button>();
        restart.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        gameOverScreen.enabled = false;
        text_GameOver.enabled = false;
        text_ReStart.enabled = false;
        text_Quit.enabled = false;
        button_Quit.enabled = false;
        button_Stop.enabled = true;
        image_Stop.enabled = true;
        newBoxController.enabled = true;
        playerMovement.enabled = true;
        restart.enabled = false;
    }
}
