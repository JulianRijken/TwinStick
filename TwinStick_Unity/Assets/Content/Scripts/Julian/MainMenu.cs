using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject homeScreen = null;
    [SerializeField] private GameObject playScreen = null;
    [SerializeField] private GameObject optionsScreen = null;
    [SerializeField] private GameObject exitWindow = null;

    private GameObject[] menuScreens = null;

    private void Start()
    {
        exitWindow.SetActive(false);

        Cursor.visible = true;
        Time.timeScale = 1;

        menuScreens = new GameObject[] { optionsScreen, homeScreen, playScreen };
        SetScreenActive(homeScreen);
    }


    /// <summary>
    /// Disables all screens
    /// </summary>
    private void DisableAllScreens()
    {
        for (int i = 0; i < menuScreens.Length; i++)
        {
            menuScreens[i].SetActive(false);
        }
    }

    /// <summary>
    /// Disables all screens and enables one
    /// </summary>
    private void SetScreenActive(GameObject screen)
    {
        for (int i = 0; i < menuScreens.Length; i++)
        {
            menuScreens[i].SetActive(false);
        }

        screen.SetActive(true);
    }

    /// <summary>
    /// Starts The Game
    /// </summary>
    public void StartGame()
    {
        GameManager.instance.statsController.AddTimesPlayed();
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Opens THe level select screen
    /// </summary>
    public void OpenLevelSelect()
    {
        SetScreenActive(playScreen);
    }

    /// <summary>
    /// Opens the home screen
    /// </summary>
    public void OpenHome()
    {
        SetScreenActive(homeScreen);
    }

    /// <summary>
    /// Opens the home screen
    /// </summary>
    public void OpenOptions()
    {
        SetScreenActive(optionsScreen);
    }

    /// <summary>
    /// Opens the exit screen
    /// </summary>
    public void OpenExitScreen()
    {
        exitWindow.SetActive(true);
    }

    /// <summary>
    /// Closes Exit Screen
    /// </summary>
    public void CloseExitScreen()
    {
        exitWindow.SetActive(false);
    }

    /// <summary>
    /// Closes the application
    /// </summary>
    public void ExitApplication()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }
}
