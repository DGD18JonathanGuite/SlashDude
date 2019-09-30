using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerBossExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LifeTime());
    }

    // Update is called once per frame
    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
