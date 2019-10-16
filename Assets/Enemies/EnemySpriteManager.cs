using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteManager : MonoBehaviour
{
    public Sprite Running, Attacking, Idle;
    
    //public Sprite ChangeSprite(int i)
    //{
    //    if (i == 1)
    //        return Running;
    //    else if (i == 2)
    //        return Attacking;
    //    else
    //        return Idle;
    //}

    public void ChangeSprite()
    {
        if (GetComponent<EnemyRunner>())
            GetComponent<EnemyRunnerSpriteManager>().ChangeRunnerSprite();
        //if(GetComponent<JumperEnemy>())
        //    GetComponent<>
    }
}
