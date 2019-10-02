using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float Grav;
    public PlayerStats Status;

    private void Start()
    {
        UpdateStat(0);
        PlayerStats.getInstance()._grav = Grav;
    }

    private void OnEnable()
    {
        EventManager.UpdateStats += UpdateStat;
    }

    private void OnDisable()
    {
        EventManager.UpdateStats -= UpdateStat;
    }

    void UpdateStat(int i)
    {
        Status = PlayerStats.getInstance();
    }


}
