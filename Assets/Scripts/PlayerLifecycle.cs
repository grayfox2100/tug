using System;
using UnityEngine;

public class PlayerLifecycle : ILifecycle
{
    [NonSerialized] public static float jumpForce = 50.0f;
    
    public void DoLifecycle()
    {
        Moving(Input.GetAxis("Horizontal"), LevelData.Player.speed, LevelData.Player.body);
        Jumping();
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
        Bounds bounds = LevelData.Player.gameObject.GetComponent<CircleCollider2D>().bounds;
        Vector3 max = bounds.max; 
        Vector3 min = bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        return false || hit != null;
    }
}