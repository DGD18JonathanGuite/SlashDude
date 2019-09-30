using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.PlayerIsHit += _PlayerDamage;
    }

    void _PlayerDamage()
    {

    }
}
