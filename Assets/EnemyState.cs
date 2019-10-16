﻿using System.Collections;
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
            if (value)
            {
                //GetComponent<SpriteRenderer>().flipX = false;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                _directionmodifier = 1;

            }
            else
            {
                //GetComponent<SpriteRenderer>().flipX = true;
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

    int bossstate = 0;
    public int _bossstate
    {
        get
        {
            return bossstate;
        }

        set
        {
            bossstate = value;
        }
    }
    //Boss States
    //0 >> normal
    //1 >> one type is dead
    //2 >> another type is dead
    //3 >> both types are dead
}