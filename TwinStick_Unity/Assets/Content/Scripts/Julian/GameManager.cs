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

    public bool[] unlockedLevels = new bool[3];


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

        for (int i = 0; i < unlockedLevels.Length; i++)
        {
            unlockedLevels[i] = false;
        }

        unlockedLevels[0] = true;
    }



    /// <summary>
    /// Sets The Menu State
    /// </summary>
    public void SetMenuState(GameMenuState _state)
    {
        menuState = _state;
    }

    public void SetLevelUnlocked(int _level, bool _unlocked)
    {
        unlockedLevels[_level] = _unlocked;
    }

    /// <summary>
    /// Retuns the menuState
    /// </summary>
    public GameMenuState GetMenuState()
    {
        return menuState;
    }

    /// <summary>
    /// Is Called Once exit to menu is called
    /// </summary>
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
