using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPoolScript : MonoBehaviour
{
    public Vector2 _flatcollisionsize;
    public Vector2 _fallingcollisionsize;
    public Vector2 _flatsize;
    public Vector2 _fallingsize;

    public Sprite _flatsprite;
    public Sprite _fallingsprite;

    public bool _playerisburning = false;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = _fallingsprite;
        transform.localScale = _fallingsize;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Floor")
        {
            Flat();
        }

        if (collision.tag == "Player")
            _playerisburning = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            _playerisburning = false;
    }

    void Flat()
    {
        GetComponent<SpriteRenderer>().sprite = _flatsprite;
        transform.localScale = _flatsize;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnDestroy()
    {
        GameObject.Find("Player").GetComponent<PlayerCollision>().CheckforStillBurns();
    }
}