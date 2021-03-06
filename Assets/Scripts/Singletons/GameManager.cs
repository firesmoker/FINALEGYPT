﻿using System.Collections;
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

	public int level = 1;
    public static float yDeathLimit { get; set; } = -17f;
    public float yDeathLimitPublic = -17f;
    public GameObject player;
    private static Player2D _playerScript;
    public GameObject bg;
    //public AudioClip bgMusic;
    //private AudioSource audioSource;
    public static int scenePlayerDied;
    [SerializeField] private bool gameIsPaused = false;
    [SerializeField] private static bool _gameIsOver = false;
    private static bool _levelEnded = false;
    //public  bool levelendedCheck;
    [SerializeField] private GameObject _levelStartPoint, _levelEndPoint;
    public float distanceToEnd;
    Vector2 levelStartVectorPosition, levelEndVectorPosition;
    public bool changeTo4 = false;



    private void Awake()
    {
        _instance = this;
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        yDeathLimit = yDeathLimitPublic;
        //levelendedCheck = _levelEnded;
        if (_levelStartPoint == null)
            _levelStartPoint = new GameObject();
        if (_levelEndPoint == null)
            _levelEndPoint = new GameObject();
        levelStartVectorPosition = new Vector2(_levelStartPoint.transform.position.x, _levelStartPoint.transform.position.y);
        levelEndVectorPosition = new Vector2(_levelEndPoint.transform.position.x, _levelEndPoint.transform.position.y);
        //Vector2 distanceVector = new Vector2(levelStartVectorPosition, levelEndVectorPosition);
        distanceToEnd = Vector2.Distance(levelStartVectorPosition, levelEndVectorPosition);
        if (GameObject.Find("Background Music") == null)
        {
            bg = new GameObject("Background Music");
            DontDestroyOnLoad(bg);
            bgFmodMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music");
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(bgFmodMusic, bg.GetComponent<Transform>(), bg.GetComponent<Rigidbody2D>());
            bgFmodMusic.start();
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
        bgFmodMusic.setParameterByName("Level", level);
    }

    private void Update()
    {
        PlayerDistanceToEnd();
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    
        //}
    }

    public void PlayerDistanceToEnd()
    {
        Vector2 playerPositionVector = new Vector2(player.transform.position.x, player.transform.position.y);
        float playerDistanceToEnd = Vector2.Distance(playerPositionVector, levelEndVectorPosition);
        float percentageToEnd = (playerDistanceToEnd / distanceToEnd)*100f;
        Debug.Log("percentage to end is " + percentageToEnd);
        float currentFmodLevel = 0;
        bgFmodMusic.getParameterByName("Level", out currentFmodLevel);
        if (currentFmodLevel != 3)
        {
            if (percentageToEnd <= 0)
                bgFmodMusic.setParameterByName("DistanceToEnd", 0);
            else
                bgFmodMusic.setParameterByName("DistanceToEnd", percentageToEnd);
        }
        else
        {
            if(!changeTo4)
                bgFmodMusic.setParameterByName("DistanceToEnd", 0);
            else
                bgFmodMusic.setParameterByName("DistanceToEnd", 100);
        }
        
    }

    public static void FmodChange()
    {
        bgFmodMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(1);
    }

    public static void FadeEnded()
    {
        Time.timeScale = 1;
        _levelEnded = false;
        if (_gameIsOver)
        {
            _gameIsOver = false;
            SceneManager.LoadScene(GetSceneIndex());
        }
        else
        {
            SceneManager.LoadScene(GetSceneIndex() + 1);
        }
    }

    public static void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartMenuScene");
    }

    private static int GetSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
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

    public static void WinLevel()
    {
        if(!_levelEnded)
            EndLevel();
    }

    public void ToLevel4()
    {
        changeTo4 = true;
        //bgFmodMusic.setParameterByName("Level", 4);
    }

    public static void WinLevel(int nextLevelNumber)
    {
        bgFmodMusic.setParameterByName("Level", nextLevelNumber);
        if (!_levelEnded)
            EndLevel();
    }

    public static void GameOver()
    {
        if(!_levelEnded)
        {
            _gameIsOver = true;
            _playerScript.DisableInput(true);
            EndLevel();
        }
    }

    private static void EndLevel()
    {
        _levelEnded = true;
        UIManager.FadeToBlack();
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
