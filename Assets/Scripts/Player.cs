/*
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    /*public float livesMin = 1.0f;
    public float livesMax = 3.0f;
    private int _playerLives;
    private int _playerFullLives;
    private int _playerSpawnY;
    private Menu _pauseMenu;#1#
    
    void Start()
    {
        /*if(tier == 0) TierGen();
        if(body == null) bodyInitialize();#1#
        //_pauseMenu = GameObject.Find("Canvas").GetComponent<Menu>();
        //_playerSpawnY = (int) gameObject.transform.position.y;
        //_playerFullLives = _playerLives = TierBasedGen(tier, livesMin, livesMax);
    }

    void Update()
    {
        /*if (_playerLives < 1)
        {
            gameObject.SetActive(false);
            _pauseMenu.LevelDone();
        }#1#
    }
    
    /*public void Respawn()
    {
        _playerLives = _playerFullLives;
        gameObject.transform.position = new Vector3(0, _playerSpawnY + 1);
        gameObject.SetActive(true);
    }#1#
    
    /*private void OnGUI()
    {
        GUI.Label(new Rect(50,50,250,250), "Lives: " + _playerLives );
    }#1#

    /*private void OnCollisionEnter2D(Collision2D collision)
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
    }#1#
}
*/
