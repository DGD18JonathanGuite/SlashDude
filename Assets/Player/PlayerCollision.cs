using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void Start()
    {
        IgnoreCollisions(0);
    }

    private void OnEnable()
    {
        EventManager.EnemySpawn += IgnoreCollisions;
    }

    private void OnDisable()
    {
        EventManager.EnemySpawn -= IgnoreCollisions;
    }

    void IgnoreCollisions(int i)
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Enemy"))
            Physics2D.IgnoreCollision(item.GetComponent<Collider2D>(), GetComponent<BoxCollider2D>());
    }
}
