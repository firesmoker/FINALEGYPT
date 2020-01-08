using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    private static UIManager _instance;
    public static new UIManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("SpawnManager is null");
            return _instance;
        }

    }

    //public GameObject panel;
    public GameObject panel;
    private static GameObject staticPanel;
    public GameObject fadePanel;
    private static GameObject staticFadePanel;
    public bool gameIsPaused = false;
    public float fadeoutSpeed = 0.05f;
    private static float _staticFadeoutSpeed;

    

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        staticPanel = panel;
        staticFadePanel = fadePanel;
        _staticFadeoutSpeed = fadeoutSpeed;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if (!gameIsPaused)
            {
                PauseMenu(true);
                
            }
            else
            {
                PauseMenu(false);
                
            }
        }
    }

    public static void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
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

    public void LoadPlayerDiedScene()
    {
        SceneManager.LoadScene(GameManager.scenePlayerDied);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseMenu(bool pause)
    {
        if(pause)
        {
            panel.SetActive(true);
            gameIsPaused = true;
            Time.timeScale = 0f;
            GameManager.DisablePlayer(true);
        }
        else
        {
            panel.SetActive(false);
            gameIsPaused = false;
            Time.timeScale = 1f;
            GameManager.DisablePlayer(false);
        }
    }

    public static void FadeToBlack()
    {
        staticFadePanel.SetActive(true);
        _instance.StartCoroutine(FadeToBlackRoutine());
    }

    static IEnumerator FadeToBlackRoutine()
    {
        Image panelImage = staticFadePanel.GetComponent<Image>();
        Color color = panelImage.color;
        color.a = 0f;
        while (panelImage.color.a < 1f)
        {
            yield return new WaitForSeconds(0.05f);
            color.a += _staticFadeoutSpeed;
            panelImage.color = color;
        }
        GameManager.FadeEnded();
    }

    //public static void PlayerDeathScene(int sceneIndex)
    //{
    //    scenePlayerDied = sceneIndex;
    //}
}
