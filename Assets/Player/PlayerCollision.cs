using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void Start()
    {
        IgnoreCollisions();
    }

    private void OnEnable()
    {
        EventManager.EnemySpawn += IgnoreCollisions;
    }

    private void OnDisable()
    {
        EventManager.EnemySpawn -= IgnoreCollisions;
    }

    void IgnoreCollisions()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Enemy"))
            Physics2D.IgnoreCollision(item.GetComponent<Collider2D>(), GetComponent<BoxCollider2D>());
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Boss"))
            Physics2D.IgnoreCollision(item.GetComponent<Collider2D>(), GetComponent<BoxCollider2D>());
    }

    public bool _playerisburning = false;    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PoisonPool" && !_playerisburning)
        {
            _playerisburning = true;
            StartCoroutine(PlayerIsBurning());
        }
    }

    IEnumerator PlayerIsBurning()
    {
        int count = 0;


        GetComponent<SpriteRenderer>().color = new Color32(75, 200, 35, 255);
        while(_playerisburning)
        {

            count++;
            yield return new WaitForSeconds(0.1f);

            //if (GameObject.FindGameObjectsWithTag("PoisonPool").Length == 0)
            //{
            //    Debug.Log("AAAA");
            //    _playerisburning = false;
            //    break;
            //}

            if (count > 25)
            {
                EventManager.PlayerIsHit();
                count = 0;
                Debug.Log("BURNING");
            }
        }
        GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PoisonPool")
        {
            CheckforStillBurns();   
        }
    }

    public void CheckforStillBurns()
    {
        if (GameObject.FindGameObjectsWithTag("PoisonPool").Length > 0)
        {
            foreach (GameObject _poisonpool in GameObject.FindGameObjectsWithTag("PoisonPool"))
            {
                if (_poisonpool.GetComponent<PoisonPoolScript>()._playerisburning)
                {
                    break;
                }

                _playerisburning = false;
            }
        }
        else
        {
            _playerisburning = false;
            Debug.Log("nulls");
        }
    }
}