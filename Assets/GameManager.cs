using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.ChangeLevel += ChangeLevel;
    }

    private void OnDisable()
    {
        EventManager.ChangeLevel -= ChangeLevel;
    }

    public int _levelnumber = 0;

    private void Start()
    {
        _levelnumber = SceneManager.GetActiveScene().buildIndex;
    }

    public void ChangeLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
