using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float Grav;
    public PlayerStats Status;

    public bool[] LevelsFinished;
    public bool leftdoor;

    public int spawnnumber;

    private void OnEnable()
    {
        EventManager.UpdateStats += UpdateStat;
        EventManager.ChangeLevel += ResetStat;        
    }
    

    private void OnDisable()
    {
        EventManager.UpdateStats -= UpdateStat;
        EventManager.ChangeLevel -= ResetStat;
    }

    private void Start()
    {
        UpdateStat();
        PlayerStats.getInstance()._grav = Grav;
    }

    void UpdateStat()
    {
        Status = PlayerStats.getInstance();
    }

    void ResetStat()
    {
        PlayerStats.getInstance()._ischarging = false;
        PlayerStats.getInstance()._ismoving = false;
        PlayerStats.getInstance()._jumping = false;
    }

    public void RestartGameStats()
    {
        for(int i = 0; i < LevelsFinished.Length; i++)
        {
            LevelsFinished[i] = false;
        }

        PlayerStats.getInstance()._playerhealth = 3;        

        PlayerStats.getInstance()._ischarging = false;
        PlayerStats.getInstance()._ismoving = false;
        PlayerStats.getInstance()._jumping = false;
    }
}