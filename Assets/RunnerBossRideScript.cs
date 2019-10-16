using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerBossRideScript : MonoBehaviour
{
    public GameObject[] RideNodes;
    public Sprite Rider;

    private void Start()
    {
        RideSet(GetComponent<RunnerBoss>().absorption);
    }    

    public void RideSet(int i)
    {
        int ride = i;
        foreach(GameObject item in RideNodes)
        {
            if (ride > 0)
                item.GetComponent<SpriteRenderer>().sprite = Rider;
            else
                item.GetComponent<SpriteRenderer>().sprite = null;

            ride--;
        }
    }
}
