using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteManager : MonoBehaviour
{
    public Sprite Static, Attack, Run;

    private void OnEnable()
    {
        EventManager.ChangePlayerSpriteAnimation += ChangeSprite;
        EventManager.Movement += ChangeRotation;
    }

    private void OnDisable()
    {
        EventManager.ChangePlayerSpriteAnimation -= ChangeSprite;
        EventManager.Movement -= ChangeRotation;
    }

    void ChangeSprite(int i)
    {
        if (i == 0)
            GetComponent<SpriteRenderer>().sprite = Static;
        if (i == 1)
            GetComponent<SpriteRenderer>().sprite = Attack;
        if (i == 2)
            GetComponent<SpriteRenderer>().sprite = Run;
    }

    void ChangeRotation(bool isleft)
    {
        if (isleft)
            transform.rotation = Quaternion.Euler(new Vector2(0, -180));
        if (!isleft)
            transform.rotation = Quaternion.Euler(new Vector2(0, 0));
    }
}
