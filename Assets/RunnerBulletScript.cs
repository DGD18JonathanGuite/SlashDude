using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerBulletScript : MonoBehaviour
{
    public ParticleSystem BulletHit;    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EventManager.PlayerIsHit();
        }

        if (collision.gameObject.name != "Roof" && collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Boss")
        {
            Instantiate(BulletHit, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
