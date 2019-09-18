using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{    
    public PlayerStats Status;

    private void Start()
    {
        UpdateStat(0);
        Debug.Log(PlayerStats.getInstance()._istakenjump);
    }

    private void OnEnable()
    {
        EventManager.UpdateStats += UpdateStat;
    }

    void UpdateStat(int i)
    {
        Status = PlayerStats.getInstance();
    }


}
