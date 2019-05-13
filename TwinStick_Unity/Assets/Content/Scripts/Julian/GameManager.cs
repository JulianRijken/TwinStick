using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public StatsController statsController;
    public InventoryController inventory;
    public Player player;
    public NotificationCenter notificationCenter;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        notificationCenter = new NotificationCenter();
    }


    void Start()
    {
        statsController = new StatsController();
        inventory = new InventoryController();
        player = FindObjectOfType<Player>();
    }

}

public class NotificationCenter {

    public delegate void GunAmmoUpdateAction(int newAmmoInMag, ItemSlot itemSlot);

    public event GunAmmoUpdateAction OnGunAmmoUpdated;

    public void FireOnGunAmmoUpdated(int newAmmoInMag, ItemSlot itemSlot)
    {
        OnGunAmmoUpdated?.Invoke(newAmmoInMag, itemSlot);
    }
}
