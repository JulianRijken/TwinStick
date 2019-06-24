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

        notificationCenter.OnPlayerDie += OnPlayerDie;
        notificationCenter.OnPlayerEscaped += OnPlayerEscape;
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


    private void OnPlayerDie()
    {
        //todo Dit Kan natuurlijk veranderd worden naar een mooien fade out
        SceneManager.LoadScene(0);
    }

    private void OnPlayerEscape()
    {
        //todo Dit Kan natuurlijk veranderd worden naar een mooien fade out
        SceneManager.LoadScene(0);

    }

    private void OnExitToMenu()
    {
        //todo Dit Kan natuurlijk veranderd worden naar een mooien fade out
        SceneManager.LoadScene(0);
    }


    private void OnDestroy()
    {
        notificationCenter.OnPlayerDie -= OnPlayerDie;
        notificationCenter.OnPlayerEscaped -= OnPlayerEscape;
        notificationCenter.OnExitToMenu -= OnExitToMenu;
    }


}
