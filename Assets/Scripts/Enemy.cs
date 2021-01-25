/*
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Characters
{
    //public float speed = 500.0f;
    //public float weightMin = 1.0f;
    //public float weightMax = 5.0f;
    
    //private Rigidbody2D _body;
    //private float _enemySize;
    //private int _enemyWeight;
    //private int _moveDirection = 1;
    
    /*void Start()
    {
        //_body = GetComponent<Rigidbody2D>();
        //StatsGen();
    }#1#

    /*void Update()
    {
        Moving(_moveDirection, speed, _body);
        ObstacleCheck();
    }#1#
    
    /*private void ObstacleCheck()
    {
        if (FloorCheck() || WallCheck() || EnemyCheck())
        {
            _moveDirection *= -1;
        }
    }#1#

    /*private bool EnemyCheck()
    {
        Vector2 forwardDirection = _moveDirection > 0 ? Vector2.right : Vector2.left;
        RaycastHit2D hitWall = Physics2D.Raycast(transform.position, forwardDirection, _enemySize);
        
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
        Vector2 forwardDirection = _moveDirection > 0 ? Vector2.right : Vector2.left;
        RaycastHit2D hitWall = Physics2D.Raycast(transform.position, forwardDirection, _enemySize);
        
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
        Vector2 floorCheckDirection = new Vector2(_moveDirection > 0 ? 1.0f : -1.0f, -0.5f);
        RaycastHit2D hitFloor = Physics2D.Raycast(transform.position, floorCheckDirection, _enemySize + 0.25f);

        if (hitFloor.collider == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    } #1#
    
    /*private void StatsGen()
    {
        //int enemyTier = TierGen();
        //_enemySize = SizeGen(enemyTier);
        //transform.localScale = new Vector3(_enemySize,_enemySize);
        //_body.mass = TierBasedGen(enemyTier, weightMin, weightMax);
    }#1#
}
*/
