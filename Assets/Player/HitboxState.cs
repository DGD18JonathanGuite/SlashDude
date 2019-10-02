using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxState : MonoBehaviour
{
    Vector2 OriginalSize;
    Vector2 OriginalOffset;

    private void Start()
    {
    }

    private void OnEnable()
    {
        OriginalSize = GetComponent<BoxCollider2D>().size;
        OriginalOffset = GetComponent<BoxCollider2D>().offset;
        EventManager.ChangeHitbox += ChangeAttackHitbox;
    }

    private void OnDisable()
    {
        EventManager.ChangeHitbox -= ChangeAttackHitbox;
    }

    void ChangeAttackHitbox(int state)
    {
        if (state == 0)
            Idle();
        else if (state == 1)
            Attacking();
    }

    void Attacking()
    {
        if (PlayerStats.getInstance()._jumping)
        {
            GetComponent<BoxCollider2D>().size = new Vector2(0.2f, 0.3f);
            GetComponent<BoxCollider2D>().offset = new Vector2(0.17f, -0.08f);
        }
    }

    void Idle()
    {
        GetComponent<BoxCollider2D>().size = OriginalSize;
        GetComponent<BoxCollider2D>().offset = OriginalOffset;
    }
}
