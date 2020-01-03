using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
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

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartMenuScene");
    }

    public void LoadOptionsScreen()
    {
        SceneManager.LoadScene("Options Screen");
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        WaitForTime();
        SceneManager.LoadScene("GameOverScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
