using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifecycle : ILifecycle
{
    public float livesMin = 1.0f;
    public float livesMax = 3.0f;
    private float jumpForce = 50.0f;
    private int _playerLives;
    private int _playerFullLives;
    private int _playerSpawnY;
    //TODO: UI
    //private Menu _pauseMenu;
    private GameObject _character;
    private Character _characterObject;
    private CircleCollider2D _playerCollider;
    
    public PlayerLifecycle(GameObject character, Character characterObject)
    {
        _character = character;
        _characterObject = characterObject;
        _playerSpawnY = (int) character.transform.position.y;
        _playerFullLives = _playerLives = characterObject.TierBasedGen(characterObject.tier, livesMin, livesMax);
        _playerCollider = _character.GetComponent<CircleCollider2D>();
    }
    
    public void DoLifecycle()
    {
        Moving(Input.GetAxis("Horizontal"), _characterObject.speed, _characterObject.body);
        Jumping(_characterObject.body,_playerCollider, jumpForce);
        HealthCheck(_character);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _characterObject.body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _playerLives--;
        } else if (collision.gameObject.CompareTag("Death"))
        {
            _character.SetActive(false);
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
        if (_playerLives < 1)
        {
            character.SetActive(false);
            //TODO: UI
            //_pauseMenu.LevelDone();
        }
    }
    
    private void Respawn(GameObject character)
    {
        _playerLives = _playerFullLives;
        character.transform.position = new Vector3(0, _playerSpawnY + 1);
        character.SetActive(true);
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