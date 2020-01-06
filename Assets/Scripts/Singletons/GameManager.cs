using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoSingleton<GameManager>
{
    static FMOD.Studio.EventInstance bgFmodMusic;
    public int currentSceneIndex;
    private static float _yDeathLimit = -30f;
    public static float yDeathLimit
    {
        get
        {
            return _yDeathLimit;
        }
    }
    public GameObject player;
    public GameObject bg;
    public AudioClip bgMusic;
    private AudioSource audioSource;
    public static int scenePlayerDied;

    void Start()
    {
        if(GameObject.Find("Background Music") == null)
        {
            bg = new GameObject("Background Music");
            DontDestroyOnLoad(bg);
            bgFmodMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music");
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(bgFmodMusic, bg.GetComponent<Transform>(), bg.GetComponent<Rigidbody2D>());
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (currentSceneIndex != 1)
            {
                bgFmodMusic.start();
            }
        }
        else
        {
            bg = GameObject.Find("Background Music");
            
            FMOD.Studio.PLAYBACK_STATE playbackState;
            bgFmodMusic.getPlaybackState(out playbackState);
            bool isPlaying = playbackState != FMOD.Studio.PLAYBACK_STATE.STOPPED;
            
            if(!isPlaying)
                bgFmodMusic.start();
        }
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
    }

    public static void FmodChange()
    {
        bgFmodMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
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
