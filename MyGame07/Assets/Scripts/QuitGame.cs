using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour {

    private Button button_Quit;

	void Awake()
    {
        button_Quit = gameObject.GetComponent<Button>();
        button_Quit.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        print("QuitGame");
        Application.Quit();
    }
}
