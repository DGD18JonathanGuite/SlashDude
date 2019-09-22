using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathParticles : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyParticles());
    }

    IEnumerator DestroyParticles()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
