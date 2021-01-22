using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Characters : MonoBehaviour
{
    [NonSerialized] public float speed = 500.0f;
    [NonSerialized] public float jumpForce = 50.0f;
    [NonSerialized] public int tier;
    [NonSerialized] public float size;
    [NonSerialized] public Rigidbody2D body;
    public float weightMin = 1.0f;
    public float weightMax = 5.0f;
    private IMoving _mover;
    
    private void Start()
    {
        if (gameObject.CompareTag("Player"))
        {
            _mover = new PlayerMoving();
        }
        else
        {
            _mover = new EnemyMoving();
        }
        
        if(body == null) bodyInitialize();
        if(tier == 0) TierGen();
        body.mass = TierBasedGen(tier, weightMin, weightMax);
        size = SizeGen(tier);
        transform.localScale = new Vector3(size,size);
    }

    private void Update()
    {
        _mover.DoMoving(gameObject, this);
    }

    protected void bodyInitialize()
    {
        body = GetComponent<Rigidbody2D>();
    }
    
    protected void TierGen()
    {
        System.Random rnd = new System.Random();
        tier = rnd.Next(1,6); // Count of tiers depends on size (0.5f to 1.0f)
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
