using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    int currentSceneIndex;
    public float yDeathLimit;
    public GameObject player;
    public AudioClip bgMusic;
    private AudioSource audio;

    // Use this for initialization
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        audio = GetComponent<AudioSource>();
        audio.clip = bgMusic;
        audio.Play();
    }

    private void Update()
    {
        if (player.transform.position.y <= yDeathLimit)
        {
            GameOver();
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

    public static void GameOver()
    {
        //WaitForTime();
        SceneManager.LoadScene("GameOverScene");
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
