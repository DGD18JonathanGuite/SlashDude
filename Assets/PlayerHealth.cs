using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject[] HP;
    public int hitcount = 0;

    private void OnEnable()
    {
        EventManager.PlayerIsHit += HealthDown;
    }

    private void OnDisable()
    {
        EventManager.PlayerIsHit -= HealthDown;
    }

    private void Start()
    {
        for(int i = 0; i < PlayerStats.getInstance()._playerhealth; i++)
        {
            //Debug.Log("Playerhealth " + PlayerStats.getInstance()._playerhealth);
            HP[(HP.Length-1) - i].SetActive(true);
        }
    }

    void HealthDown()
    {
        HP[hitcount].SetActive(false);
        PlayerStats.getInstance()._playerhealth--;

        if(PlayerStats.getInstance()._playerhealth <= 0)
        {
            EventManager.PlayerisDead();
        }
        hitcount++;
    }
}
