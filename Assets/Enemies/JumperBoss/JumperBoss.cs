using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperBoss : MonoBehaviour
{
    GameObject Player;
    public GameObject PoisonPool;
    public float forcex = 100, forcey = 650;

    public Vector2 HopDirection;

    IEnumerator _BossJump;

    public bool BossJumpIsRunning = false;

    void Start()
    {
        Player = GameObject.Find("Player");

        _BossJump = BossJump();

        StartCoroutine(_BossJump);
    }

    private void Update()
    {
        if (Player.transform.position.x < transform.position.x)
            GetComponent<EnemyState>()._facingleft = true;
        else if (Player.transform.position.x > transform.position.x)
            GetComponent<EnemyState>()._facingleft = false;
    }

    IEnumerator BossJump()
    {
        BossJumpIsRunning = true;

        yield return new WaitForSeconds(2f);

        if (BossJumpIsRunning)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(forcex * -GetComponent<EnemyState>()._directionmodifier, forcey));
            StartCoroutine(DropPoison());
        }

    }

    IEnumerator DropPoison()
    {
        while (BossJumpIsRunning)
        {
            yield return new WaitForSeconds((forcex / 500));
            Instantiate(PoisonPool, transform.position - new Vector3(0, -0.5f, 0), Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            if (_hopping)
            {
                StartCoroutine(Hop());
            }
            else
                ResetandJumpAgain();
        }

        if (collision.gameObject.tag == "Wall")
        {
            BossJumpIsRunning = false;
            if (_hopping && _hopnumber > 3)
            {
                _hopping = false;
                _hoppingdamage = false;

                BounceOffWall(collision.gameObject);
            }
            else if(_hopping && _hopnumber <= 3)
            {
                HopDropOffWall();
            }
            else
            {
                BounceOffWall(collision.gameObject);
            }            
        }

        if (collision.gameObject.tag == "Player")
        {
            if (_hoppingdamage)
                EventManager.PlayerIsHit();
        }
    }

    void HopDropOffWall()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(20, -20f));
        HopDirection = new Vector2(forcex * -GetComponent<EnemyState>()._directionmodifier / 5, forcey / 1.5f);
    }

    void BounceOffWall(GameObject col)
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Rigidbody2D>().AddForce(new Vector2((transform.position.x - col.transform.position.x) * 100, 0));        
    }


    void ResetandJumpAgain()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        StartCoroutine(BossJump());
    }


    public bool _hopping = false;
    public bool _hoppingdamage = false;

    public int _hopnumber = 0;


    public void BossisHit()
    {
        if (_hopping)
            _hoppingdamage = false;

        StartCoroutine(StartHop());
    }

    public IEnumerator StartHop()
    {
        _hopnumber = 0;
        BossJumpIsRunning = false;
        _hopping = true;

        yield return new WaitForSeconds(1f);

        HopDirection = new Vector2(forcex * -GetComponent<EnemyState>()._directionmodifier / 5, forcey / 1.5f);
        StartCoroutine(Hop());
    }

    IEnumerator Hop()
    {
        _hopnumber++;
        _hoppingdamage = false;

        Debug.Log("Hop");
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Rigidbody2D>().AddForce(HopDirection);
        StartCoroutine(HopValue());
        yield return new WaitForEndOfFrame();

    }

    IEnumerator HopValue()
    {
        while (GetComponent<Rigidbody2D>().velocity.y >= 0)
        {
            yield return new WaitForEndOfFrame();
            Debug.Log("HopValue");
            
        }
        if (_hopping)
            _hoppingdamage = true;
    }
}