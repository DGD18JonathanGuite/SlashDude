using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject Runner;
    public GameObject Jumper;
    public GameObject Flyer;

    public GameObject BRunner;
    public GameObject BJumper;
    public GameObject BFlyer;

    public GameObject[] RunnerSpawnPoints;
    public GameObject[] FlyerSpawnPoints;

    public float rate = 1.5f;

    private void Start()
    {
        //StartCoroutine(SpawnRunner());
    }

    IEnumerator SpawnRunner()
    {
        int spawnpointindex = Random.Range(0, 2);
        while (gameObject)
        {
            yield return new WaitForSeconds(rate);
            Instantiate(Runner, RunnerSpawnPoints[spawnpointindex].transform.position, Quaternion.identity);
            spawnpointindex = Random.Range(0, 2);
        }
    }

    IEnumerator SpawnJumper()
    {
        int spawnpointindex = Random.Range(0, 2);
        while (gameObject)
        {
            yield return new WaitForSeconds(rate);
            Instantiate(Jumper, RunnerSpawnPoints[spawnpointindex].transform.position, Quaternion.identity);
            spawnpointindex = Random.Range(0, 2);
        }
    }

    IEnumerator SpawnFlyer()
    {
        int spawnpointindex = Random.Range(0, 2);
        while (gameObject)
        {
            yield return new WaitForSeconds(rate);
            Instantiate(Flyer, FlyerSpawnPoints[spawnpointindex].transform.position, Quaternion.identity);
            spawnpointindex = Random.Range(0, 2);
        }
    }



    //DEBUG
    void ESpawnRunner()
    {
        int spawnpointindex = Random.Range(0, 2);
        Instantiate(Runner, RunnerSpawnPoints[spawnpointindex].transform.position, Quaternion.identity);
    }

    void ESpawnJumper()
    {
        int spawnpointindex = Random.Range(0, 2);
        Instantiate(Jumper, RunnerSpawnPoints[spawnpointindex].transform.position, Quaternion.identity);
    }

    void ESpawnFlyer()
    {
        int spawnpointindex = Random.Range(0, 2);
        Instantiate(Flyer, FlyerSpawnPoints[spawnpointindex].transform.position, Quaternion.identity);
    }

    void SpawnBRunner()
    {
        Instantiate(BRunner, new Vector2(0, 0.5f), Quaternion.identity);
    }

    void SpawnBJumper()
    {
        Instantiate(BJumper, new Vector2(0, 0.5f), Quaternion.identity);
    }

    void SpawnBFlyer()
    {
        int spawnpointindex = Random.Range(0, 2);
        Instantiate(BFlyer, new Vector2(0, 0.5f), Quaternion.identity);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ESpawnRunner();
            EventManager.EnemySpawn();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ESpawnJumper();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ESpawnFlyer();
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            SpawnBRunner();
        }
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            SpawnBJumper();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SpawnBFlyer();
        }
    }
}
