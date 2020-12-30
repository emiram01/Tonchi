using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausedUI = null;
    public static bool paused = false;

    public void Resume()
    {
        pausedUI.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
        paused = false;
    }

    public void Quit()
    {
        Application.Quit();
    }

    void Pause()
    {
        pausedUI.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
        paused = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(paused)
            {
                Resume();
            } else {
                Pause();
            }
        }
    }
}
