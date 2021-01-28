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
    
    public void Init(ILifecycle lifecycler)
    {
        _lifecycler = lifecycler;
    }
    
    private void Start()
    {
        if(body == null) BodyInitialize();
        if(tier == 0) TierGen();
        body.mass = TierBasedGen(tier, weightMin, weightMax);
        size = SizeGen(tier);
        transform.localScale = new Vector3(size,size);
        Debug.Log("LevelData.Player.tier: " + LevelData.Player.tier);
        Debug.Log("tier: " + tier);
    }

    private void Update()
    {
        _lifecycler.DoLifecycle();
    }

    private void BodyInitialize()
    {
        body = GetComponent<Rigidbody2D>();
    }
    
    private void TierGen()
    {
        System.Random rnd = new System.Random();
        tier = rnd.Next(1,6); // Count of tiers depends on size (0.5f to 1.0f)
    }

    private float SizeGen(int charTier)
    {
        const float baseSize = 0.4f;
        return baseSize + (charTier * 0.1f);
    }

    public int TierBasedGen(int charTier, float min, float max)
    {
        return (int)Math.Round((((max - min) / 6) * charTier) + 1);
    }
}
