using System;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [NonSerialized] public int livesMin = 1;
    [NonSerialized] public int livesMax = 3;
    [NonSerialized] public int PlayerFullLives;
    [NonSerialized] public int PlayerLives;
    public delegate void PlayerLivesEventHandler();
    public static event PlayerLivesEventHandler LifeChange;
    public static event PlayerLivesEventHandler Dying;

    void Start()
    {
        LivesGen();
    }
    public void Respawn()
    {
        Heal();
        LevelData.Player.gameObject.transform.position = LevelData.PlayerSpawn;
        LevelData.Player.gameObject.SetActive(true);
    }

    public void GetDamage(int damage = 1)
    {
        PlayerLives -= damage;
        if (LifeChange != null) LifeChange.Invoke();
        
        if (PlayerLives < 1)
        {
            if (Dying != null) Dying.Invoke();
            LevelData.Player.gameObject.SetActive(false);
        } 
    }
    private void Heal()
    {
        PlayerLives = PlayerFullLives;
        if (LifeChange != null) LifeChange.Invoke();
    }
    private void LivesGen()
    {
        PlayerFullLives = PlayerLives =  LevelData.Player.TierBasedGen(LevelData.Player.tier, livesMin, livesMax);
        if (LifeChange != null) LifeChange.Invoke();
    }

}
