using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Characters : MonoBehaviour
{
    public float speed = 500.0f;
    public float jumpForce = 12.0f;
    public float weightMin = 1.0f;
    public float weightMax = 5.0f;
    public int tier;
    public float size;
    public Rigidbody2D body;
    public IMoving mover;
    
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

    private void Start()
    {
        if (gameObject.CompareTag("Player"))
        {
            mover = new PlayerMoving();
        }
        else
        {
            mover = new EnemyMoving();
        }
        
        body = GetComponent<Rigidbody2D>();
        tier = TierGen();
        body.mass = TierBasedGen(tier, weightMin, weightMax);
        size = SizeGen(tier);
        transform.localScale = new Vector3(size,size);
    }

    private void Update()
    {
        mover.DoMoving(gameObject, this);
    }
}
