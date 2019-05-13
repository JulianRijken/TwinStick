using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private PlayerUI playerUI = null;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;

        // Subscribe to the notification center
        gameManager.notificationCenter.OnGunMagAmmoUpdated += HandleAmmoInMag;
        gameManager.notificationCenter.OnPlayerHealthChange += HandlePlayerHealth;
        gameManager.notificationCenter.OnGunInventoyAmmoUpdated += HandleAmmoInInventoy;
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


    private void OnDestroy()
    {
        // Unsubscrbe From the notification center
        gameManager.notificationCenter.OnGunMagAmmoUpdated -= HandleAmmoInMag;
        gameManager.notificationCenter.OnPlayerHealthChange -= HandlePlayerHealth;
        gameManager.notificationCenter.OnGunInventoyAmmoUpdated -= HandleAmmoInInventoy;
    }
}
