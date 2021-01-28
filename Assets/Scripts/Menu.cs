using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private static bool gameIsPaused = false;
    private static bool levelDone = false;
    public GameObject PauseMenuUI;
    public GameObject ResumeButton;
    //TODO: UI
    //private Player playerScript;
    
    
    
    private void Start()
    {
        //TODO: UI
        //playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (levelDone)
        {
            ResumeButton.SetActive(false);
            Pause();
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        gameIsPaused = false;
    }

    private void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    private void Restart()
    {
        levelDone = false;
        Resume();
        //TODO: UI
        //playerScript.Respawn();
    }

    private void AnotherLevel()
    {
        levelDone = false;
        Resume();
        SceneManager.LoadScene("SampleScene");
    }

    private void LevelDone()
    {
        levelDone = true;
    }

    private void Exit()
    {
        Application.Quit();
    }
}
