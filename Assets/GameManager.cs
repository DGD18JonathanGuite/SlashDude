using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Stats;

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
        DontDestroyOnLoad(GameObject.Find("Stats"));
        _levelnumber = SceneManager.GetActiveScene().buildIndex;
    }

    public void ChangeLevel()
    {
        if (SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1).IsValid())
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            SceneManager.LoadScene(0);
    }
}
