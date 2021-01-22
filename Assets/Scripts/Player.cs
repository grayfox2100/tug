using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : Characters
{
    //public float speed = 500.0f;
    //public float jumpForce = 12.0f;
    //public float weightMin = 1.0f;
    //public float weightMax = 5.0f;
    public float livesMin = 1.0f;
    public float livesMax = 3.0f;
    
    //private CircleCollider2D _playerCollider;
    private Menu _pauseMenu;
    //private int _playerWeight;
    private int _playerLives;
    private int _playerFullLives;
    private int _playerSpawnY;
    //private float _playerSize;
    
    void Start()
    {
        //_body = GetComponent<Rigidbody2D>();
        //_playerCollider = GetComponent<CircleCollider2D>();
        _pauseMenu = GameObject.Find("Canvas").GetComponent<Menu>();
        _playerSpawnY = (int) gameObject.transform.position.y;
        _playerFullLives = _playerLives = TierBasedGen(tier, livesMin, livesMax);
        //StatsGen();
    }

    void Update()
    {
        /*Moving(Input.GetAxis("Horizontal"), speed, _body);
        Jumping();*/
        //LivesCheck();
        if (_playerLives < 1)
        {
            gameObject.SetActive(false);
            _pauseMenu.LevelDone();
        }
    }
    
    /*private void Jumping()
    {
        if (CheckGround() && Input.GetKeyDown(KeyCode.Space))
        {
            _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private bool CheckGround()
    {
        var bounds = _playerCollider.bounds;
        Vector3 max = bounds.max; 
        Vector3 min = bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        return false || hit != null;
    }*/
    
    /*private void LivesCheck()
    {
        if (_playerLives < 1)
        {
            gameObject.SetActive(false);
            _pauseMenu.LevelDone();
        }
    }*/
    
    /*private void StatsGen()
    {
        /#1#/int playerTier = TierGen();
        //_playerSize = SizeGen(playerTier);
        //transform.localScale = new Vector3(_playerSize,_playerSize);
        //_body.mass = TierBasedGen(playerTier, weightMin, weightMax);#1#
        _playerFullLives = _playerLives = TierBasedGen(tier, livesMin, livesMax);
    }*/
    
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
