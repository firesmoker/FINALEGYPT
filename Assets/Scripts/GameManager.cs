using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public int currentSceneIndex;
    public float yDeathLimit;
    public GameObject player;
    public AudioClip bgMusic;
    private AudioSource audio;
    public static int scenePlayerDied;

    // Use this for initialization
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneIndex != 0)
        {
            scenePlayerDied = currentSceneIndex;
            audio = GetComponent<AudioSource>();
            audio.clip = bgMusic;
            audio.Play();
        }
    }

    private void Update()
    {
        if(player != null)
        {
            if (player.transform.position.y <= yDeathLimit)
            {
                GameOver();
            }
        }
    }

    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(1);
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public static void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartMenuScene");
    }

    public static void LoadOptionsScreen()
    {
        SceneManager.LoadScene("Options Screen");
    }

    public static void LoadNextScene()
    {
        SceneManager.LoadScene(0);
    }

    public static void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public static void GameOver()
    {
        //WaitForTime();
        //scenePlayerDied = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("GameOverScene");
    }

    //public static void GameOver(int sceneIndex)
    //{
    //    UIManager.scenePlayerDied = sceneIndex;
    //    SceneManager.LoadScene("GameOverScene");
    //}
    public static void GameOver(float delay)
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void GameOverDelay(float delay)
    {
        StartCoroutine(GameOverDelayRoutine(delay));
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator GameOverDelayRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.GameOver();
    }
}
