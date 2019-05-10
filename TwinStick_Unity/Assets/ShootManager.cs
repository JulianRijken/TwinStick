using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour {

    [Header("For Key Input")]
    public bool keyInput;
    public KeyCode shootKey;
    public KeyCode reloadKey;

    [Header("Ammo is needed")]
    public GameObject ammo;
    public GameObject flash;

    [Header("Time between shots")]
    public float shotDelay;
    private float shootDelayTimer;

    [Header("Shoots multiple ammo")]
    public bool burst = false;
    public int burstCount = 5;

    [Header("Optonal")]
    public GameObject fireAudio;

    [Header("Reloading")]
    public bool needsToReload = true;
    public int fullMagSize = 10;
    public float reloadTime;
    [HideInInspector] public bool isReloading = false;
    [HideInInspector] public int magCount = 10;

    public int magazinesAmmo = 40;


    [Header("Shoots every frame")]
    public bool autoShoot = false;


    private void Start()
    {
        magCount = fullMagSize;
        if (magCount <= 0)
            magCount = 0;
    }


    void Update()
    {


        // Delay
        shootDelayTimer += Time.deltaTime;

        // Auto Shoot
        if(autoShoot)
        {
            Shoot(shotDelay);         
        }

        // Key Input
        if(keyInput)
        {
            if(Input.GetKey(shootKey))
            {
                Shoot(shotDelay);
            }

            if (Input.GetKey(reloadKey))
            {
                Reload();
            }
        }


        magazinesAmmo = Mathf.Clamp(magazinesAmmo, 0, 1000);

    }

    public void Reload()
    {

        if (isReloading == false && magazinesAmmo > 0)
        {

             StartCoroutine(ReloadMag());

        }


    }

    private IEnumerator ReloadMag()
    {

        int ammoNeeded = fullMagSize - magCount;

        if (ammoNeeded <= magazinesAmmo)
        {
            magazinesAmmo -= ammoNeeded;
        }
        else
        {
            ammoNeeded = magazinesAmmo;
            magazinesAmmo = 0;
        }

        

        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        magCount += ammoNeeded;

        isReloading = false;
    }




    public void Shoot(float _shootDelay)
    {
        shotDelay = _shootDelay;

        if (shootDelayTimer >= shotDelay && magCount > 0 && isReloading == false)
        {
            shootDelayTimer = 0;


            if (burst)
            {
                for (int i = 0; i < burstCount; i++)
                {
                    SpawnAmmo();
                }
            }
            else
            {

                SpawnAmmo();
            }
        }
    }

    private void SpawnAmmo()
    {
        // Manage reloading 
        if(needsToReload)
        {
            magCount--;
        }


        // Spawn ammo
        if (ammo != null)
        {
            Instantiate(ammo, transform.position, transform.rotation);
            PlayAudio();
        }
        else
        {
            Debug.LogError("Ammo Gameobject is NULL");
        }


        if (flash != null)
            Instantiate(flash, transform.position, transform.rotation);
    }


    void PlayAudio()
    {
        if (fireAudio != null)
        {
           Instantiate(fireAudio, transform.position, transform.rotation);
        }
    }

}
