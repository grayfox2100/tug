using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject PauseMenuUI;

    void Update()
    {
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
        Resume();
        
        GameObject player = GameObject.FindWithTag("Player");
        GameObject levelGen = GameObject.Find("LevelGen");

        if (player != null && levelGen != null)
        {
            int playerStartY = levelGen.GetComponent<LevelGen>().startPoint;
            player.transform.position = new Vector3(0, playerStartY + 1);
        }
    }

    public void AnotherLevel()
    {
        Resume();
        SceneManager.LoadScene("SampleScene");
    }
    
}
