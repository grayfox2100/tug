using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerLifecycle : ILifecycle
{
    private float jumpForce = 50.0f;
    
    public void DoLifecycle()
    {
        Moving(Input.GetAxis("Horizontal"), LevelData.Player.speed, LevelData.Player.body);
        Jumping();
        HealthCheck();
    }
    
    private void HealthCheck()
    {
        if (LevelData.PlayerLives < 1)
        {
            LevelData.Player.gameObject.SetActive(false);
            //TODO: UI
            //_pauseMenu.LevelDone();
        }
    }
    
    public void Respawn()
    {
        LevelData.PlayerLives = LevelData.PlayerFullLives;
        LevelData.Player.gameObject.transform.position = LevelData.PlayerSpawn;
        LevelData.Player.gameObject.SetActive(true);
    }
    
    private void Moving(float direction, float speed, Rigidbody2D body)
    {
        float deltaX = direction * speed * Time.deltaTime; 
        Vector2 movement = new Vector2(deltaX, body.velocity.y);
        body.velocity = movement;
    }
    
    private void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CheckGround())
        {
            LevelData.Player.body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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