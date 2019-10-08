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
        EventManager.PlayerisDead += PlayerDied;

        if (!GameObject.Find("Stats"))
            Stats.SetActive(true);
    }

    private void OnDisable()
    {
        EventManager.ChangeLevel -= ChangeLevel;
        EventManager.PlayerisDead -= PlayerDied;
    }

    public int _levelnumber = 0;

    private void Start()
    {
        
        DontDestroyOnLoad(GameObject.Find("Stats"));
        _levelnumber = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Level Number: " + _levelnumber);
    }

    public void ChangeLevel()
    {        
        
        if ((SceneManager.GetActiveScene().buildIndex) < 3)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            SceneManager.LoadScene(0);
        //Debug.Log("Is the next scene available? " + SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1).IsValid());
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void PlayerDied()
    {
        StartCoroutine(PlayerDeath());
    }

    IEnumerator PlayerDeath()
    {
        GameObject.Find("Stats").GetComponent<Stats>().RestartGameStats();

        yield return new WaitForSeconds(1.4f);
        SceneManager.LoadScene(0);
    }
}
