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
            if(gameObject.tag == "Enemy" || gameObject.tag == "PoisonPool")
            EnemyDeath();

            if(gameObject.tag == "Boss")
            {
                ParticleSystem PDeath = Instantiate(ParticleDeath, transform.position, Quaternion.Euler(GameObject.Find("Player").transform.rotation.eulerAngles + new Vector3(0, 90, 0)));

                if (GetComponent<RunnerBoss>())
                    GetComponent<RunnerBoss>().BossisHit();
                if (GetComponent<JumperBoss>())
                    GetComponent<JumperBoss>().BossisHit();
                if (GetComponent<FlyerBoss>())
                    GetComponent<FlyerBoss>().BossisHit();
            }
        }
    }

    public void EnemyDeath()
    {
        ParticleSystem PDeath = Instantiate(ParticleDeath, transform.position, Quaternion.Euler(GameObject.Find("Player").transform.rotation.eulerAngles + new Vector3(0,90,0)));
        Destroy(gameObject);
    }


    //FOR WHEN ABSORBED BY RUNNER BOSS
    public void EnemyAbsorbed(GameObject _target)
    {
        ParticleSystem PDeath = Instantiate(ParticleDeath, transform.position, Quaternion.Euler(GameObject.Find("Player").transform.rotation.eulerAngles + new Vector3(0, 90, 0)));
        StartCoroutine(PDeath.GetComponent<EnemyDeathParticles>().MoveTowardsTarget(_target));
        Destroy(gameObject);
    }
}