using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2ASpawn : MonoBehaviour
{
    public SpawnEnemyClass[] spawn1;
    public SpawnEnemyClass[] spawn2;
    public SpawnEnemyClass[] spawn3;
    public SpawnEnemyClass[] spawnboss;

    public SpawnEnemyClass[][] LvlSpawns;

    private void Start()
    {
        LvlSpawns = new SpawnEnemyClass[][] { spawn1, spawn2, spawn3, spawnboss};
    }
}
