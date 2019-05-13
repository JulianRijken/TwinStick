using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public StatsController statsController;
    public InventoryController inventory;
    public NotificationCenter notificationCenter;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        notificationCenter = new NotificationCenter();

        notificationCenter.OnPlayerDie += OnPlayerDie;
    }


    void Start()
    {
        statsController = new StatsController();
        inventory = new InventoryController();;
    }


    private void OnPlayerDie()
    {
        SceneManager.LoadScene(0);
    }


    private void OnDestroy()
    {
        notificationCenter.OnPlayerDie -= OnPlayerDie;
    }


}

public class NotificationCenter {

    public delegate void GunMagUpdateAction(int newAmmoInMag);
    public event GunMagUpdateAction OnGunMagAmmoUpdated;
    public void FireGunMagAmmoChange(int newAmmoInMag)
    {
        OnGunMagAmmoUpdated?.Invoke(newAmmoInMag);
    }

    public delegate void GunInventoyUpdateAction(ItemSlot itemSlot);
    public event GunInventoyUpdateAction OnGunInventoyAmmoUpdated;
    public void FireGunInventoyAmmoChange(ItemSlot itemSlot)
    {
        OnGunInventoyAmmoUpdated?.Invoke(itemSlot);
    }

    public delegate void PlayerHealthUpdateAction(float newHealth,float newMaxHealth);
    public event PlayerHealthUpdateAction OnPlayerHealthChange;
    public void FirePlayerHealthChange(float newHealth, float newMaxHealth)
    {
        OnPlayerHealthChange?.Invoke(newHealth,newMaxHealth);
    }

    public delegate void PlayerDiedAction();
    public event PlayerDiedAction OnPlayerDie;
    public void FirePlayerDied()
    {
        OnPlayerDie?.Invoke();
    }

    public delegate void ItemAddedAction();
    public event ItemAddedAction OnItemAdded;
    public void FireItemAdded()
    {
        OnItemAdded?.Invoke();
    }

}
