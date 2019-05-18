using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private PlayerUI playerUI = null;
    [SerializeField] private GameObject pauseMenu = null;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.instance;

        gameManager.notificationCenter.OnGunMagAmmoUpdated += HandleAmmoInMag;
        gameManager.notificationCenter.OnPlayerHealthChange += HandlePlayerHealth;
        gameManager.notificationCenter.OnGunInventoyAmmoUpdated += HandleAmmoInInventoy;
        gameManager.notificationCenter.OnGamePaused += HandlePauseGame;
        gameManager.notificationCenter.OnArmorHealthChange += HandleArmorHealth;
    }

    private void Start()
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
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
    private void HandlePauseGame(bool paused)
    {
        Time.timeScale = paused ? 0 : 1;

        if(pauseMenu != null)
        pauseMenu.SetActive(paused);
    }

    #endregion


    #region UIButtons

    /// <summary>
    /// Calls the un pause event
    /// </summary>
    public void Return()
    {
        GameManager.instance.notificationCenter.FireGamePaused(false);
    }

    /// <summary>
    /// Calls the Exit to menu event and the un paused
    /// </summary>
    public void QuitToMainMenu()
    {
        GameManager.instance.notificationCenter.FireExitToMenu();
        GameManager.instance.notificationCenter.FireGamePaused(false);
    }

    #endregion

    private void OnDestroy()
    {
        gameManager.notificationCenter.OnGunMagAmmoUpdated -= HandleAmmoInMag;
        gameManager.notificationCenter.OnPlayerHealthChange -= HandlePlayerHealth;
        gameManager.notificationCenter.OnGunInventoyAmmoUpdated -= HandleAmmoInInventoy;
        gameManager.notificationCenter.OnGamePaused -= HandlePauseGame;
        gameManager.notificationCenter.OnArmorHealthChange -= HandleArmorHealth;
    }
}
