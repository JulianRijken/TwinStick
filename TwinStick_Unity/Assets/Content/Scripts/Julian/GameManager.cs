﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public StatsController statsController;
    public InventoryController inventory;
    public Player player;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        statsController = new StatsController();
        inventory = new InventoryController();
        player = FindObjectOfType<Player>();
    }

}
