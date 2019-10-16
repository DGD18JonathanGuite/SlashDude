using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsScript : MonoBehaviour
{
    public GameObject LeftDoor, RightDoor, LeftDoorHalf, RightDoorHalf;


    private void Start()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>()._levelnumber == 0)
        {
            DecideDoorDestination();
        }
    }

    void DecideDoorDestination()
    {
        int ldoor;
        int rdoor;

        ldoor = Random.Range(0, 3);
        rdoor = Random.Range(0, 3);

        while (GameObject.Find("Stats").GetComponent<Stats>().LevelsFinished[ldoor])
        {
            ldoor = Random.Range(0, 3);
        }

        int levelsfinished = 0;

        for (int i = 0; i < GameObject.Find("Stats").GetComponent<Stats>().LevelsFinished.Length; i++)
        {
            if (GameObject.Find("Stats").GetComponent<Stats>().LevelsFinished[i])
                levelsfinished++;
        }

        //Debug.Log("Levels finished: " + levelsfinished);

        while ((GameObject.Find("Stats").GetComponent<Stats>().LevelsFinished[rdoor] || rdoor == ldoor) && levelsfinished < 2)
        {
            rdoor = Random.Range(0, 3);
        }

        if (levelsfinished >= 2)
        {
            Debug.Log("shut right door");
            rdoor = -1;
        }

        LeftDoorHalf.transform.GetChild(0).GetComponent<DoorIndiv>().doorid = ldoor;
        RightDoorHalf.transform.GetChild(0).GetComponent<DoorIndiv>().doorid = rdoor;

        DoorOpen();
    }

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
        if (GameObject.Find("GameManager").GetComponent<GameManager>()._levelnumber > 0)
        {
            Debug.Log("Open One Side");

            if (GameObject.Find("Stats").GetComponent<Stats>().leftdoor)
            {
                LeftDoor.SetActive(false);
                LeftDoorHalf.SetActive(true);
            }
            else if(!GameObject.Find("Stats").GetComponent<Stats>().leftdoor)
            {
                RightDoor.SetActive(false);
                RightDoorHalf.SetActive(true);
            }
        }
        else
        {
            LeftDoor.SetActive(false);
            LeftDoorHalf.SetActive(true);

            if (RightDoorHalf.transform.GetChild(0).GetComponent<DoorIndiv>().doorid >= 0)
            {
                RightDoor.SetActive(false);
                RightDoorHalf.SetActive(true);
            }
            else
            {
                RightDoor.SetActive(true);
                RightDoorHalf.SetActive(false);
            }
        }
    }
}
