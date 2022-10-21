using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausing : MonoBehaviour {

    public static bool gameIsPaused = false;

    public GameObject pauseUI;

    public GameObject player;

    public AudioSource bGM;

    public AudioClip click;
    AudioSource audioSource;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!player.GetComponent<PlayerInput>().timerReached) {
            Resume();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.Semicolon))
        {
            if (!player.GetComponent<Player>().died)
                if(player.GetComponent<PlayerInput>().timerReached)
                    if (gameIsPaused)
                    {
                        audioSource.PlayOneShot(click, 1F);
                        Resume();
                    }
                    else
                    {
                        audioSource.PlayOneShot(click, 1F);
                        Pause();
                    }
        }
	}

    public void Resume()
    {
        bGM.UnPause();
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        bGM.Pause();
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
}
