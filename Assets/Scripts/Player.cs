using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 500.0f;
    public float jumpForce = 12.0f;
    public float weightMin = 1.0f;
    public float weightMax = 5.0f;
    public float livesMin = 1.0f;
    public float livesMax = 3.0f;

    private Rigidbody2D _body;
    private CircleCollider2D _playerCollider;
    private int playerWeight;
    private int playerLives;
    private float playerSize;
    
    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _playerCollider = GetComponent<CircleCollider2D>();

       // Generation player stats{
        System.Random rnd = new System.Random();
        int playerTier = rnd.Next(1,6); // Player size from 0.5f to 1.0f
        switch (playerTier)
        {
            case 1: 
                playerSize = 0.5f;
                break;            
            case 2: 
                playerSize = 0.6f;
                break;            
            case 3: 
                playerSize = 0.7f;
                break;            
            case 4: 
                playerSize = 0.8f;
                break;            
            case 5: 
                playerSize = 0.9f;
                break;
            default:
                playerSize = 1.0f;
                break;
        }
        playerWeight = (int)Math.Round((((weightMax - weightMin) / 6) * playerTier) + 1);
        playerLives = (int)Math.Round((((livesMax - livesMin) / 6) * playerTier) + 1);
        // }
        
        _body.mass = playerWeight;
        transform.localScale = new Vector3(playerSize,playerSize);
    }

    // Update is called once per frame
    void Update()
    {
        // Player moving{
        float deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime; // Calculate power of velocity
        Vector2 movement = new Vector2(deltaX, _body.velocity.y); // Make a new v2 for player velocity along X axis
        _body.velocity = movement; // Assign new velocity value
        // }

        // Player grounded checking{
        Vector3 max = _playerCollider.bounds.max; 
        Vector3 min = _playerCollider.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        bool grounded = false || hit != null;
        
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        // }
        
        // Player lives check
        if (playerLives < 0)
        {
            Destroy(gameObject);
            Debug.Log("You Died!");
        }
        // }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            playerLives--;
            Debug.Log("Player recive damage! Lives left: " + playerLives);
        } else if (collision.gameObject.CompareTag("Death"))
        {
            Destroy(gameObject);
            Debug.Log("You Died!");
        } else if (collision.gameObject.CompareTag("Finish"))
        {
            Menu pauseMenu = GameObject.Find("Canvas").GetComponent<Menu>();
            pauseMenu.LevelDone();
            //Destroy(gameObject);
            //Debug.Log("You Died! But win.");
        }
    }
}
