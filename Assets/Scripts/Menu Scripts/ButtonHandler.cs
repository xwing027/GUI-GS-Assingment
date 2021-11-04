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

    //regions are not strict e.g. exit is used in other places too, this is just for organisation
    #region Main Menu 
    public void Play()
    {
        playMenu.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
    }

    public void Options()
    {
        options.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
    }

    public void Exit()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
    #endregion

    #region Play Menu
    public void PlayNew()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayLoad()
    {

    }

    public void PlayCont()
    {

    }
    #endregion

    #region Pause Menu
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    #endregion
}
