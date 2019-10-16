using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerBossMinionSpawn : MonoBehaviour
{
    public GameObject RunnerEnemy;
    public Vector3 RunnerSpawnPoint1;
    public Vector3 RunnerSpawnPoint2;

    private void Start()
    {
        RunnerSpawnPoint1 = GameObject.Find("RunnerSpawnPoint").transform.position;
        RunnerSpawnPoint2 = GameObject.Find("RunnerSpawnPoint (1)").transform.position;
    }

    public void MinionSpawn(float bossposition)
    {
        if (GetComponent<EnemyState>()._facingleft)
            Instantiate(RunnerEnemy, RunnerSpawnPoint2, Quaternion.identity);
        else
            Instantiate(RunnerEnemy, RunnerSpawnPoint1, Quaternion.identity);
    }

    //public void MinionSpawn(float bossposition)
    //{
    //    if (bossposition <= 0)
    //        StartCoroutine (Spawn(RunnerSpawnPoint2, RunnerSpawnPoint1));
    //    else
    //        StartCoroutine(Spawn(RunnerSpawnPoint1, RunnerSpawnPoint2));
    //}

    //IEnumerator Spawn(Vector3 Spawn1, Vector3 Spawn2)
    //{
    //    Instantiate(RunnerEnemy, Spawn1, Quaternion.identity);
    //    yield return new WaitForSeconds(0.4f);
    //    Instantiate(RunnerEnemy, Spawn2, Quaternion.identity);
    //}
}
