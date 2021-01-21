using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : Characters
{
    public float speed = 500.0f;
    public float jumpForce = 12.0f;
    public float weightMin = 1.0f;
    public float weightMax = 5.0f;
    public float livesMin = 1.0f;
    public float livesMax = 3.0f;
    
    private Rigidbody2D body;
    private CircleCollider2D playerCollider;
    private Menu pauseMenu;
    
    private int playerWeight;
    private int playerLives;
    private int playerFullLives;
    private int playerSpawnY;
    private float playerSize;
    
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CircleCollider2D>();
        pauseMenu = GameObject.Find("Canvas").GetComponent<Menu>();

        Create();
    }

    void Update()
    {
        Moving(new PlayerMoving(), Input.GetAxis("Horizontal"), speed, body);
        Jumping();
        LivesCheck();
    }
    
    private void Jumping()
    {
        if (CheckGround() && Input.GetKeyDown(KeyCode.Space))
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private bool CheckGround()
    {
        var bounds = playerCollider.bounds;
        Vector3 max = bounds.max; 
        Vector3 min = bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        return false || hit != null;
    }
    
    private void LivesCheck()
    {
        if (playerLives < 1)
        {
            gameObject.SetActive(false);
            pauseMenu.LevelDone();
        }
    }
    
    private void Create()
    {
        StatsGen();
        
        playerFullLives = playerLives;
        body.mass = playerWeight;
        transform.localScale = new Vector3(playerSize,playerSize);
        playerSpawnY = (int) gameObject.transform.position.y;
    }

    private void StatsGen()
    {
        System.Random rnd = new System.Random();
        int playerTier = rnd.Next(1,6); // Player size from 0.5f to 1.0f
        
        playerSize = SizeGen(playerTier);
        playerWeight = tierBasedGen(playerTier, weightMin, weightMax);
        playerLives = tierBasedGen(playerTier, livesMin, livesMax);
    }
    
    public void Respawn()
    {
        playerLives = playerFullLives;
        gameObject.transform.position = new Vector3(0, playerSpawnY + 1);
        gameObject.SetActive(true);
    }
    
    private void OnGUI()
    {
        GUI.Label(new Rect(50,50,250,250), "Lives: " + playerLives );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            playerLives--;
        } else if (collision.gameObject.CompareTag("Death"))
        {
            gameObject.SetActive(false);
            pauseMenu.LevelDone();
        } else if (collision.gameObject.CompareTag("Finish"))
        {
            pauseMenu.LevelDone();
        }
    }
}
