using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : Characters
{
    public float livesMin = 1.0f;
    public float livesMax = 3.0f;
    
    private Menu _pauseMenu;
    private int _playerLives;
    private int _playerFullLives;
    private int _playerSpawnY;
    
    void Start()
    {
        _pauseMenu = GameObject.Find("Canvas").GetComponent<Menu>();
        _playerSpawnY = (int) gameObject.transform.position.y;
        _playerFullLives = _playerLives = TierBasedGen(tier, livesMin, livesMax);
    }

    void Update()
    {
        if (_playerLives < 1)
        {
            gameObject.SetActive(false);
            _pauseMenu.LevelDone();
        }
    }
    
    public void Respawn()
    {
        _playerLives = _playerFullLives;
        gameObject.transform.position = new Vector3(0, _playerSpawnY + 1);
        gameObject.SetActive(true);
    }
    
    private void OnGUI()
    {
        GUI.Label(new Rect(50,50,250,250), "Lives: " + _playerLives );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _playerLives--;
        } else if (collision.gameObject.CompareTag("Death"))
        {
            gameObject.SetActive(false);
            _pauseMenu.LevelDone();
        } else if (collision.gameObject.CompareTag("Finish"))
        {
            _pauseMenu.LevelDone();
        }
    }
}
