using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationCenter
{
    /// <summary>
    /// Fires the Gun Mag Ammo Change event
    /// </summary>
    public void FireGunMagAmmoChange(int newAmmoInMag)
    {
        OnGunMagAmmoUpdated?.Invoke(newAmmoInMag);
    }
    public delegate void GunMagUpdateAction(int newAmmoInMag);
    public event GunMagUpdateAction OnGunMagAmmoUpdated;

    /// <summary>
    /// Fires Gun Inventoy Ammo Change event
    /// </summary>
    public void FireGunInventoyAmmoChange(ItemSlot itemSlot)
    {
        OnGunInventoyAmmoUpdated?.Invoke(itemSlot);
    }
    public delegate void GunInventoyUpdateAction(ItemSlot itemSlot);
    public event GunInventoyUpdateAction OnGunInventoyAmmoUpdated;

    /// <summary>
    /// Fires Player Health Change event
    /// </summary>
    public void FirePlayerHealthChange(float newHealth, float newMaxHealth)
    {
        OnPlayerHealthChange?.Invoke(newHealth, newMaxHealth);
    }
    public delegate void PlayerHealthUpdateAction(float newHealth, float newMaxHealth);
    public event PlayerHealthUpdateAction OnPlayerHealthChange;

    /// <summary>
    /// Fires Player ArmorHealth Change event
    /// </summary>
    public void FireArmorHealthChange(float newArmorHealth, float newArmorMaxHealth)
    {
        OnArmorHealthChange?.Invoke(newArmorHealth, newArmorMaxHealth);
    }
    public delegate void ArmorHealthUpdateAction(float newArmorHealth, float newArmorMaxHealth);
    public event ArmorHealthUpdateAction OnArmorHealthChange;

    /// <summary>
    /// Fires Player Died event
    /// </summary>
    public void FirePlayerDied()
    {
        OnPlayerDie?.Invoke();
    }
    public delegate void PlayerDiedAction();
    public event PlayerDiedAction OnPlayerDie;

    /// <summary>
    /// Fires Exit To Menu event
    /// </summary>
    public void FireExitToMenu()
    {
        OnExitToMenu?.Invoke();
    }
    public delegate void ExitToMenuAction();
    public event ExitToMenuAction OnExitToMenu;

    /// <summary>
    /// Fires Item Added event
    /// </summary>
    public void FireItemAdded()
    {
        OnItemAdded?.Invoke();
    }
    public delegate void ItemAddedAction();
    public event ItemAddedAction OnItemAdded;

    /// <summary>
    /// Fires Game Paused event
    /// </summary>
    public void FireGamePaused()
    {
        GameManager.instance.gamePaused = true;
        OnGamePaused?.Invoke();
    }

    public delegate void GamePausedAction();
    public event GamePausedAction OnGamePaused;


    /// <summary>
    /// Fires Game Paused event
    /// </summary>
    public void FireGameUnPaused()
    {
        GameManager.instance.gamePaused = false;
        OnGameUnPaused?.Invoke();
    }
    public delegate void GameUnPausedAction();
    public event GameUnPausedAction OnGameUnPaused;

}
