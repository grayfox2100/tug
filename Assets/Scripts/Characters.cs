using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    protected void Moving(IMoving moving, float direction, float speed, Rigidbody2D body)
    {
        moving.DoMoving(direction, speed, body);
    }

    protected int TierGen()
    {
        System.Random rnd = new System.Random();
        return rnd.Next(1,6); // Count of tiers depends on size (0.5f to 1.0f)
    }

    protected float SizeGen(int tier)
    {
        const float baseSize = 0.4f;
        return baseSize + (tier * 0.1f);
    }

    protected int TierBasedGen(int tier, float min, float max)
    {
        return (int)Math.Round((((max - min) / 6) * tier) + 1);
    }
}
