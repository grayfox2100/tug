using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 500.0f;
    public float weightMin = 1.0f;
    public float weightMax = 5.0f;
    
    private Rigidbody2D _body;
    private CircleCollider2D _enemyCollider;
    private int enemyWeight;
    private float enemySize;
    private int moveDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _enemyCollider = GetComponent<CircleCollider2D>();

        // Generation enemy stats{
        System.Random rnd = new System.Random();
        int playerTier = rnd.Next(1,6); // Enemy size from 0.5f to 1.0f
        switch (playerTier)
        {
            case 1: 
                this.enemySize = 0.5f;
                break;            
            case 2: 
                this.enemySize = 0.6f;
                break;            
            case 3: 
                this.enemySize = 0.7f;
                break;            
            case 4: 
                this.enemySize = 0.8f;
                break;            
            case 5: 
                this.enemySize = 0.9f;
                break;
            default:
                this.enemySize = 1.0f;
                break;
        }
        this.enemyWeight = (int)Math.Round((((weightMax - weightMin) / 6) * playerTier) + 1);
        // }
        
        _body.mass = this.enemyWeight;
        transform.localScale = new Vector3(enemySize,enemySize);
        moveDirection = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // Enemy moving{
        float deltaX = moveDirection * speed * Time.deltaTime; // Calculate power of velocity
        Vector2 movement = new Vector2(deltaX, _body.velocity.y); // Make a new v2 for player velocity along X axis
        _body.velocity = movement; // Assign new velocity value
        // }
        
        int layerMask = LayerMask.GetMask("Blocks");
        // Wall check{
        Vector2 wallCheckDirection = moveDirection > 0 ? Vector2.right : Vector2.left;
        RaycastHit2D hitWall = Physics2D.Raycast(transform.position, wallCheckDirection, 0.75f, layerMask);
        
        Debug.DrawRay(transform.position, wallCheckDirection, Color.yellow);
        
        if (hitWall.collider != null)
        {
            Debug.Log("wall");
            this.moveDirection *= -1;
        }
        // }
        
        // Floor check{
        Vector2 floorCheckDirection = new Vector2(moveDirection > 0 ? 1.0f : -1.0f, -0.5f);
        RaycastHit2D hitFloor = Physics2D.Raycast(transform.position, floorCheckDirection, 1.0f, layerMask);
        
        Debug.DrawRay(transform.position, floorCheckDirection, Color.red);
        
        if (hitFloor.collider == null)
        {
            Debug.Log("floor");
            this.moveDirection *= -1;
        }
        // }

    }
    
}
