﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject options = null;
    [SerializeField] private GameObject home = null;
    [SerializeField] private GameObject credits = null;
    [SerializeField] private GameObject exit = null;

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
        GameManager.instance.statsController.AddTimesPlayed();
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

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
# endif

        Application.Quit();
    }
}
