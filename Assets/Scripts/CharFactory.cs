
using UnityEngine;

public static class CharFactory
{
    public enum Types
    {
        Player,
        Enemy
    }
    
    public static Character Create(Types type, Vector3 spawnPoint)
    {
        GameObject obj = new GameObject();
        
        obj.AddComponent<Rigidbody2D>();
        obj.AddComponent<CircleCollider2D>();

        obj.transform.position = spawnPoint;
        
        SpriteRenderer sp = obj.AddComponent<SpriteRenderer>();
        Character scriptComponent = obj.AddComponent<Character>();
        
        switch (type)
        {
            case Types.Player:
                obj.name = "Player";
                obj.tag = "Player";
                sp.sprite = Resources.Load<Sprite>("Sprites/player");
                scriptComponent.Init(new PlayerLifecycle());
                obj.AddComponent<PlayerLife>();
                obj.AddComponent<PlayerCollision>();
                break;
            case Types.Enemy:
                obj.name = "Enemy";
                obj.tag = "Enemy";
                sp.sprite = Resources.Load<Sprite>("Sprites/enemy");

                EnemyLifecycle tmp = new EnemyLifecycle();
                tmp.Enemy = scriptComponent;
                scriptComponent.Init(tmp);
                
                break;
            default:
                break;
        }

        return scriptComponent;
    }
}
