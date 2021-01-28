using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public delegate void PlayerCollisionEventHandler();
    public static event PlayerCollisionEventHandler GetDamage;
    public static event PlayerCollisionEventHandler Dying;
    public static event PlayerCollisionEventHandler Finish;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //LevelData.Player.body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //LevelData.PlayerLives--;
            Debug.Log("Enemy damage");
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
