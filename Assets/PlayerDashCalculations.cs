using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashCalculations
{
    public static float DashValue(int chargenumber, float dashspeed)
    {
        float xmove = 0;

        if (PlayerStats.getInstance()._istakenjump)
        {
            if (PlayerStats.getInstance()._jumping)
            {
                Debug.Log(chargenumber);
                xmove = (chargenumber / Mathf.Abs(chargenumber)) * 2 * dashspeed;
                //ymove = -100;
                PlayerStats.getInstance()._candash = true;

            }
            else if (!PlayerStats.getInstance()._jumping)
            {
                xmove = (chargenumber / Mathf.Abs(chargenumber)) * 50;
                PlayerStats.getInstance()._candash = false;
            }            
        }
        else
        {
            xmove = chargenumber * dashspeed;
            PlayerStats.getInstance()._candash = true;         
        }
        return xmove;
    }
}
