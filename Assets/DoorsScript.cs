using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsScript : MonoBehaviour
{
    public GameObject LeftDoor, RightDoor, LeftDoorHalf, RightdDoorHalf;

    private void OnEnable()
    {
        EventManager.OpenDoors += DoorOpen;
    }

    private void OnDisable()
    {
        EventManager.OpenDoors -= DoorOpen;
    }

    void DoorOpen()
    {
        Debug.Log("Open Door");

        LeftDoor.SetActive(false);
        RightDoor.SetActive(false);

        LeftDoorHalf.SetActive(true);
        RightdDoorHalf.SetActive(true);
    }
}
