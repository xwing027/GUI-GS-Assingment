using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ButtonHandler : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject options;
    public GameObject playMenu;
    public GameObject pauseMenu;
    Scene currentScene;
    int sceneIndex;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    public void Exit()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    #region Play Menu
    public void PlayNew()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayLoad()
    {
        SceneManager.LoadScene(2);
    }
    #endregion

    #region Pause Menu
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Return()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    #endregion

    public void Back()
    {
        if (currentScene.buildIndex == 0)
        {
            options.gameObject.SetActive(false);
            mainMenu.gameObject.SetActive(true);
        }
        if (currentScene.buildIndex == 2)
        {
            options.gameObject.SetActive(false);
            pauseMenu.gameObject.SetActive(true);
        }
    }
}
