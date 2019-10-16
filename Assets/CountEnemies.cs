using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountEnemies : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.EnemyisDead += enemycount;
    }

    private void OnDisable()
    {
        EventManager.EnemyisDead -= enemycount;
    }

    void enemycount()
    {
        //Debug.Log("No. of existing enemies " + GameObject.FindGameObjectsWithTag("Enemy").Length);

        StartCoroutine(_enemycount());
    }

    IEnumerator _enemycount()
    {
        yield return new WaitForSeconds(1);

        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && GameObject.FindGameObjectsWithTag("Boss").Length == 0)
            EventManager.OpenDoors();
    }
}