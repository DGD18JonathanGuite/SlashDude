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

    public static System.Action<int> UpdateStats;
}
