using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Characters
{
    public float speed = 500.0f;
    public float weightMin = 1.0f;
    public float weightMax = 5.0f;
    
    private Rigidbody2D body;
    
    private float enemySize;
    private int enemyWeight;
    private int moveDirection = 1;
    
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        StatsGen();
    }

    void Update()
    {
        Moving(new EnemyMoving(), moveDirection, speed, body);
        ObstacleCheck();
    }
    
    private void ObstacleCheck()
    {
        if (FloorCheck() || WallCheck() || EnemyCheck())
        {
            moveDirection *= -1;
        }
    }

    private bool EnemyCheck()
    {
        Vector2 forwardDirection = moveDirection > 0 ? Vector2.right : Vector2.left;
        RaycastHit2D hitWall = Physics2D.Raycast(transform.position, forwardDirection, enemySize);
        
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
        RaycastHit2D hitWall = Physics2D.Raycast(transform.position, forwardDirection, enemySize);
        
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
        RaycastHit2D hitFloor = Physics2D.Raycast(transform.position, floorCheckDirection, enemySize + 0.25f);

        if (hitFloor.collider == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    } 
    
    private void StatsGen()
    {
        int enemyTier = TierGen();
        enemySize = SizeGen(enemyTier);
        transform.localScale = new Vector3(enemySize,enemySize);
        body.mass = TierBasedGen(enemyTier, weightMin, weightMax);
    }
}
