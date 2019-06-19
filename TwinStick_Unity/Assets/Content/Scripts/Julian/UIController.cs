using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public enum GameMenuState
{
    clear = 0,
    pauseMenu = 1,
    inventoy = 2,
}

/// <summary>
/// This class is used to controll the menu's like the pause menu en for sorting all the diffrent ui
/// </summary>
public class UIController : MonoBehaviour
{

    [SerializeField] private GameObject hud = null;

    [SerializeField] private GameObject pauseMenu = null;
    [SerializeField] private GameObject inventoryMenu = null;
    [SerializeField] private GameObject exitWindow = null;

    [Header("Crosshair")]
    [SerializeField] private Transform crosshair = null;
    [SerializeField] private Vector3 crosshairOffset = Vector3.up;

    private GameObject[] menuScreens = null;

    private NotificationCenter notificationCenter;
    private Camera uiCamera;

    private void Awake()
    {
        notificationCenter = GameManager.instance.notificationCenter;
        menuScreens = new GameObject[] { pauseMenu, inventoryMenu , hud};

        SetScreenActive(hud,GameMenuState.clear);

        uiCamera = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
    }


    private void LateUpdate()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameManager.instance.GetMenuState() == GameMenuState.clear)           
                OpenPauseMenu();            
            else            
                ClearMenus();         
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(GameManager.instance.GetMenuState() == GameMenuState.inventoy)
            {
                ClearMenus();
            }
            else if(GameManager.instance.GetMenuState() == GameMenuState.clear)
            {
                OpenInventoy();
            }
        }

        Vector3 _crosshairPos = uiCamera.ScreenToWorldPoint(Input.mousePosition + crosshairOffset);
        _crosshairPos.z = transform.position.z;
        crosshair.position = _crosshairPos;

    }



    private void OpenPauseMenu()
    {
        Time.timeScale = 0;
        GameManager.instance.notificationCenter.FireBlur(true);
        Cursor.visible = true;
        SetScreenActive(pauseMenu, GameMenuState.pauseMenu);
    }

    private void OpenInventoy()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        SetScreenActive(inventoryMenu, GameMenuState.inventoy);
        //todo Maak mis een aparte class voor de inventoyUI
    }

    public void ClearMenus()
    {
        Time.timeScale = 1;
        GameManager.instance.notificationCenter.FireBlur(false);
        Cursor.visible = false;
        SetScreenActive(hud, GameMenuState.clear);
    }


    public void QuitToMainMenu()
    {
        GameManager.instance.notificationCenter.FireExitToMenu();
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
    /// Disables all screens and enables one
    /// </summary>
    private void SetScreenActive(GameObject _activeScreen)
    {
        for (int i = 0; i < menuScreens.Length; i++)
        {
            menuScreens[i].SetActive(false);
        }
        if (_activeScreen != null)
            _activeScreen.SetActive(true);
    }

    /// <summary>
    /// Disables all screens and enables one and the menu state
    /// </summary>
    private void SetScreenActive(GameObject _activeScreen,GameMenuState menuState)
    {
        for (int i = 0; i < menuScreens.Length; i++)
        {
            menuScreens[i].SetActive(false);
        }

        GameManager.instance.SetMenuState(menuState);
        if(_activeScreen != null)
        _activeScreen.SetActive(true);
    }

}


