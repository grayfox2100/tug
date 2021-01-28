using System;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [NonSerialized] public int livesMin = 1;
    [NonSerialized] public int livesMax = 3;
    [NonSerialized] public int PlayerFullLives;
    [NonSerialized] public int PlayerLives;

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
    
    public void Heal()
    {
        PlayerLives = PlayerFullLives;
    }

    public void LivesGen()
    {
        PlayerFullLives = PlayerLives =  LevelData.Player.TierBasedGen(LevelData.Player.tier, livesMin, livesMax);
    }

}
