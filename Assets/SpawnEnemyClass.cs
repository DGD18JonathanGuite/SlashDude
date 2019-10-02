using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnEnemyClass
{    
    public float timing;
    public GameObject Enemy;
    public GameObject SpawnPoint;
    
    public SpawnEnemyClass(float _timing, GameObject _Enemy, GameObject _SpawnPoint)
    {
        timing = _timing;
        Enemy = _Enemy;
        SpawnPoint = _SpawnPoint;
    }
}