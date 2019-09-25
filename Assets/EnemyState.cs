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

    public int _directionmodifier = 1;
    bool facingleft = false;
    public bool _facingleft
    {
        get
        {
            return facingleft;
        }

        set
        {
            facingleft = value;
            if(value)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                _directionmodifier = 1;
                
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = true;
                transform.rotation = Quaternion.Euler(0, -180, 0);
                _directionmodifier = -1;
            }
        }
    }

    int bosshealth = 0;
    public int _bosshealth
    {
        get
        {
            return bosshealth;
        }

        set
        {
            bosshealth = value;
        }
    }
}