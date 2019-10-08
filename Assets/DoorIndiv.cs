using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorIndiv : MonoBehaviour
{
    public Material Runner, Poison, Fly;
    public int _doorid;
    public int doorid
    {
        get
        {
            return _doorid;
        }

        set
        {
            _doorid = value;

            if (value == 0)
            {
                Debug.Log("Color0");
                GetComponent<SpriteRenderer>().material = Runner;
            }
            if (value == 1)
            {
                GetComponent<SpriteRenderer>().material = Poison;
                Debug.Log("Color1");
            }
            if (value == 2)
            {
                GetComponent<SpriteRenderer>().material = Fly;
                Debug.Log("Color2");
            }
        }
    }
}
