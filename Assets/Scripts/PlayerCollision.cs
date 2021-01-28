using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public delegate void PlayerCollisionEventHandler();
    public static event PlayerCollisionEventHandler GetDamage;
    public static event PlayerCollisionEventHandler Dying;
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
            _playerLife.PlayerLives--;
            
            if (GetDamage != null) GetDamage.Invoke();
            
            if (_playerLife.PlayerLives < 1)
            {
                if (Dying != null) Dying.Invoke();
                LevelData.Player.gameObject.SetActive(false);
            } 
        } 
        else if (collision.gameObject.CompareTag("Death"))
        {
            if (Dying != null) Dying.Invoke();
            LevelData.Player.gameObject.SetActive(false);
        } 
        else if (collision.gameObject.CompareTag("Finish"))
        {
            if (Finish != null) Finish.Invoke();
        }
    }
}
