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

    public enum Types
    {
        Player,
        Enemy
    }
    
    public static Character Create(ILifecycle lifecycler, Vector3 spawnPoint, Types type)
    {
        GameObject obj = new GameObject();
        obj.transform.position = spawnPoint;
        obj.AddComponent<Rigidbody2D>();
        obj.AddComponent<CircleCollider2D>(); 
        
        SpriteRenderer sp = obj.AddComponent<SpriteRenderer>();
        Character scriptComponent = obj.AddComponent<Character>();
        scriptComponent.Init(lifecycler);
        
        switch (type)
        {
            case Types.Player:
                obj.name = "Player";
                obj.tag = "Player";
                sp.sprite = Resources.Load<Sprite>("Sprites/player");
                break;
            case Types.Enemy:
                obj.name = "Enemy";
                obj.tag = "Enemy";
                sp.sprite = Resources.Load<Sprite>("Sprites/enemy");
                break;
            default:
                break;
        }

        return scriptComponent;
    }

    private void Init(ILifecycle lifecycler)
    {
        _lifecycler = lifecycler;
    }
    
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
        if(body == null) BodyInitialize();
        if(tier == 0) TierGen();
        body.mass = TierBasedGen(tier, weightMin, weightMax);
        size = SizeGen(tier);
        transform.localScale = new Vector3(size,size);
    }

    private void Update()
    {
        _lifecycler.DoLifecycle(gameObject);
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
