using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoSingleton<GameManager>
{
    private static GameManager _instance;
    public static new GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("SpawnManager is null");
            return _instance;
        }

    }

    static FMOD.Studio.EventInstance bgFmodMusic;
    public static int currentSceneIndex;

    public static float yDeathLimit { get; } = -17f;
    public GameObject player;
    private static Player2D _playerScript;
    public GameObject bg;
    //public AudioClip bgMusic;
    //private AudioSource audioSource;
    public static int scenePlayerDied;
    [SerializeField] private bool gameIsPaused = false;
    [SerializeField] private static bool _gameIsOver = false;





    private void Awake()
    {
        _instance = this;
    }
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

            _playerScript = player.GetComponent<Player2D>();
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
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    
        //}
    }

    public static void FmodChange()
    {
        bgFmodMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(1);
    }

    public static void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currentSceneIndex);
        _gameIsOver = false;
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
        if(!_gameIsOver)
        {
            _gameIsOver = true;
            _playerScript.DisableInput(true);
            UIManager.FadeToBlack();
        }
            
        //SceneManager.LoadScene("GameOverScene");
    }

    public void GameOverDelay(float delay)
    {
        StartCoroutine(GameOverDelayRoutine(delay));
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    public static void DisablePlayer(bool disable)
    {
        if(disable)
        {
            _playerScript.enabled = false;
        }
        else
        {
            _playerScript.enabled = true;
        }
    }

    //IEnumerator FadeToBlack()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //}

    IEnumerator GameOverDelayRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.GameOver();
    }
}
