using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    //public GameObject panel;
    public Image panelImage;

    // Start is called before the first frame update
    //public static int scenePlayerDied = 0;

    //private void Start()
    //{
    //    scenePlayerDied = 
    //}
    public void RestartScene()
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

    //public static void PlayerDeathScene(int sceneIndex)
    //{
    //    scenePlayerDied = sceneIndex;
    //}
}
