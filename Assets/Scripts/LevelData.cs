using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
    public static Character Player;
    public static Vector3 PlayerSpawn;
    public static CircleCollider2D PlayerCollider;
    public static int PlayerFullLives;
    public static int PlayerLives;

    public static List<Character> Enemies;
}
