using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [HideInInspector] public static GameManager instance;

    [HideInInspector] public StatsController statsController;
    [HideInInspector] public InventoryController inventory;
    [HideInInspector] public NotificationCenter notificationCenter;

    [HideInInspector] public GameMenuState menuState;



    private void Awake()
    {
        if (instance == null)       
            instance = this;        
        else        
            Destroy(gameObject);
        

        DontDestroyOnLoad(gameObject);


        notificationCenter = new NotificationCenter();
        statsController = new StatsController();
        inventory = new InventoryController(); ;

        notificationCenter.OnExitToMenu += OnExitToMenu;

    }

    /// <summary>
    /// Sets The Menu State
    /// </summary>
    public void SetMenuState(GameMenuState _state)
    {
        menuState = _state;
    }

    /// <summary>
    /// Retuns the menuState
    /// </summary>
    public GameMenuState GetMenuState()
    {
        return menuState;
    }



    private void OnExitToMenu()
    {
        SceneManager.LoadScene(0);
        inventory.ClearInventoy();
    }




    private void OnDestroy()
    {
        notificationCenter.OnExitToMenu -= OnExitToMenu;
    }


}
