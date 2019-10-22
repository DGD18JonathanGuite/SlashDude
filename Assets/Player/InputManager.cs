using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    //DEBUG
    private void Start()
    {
        //Debug.Log(PlayerStats.getInstance()._istakendash);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) /*&& !Input.GetKey(KeyCode.D)*/)
            EventManager.Movement(true);
        if (Input.GetKeyDown(KeyCode.D) /*&& !Input.GetKey(KeyCode.A)*/)
            EventManager.Movement(false);

        //if ((Input.GetKeyUp(KeyCode.A) || (Input.GetKeyUp(KeyCode.D))) && ((!Input.GetKey(KeyCode.A) || (!Input.GetKey(KeyCode.D)))))
        //    EventManager.Stop(1);

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A))
                EventManager.Movement(true);
            else if (Input.GetKey(KeyCode.D))
                EventManager.Movement(false);
            else
                EventManager.Stop(1);
        }



        //0 is dash
        //1 is jump

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    PlayerStats.getInstance()._istakendash = true;
        //    EventManager.CheckforItems(0);
        //}
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    PlayerStats.getInstance()._istakenjump = true;
        //    EventManager.CheckforItems(1);

        //    Debug.Log("Random [0,3] = " + Random.Range(0, 3));
        //}
    }
}
