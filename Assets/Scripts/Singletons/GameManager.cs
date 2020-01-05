using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoSingleton<GameManager>
{
    public int currentSceneIndex;
    public float yDeathLimit;
    public GameObject player;
    public AudioClip bgMusic;
    private AudioSource audioSource;
    public static int scenePlayerDied;
    //public PostProcessVolume postProcessVolume;

    // Use this for initialization
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneIndex != 1)
        {
            scenePlayerDied = currentSceneIndex;
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = bgMusic;
            audioSource.Play();
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
    
    //IEnumerator FadeToBlack(float time)
    //{
    //    //f(postProcessVolume != null)
    //    //
    //    //   ColorParameter colorFillter = postProcessVolume.GetComponent<ColorGrading>().colorFilter;
    //    //   while (colorFillter.value.grayscale > 0)
    //    //   {
    //    //       yield return new WaitForSeconds(time);
    //    //       Color color = colorFillter.value;
    //    //       color.in
    //    //       colorFillter.value = color;
    //    //
    //    //   }
    //    //
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
