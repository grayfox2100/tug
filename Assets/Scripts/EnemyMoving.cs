using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMoving : IMoving
{
    private int _moveDirection = 1;
    
    public void DoMoving(GameObject character, Character characterObject)
    {
        Moving(characterObject.speed, characterObject.body);
        ObstacleCheck(character.transform, characterObject.size);
    }

    private void Moving(float speed, Rigidbody2D body)
    {
        float deltaX = _moveDirection * speed * Time.deltaTime; 
        Vector2 movement = new Vector2(deltaX, body.velocity.y);
        body.velocity = movement;
    }
    
    private void ObstacleCheck(Transform characterTransform, float size)
    {
        if (FloorCheck(characterTransform, size) || WallCheck(characterTransform, size) || EnemyCheck(characterTransform, size))
        {
            _moveDirection *= -1;
        }
    }
    
    private bool EnemyCheck(Transform characterTransform, float size)
    {
        Vector2 forwardDirection = _moveDirection > 0 ? Vector2.right : Vector2.left;
        RaycastHit2D hitWall = Physics2D.Raycast(characterTransform.position, forwardDirection, size);
        
        if (hitWall.collider != null && hitWall.collider.CompareTag("Enemy"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    private bool WallCheck(Transform characterTransform, float size)
    {
        Vector2 forwardDirection = _moveDirection > 0 ? Vector2.right : Vector2.left;
        RaycastHit2D hitWall = Physics2D.Raycast(characterTransform.position, forwardDirection, size);
        
        if (hitWall.collider != null && hitWall.collider.CompareTag("Wall"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    private bool FloorCheck(Transform characterTransform, float size)
    {
        Vector2 floorCheckDirection = new Vector2(_moveDirection > 0 ? 1.0f : -1.0f, -0.5f);
        RaycastHit2D hitFloor = Physics2D.Raycast(characterTransform.position, floorCheckDirection, size + 0.25f);

        if (hitFloor.collider == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    } 
}