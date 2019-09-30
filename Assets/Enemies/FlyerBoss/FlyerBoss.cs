using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerBoss : MonoBehaviour
{
    GameObject Player;

    public float movespeed = 100;
    public float attackspeed = 200;
    public float slidetimer = 1;

    public float forcex = 100, forcey = 200;
    public float airheight;
    public float AttackForce;

    public bool AltAttack = false;
    public bool RunningWallHit = false;

    void Start()
    {
        Player = GameObject.Find("Player");
        StartCoroutine(Fly(1));
    }

    IEnumerator Fly(float xmove)
    {
        Debug.Log("Flyyyyyy");

        Stop();
        GetComponent<EnemyState>()._running = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        int count = Random.Range(200,250);

        float xforce = xmove;
        float yforce = 0;

        if (transform.position.x < 0)
            xforce = 1;
        else
            xforce = -1;

        while (!GetComponent<EnemyState>()._attacking)
        {
            if (transform.position.y < airheight)
                yforce = 1;
            else
                yforce = 0;

            yield return new WaitForSeconds(0.01f);
            transform.Translate((new Vector2(xforce, yforce)).normalized / movespeed);
            count--;

            if(count == 0)
            {          
                AttackDownwards();
            }
        }
    }

    //void Move(float i)
    //{
    //    Stop();
    //    GetComponent<Rigidbody2D>().AddForce(new Vector2(forcex * i, 0));
    //}

    void AttackDownwards()
    {
        GetComponent<EnemyState>()._attacking = true;
        GetComponent<EnemyState>()._running = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        Stop();

        AttackForce = ((Player.transform.position.x - transform.position.x) * GetComponent<EnemyState>()._directionmodifier) / 2;

        //Debug.Log("AtkDwn");
        GetComponent<Rigidbody2D>().AddForce(new Vector2((Player.transform.position.x - transform.position.x) * GetComponent<EnemyState>()._directionmodifier, (Player.transform.position.y - transform.position.y)).normalized * attackspeed);
    }

    void AttackHorizontal()
    {
        Stop();
        GetComponent<Rigidbody2D>().AddForce(new Vector2((AttackForce) * GetComponent<EnemyState>()._directionmodifier, 0).normalized * attackspeed);
        StartCoroutine(Slide());
    }

    IEnumerator Slide()
    {
        yield return new WaitForSeconds(slidetimer);

        if (GetComponent<EnemyState>()._attacking)
        {
            GetComponent<EnemyState>()._attacking = false;
            GetComponent<EnemyState>()._running = true;

            Stop();
            GetComponent<Rigidbody2D>().AddForce(new Vector2((AttackForce) * GetComponent<EnemyState>()._directionmodifier, 0).normalized * attackspeed / 2);
            yield return new WaitForSeconds(slidetimer*2f);
            StartCoroutine(FlyUp(GetComponent<EnemyState>()._directionmodifier));
        }
    }

    IEnumerator FlyUp(float direction)
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;


        Debug.Log("Flyup");

        Stop();

        Debug.Log("Flyup at "+(new Vector2(forcex * direction, forcey).normalized * attackspeed));
        GetComponent<Rigidbody2D>().AddForce(new Vector2(forcex * direction, forcey).normalized * attackspeed);

        while (transform.position.y < airheight)
        {
            yield return new WaitForEndOfFrame();
        }

        float seconds = Random.Range(0, 3);
        yield return new WaitForSeconds(seconds * 0.1f);

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        StartCoroutine(Fly(direction));
    }

    public void Stop()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Triggered");
        if (collision.gameObject.tag == "Floor" && !AltAttack)
        {
            AttackHorizontal();
        }

        if (collision.gameObject.tag == "Wall" && !AltAttack)
        {
            float direction = transform.position.x - collision.transform.position.x;

            if (GetComponent<EnemyState>()._attacking)
            {
                GetComponent<EnemyState>()._attacking = false;
                StartCoroutine(FlyUp(direction / Mathf.Abs(direction)));
            }

            else if (GetComponent<EnemyState>()._running)
            {
                //Debug.Log("running flyup");
                RunningWallHit = true;
            }
        }
    }

    public void BossisHit()
    {
        if(AltAttack)
        {
            GetComponent<FlyerBossAltMove>().ResetCoroutines();
        }

        AltAttack = true;

        Stop();
        StopAllCoroutines();
        StartCoroutine(GetComponent<FlyerBossAltMove>().SliceDown(airheight));
    }
}