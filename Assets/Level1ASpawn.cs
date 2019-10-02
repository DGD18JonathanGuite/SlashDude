using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1ASpawn : MonoBehaviour
{
    public SpawnEnemyClass[] spawn1;
    public SpawnEnemyClass[] spawn2;
    public SpawnEnemyClass[] spawn3;

    public SpawnEnemyClass[][] Lvl1ASpawns;

    private void Start()
    {
        Lvl1ASpawns = new SpawnEnemyClass[][] { spawn1, spawn2, spawn3};
    }
}