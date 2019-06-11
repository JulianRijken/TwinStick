using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject exitWindow = null;

    private GameObject[] menuScreens = null;

    private void Start()
    {
        exitWindow.SetActive(false);

        Cursor.visible = true;
        Time.timeScale = 1;
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



