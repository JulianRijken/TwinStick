using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject home;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject exit;

    private GameObject[] menuScreens = null;

    private void Start()
    {
        menuScreens = new GameObject[] { options, home, credits, exit };
        SetScreenActive(home);
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


    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }


    public void OpenOptions()
    {
        SetScreenActive(options);
    }

    public void OpenHome()
    {
        SetScreenActive(home);
    }

    public void OpenCredits()
    {
        SetScreenActive(credits);
    }

    public void OpenExit()
    {
        SetScreenActive(exit);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
