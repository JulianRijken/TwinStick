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
        gameManager.notificationCenter.OnGunAmmoUpdated += HandleOnGunAmmoUpdated;
    }

    private void HandleOnGunAmmoUpdated(int newAmmoInMag, ItemSlot itemSlot)
    {
        playerUI.SetAmmoInMag(newAmmoInMag);
        playerUI.SetAmmoInInventory(itemSlot != null ? itemSlot.count : 0);
    }

    private void OnDestroy()
    {
        gameManager.notificationCenter.OnGunAmmoUpdated -= HandleOnGunAmmoUpdated;
    }

    private void LateUpdate()
    {
        // handle Player UI
        if (playerUI != null)
        {
            Player player = gameManager.player;
            if (player != null)
            {
                playerUI.SetHealth(player.GetHealth(), player.GetMaxHealth());
            }

        }
    }
}
