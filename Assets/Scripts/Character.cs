using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Character : MonoBehaviour
{
    [NonSerialized] public float speed = 500.0f;
    [NonSerialized] public int tier;
    [NonSerialized] public float size;
    [NonSerialized] public Rigidbody2D body;
    public float weightMin = 1.0f;
    public float weightMax = 5.0f;
    private ILifecycle _lifecycler;

    public void LifecyclerInit(ILifecycle lc)
    {
        _lifecycler = lc;
    }

    /*public Character(IMoving mv)
    {
        _mover = mv; 
    }  */
    
    private void Start()
    {
        /*if (gameObject.CompareTag("Player"))
        {
            _mover = new PlayerMoving();
        }
        else
        {
            _mover = new EnemyMoving();
        }*/
        if(body == null) bodyInitialize();
        if(tier == 0) TierGen();
        body.mass = TierBasedGen(tier, weightMin, weightMax);
        size = SizeGen(tier);
        transform.localScale = new Vector3(size,size);
    }

    private void Update()
    {
        //_lifecycler.DoLifecycle(gameObject, this);
        _lifecycler.DoLifecycle(/*gameObject, this*/);
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

    public int TierBasedGen(int tier, float min, float max)
    {
        return (int)Math.Round((((max - min) / 6) * tier) + 1);
    }
}
