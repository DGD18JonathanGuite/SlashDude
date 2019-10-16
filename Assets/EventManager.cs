using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public static System.Action<bool> Movement;
    public static System.Action<int> Stop;

    public static System.Action<int> ChangePlayerSpriteAnimation;

    public static System.Action<int> CheckforItems;

    public static System.Action<int> ExecuteMov;

    public static System.Action UpdateStats;

    public static System.Action<int> ChangeHitbox;

    public static System.Action EnemySpawn;

    public static System.Action PlayerIsHit;

    public static System.Action ChangeLevel;

    public static System.Action EnemyisDead;

    public static System.Action OpenDoors;

    public static System.Action PlayerisDead;

    public static System.Action EnemyisHit;
}
