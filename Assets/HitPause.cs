using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPause : MonoBehaviour
{
    public float hitstunduration = 0.2f;
    bool frozen = false;

    private void OnEnable()
    {
        EventManager.EnemyisHit += Freeze;
    }

    private void OnDisable()
    {
        EventManager.EnemyisHit -= Freeze;
    }

    void Freeze()
    {
        if(!frozen)
        StartCoroutine(_HitPause());
    }

    IEnumerator _HitPause()
    {
        frozen = true;
        var original = Time.timeScale;
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(hitstunduration);

        Time.timeScale = original;

        frozen = false;
    }
}
