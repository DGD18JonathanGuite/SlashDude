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

    Animator RBossAnimator;

    public ParticleSystem Damaged;

    EnemyState _EnemyState;

    IEnumerator BossIdleAttack;
    IEnumerator AtkMove;
    IEnumerator Shake;

    public float shake_intensity;
    public float shake_time;
    public float shake_time_change = 0.01f;

    public float attack_stop_distance = 0.9f;

    public int _absorption = 1;
    public int absorption
    {
        get { return _absorption; }
        set
        {
            _absorption = value;
            GetComponent<RunnerBossRideScript>().RideSet(value);
            if (value <= 0)
            {
                _EnemyState._bosshealth--;
                shake_time -= shake_time_change;

                BossHealthState(_EnemyState._bosshealth);                

                StopCoroutine(BossIdleAttack);
                RBossAnimator.SetBool("RBoss_IdleAttack", false);
                StartCoroutine(BossAttack());
            }
        }
    }

    void BossHealthState(int i)
    {
        if (i == 3)
            GetComponent<SpriteRenderer>().color = new Color32(255, 180, 180, 255);
        if (i == 2)
            GetComponent<SpriteRenderer>().color = new Color32(255, 105, 105, 255);
        if (i == 1)
            GetComponent<SpriteRenderer>().color = new Color32(255, 30, 30, 255);
    }

    //COUNTER THAT DECIDED WHEN ABSORPTION VALUE GOES DOWN
    public int _absorption_damage = 0;
    int absorption_damage
    {
        get
        {
            return _absorption_damage;
        }
        set
        {
            _absorption_damage = value;

            if (_absorption_damage == 3 && absorption > 0)
            {                
                absorption--;
                absorption_damage = 0;
            }
        }
    }    

    public float attackmovespeed_original = 0.02f;
    public float attackmovespeed;

    void Start()
    {
        RBossAnimator = GetComponent<Animator>();
        Player = GameObject.Find("Player");

        BossIdleAttack = IdleAttack();
        AtkMove = AttackMove();
        Shake = Shaker();


        _EnemyState = GetComponent<EnemyState>();
        _EnemyState._bosshealth = 4;

        StartCoroutine(BossIdleAttack);
    }

    private void Update()
    {
        if (!_EnemyState._attacking)
        {
            if (Player.transform.position.x < transform.position.x)
                GetComponent<EnemyState>()._facingleft = true;
            else if (Player.transform.position.x > transform.position.x)
                GetComponent<EnemyState>()._facingleft = false;
        }
    }

    IEnumerator IdleAttack()
    {
        float[] zrotation = new float[]{15,30,45,60,90};

        while(gameObject)
        {
            RBossAnimator.SetBool("RBoss_IdleAttack", true);

            for (int i = absorption*3; i > 0; i--)
            {
                

                GameObject B = Instantiate(Bullet, Node.transform.position, Quaternion.identity);
                B.GetComponent<Rigidbody2D>().AddForce(new Vector2(zrotation[Random.Range(0, zrotation.Length)] * -_EnemyState._directionmodifier, 300));

                
                yield return new WaitForSeconds(0.2f);
            }

            RBossAnimator.SetBool("RBoss_IdleAttack", false);
            yield return new WaitForSeconds(1.5f);
        }
    }

    IEnumerator AttackMove()
    {
        Debug.Log("RunnerBossMove");
        while (gameObject)
        {
            yield return new WaitForEndOfFrame();
            transform.Translate(new Vector2(-attackmovespeed * Time.deltaTime * 30, 0));
        }
    }

    private void FixedUpdate()
    {
        
    }

    IEnumerator Shaker()
    {
        transform.Translate(shake_intensity / 2, 0, 0);

        while (gameObject)
        {
            yield return new WaitForSeconds(0.1f);
            transform.Translate(-shake_intensity, 0, 0);

            yield return new WaitForSeconds(0.1f);
            transform.Translate(shake_intensity, 0, 0);
        }
    }

    IEnumerator BossAttack()
    {
        again:
        for(int i = 3; i > 0; i--)
        {
            _EnemyState._attacking = true;
            RBossAnimator.SetBool("RBoss_BossAttack", true);

            StartCoroutine(Attack());

            yield return new WaitUntil(() => !_EnemyState._attacking);
        }

        if (absorption <= 0)
        {
            _EnemyState._bosshealth--;
            if (_EnemyState._bosshealth > 0)
            {
                shake_time -= shake_time_change;
                goto again;
            }
            else
            {
                RunnerBossDeath();                
            }
        }
        else
        {
            attackmovespeed = attackmovespeed_original;

            if (absorption > 4)
                absorption = 4;

            StopCoroutine(AtkMove);
            RBossAnimator.SetBool("RBoss_BossAttack", false);

            StartCoroutine(BossIdleAttack);
        }
    }

    bool _wallishit;
    IEnumerator Attack()
    {
        Debug.Log("Attack");

        _wallishit = false;

        StopCoroutine(AtkMove);
        RBossAnimator.SetBool("RBoss_BossAttack", false);

        StartCoroutine(Shake);
        GetComponent<RunnerBossMinionSpawn>().MinionSpawn(transform.position.x);

        yield return new WaitForSeconds(shake_time);
        StopCoroutine(Shake);

        StartCoroutine(AtkMove);
        RBossAnimator.SetBool("RBoss_BossAttack", true);

        GameObject _Explosion = Instantiate(Explosion, ExplosionNode.transform.position, Quaternion.identity);
        _Explosion.GetComponent<RunnerBossExplosion>().SetNode(ExplosionNode);

        yield return new WaitUntil(() => _wallishit);
        yield return new WaitUntil(() => (Mathf.Abs(transform.position.x) <= attack_stop_distance));
        //yield return new WaitForSeconds(1f);
        
        Destroy(_Explosion);
        RBossAnimator.SetBool("RBoss_BossAttack", false);
        _EnemyState._attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.GetComponent<EnemyRunner>())
                Absorption(collision.gameObject);
        }

        if (collision.gameObject.tag == "Wall" && _EnemyState._attacking)
        {
            _wallishit = true;

            GetComponent<RunnerBossMinionSpawn>().WallHitMinionSpawn(collision.name);


            if (GetComponent<EnemyState>()._facingleft)
                GetComponent<EnemyState>()._facingleft = false;
            else if (!GetComponent<EnemyState>()._facingleft)
                GetComponent<EnemyState>()._facingleft = true;
        }

    }

    public void BossisHit()
    {
        if (_EnemyState._attacking)
        {
                Instantiate(Damaged, Node.transform.position, Quaternion.identity);
        }

        else
        {
            if (absorption > 0)
            {
                Instantiate(Damaged, Node.transform.position, Quaternion.identity);
                absorption_damage++;
            }
        }
    }

    void Absorption(GameObject absorbed)
    {
        absorbed.GetComponent<PlayerAttack>().EnemyAbsorbed(Node);
        absorption++;
    }

    bool deathshake = false;
    void RunnerBossDeath()
    {
        StartCoroutine(RBDeath());
    }

    IEnumerator RBDeath()
    {
        StopCoroutine(AtkMove);
        StopCoroutine(BossIdleAttack);        

        StartCoroutine(Shake);
        StartCoroutine(DeathShakeDuration());

        yield return new WaitForEndOfFrame();

        while (deathshake)
        {
            Instantiate(Damaged, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.01f);
        }

        StopCoroutine(Shake);
        EventManager.EnemyisDead();
        Destroy(gameObject);
    }

    IEnumerator DeathShakeDuration()
    {
        deathshake = true;
        yield return new WaitForSeconds(2f);
        deathshake = false;
    }
}