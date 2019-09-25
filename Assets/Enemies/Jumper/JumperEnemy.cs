using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperEnemy : MonoBehaviour
{
    GameObject Player;
    public GameObject PoisonPool;
    public float forcex = 50, forcey = 200;

    void Start()
    {
        Player = GameObject.Find("Player");
        //StartCoroutine(Jumper());
    }

    private void Update()
    {
        if (Player.transform.position.x < transform.position.x)
            GetComponent<EnemyState>()._facingleft = true;
        else if (Player.transform.position.x > transform.position.x)
            GetComponent<EnemyState>()._facingleft = false;
    }

    IEnumerator Jumper()
    {
            yield return new WaitForSeconds(2f);
            Debug.Log("Jump");
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            GetComponent<Rigidbody2D>().AddForce(new Vector2(forcex * -GetComponent<EnemyState>()._directionmodifier, forcey));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered");
        if (collision.gameObject.tag == "Floor")
        {
            Instantiate(PoisonPool, new Vector2(transform.position.x, 0.07f), Quaternion.identity);
            ResetandJumpAgain();
        }

        if (collision.gameObject.tag == "Wall")
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Rigidbody2D>().AddForce(new Vector2((transform.position.x - collision.gameObject.transform.position.x) *100,0));
        }
    }

    void ResetandJumpAgain()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        StartCoroutine(Jumper());
    }
}
