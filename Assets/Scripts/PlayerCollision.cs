using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public delegate void PlayerCollisionEventHandler();
    public static event PlayerCollisionEventHandler Finish;

    private PlayerLife _playerLife;
    
    void Start()
    { 
        _playerLife = LevelData.Player.gameObject.GetComponent<PlayerLife>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            LevelData.Player.body.AddForce(Vector2.up * PlayerLifecycle.jumpForce, ForceMode2D.Impulse);
            _playerLife.GetDamage();

        } 
        else if (collision.gameObject.CompareTag("Death"))
        {
            _playerLife.GetDamage(_playerLife.PlayerFullLives);
        } 
        else if (collision.gameObject.CompareTag("Finish"))
        {
            if (Finish != null) Finish.Invoke();
        }
    }
}
