using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteManager : MonoBehaviour
{
    public Animator PlayerAnimator;
    public Sprite Static, Attack, Run;

    private void OnEnable()
    {
        EventManager.ChangePlayerSpriteAnimation += ChangeSprite;
        EventManager.Movement += ChangeRotation;
        EventManager.EnemyisHit += AttackAnim;
    }

    private void OnDisable()
    {
        EventManager.ChangePlayerSpriteAnimation -= ChangeSprite;
        EventManager.Movement -= ChangeRotation;
        EventManager.EnemyisHit -= AttackAnim;
    }

    void AttackAnim()
    {
        //Debug.Log("AttackAnimation");
        StartCoroutine(AttackAnimation());
    }

    IEnumerator AttackAnimation()
    {
        bool sliding, running;

        sliding = PlayerAnimator.GetBool("PlayerisSliding");
        running = PlayerAnimator.GetBool("PlayerisRunning");

        PlayerAnimator.SetBool("PlayerisSliding", false);
        PlayerAnimator.SetBool("PlayerisRunning", false);

        PlayerAnimator.SetBool("PlayerisAttacking", true);
        yield return new WaitForSeconds(0.02f);
        PlayerAnimator.SetBool("PlayerisAttacking", false);

        if(PlayerAnimator.GetBool("Left"))
            PlayerAnimator.SetBool("Left", false);
        else
            PlayerAnimator.SetBool("Left", true);

        PlayerAnimator.SetBool("PlayerisSliding", PlayerStats.getInstance()._canattack);
        PlayerAnimator.SetBool("PlayerisRunning", PlayerStats.getInstance()._ismoving);
    }

    void ChangeSprite(int i)
    {
        //if (i == 0)
        //    PlayerAnimator.SetBool("PlayerisRunning", false);

        if (i == 1)
            PlayerAnimator.SetBool("PlayerisSliding", true);
        else if (i == -1)
            PlayerAnimator.SetBool("PlayerisSliding", false);

        if (i == 2)
            PlayerAnimator.SetBool("PlayerisRunning", true);
        else if (i == -2)
            PlayerAnimator.SetBool("PlayerisRunning", false);
        
        //if (PlayerStats.getInstance()._canattack)
        //    PlayerAnimator.SetBool("PlayerisSliding", true);
        //else if (!PlayerStats.getInstance()._canattack)
        //    PlayerAnimator.SetBool("PlayerisSliding", false);

        //if (PlayerStats.getInstance()._ismoving)
        //    PlayerAnimator.SetBool("PlayerisRunning", true);
        //else if (!PlayerStats.getInstance()._ismoving)
        //    PlayerAnimator.SetBool("PlayerisRunning", false);
    }

    void ChangeRotation(bool isleft)
    {
        if (isleft)
            transform.rotation = Quaternion.Euler(new Vector2(0, -180));
        if (!isleft)
            transform.rotation = Quaternion.Euler(new Vector2(0, 0));
    }
}
