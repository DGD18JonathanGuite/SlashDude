using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalMovement : MonoBehaviour
{
    //NEEDED TO HAVE THE PLAYER SWITCH DIRECTIONS FREELY
    bool MovementTriggered = false;

    public void Movement(bool left)
    {
        StartCoroutine(CheckforCharge(left));
    }

    IEnumerator CheckforCharge(bool left)
    {
        yield return new WaitForEndOfFrame();
        if (!PlayerStats.getInstance()._ischarging)
            StartMoving(left);
    }

    public void StartMoving(bool left)
    {
        MovementTriggered = true;

        PlayerStats.getInstance()._isstopping = false;
        StartCoroutine(CalibrateSpeed(left));

        if (!PlayerStats.getInstance()._ismoving)
            StartCoroutine(StartMovement());
    }

    bool _keepmoving = false;

    private void FixedUpdate()
    {
        //if (_keepmoving)
        //{
        //    GetComponent<Rigidbody2D>().AddForce(new Vector2(GetComponent<PlayerMovement>().horizontalmov * 2, 0));
        //}
    }

    IEnumerator StartMovement()
    {
        PlayerStats.getInstance()._ismoving = true;
        _keepmoving = true;

        yield return new WaitForSeconds(0.01f);
        
        while (_keepmoving)
        {
            //Debug.Log("Moving at: " + GetComponent<PlayerMovement>().horizontalmov * 2);

            //if (PlayerStats.getInstance()._jumping)
            //    GetComponent<Rigidbody2D>().AddForce(new Vector2(GetComponent<PlayerMovement>().horizontalmov * 10, 0));
            //else
            GetComponent<Rigidbody2D>().AddForce(new Vector2(GetComponent<PlayerMovement>().horizontalmov * GetComponent<PlayerMovement>().speedadjust * Time.deltaTime, 0));
            //transform.position += new Vector3(GetComponent<PlayerMovement>().horizontalmov*Time.deltaTime, 0, 0);

            //  yield return new WaitForSeconds(0.01f);
            yield return new WaitForEndOfFrame();

            if (GetComponent<PlayerMovement>().horizontalmov == 0 && PlayerStats.getInstance()._isstopping)
                _keepmoving = false;
        }

        PlayerStats.getInstance()._ismoving = false;
    }

    IEnumerator CalibrateSpeed(bool left)
    {
        bool condition;
        //Debug.Log("ReCalibrate");
        //if (PlayerStats.getInstance()._istakenjump)
        //    condition = PlayerStats.getInstance()._jumping;
        //else
        //    condition = PlayerStats.getInstance()._isstopping;

       condition = PlayerStats.getInstance()._isstopping;

        while (!condition)
        {
            if (!left && GetComponent<PlayerMovement>().horizontalmov < GetComponent<PlayerMovement>().horizontalmax)
            {
                GetComponent<PlayerMovement>().horizontalmov += GetComponent<PlayerMovement>().speed;
            }
            else if (left && GetComponent<PlayerMovement>().horizontalmov > -GetComponent<PlayerMovement>().horizontalmax)
            {
                GetComponent<PlayerMovement>().horizontalmov -= GetComponent<PlayerMovement>().speed;
            }

            yield return new WaitForSeconds(0.01f);

            if (PlayerStats.getInstance()._istakenjump)
                condition = PlayerStats.getInstance()._jumping;
            else
                condition = PlayerStats.getInstance()._isstopping;
        }
    }

    public void Stop(int one)
    {
        //Debug.Log("Stop");
        PlayerStats.getInstance()._isstopping = true;

        EventManager.ChangePlayerSpriteAnimation(0);

        StartCoroutine(Stop());
    }

    IEnumerator Stop()
    {
        MovementTriggered = false;

        //DEBUG
        //Debug.Log("Velocity " + GetComponent<Rigidbody2D>().velocity.x);
        if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > 1)
        PlayerStats.getInstance()._canattack = true;

        yield return new WaitForSeconds(0.01f);

        while (PlayerStats.getInstance()._isstopping && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > 0.5f || PlayerStats.getInstance()._ismoving)
        {
          //Debug.Log("Stopping");

            yield return new WaitForSeconds(0.01f);

            if (GetComponent<PlayerMovement>().horizontalmov > 0)
            {
                if (GetComponent<PlayerMovement>().horizontalmov < -2 * GetComponent<PlayerMovement>().speed)
                {
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    PlayerStats.getInstance()._isstopping = false;
                }
                else
                    GetComponent<PlayerMovement>().horizontalmov -= GetComponent<PlayerMovement>().speed;
            }
            else if (GetComponent<PlayerMovement>().horizontalmov < 0)
            {
                if (GetComponent<PlayerMovement>().horizontalmov > 2 * GetComponent<PlayerMovement>().speed)
                {
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    PlayerStats.getInstance()._isstopping = false;
                }
                else
                    GetComponent<PlayerMovement>().horizontalmov += GetComponent<PlayerMovement>().speed;
            }

            if (MovementTriggered)
                break;

        }

        PlayerStats.getInstance()._canattack = false;
        PlayerStats.getInstance()._isstopping = false;
    }
}