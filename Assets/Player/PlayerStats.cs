using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public float _grav;

    public bool _jumping = false;
    public bool _candash = false;

    bool ismoving = false;
    public bool _ismoving
    {
        get
        {
            return ismoving;
        }

        set
        {
            ismoving = value;
                        
            if (value == true)
            {                
                EventManager.ChangePlayerSpriteAnimation(2);
            }

            else if(value == false)
            {
                EventManager.ChangePlayerSpriteAnimation(-2);
            }
        }
    }


    public bool _ischarging = false;

    public bool _istakenexplosion = false;
    public bool _istakenpoison = false;

    public int _playerhealth;

    bool canattack = false;
    public bool _canattack
    {
        get
        {
            return canattack;
        }
        set
        {
            canattack = value;
            if (value)
            {
                EventManager.ChangePlayerSpriteAnimation(1);
                EventManager.ChangeHitbox(1);
            }
            else
            {
                EventManager.ChangePlayerSpriteAnimation(-1);
                EventManager.ChangeHitbox(0);
            }
        }
    }

    bool istakendash = false;
    public bool _istakendash
    {
        get
        {
            return istakendash;
        }

        set
        {
            istakendash = value;
        }
    }

    bool istakenjump = false;
    public bool _istakenjump
    {
        get
        {
            return istakenjump;
        }

        set
        {
            istakenjump = value;
        }
    }

    bool stopping = false;
    public bool _isstopping
    {
        get
        {
            return stopping;
        }
        set
        {
            stopping = value;
            if (value == false)
            {
                EventManager.ChangePlayerSpriteAnimation(0);
            }
        }
    }

    private static PlayerStats _instance;
    public static PlayerStats getInstance()
    {
        if (_instance == null)
        {
            _instance = new PlayerStats();
        }
        return _instance;
    }

    public PlayerStats()
    {
        _playerhealth = 3;
    }
}