using UnityEngine;
using System.Collections;

public class StopGame : MonoBehaviour {

    public static bool pause = false;
    private bool isPaused;

    //static StopGame ()
    //{
    //    pause = false;
    //}

    void OnEnable()
    {
        isPaused = pause;
    }

	void Update () {
        if (!pause)
        {
            if (isPaused)
            {
                isPaused = false;
                OnPauseExit();
            }
        }
        else
        {
            if (!isPaused)
            {
                isPaused = true;
                OnPauseEnter();
            }
        }
	}

    void OnPauseEnter ()
    {
        Time.timeScale = 0;
    }

    void OnPauseExit ()
    {
        Time.timeScale = 1;
    }
}
