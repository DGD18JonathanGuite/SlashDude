using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteMovement : MonoBehaviour
{
    bool _moveanalysis = false;

    public float jumpheight = 0;
    public float gravdamp = 0;
    public float gravgain = 1;
    public int dashspeed = 0;
    
    private void OnEnable()
    {
        EventManager.CheckforItems += CheckItem;
    }

    private void OnDisable()
    {
        EventManager.CheckforItems -= CheckItem;
    }

    void CheckItem(int code)
    {
        if((code == 0|| code == 1) && !_moveanalysis)
        {
            EventManager.ExecuteMov += MoveAnalysis;
            _moveanalysis = true;
        }
    }


    void MoveAnalysis(int chargenumber)
    {
        float xmove = 0;
        float ymove = 0;

        if (PlayerStats.getInstance()._istakendash)
        {
            GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            xmove = PlayerDashCalculations.DashValue(chargenumber, dashspeed);
        }
        else
        {
            if (!PlayerStats.getInstance()._jumping)
                xmove = chargenumber * 20;
            else
                xmove = 0;

            PlayerStats.getInstance()._candash = false;
        }



        //Get Jump Value
        if (PlayerStats.getInstance()._istakenjump)
        {
            if (!PlayerStats.getInstance()._jumping)
                ymove = jumpheight; //Mathf.Abs(chargenumber) * jumpheight / 2;
            else if (PlayerStats.getInstance()._istakendash && PlayerStats.getInstance()._jumping)
                ymove = -100;
        }

        StartCoroutine(Move(xmove, ymove, chargenumber));
    }

    IEnumerator Move(float _xmove, float _ymove, int charge)
    {
        bool dashing = false;

        if (PlayerStats.getInstance()._candash)
        {
            dashing = true;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }


        EventManager.ChangePlayerSpriteAnimation(2);
        yield return new WaitForSeconds(0.05f);


        Debug.Log("Xmove: " + _xmove + "YMove: " + _ymove);

        if(!PlayerStats.getInstance()._jumping && PlayerStats.getInstance()._istakenjump)
        {
            StartCoroutine(PullDown(charge, _ymove));
        }

        GetComponent<Rigidbody2D>().AddForce(new Vector2(_xmove, _ymove));

        if (_ymove > 0)
        {
            PlayerStats.getInstance()._jumping = true;
        }

        yield return new WaitForSeconds(0.01f);

        if (PlayerStats.getInstance()._jumping)
        {
            if (PlayerStats.getInstance()._istakendash)
                PlayerStats.getInstance()._candash = true;
            if (!PlayerStats.getInstance()._istakendash)
                StartCoroutine(DownwardSlash());
        }


        if (dashing)
        {
            yield return new WaitForSeconds(0.02f);
            StartCoroutine(Slash());
            yield return new WaitForSeconds(0.02f);

            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Discrete;
    }

    IEnumerator Slash()
    {        
        PlayerStats.getInstance()._canattack = true;
        yield return new WaitForSeconds(0.6f);
        PlayerStats.getInstance()._canattack = false;
        GetComponent<Rigidbody2D>().gravityScale = PlayerStats.getInstance()._grav;
    }

    IEnumerator DownwardSlash()
    {
        while (GetComponent<Rigidbody2D>().velocity.y > 0)
            yield return new WaitForEndOfFrame();

        PlayerStats.getInstance()._canattack = true;          
    }

    IEnumerator PullDown(int charge, float ymove)
    {

        float top = (gravdamp * Mathf.Abs(charge));

        GetComponent<Rigidbody2D>().gravityScale -= top;

        for (int i = 0; i < top/gravgain; i++)
        {
            GetComponent<Rigidbody2D>().gravityScale += gravgain;
            yield return new WaitForSeconds(0.1f);
        }
        GetComponent<Rigidbody2D>().gravityScale = PlayerStats.getInstance()._grav;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            PlayerStats.getInstance()._jumping = false;

            if (PlayerStats.getInstance()._istakenjump)
            {
                PlayerStats.getInstance()._candash = false;
                PlayerStats.getInstance()._canattack = false;
            }

            GetComponent<Rigidbody2D>().gravityScale = PlayerStats.getInstance()._grav;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;            
        }
    }
}