using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public ParticleSystem PlayerHurt;

    private void OnEnable()
    {
        EventManager.PlayerIsHit += _PlayerDamage;
    }

    private void OnDisable()
    {
        EventManager.PlayerIsHit -= _PlayerDamage;
    }

    void _PlayerDamage()
    {
        Debug.Log("PlayerisHit");
        Instantiate(PlayerHurt, transform.position, Quaternion.identity);
    }
}
