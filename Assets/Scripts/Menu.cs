using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private static bool gameIsPaused = false;
    private static bool levelDone = false;
    public GameObject PauseMenuUI;
    public GameObject ResumeButton;
    public GameObject LifePanel;
    private PlayerLife _playerLife;

    private void Start()
    {
        PlayerCollision.Dying += this.LevelDone;
        PlayerCollision.Finish += this.LevelDone;
        
        _playerLife = LevelData.Player.gameObject.GetComponent<PlayerLife>();

        Text lifeText = LifePanel.GetComponent<Text>();
        Debug.Log("Player lives: " + _playerLife.PlayerLives);
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
        _playerLife.Respawn();
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
