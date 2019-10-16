using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunner : MonoBehaviour
{
    GameObject Player;
    public float movespeeddamp = 50;
    
    void Start()
    {
        Player = GameObject.Find("Player");
        StartCoroutine(MoveToPlayer());
    }

    private void Update()
    {
        if (Player.transform.position.x < transform.position.x && GetComponent<EnemyState>()._running)
            GetComponent<EnemyState>()._facingleft = true;
        else if (Player.transform.position.x > transform.position.x && GetComponent<EnemyState>()._running)
            GetComponent<EnemyState>()._facingleft = false;
    }

    IEnumerator MoveToPlayer()
    {
        GetComponent<EnemyState>()._running = true;
        yield return new WaitForEndOfFrame();
        
        while (Vector2.Distance(transform.position, Player.transform.position) > 0.5f)
        {
            transform.Translate(new Vector2((Player.transform.position.x - transform.position.x) * GetComponent<EnemyState>()._directionmodifier, 0).normalized/movespeeddamp);
            yield return new WaitForSeconds(0.01f);
        }
        GetComponent<EnemyState>()._running = false;
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        Vector2 attackmove = new Vector2((Player.transform.position.x - transform.position.x), 0).normalized / 50;

    again:
        GetComponent<EnemyRunnerSpriteManager>().EnemyAnimator.SetBool("RunnerhasSpotted", true);
        yield return new WaitForSeconds(0.5f);
        GetComponent<EnemyRunnerSpriteManager>().EnemyAnimator.SetBool("RunnerhasSpotted", false);

        GetComponent<EnemyState>()._attacking = true;
        for(int i = 0; i < 20; i++)
        {
            transform.Translate(attackmove * GetComponent<EnemyState>()._directionmodifier);
            yield return new WaitForSeconds(0.01f);
        }
        GetComponent<EnemyState>()._attacking = false;
        yield return new WaitForSeconds(1f);

        if (Vector2.Distance(transform.position, Player.transform.position) > 0.5f)
            StartCoroutine(MoveToPlayer());
        else
            goto again;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && GetComponent<EnemyState>()._attacking)
            EventManager.PlayerIsHit();
    }
}