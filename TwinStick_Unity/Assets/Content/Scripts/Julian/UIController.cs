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

    private GameObject[] menuScreens = null;

    private NotificationCenter notificationCenter;


    private void Awake()
    {
        notificationCenter = GameManager.instance.notificationCenter;
        menuScreens = new GameObject[] { pauseMenu, inventoryMenu};

        SetScreenActive(hud,GameMenuState.clear);
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

    }


    private void OpenPauseMenu()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        SetScreenActive(pauseMenu, GameMenuState.pauseMenu);
    }

    private void OpenInventoy()
    {
        Cursor.visible = true;
        SetScreenActive(inventoryMenu, GameMenuState.inventoy);
        //todo Maak mis een aparte class voor de inventoyUI
    }

    public void ClearMenus()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        SetScreenActive(null, GameMenuState.clear);
    }


    public void QuitToMainMenu()
    {
        GameManager.instance.notificationCenter.FireExitToMenu();
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





//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;


//public enum GameMenuState
//{
//    clear = 0,
//    pauseMenu = 1,
//    inventoy = 2,
//}

//public class UIController : MonoBehaviour
//{

//    [SerializeField] private PlayerUI playerUI = null;
//    [SerializeField] private GameObject pauseMenu = null;
//    [SerializeField] private GameObject gameCursor = null;
//    [SerializeField] private Vector3 mouseOffset = Vector3.zero;
//    [SerializeField] private Image fadeImage = null;

//    private NotificationCenter notificationCenter;
//    private Camera mainCamera;


//    private void Awake()
//    {
//        notificationCenter = GameManager.instance.notificationCenter;

//        notificationCenter.OnGamePaused += HandlePauseGame;
//        notificationCenter.OnGameUnPaused += HandleUnPauseGame;
//        notificationCenter.OnArmorHealthChange += HandleArmorHealth;
//        notificationCenter.OnGunMagAmmoUpdated += HandleAmmoInMag;
//        notificationCenter.OnPlayerHealthChange += HandlePlayerHealth;
//        notificationCenter.OnGunInventoyAmmoUpdated += HandleAmmoInInventoy;
//    }


//    private void Start()
//    {
//        mainCamera = Camera.main;

//        HandleUnPauseGame();
//    }

//    private void LateUpdate()
//    {
//        //todo Verander dit en zorg voor een input manager mischien
//        if (Input.GetKeyDown(KeyCode.Escape))
//        {
//            if (GameManager.instance.gamePaused)
//                notificationCenter.FireGameUnPaused();
//            else
//                notificationCenter.FireGamePaused();
//        }

//        gameCursor.transform.position = GetMousePos(mainCamera);
//    }

//    /// <summary>
//    /// Returns The Mouse Pos
//    /// </summary>
//    private Vector3 GetMousePos(Camera cam)
//    {
//        if (cam != null)
//        {
//            Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
//            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

//            float rayLength;
//            groundPlane.Raycast(cameraRay, out rayLength);

//            Vector3 hitPoint = cameraRay.GetPoint(rayLength);
//            hitPoint += mouseOffset;

//            hitPoint = cam.WorldToScreenPoint(hitPoint);
//            return hitPoint;
//        }

//        return Vector3.zero;
//    }


//    #region Handel

//    /// <summary>
//    /// Handles the ammo in mag
//    /// </summary>
//    private void HandleAmmoInMag(int newAmmoInMag)
//    {
//        playerUI.SetAmmoInMag(newAmmoInMag);
//    }

//    /// <summary>
//    /// Handles the ammo in inventoy
//    /// </summary>
//    private void HandleAmmoInInventoy(ItemSlot itemSlot)
//    {
//        playerUI.SetAmmoInInventory(itemSlot != null ? itemSlot.count : 0);
//    }

//    /// <summary>
//    /// Handles Player Health
//    /// </summary>
//    private void HandlePlayerHealth(float newHealth, float newMaxHealth)
//    {
//        playerUI.SetHealth(newHealth, newMaxHealth);
//    }

//    /// <summary>
//    /// Handles Armor Health
//    /// </summary>
//    private void HandleArmorHealth(float newArmorHealth, float newMaxArmorHealth)
//    {
//        playerUI.SetArmorHealth(newArmorHealth, newMaxArmorHealth);
//    }

//    /// <summary>
//    /// handles Pause menu
//    /// </summary>
//    private void HandlePauseGame()
//    {
//        Time.timeScale = 0;
//        Cursor.visible = true;

//        gameCursor.SetActive(false);
//        pauseMenu.SetActive(true);
//    }

//    /// <summary>
//    /// handles Un Pause menu
//    /// </summary>
//    private void HandleUnPauseGame()
//    {
//        Time.timeScale = 1;
//        Cursor.visible = false;
//        gameCursor.SetActive(true);
//        pauseMenu.SetActive(false);
//    }

//    #endregion


//    #region UIButtons

//    /// <summary>
//    /// Calls the un pause event
//    /// </summary>
//    public void Return()
//    {
//        GameManager.instance.notificationCenter.FireGameUnPaused();
//    }

//    /// <summary>
//    /// Calls the Exit to menu event and the un paused
//    /// </summary>
//    public void QuitToMainMenu()
//    {
//        GameManager.instance.notificationCenter.FireExitToMenu();
//    }

//    #endregion

//    private void OnDestroy()
//    {
//        notificationCenter.OnGunMagAmmoUpdated -= HandleAmmoInMag;
//        notificationCenter.OnPlayerHealthChange -= HandlePlayerHealth;
//        notificationCenter.OnGunInventoyAmmoUpdated -= HandleAmmoInInventoy;
//        notificationCenter.OnGamePaused -= HandlePauseGame;
//        notificationCenter.OnGameUnPaused -= HandleUnPauseGame;
//        notificationCenter.OnArmorHealthChange -= HandleArmorHealth;
//    }
//}
