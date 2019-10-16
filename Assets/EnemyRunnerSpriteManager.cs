using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunnerSpriteManager : MonoBehaviour
{
    public Animator EnemyAnimator;

    public void ChangeRunnerSprite()
    {
        if (GetComponent<EnemyState>()._attacking)
            EnemyAnimator.SetBool("RunnerisAttacking", true);
        else if(!GetComponent<EnemyState>()._attacking)
            EnemyAnimator.SetBool("RunnerisAttacking", false);

        if(GetComponent<EnemyState>()._running)
            EnemyAnimator.SetBool("RunnerisRunning", true);
        else if(!GetComponent<EnemyState>()._running)
            EnemyAnimator.SetBool("RunnerisRunning", false);
    }
}
