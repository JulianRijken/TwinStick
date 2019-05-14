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

    private void Start()
    {
        gameManager = GameManager.instance;

        // Subscribe to the notification center
        gameManager.notificationCenter.OnGunMagAmmoUpdated += HandleAmmoInMag;
        gameManager.notificationCenter.OnPlayerHealthChange += HandlePlayerHealth;
        gameManager.notificationCenter.OnGunInventoyAmmoUpdated += HandleAmmoInInventoy;
        gameManager.notificationCenter.OnGamePaused += HandlePauseGame;

        if (pauseMenu != null)
            pauseMenu.SetActive(false);
    }

    private void HandleAmmoInMag(int newAmmoInMag)
    {
        playerUI.SetAmmoInMag(newAmmoInMag);
    }

    private void HandleAmmoInInventoy(ItemSlot itemSlot)
    {
        playerUI.SetAmmoInInventory(itemSlot != null ? itemSlot.count : 0);
    }


    private void HandlePlayerHealth(float newHealth, float newMaxHealth)
    {
        playerUI.SetHealth(newHealth, newMaxHealth);
    }

    private void HandlePauseGame(bool paused)
    {
        Time.timeScale = paused ? 0 : 1;

        if(pauseMenu != null)
        pauseMenu.SetActive(paused);
    }



    // UI Buuttons

    public void Return()
    {
        GameManager.instance.notificationCenter.FireGamePaused(false);
    }

    public void QuitToMainMenu()
    {
        GameManager.instance.notificationCenter.FireExitToMenu();
        GameManager.instance.notificationCenter.FireGamePaused(false);

    }



    private void OnDestroy()
    {
        // Unsubscrbe From the notification center
        gameManager.notificationCenter.OnGunMagAmmoUpdated -= HandleAmmoInMag;
        gameManager.notificationCenter.OnPlayerHealthChange -= HandlePlayerHealth;
        gameManager.notificationCenter.OnGunInventoyAmmoUpdated -= HandleAmmoInInventoy;
        gameManager.notificationCenter.OnGamePaused -= HandlePauseGame;
    }
}
