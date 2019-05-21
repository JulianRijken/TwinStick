using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{

    private enum GunState{Active,Reloading,Empty }

    [Header("Required")]
    [SerializeField] private ItemID ammoType = ItemID.Ammo;
    [SerializeField] private float reloadTime = 1f;
    [SerializeField] private int magSize = 30;
    [SerializeField] private int startingAmmo = 30;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Projectile projectile = null;

    [Header("Extra")]
    [SerializeField] private bool infintAmmo = false;
    [SerializeField] private ParticleSystem[] muzzleFlashses = new ParticleSystem[0];
    [SerializeField] private GameObject gunShotAudio = null;
    [SerializeField] private GameObject gunEmptyAudio = null;
    [SerializeField] public Lazer lazer = null;


    private GunState gunState = GunState.Active;
    private int ammoInMag = 0;
    private bool chamberLoaded = true;


    private void Start()
    {
        chamberLoaded = true;
        ammoInMag = startingAmmo;

        GameManager.instance.notificationCenter.OnItemAdded += OnItemAdded;
        GameManager.instance.notificationCenter.FireGunMagAmmoChange(ammoInMag);
        GameManager.instance.notificationCenter.FireGunInventoyAmmoChange(GameManager.instance.inventory.GetItemSlot(ammoType));
    }


    /// <summary>
    /// Turns the lazer on or off
    /// </summary>
    protected override void OnUseGadget()
    {
        if(lazer != null)
        {
            lazer.SwitchPower();
        }
    }



    /// <summary>
    /// Triggers the gun to shoot Once
    /// </summary>
    protected override void OnAttack()
    {
        if(gunState.Equals(GunState.Active) || infintAmmo)
        {
            if (ammoInMag > 0 || infintAmmo)
            {
                OnShot();
            }
            else
            {
                gunState = GunState.Empty;
                OnReload();
            }

        }
    }

    /// <summary>
    /// Is called when the gun is shot
    /// </summary>
    private void OnShot()
    {
        if (muzzleFlashses.Length > 0)
        {
            for (int i = 0; i < muzzleFlashses.Length; i++)
            {
                muzzleFlashses[i].Play();
            }
        }


        // Instantiate bullet
        if (projectile != null)
        {
            Quaternion _projectileRotation = shootPoint.rotation;
            _projectileRotation.eulerAngles += new Vector3(0, Random.Range(-3f, 3f), Random.Range(-3f, 3f));
            Instantiate(projectile, shootPoint.position, _projectileRotation);

        }

        if (!infintAmmo)
            ammoInMag--;

        if (gunShotAudio != null)
            Instantiate(gunShotAudio);

        GameManager.instance.notificationCenter.FireGunMagAmmoChange(ammoInMag);
    }



    /// <summary>
    /// Calls the reload function and checks if the mag is not full
    /// </summary>
    protected override void OnReload()
    { 
        if(!gunState.Equals(GunState.Reloading) && ammoInMag != magSize)
        {   
            StartCoroutine(IReloadMag());
        }
    }

    /// <summary>
    /// Reloads The magasine
    /// </summary>
    private IEnumerator IReloadMag()
    {
        gunState = GunState.Reloading;
        ItemSlot itemSlot = GameManager.instance.inventory.GetItemSlot(ammoType);

        // Check if inventoy has ammo
        if (itemSlot != null && itemSlot.count > 0)
        {
            int ammoRequired = magSize - ammoInMag;

            // Check if the inventoy has the required ammo count
            if (itemSlot.count >= ammoRequired)
            {
                yield return new WaitForSeconds(reloadTime);
                itemSlot.count -= ammoRequired;
                ammoInMag = magSize;
            }
            else // Else just takes what is left in the inventory
            {
                yield return new WaitForSeconds(reloadTime);
                ammoInMag += itemSlot.count;
                itemSlot.count = 0;

            }

            GameManager.instance.notificationCenter.FireGunMagAmmoChange(ammoInMag);
            GameManager.instance.notificationCenter.FireGunInventoyAmmoChange(GameManager.instance.inventory.GetItemSlot(ammoType));

        }
        else
        {
            OnReloadDenied();
        }



        if (ammoInMag > 0)
            gunState = GunState.Active;
        else
            gunState = GunState.Empty;


    }

    /// <summary>
    /// Is called When the reload is denied
    /// </summary>
    private void OnReloadDenied()
    {
        if (gunEmptyAudio != null)
            Instantiate(gunEmptyAudio);
    }



    /// <summary>
    /// Runs when a itme is added to the inventoy
    /// </summary>
    private void OnItemAdded()
    {
        GameManager.instance.notificationCenter.FireGunInventoyAmmoChange(GameManager.instance.inventory.GetItemSlot(ammoType));
    }



    /// <summary>
    /// Returns the ammo from the magasine
    /// </summary>
    /// <returns></returns>
    public int GetAmmoInMag()
    {
        return ammoInMag;
    }

    /// <summary>
    /// Returns the type of ammo used for the gun
    /// </summary>
    public ItemID GetAmmoType()
    {
        return ammoType;
    }


    /// <summary>
    /// Is called When the weapon refreshes
    /// </summary>
    public override void OnRefresh()
    {
        // je kan dit later nog veranderen als je ook attachmants will toevoegen

        base.OnRefresh();
        StopAllCoroutines();
        ammoInMag = magSize;
    }

    /// <summary>
    /// Draws The Gun Shooting Diraction And Point
    /// </summary>
    private void OnDrawGizmos()
    {
        if (shootPoint != null)
        {
            Gizmos.color = Color.red;

            Vector3 direction = shootPoint.forward;

            Gizmos.DrawRay(shootPoint.position, direction);
            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + 20f, 0) * new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - 20f, 0) * new Vector3(0, 0, 1);
            Gizmos.DrawRay(shootPoint.position + direction, right * 0.25f);
            Gizmos.DrawRay(shootPoint.position + direction, left * 0.25f);
        }
    }



}



