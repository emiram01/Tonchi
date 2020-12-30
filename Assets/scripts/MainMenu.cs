using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   
    [SerializeField] private Animator animator = null;
    [SerializeField] private AudioSource menuMusic = null;
    private IntroLoop clip;

    void Awake()
    {
        clip = new IntroLoop(menuMusic, 0f, 14.328f, 68.59f);
        clip.start();
    }

    void Update()
    {
        clip.checkTime();
    }

    void PlayGame()
    {
        animator.SetTrigger("Start");
        Invoke("Play", 1f);
    }

    void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
