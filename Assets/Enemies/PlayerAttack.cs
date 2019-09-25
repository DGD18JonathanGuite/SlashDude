using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public ParticleSystem ParticleDeath;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerAttack" && PlayerStats.getInstance()._canattack)
        {
            if(gameObject.tag == "Enemy")
            EnemyDeath();

            if(gameObject.tag == "Boss")
            {

            }
        }
    }

    void EnemyDeath()
    {
        ParticleSystem PDeath = Instantiate(ParticleDeath, transform.position, Quaternion.Euler(GameObject.Find("Player").transform.rotation.eulerAngles + new Vector3(0,90,0)));
        Destroy(gameObject);
    }
}