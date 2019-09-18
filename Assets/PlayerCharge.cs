using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharge : MonoBehaviour
{
    bool _isleft = false;

    bool chargeisset = false;

    int chargenumber = 0;
    int _chargenumber
    {
        get
        {
            return chargenumber;
        }
        set
        {
            chargenumber = value;
            if (value == 1 || value == -1)
                EventManager.ChangePlayerSpriteAnimation(0);
            else if (value == 2 || value == -2)
                EventManager.ChangePlayerSpriteAnimation(1);
            else if (value == 5 || value == -5)
                EventManager.ChangePlayerSpriteAnimation(2);
            else if (value == 0)
                EventManager.ChangePlayerSpriteAnimation(0);
        }
    }

    private void OnEnable()
    {
        EventManager.CheckforItems += Check;
    }
    //DEBUG
    //EventManager.Movement += Charge;
    //EventManager.Stop += StopCharging;

    private void OnDisable()
    {
        EventManager.CheckforItems -= Check;
        EventManager.Movement -= Charge;
    }

    void Check(int i)
    {
        if ((PlayerStats.getInstance()._istakendash || PlayerStats.getInstance()._istakenjump || PlayerStats.getInstance()._istakenexplosion || PlayerStats.getInstance()._istakenpoison) && !chargeisset)
        {
            chargeisset = true;
            EventManager.Movement += Charge;
            EventManager.Stop += StopCharging;
        }
    }

    void Charge(bool isleft)
    {
        //Debug.Log("Charge");
        if (!PlayerStats.getInstance()._jumping)
        {
            PlayerStats.getInstance()._ischarging = true;
            _isleft = isleft;
            StartCoroutine(ChargingDash(isleft));
        }
        else
        {
            int _cnumber = 1;
            if (isleft)
                _cnumber = -_cnumber;
            EventManager.ExecuteMov(_cnumber);
        }
            
    }

    void StopCharging(int i)
    {
        PlayerStats.getInstance()._ischarging = false;
    }


    IEnumerator ChargingDash(bool isleft)
    {
        int timer = 0;

        while (PlayerStats.getInstance()._ischarging)
        {
            if (timer < 10)
                timer += 1;
            yield return new WaitForSeconds(0.1f);

            if (timer > 3)
            {
                if (timer > 6)
                {
                    if (timer > 9)
                    {
                        _chargenumber = 3;
                    }
                }
                else
                    _chargenumber = 2;
            }

            else
                _chargenumber = 1;
        }

        if (isleft)
            _chargenumber = -_chargenumber;
        EventManager.ExecuteMov(_chargenumber);
        _chargenumber = 0;
    }
}