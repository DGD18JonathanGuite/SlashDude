using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteManager : MonoBehaviour
{
    public Sprite Static, Attack, Run;

    private void OnEnable()
    {
        EventManager.ChangePlayerSpriteAnimation += ChangeSprite;
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
}
