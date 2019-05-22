﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [HideInInspector] public static GameManager instance;

    [HideInInspector] public StatsController statsController;
    [HideInInspector] public InventoryController inventory;
    [HideInInspector] public NotificationCenter notificationCenter;

    [HideInInspector] public bool gamePaused;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
        }

        DontDestroyOnLoad(gameObject);


        notificationCenter = new NotificationCenter();
        statsController = new StatsController();
        inventory = new InventoryController(); ;

        notificationCenter.OnPlayerDie += OnPlayerDie;
        notificationCenter.OnExitToMenu += OnExitToMenu;

    }

    private void Update()
    {
        //todo Verander dit en zorg voor een input manager
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
                notificationCenter.FireGameUnPaused();
            else
                notificationCenter.FireGamePaused();
        }
    }


    private void OnPlayerDie()
    {
        SceneManager.LoadScene(0);
    }

    private void OnExitToMenu()
    {
        SceneManager.LoadScene(0);
    }


    private void OnDestroy()
    {
        notificationCenter.OnPlayerDie -= OnPlayerDie;
        notificationCenter.OnExitToMenu -= OnExitToMenu;
    }


}
