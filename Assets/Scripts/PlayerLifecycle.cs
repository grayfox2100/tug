using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerLifecycle : ILifecycle
{
    private float jumpForce = 50.0f;
    
    //TODO: UI
    //private Menu _pauseMenu;
    
    public void DoLifecycle()
    {
        Moving(Input.GetAxis("Horizontal"), LevelData.Player.speed, LevelData.Player.body);
        Jumping(LevelData.Player.body);
        HealthCheck(LevelData.Player.gameObject);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            LevelData.Player.body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            LevelData.PlayerLives--;
        } else if (collision.gameObject.CompareTag("Death"))
        {
            LevelData.Player.gameObject.SetActive(false);
            //TODO: UI
            //_pauseMenu.LevelDone();
        } else if (collision.gameObject.CompareTag("Finish"))
        {
            //TODO: UI
            //_pauseMenu.LevelDone();
        }
    }
    
    private void HealthCheck(GameObject character)
    {
        if ( LevelData.PlayerLives < 1)
        {
            character.SetActive(false);
            //TODO: UI
            //_pauseMenu.LevelDone();
        }
    }
    
    private void Respawn(GameObject character)
    {
        LevelData.PlayerLives = LevelData.PlayerFullLives;
        character.transform.position = LevelData.PlayerSpawn;
        character.SetActive(true);
    }
    
    private void Moving(float direction, float speed, Rigidbody2D body)
    {
        float deltaX = direction * speed * Time.deltaTime; 
        Vector2 movement = new Vector2(deltaX, body.velocity.y);
        body.velocity = movement;
    }
    
    private void Jumping(Rigidbody2D body)
    {
        if (Input.GetKeyDown(KeyCode.Space) && CheckGround())
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private bool CheckGround()
    {
        Bounds bounds = LevelData.PlayerCollider.bounds;
        Vector3 max = bounds.max; 
        Vector3 min = bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        return false || hit != null;
    }
}