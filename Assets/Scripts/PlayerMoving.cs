using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoving : IMoving
{
    //private float jumpForce = 12.0f;
    
    public void DoMoving(GameObject character, Characters characterObject)
    {
        Moving(Input.GetAxis("Horizontal"), characterObject.speed, characterObject.body);
        Jumping(characterObject.body,character.GetComponent<CircleCollider2D>(), characterObject.jumpForce);
    }
    
    private void Moving(float direction, float speed, Rigidbody2D body)
    {
        float deltaX = direction * speed * Time.deltaTime; 
        Vector2 movement = new Vector2(deltaX, body.velocity.y);
        body.velocity = movement;
    }
    
    private void Jumping(Rigidbody2D body, CircleCollider2D playerCollider, float jumpForce)
    {
        if (CheckGround(playerCollider) && Input.GetKeyDown(KeyCode.Space))
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private bool CheckGround(CircleCollider2D playerCollider)
    {
        var bounds = playerCollider.bounds;
        Vector3 max = bounds.max; 
        Vector3 min = bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        return false || hit != null;
    }
}