using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float speed = 500.0f;
    public float weightMin = 1.0f;
    public float weightMax = 5.0f;
    
    private Rigidbody2D body;
    
    private float enemySize;
    private int enemyWeight;
    private int moveDirection;
    
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        Create();
    }

    void Update()
    {
        Moving();
        ObstacleCheck();
    }

    private void Moving()
    {
        float deltaX = moveDirection * speed * Time.deltaTime;
        Vector2 movement = new Vector2(deltaX, body.velocity.y);
        body.velocity = movement;
    }

    private void ObstacleCheck()
    {
        if (FloorCheck() || WallCheck() || EnemyCheck())
        {
            this.moveDirection *= -1;
        }
    }

    private bool EnemyCheck()
    {
        Vector2 forwardDirection = moveDirection > 0 ? Vector2.right : Vector2.left;
        RaycastHit2D hitWall = Physics2D.Raycast(transform.position, forwardDirection, this.enemySize);
        
        if (hitWall.collider != null && hitWall.collider.CompareTag("Enemy"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    private bool WallCheck()
    {
        Vector2 forwardDirection = moveDirection > 0 ? Vector2.right : Vector2.left;
        RaycastHit2D hitWall = Physics2D.Raycast(transform.position, forwardDirection, this.enemySize);
        
        if (hitWall.collider != null && hitWall.collider.CompareTag("Wall"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    private bool FloorCheck()
    {
        Vector2 floorCheckDirection = new Vector2(moveDirection > 0 ? 1.0f : -1.0f, -0.5f);
        RaycastHit2D hitFloor = Physics2D.Raycast(transform.position, floorCheckDirection, this.enemySize + 0.25f);

        if (hitFloor.collider == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    } 
    
    private void Create()
    {
        StatsGen();
        
        body.mass = this.enemyWeight;
        transform.localScale = new Vector3(this.enemySize,this.enemySize);
        moveDirection = 1;
    }
    
    private void StatsGen()
    {
        System.Random rnd = new System.Random();
        int enemyTier = rnd.Next(1,6); // Enemy size from 0.5f to 1.0f

        SizeGen(enemyTier);
        
        this.enemyWeight = (int)Math.Round((((weightMax - weightMin) / 6) * enemyTier) + 1);
    }

    private void SizeGen(int tier)
    {
        switch (tier)
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
    }
}
