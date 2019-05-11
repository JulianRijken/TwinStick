using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    private enum GunType{Semi,Auto}
    private enum GunState{Active,Reloading,Empty }


    [SerializeField] private GunType gunType = GunType.Auto;
    [SerializeField] private ItemID ammoType = ItemID.Ammo;

    [SerializeField] private float timeBitweenShots = 0.1f;
    [SerializeField] private float reloadTime = 1f;
    [SerializeField] private int magSize = 30;
    [SerializeField] private int startingAmmo = 30;

    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private GameObject muzzleFlash = null;
    [SerializeField] private GameObject projectile = null;
    [SerializeField] public Lazer lazer = null;


    private GunState gunState = GunState.Active;
    private int ammoInMag = 0;
    private bool chamberLoaded = true;


    private void Start()
    {
        chamberLoaded = true;
        ammoInMag = startingAmmo;
    }

    /// <summary>
    /// Turns the lazer on or off
    /// </summary>
    public void SwitchLazer()
    {
        if(lazer != null)
        {
            lazer.SwitchPower();
        }
    }


    /// <summary>
    /// Triggers the gun to shot
    /// </summary>
    public void Shoot()
    {
        if(gunState.Equals(GunState.Active))
        {
            if (ammoInMag > 0)
            {
                if (chamberLoaded)
                {
                    if (muzzleFlash != null)
                        Instantiate(muzzleFlash, shootPoint.position, shootPoint.rotation, shootPoint);

                    if (projectile != null)
                        Instantiate(projectile, shootPoint.position, shootPoint.rotation);
                    ammoInMag--;

                    StartCoroutine(IReloadChamber());
                }
            }
            else
            {
                gunState = GunState.Empty;
            }

        }
    }

    public void ShootAuto()
    {
        if (gunType.Equals(GunType.Auto))
            Shoot();
    }


    /// <summary>
    /// ReloadsTheGunChamber
    /// </summary>
    IEnumerator IReloadChamber()
    {
        chamberLoaded = false;
        yield return new WaitForSeconds(timeBitweenShots);
        chamberLoaded = true;
    }


    /// <summary>
    /// Reloads Tha magasine
    /// </summary>
    public void ReloadMag()
    { 
        if(!gunState.Equals(GunState.Reloading))
        {   
            StartCoroutine(IReloadMag());
        }
    }

    IEnumerator IReloadMag()
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

    private void OnReloadDenied()
    {
        Debug.Log("RELOAD DENIED");
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
