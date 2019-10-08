using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public ParticleSystem PlayerHurt;

    private void OnEnable()
    {
        EventManager.PlayerIsHit += _PlayerDamage;
        EventManager.PlayerisDead += PlayerDied;
    }

    private void OnDisable()
    {
        EventManager.PlayerIsHit -= _PlayerDamage;
        EventManager.PlayerisDead -= PlayerDied;
    }

    void _PlayerDamage()
    {
        Debug.Log("PlayerisHit");
        Instantiate(PlayerHurt, transform.position, Quaternion.identity);
    }


    void PlayerDied()
    {
        Instantiate(PlayerHurt, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
