using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalmax = 10, speed = 0;
    public float verticalmov = 0, horizontalmov = 0;
    public float speedadjust = 100;

    public bool _isjumping;
    public bool _isfalling;

    
    
    void FixedUpdate()
    {
    }

    private void OnEnable()
    {
        EventManager.Movement += GetComponent<PlayerNormalMovement>().Movement;
        EventManager.Stop += GetComponent<PlayerNormalMovement>().Stop;

        EventManager.CheckforItems += CheckforDisable;
    }

    private void OnDisable()
    {
        EventManager.Movement -= GetComponent<PlayerNormalMovement>().Movement;
        EventManager.Stop -= GetComponent<PlayerNormalMovement>().Stop;

        EventManager.CheckforItems -= CheckforDisable;
    }

    void CheckforDisable(int i)
    {
        if (i == 0)
        {
            EventManager.Movement -= GetComponent<PlayerNormalMovement>().Movement;
            EventManager.Stop -= GetComponent<PlayerNormalMovement>().Stop;
        }
    }

}