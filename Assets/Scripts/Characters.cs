using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoving
{
    void DoMoving(float direction, float speed, Rigidbody2D body);
}

class EnemyMoving : IMoving
{
    public void DoMoving(float direction, float speed, Rigidbody2D body)
    {
        float deltaX = direction * speed * Time.deltaTime;
        Vector2 movement = new Vector2(deltaX, body.velocity.y);
        body.velocity = movement;
    }
}

class PlayerMoving : IMoving
{
    public void DoMoving(float direction, float speed, Rigidbody2D body)
    {
        float deltaX = direction * speed * Time.deltaTime; 
        Vector2 movement = new Vector2(deltaX, body.velocity.y);
        body.velocity = movement;
    }
}

public class Characters : MonoBehaviour
{
    public void Moving(IMoving moving, float direction, float speed, Rigidbody2D body)
    {
        moving.DoMoving(direction, speed, body);
    }
    
    public float SizeGen(int tier)
    {
        const float baseSize = 0.4f;
        return baseSize + (tier * 0.1f);
    }
}
