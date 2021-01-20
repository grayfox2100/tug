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
    private Player playerScript;
    
    private void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
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

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        gameIsPaused = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Restart()
    {
        levelDone = false;
        Resume();
        playerScript.playerRespawn();
    }

    public void AnotherLevel()
    {
        levelDone = false;
        Resume();
        SceneManager.LoadScene("SampleScene");
    }

    public void LevelDone()
    {
        levelDone = true;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
