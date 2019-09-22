using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject Runner;
    public Vector3[] RunnerSpawnPoints;

    private void Start()
    {
        StartCoroutine(SpawnRunner());
    }

    IEnumerator SpawnRunner()
    {
        int spawnpointindex = Random.Range(0, 2);
        while (gameObject)
        {
            yield return new WaitForSeconds(1.5f);
            Instantiate(Runner, RunnerSpawnPoints[spawnpointindex], Quaternion.identity);
            spawnpointindex = Random.Range(0, 2);
            Debug.Log("Spawn at " + spawnpointindex);
        }
    }
}
