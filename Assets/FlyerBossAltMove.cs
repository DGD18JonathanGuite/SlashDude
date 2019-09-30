using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerBossAltMove : MonoBehaviour
{
    GameObject Player;
    public float attackspeed = 50;

    bool slicing = false;
    bool floorishit = false;

    void Start()
    {
        Player = GameObject.Find("Player");
    }

    public IEnumerator SliceDown(float airheight)
    {
        slicing = true;
        int count = 5;

        while(count  > 0)
        {
            floorishit = false;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            transform.position = new Vector2(Player.transform.position.x, airheight);
            yield return new WaitForSeconds(1f);
            count--;

            if(count <= 0)
            GetComponent<FlyerBoss>().AltAttack = false;

            GetComponent<EnemyState>()._attacking = true;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -attackspeed));

            if (count <= 0)
            {

            }
            else
            {
                yield return new WaitUntil(() => floorishit);
                Debug.Log("FloorIsHit");
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                yield return new WaitForSeconds(0.2f);
            }
        }

        slicing = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Floor" && GetComponent<FlyerBoss>().AltAttack)
        {
            if(slicing)
            {
                GetComponent<FlyerBoss>().Stop();
                floorishit = true;
            }
        }
    }

    public void ResetCoroutines()
    {
        StopAllCoroutines();
    }
}