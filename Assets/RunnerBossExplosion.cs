using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerBossExplosion : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject Node;

    void Start()
    {
        StartCoroutine(LifeTime());
    }

    private void Update()
    {
        transform.position = Node.transform.position;
    }

    public void SetNode(GameObject _node)
    {
        Node = _node;
    }


    // Update is called once per frame
    IEnumerator LifeTime()
    {
        while (gameObject)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
            yield return new WaitForSeconds(0.2f);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            EventManager.PlayerIsHit();
    }
}