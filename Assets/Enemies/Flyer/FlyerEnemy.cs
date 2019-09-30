using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerEnemy : MonoBehaviour
{
    GameObject Player;

    public float forcex = 100, forcey = 200;
    public float airheight;
    public float AttackForce;

    void Start()
    {
        Player = GameObject.Find("Player");
        StartCoroutine(Fly());
    }

    IEnumerator Fly()
    {
        int count = Random.Range(3, 5);
        int[] direction = new int[] { -1, 1 };

        GetComponent<EnemyState>()._running = true;
        while(count>0)
        {
            Move(direction[Random.Range(0, 2)]);

            yield return new WaitForSeconds(1f);
            count--;
        }

        GetComponent<EnemyState>()._running = false;
        AttackDownwards();
    }

    void Move(float i)
    {
        Stop();
        GetComponent<Rigidbody2D>().AddForce(new Vector2(forcex * i, 0));
    }

    void AttackDownwards()
    {
        GetComponent<EnemyState>()._attacking = true;
        Stop();

        AttackForce = (Player.transform.position.x - transform.position.x *GetComponent<EnemyState>()._directionmodifier)/2;
        GetComponent<Rigidbody2D>().AddForce(new Vector2((Player.transform.position.x - transform.position.x) * GetComponent<EnemyState>()._directionmodifier, (Player.transform.position.y - transform.position.y)).normalized * 200);
    }

    void AttackHorizontal()
    {
        Stop();
        Debug.Log("Hor");
        GetComponent<Rigidbody2D>().AddForce(new Vector2(AttackForce * GetComponent<EnemyState>()._directionmodifier, 0).normalized * 200);
    }

    IEnumerator FlyUp(float direction)
    {
        Stop();

        GetComponent<Rigidbody2D>().AddForce(new Vector2(forcex * direction, forcey).normalized * 200);

        while (transform.position.y < airheight)
        {
            yield return new WaitForEndOfFrame();
        }

        float seconds = Random.Range(0, 3);
        yield return new WaitForSeconds(seconds * 0.1f);

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        StartCoroutine(Fly());
    }

    void Stop()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered");
        if (collision.gameObject.tag == "Floor")
        {
            AttackHorizontal();
        }

        if (collision.gameObject.tag == "Wall")
        {
            float direction = transform.position.x - collision.transform.position.x;

            if (GetComponent<EnemyState>()._attacking)
            {
                GetComponent<EnemyState>()._attacking = false;
                StartCoroutine(FlyUp(direction / Mathf.Abs(direction)));
            }

            if(GetComponent<EnemyState>()._running)
            {
                Move(direction / Mathf.Abs(direction));
            }
        }
    }
}