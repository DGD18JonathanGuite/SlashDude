using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    bool attacking = false;
    public bool _attacking
    {
        get
        {
            return attacking;
        }

        set
        {
            attacking = value;
            if (value)
                GetComponent<SpriteRenderer>().sprite = GetComponent<EnemySpriteManager>().ChangeSprite(2);
            else
                GetComponent<SpriteRenderer>().sprite = GetComponent<EnemySpriteManager>().ChangeSprite(0);
        }
    }

    bool running = false;
    public bool _running
    {
        get
        {
            return running;
        }

        set
        {
            running = value;
            if (value)
            {
                GetComponent<SpriteRenderer>().sprite = GetComponent<EnemySpriteManager>().ChangeSprite(1);
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = GetComponent<EnemySpriteManager>().ChangeSprite(0);
            }
        }
    }
}
