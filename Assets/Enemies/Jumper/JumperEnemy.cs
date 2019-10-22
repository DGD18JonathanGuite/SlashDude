using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperEnemy : MonoBehaviour
{
    Animator JumperAnimator;

    GameObject Player;
    EnemyState EState;
    public GameObject PoisonPool;

    public float forcex = 50;
    public float forcey = 200;

    public float jumpwait1 = 1;
    public float jumpwait2 = 1;

    bool canjump = false;

    void Start()
    {        
        Player = GameObject.Find("Player");
        EState = GetComponent<EnemyState>();
        JumperAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Player.transform.position.x < transform.position.x)
            EState._facingleft = true;
        else if (Player.transform.position.x > transform.position.x)
            EState._facingleft = false;

        if(canjump)
        {
            if(Mathf.Abs(Player.transform.position.x - transform.position.x) < 1)
            {
                EnemyRetreatJump();
            }
        }

        if (EState._running)
        {
            JumperAnimator.SetBool("JumperisJumping", true);
            JumperAnimator.SetFloat("JumperVerticalVelocity", GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            JumperAnimator.SetBool("JumperisJumping", false);

            if (canjump)         
                JumperAnimator.SetBool("JumperisLooking", true);         
            else
                JumperAnimator.SetBool("JumperisLooking", false);
        }
    }

    IEnumerator Jumper()
    {
        yield return new WaitForSeconds(jumpwait1);

        canjump = true;

        yield return new WaitForSeconds(jumpwait2);

        if (canjump)
            EnemyJump();
    }

    void EnemyJump()
    {
        canjump = false;
        EState._running = true;

        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(forcex * -GetComponent<EnemyState>()._directionmodifier, forcey));        
    }

    void EnemyRetreatJump()
    {
        canjump = false;
        EState._running = true;

        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(3*forcex * GetComponent<EnemyState>()._directionmodifier, forcey*1.5f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

         if (collision.gameObject.tag == "Floor")
        {
            EState._running = false;

            if (inpoison)
                PoisonContact.GetComponent<PoisonPoolScript>().PoisonCharged();
            else
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

    bool inpoison = false;
    GameObject PoisonContact;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PoisonPool")
        {
            inpoison = true;
            PoisonContact = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PoisonPool")
        {
            inpoison = false;
            PoisonContact = null;
        }
    }

    void ResetandJumpAgain()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        StartCoroutine(Jumper());
    }
}