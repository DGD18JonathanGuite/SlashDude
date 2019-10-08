using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public float _grav;

    public bool _jumping = false;
    public bool _candash = false;

    public bool _ismoving = false;
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
                EventManager.ChangePlayerSpriteAnimation(0);
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
            if (!value)
                EventManager.ChangePlayerSpriteAnimation(2);
        }
    }

    private static PlayerStats _instance;
    public static PlayerStats getInstance()
    {
        if (_instance == null)
        {
            _instance = new PlayerStats(false, false, false, false, false, false, false, false , false, false, 0, 3);
        }
        return _instance;        
    }

    public PlayerStats(bool dash, bool jump, bool explosion, bool poison, bool jumping, bool canattack, bool candash, bool ismoving, bool ischarging, bool stopping, float grav, int health)
    {
        _istakendash = dash;
        _istakenjump = jump;
        _istakenexplosion = explosion;
        _istakenpoison = poison;

        _jumping = jumping;
        _canattack = canattack;
        _candash = candash;

        _ismoving = ismoving;
        _ischarging = ischarging;

        _isstopping = stopping;

        _grav = grav;

        _playerhealth = health;
    }
}