using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private PlayerUI playerUI = null;
    [SerializeField] private GameObject pauseMenu = null;
    [SerializeField] private GameObject gameCursor = null;
    [SerializeField] private Vector3 mouseOffset = Vector3.zero;

    private GameManager gameManager;
    private Camera mainCamera;


    private void Awake()
    {
        gameManager = GameManager.instance;

        gameManager.notificationCenter.OnGunMagAmmoUpdated += HandleAmmoInMag;
        gameManager.notificationCenter.OnPlayerHealthChange += HandlePlayerHealth;
        gameManager.notificationCenter.OnGunInventoyAmmoUpdated += HandleAmmoInInventoy;
        gameManager.notificationCenter.OnGamePaused += HandlePauseGame;
        gameManager.notificationCenter.OnGameUnPaused += HandleUnPauseGame;
        gameManager.notificationCenter.OnArmorHealthChange += HandleArmorHealth;


    }

    private void Start()
    {
        mainCamera = Camera.main;

        HandleUnPauseGame();
    }

    private void LateUpdate()
    {
        gameCursor.transform.position = GetMousePos(mainCamera);
    }

    /// <summary>
    /// Returns The Mouse Pos
    /// </summary>
    private Vector3 GetMousePos(Camera cam)
    {
        if (cam != null)
        {
            Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

            float rayLength;
            groundPlane.Raycast(cameraRay, out rayLength);

            Vector3 hitPoint = cameraRay.GetPoint(rayLength);
            hitPoint += mouseOffset;

            hitPoint = cam.WorldToScreenPoint(hitPoint);
            return hitPoint;
        }

        return Vector3.zero;
    }


    #region Handel

    /// <summary>
    /// Handles the ammo in mag
    /// </summary>
    private void HandleAmmoInMag(int newAmmoInMag)
    {
        playerUI.SetAmmoInMag(newAmmoInMag);
    }

    /// <summary>
    /// Handles the ammo in inventoy
    /// </summary>
    private void HandleAmmoInInventoy(ItemSlot itemSlot)
    {
        playerUI.SetAmmoInInventory(itemSlot != null ? itemSlot.count : 0);
    }

    /// <summary>
    /// Handles Player Health
    /// </summary>
    private void HandlePlayerHealth(float newHealth, float newMaxHealth)
    {
        playerUI.SetHealth(newHealth, newMaxHealth);
    }

    /// <summary>
    /// Handles Armor Health
    /// </summary>
    private void HandleArmorHealth(float newArmorHealth, float newMaxArmorHealth)
    {
        playerUI.SetArmorHealth(newArmorHealth, newMaxArmorHealth);
    }

    /// <summary>
    /// handles Pause menu
    /// </summary>
    private void HandlePauseGame()
    {
        Time.timeScale = 0;
        Cursor.visible = true;

        gameCursor.SetActive(false);
        pauseMenu.SetActive(true);
    }

    /// <summary>
    /// handles Un Pause menu
    /// </summary>
    private void HandleUnPauseGame()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        gameCursor.SetActive(true);
        pauseMenu.SetActive(false);
    }

    #endregion


    #region UIButtons

    /// <summary>
    /// Calls the un pause event
    /// </summary>
    public void Return()
    {
        GameManager.instance.notificationCenter.FireGameUnPaused();
    }

    /// <summary>
    /// Calls the Exit to menu event and the un paused
    /// </summary>
    public void QuitToMainMenu()
    {
        GameManager.instance.notificationCenter.FireExitToMenu();
        GameManager.instance.notificationCenter.FireGameUnPaused();
    }

    #endregion

    private void OnDestroy()
    {
        gameManager.notificationCenter.OnGunMagAmmoUpdated -= HandleAmmoInMag;
        gameManager.notificationCenter.OnPlayerHealthChange -= HandlePlayerHealth;
        gameManager.notificationCenter.OnGunInventoyAmmoUpdated -= HandleAmmoInInventoy;
        gameManager.notificationCenter.OnGamePaused -= HandlePauseGame;
        gameManager.notificationCenter.OnGameUnPaused -= HandleUnPauseGame;
        gameManager.notificationCenter.OnArmorHealthChange -= HandleArmorHealth;
    }
}
