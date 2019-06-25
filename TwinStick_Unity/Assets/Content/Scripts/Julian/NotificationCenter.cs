using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationCenter
{


    /// <summary>
    /// Fires the Gun Mag Ammo Change event
    /// </summary>
    public void FireGunMagAmmoChange(int newAmmoInMag, int maxAmmo)
    {
        OnGunMagAmmoUpdated?.Invoke(newAmmoInMag, maxAmmo);
    }
    public delegate void GunMagUpdateAction(int newAmmoInMag, int maxAmmo);
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
    /// Fires Blur
    /// </summary>
    public void FireBlur(bool _blur)
    {
        OnBlur?.Invoke(_blur);
    }
    public delegate void BlurAction(bool _blur);
    public event BlurAction OnBlur;

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
    /// Unlocs the level
    /// </summary>
    public void FireSetLevelInteractable(int _level, bool _interactable)
    {
        OnSetLevelInteractable?.Invoke(_level, _interactable);
    }
    public delegate void SetLevelInteractableAction(int _level, bool _interactable);
    public event SetLevelInteractableAction OnSetLevelInteractable;


    /// <summary>
    /// Fires Ammo
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
    /// Fires Player Died event
    /// </summary>
    public void FirePlayerEscaped()
    {
        OnPlayerEscaped?.Invoke();
    }
    public delegate void PlayerPlayerEscaped();
    public event PlayerPlayerEscaped OnPlayerEscaped;

    /// <summary>
    /// Fires Weapon attack
    /// </summary>
    public void FirePlayerAnimation(PlayerAnimation _animation)
    {
        OnPlayerAnimation?.Invoke(_animation);
    }
    public delegate void PlayerAnimationAction(PlayerAnimation _animation);
    public event PlayerAnimationAction OnPlayerAnimation;

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
    public void FireInventoyItemChange()
    {
        OnInventoyItemChange?.Invoke();
    }
    public delegate void InventoyItemChangeAction();
    public event InventoyItemChangeAction OnInventoyItemChange;

    /// <summary>
    /// Fires stats is changed
    /// </summary>
    public void FireStatsChanged()
    {
        OnStatsChanged?.Invoke();
    }
    public delegate void StatsChangedAction();
    public event StatsChangedAction OnStatsChanged;

    /// <summary>
    /// Fires on item used
    /// </summary>
    public void FireItemUsed(ItemID _usedItem)
    {
        OnItemUsed?.Invoke(_usedItem);
    }
    public delegate void ItemUsedAction(ItemID _usedItem);
    public event ItemUsedAction OnItemUsed;

    /// <summary>
    /// Fires Game Paused event
    /// </summary>
    //public void FireGamePaused()
    //{
    //    OnGamePaused?.Invoke();
    //}
    //public delegate void GamePausedAction();
    //public event GamePausedAction OnGamePaused;


    /// <summary>
    /// Fires Game Un Paused event
    /// </summary>
    //public void FireGameUnPaused()
    //{
    //    OnGameUnPaused?.Invoke();
    //}
    //public delegate void GameUnPausedAction();
    //public event GameUnPausedAction OnGameUnPaused;




}
