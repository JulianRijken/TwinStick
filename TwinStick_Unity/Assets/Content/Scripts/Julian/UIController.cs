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

                // Manages the gun UI
                Gun gun = player.GetGun();
                if (gun != null)
                {
                    playerUI.SetGunUIActive(true);

                    playerUI.SetAmmoInMag(gun.GetAmmoInMag());

                    ItemSlot itemSlot = gameManager.inventory.GetItemSlot(gun.GetAmmoType());
                    if (itemSlot != null)
                        playerUI.SetAmmoInInventory(itemSlot.count);
                    else
                        playerUI.SetAmmoInInventory(0);
                }
                else
                {
                    playerUI.SetGunUIActive(false);
                }

            }

        }



    }
}
