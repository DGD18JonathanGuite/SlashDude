using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerBoss : MonoBehaviour
{
    GameObject Player;

    public GameObject Bullet;
    public GameObject Node;
    public GameObject Explosion;
    public GameObject ExplosionNode;

    public ParticleSystem Damaged;

    EnemyState _EnemyState;

    IEnumerator BossIdleAttack;
    IEnumerator AtkMove;

    int absorption = 3;
    float attackmovespeed = 0.02f;

    void Start()
    {
        Player = GameObject.Find("Player");

        BossIdleAttack = IdleAttack();
        AtkMove = AttackMove();

        _EnemyState = GetComponent<EnemyState>();
        _EnemyState._bosshealth = 4;

        StartCoroutine(BossIdleAttack);
    }

    private void Update()
    {
        if (Player.transform.position.x < transform.position.x)
            GetComponent<EnemyState>()._facingleft = true;
        else if (Player.transform.position.x > transform.position.x)
            GetComponent<EnemyState>()._facingleft = false;
    }

    IEnumerator IdleAttack()
    {
        float[] zrotation = new float[]{15,30,45,60,90};

        while(gameObject)
        {
            for(int i = absorption; i > 0; i--)
            {
                GameObject B = Instantiate(Bullet, Node.transform.position, Quaternion.identity);
                B.GetComponent<Rigidbody2D>().AddForce(new Vector2(zrotation[Random.Range(0, zrotation.Length)] * -_EnemyState._directionmodifier, 300));
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(1.5f);
        }        
    }

    public void BossisHit()
    {
        Debug.Log("BossisHit");
        absorption--;
        Instantiate(Damaged, Node.transform.position, Quaternion.identity);

        if (absorption == 0)
        {
            _EnemyState._bosshealth--;

            if (_EnemyState._bosshealth <= 0)
            {
                EventManager.EnemyisDead();
                Destroy(gameObject);
            }
            else
            {
                StopCoroutine(BossIdleAttack);
                StartCoroutine(BossAttack());
                StartCoroutine(AtkMove);
            }
        }
    }

    IEnumerator BossAttack()
    {
        float time = 1;

        again:
        for(int i = 5; i > 0; i--)
        {
            StartCoroutine(Attack());
            yield return new WaitForSeconds(time);
        }

        if (absorption <= 0)
        {
            _EnemyState._bosshealth--;
            if (_EnemyState._bosshealth > 0)
            {
                time -= 0.2f;
                goto again;
            }
            else
            {
                EventManager.EnemyisDead();
                Destroy(gameObject);
            }
        }
        else
        {
            if (absorption < 2)
                absorption += 3;
            else
                absorption += 1;

            StopCoroutine(AtkMove);
            StartCoroutine(IdleAttack());
        }
    }

    IEnumerator Attack()
    {
        _EnemyState._attacking = true;
        yield return new WaitForSeconds(0.2f);
        Instantiate(Explosion, ExplosionNode.transform.position, Quaternion.identity);
        _EnemyState._attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") //&& _EnemyState._attacking)
            Absorption(collision.gameObject);
    }

    void Absorption(GameObject absorbed)
    {
        absorbed.GetComponent<PlayerAttack>().EnemyAbsorbed(Node);        
        absorption++;
    }

    IEnumerator AttackMove()
    {
        while (gameObject)
        {
            yield return new WaitForSeconds(0.03f);
            transform.Translate(new Vector2(-attackmovespeed, 0));
            Debug.Log("A_Move " + _EnemyState._directionmodifier +" "+ attackmovespeed * -_EnemyState._directionmodifier);
        }
    }
}