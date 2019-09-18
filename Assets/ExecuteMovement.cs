using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteMovement : MonoBehaviour
{
    bool _moveanalysis = false;

    //public bool _dashistaken = false;
    //public bool _jumpistaken = false;

    public float jumpheight = 0;
    public int dashspeed = 0;
    
    private void OnEnable()
    {
        EventManager.CheckforItems += CheckItem;

        //DEBUG
        //EventManager.ExecuteMov += MoveAnalysis;
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
        //Debug.Log("MoveA");
        float xmove = 0;
        float ymove = 0;

        //Get Dash Value
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
                ymove = Mathf.Abs(chargenumber) * jumpheight / 2;
            else if (PlayerStats.getInstance()._istakendash && PlayerStats.getInstance()._jumping)
                ymove = -100;
        }

        StartCoroutine(Move(xmove, ymove));
    }

    IEnumerator Move(float _xmove, float _ymove)
    {
        bool dashing = false;
        float Grav = GetComponent<Rigidbody2D>().gravityScale;

        if (PlayerStats.getInstance()._candash)
        {
            dashing = true;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }


        EventManager.ChangePlayerSpriteAnimation(2);
        yield return new WaitForSeconds(0.05f);


        Debug.Log("Xmove: " + _xmove + "YMove: " + _ymove);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(_xmove, _ymove));

        if(_ymove > 0)
        PlayerStats.getInstance()._jumping = true;

        yield return new WaitForSeconds(0.01f);

        if (PlayerStats.getInstance()._jumping)
            PlayerStats.getInstance()._candash = true;

        if (dashing)
        {
            yield return new WaitForSeconds(0.04f);

            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().gravityScale = Grav;            
        }
        GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Discrete;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            PlayerStats.getInstance()._jumping = false;

            if(PlayerStats.getInstance()._istakenjump)
            PlayerStats.getInstance()._candash = false;

            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}