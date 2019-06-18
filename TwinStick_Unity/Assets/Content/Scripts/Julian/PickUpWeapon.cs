using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PickUpWeapon : MonoBehaviour
{

    [SerializeField] private WeaponID weaponID = WeaponID.knife;
    [SerializeField] private int startingAmmo = 30;

    private bool pickUpAllowed = true;
    private bool colliding = false;
    private Player player = null;
    private float timer;
    private int ammo;


    private void Awake()
    {
        colliding = false;
        timer = 0;
        ammo = startingAmmo;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Use") && pickUpAllowed && colliding && timer > 0.5f)
        {
            player.PickUpWeapon(weaponID,ammo);
            Destroy(gameObject);
        }
    }

    public WeaponID GetWeaponID()
    {
        return weaponID;
    }

    public void SetWeaponAmmo(int _ammo)
    {
        ammo = _ammo;
    }

    public int GetWeaponAmmo()
    {
        return ammo;
    }

    // Checks Collition
    private void OnTriggerEnter(Collider other)
    {
        Player _player = other.gameObject.GetComponent<Player>();
        if (_player != null)
        {
            colliding = true;
            player = _player;
         }
    }

    private void OnTriggerExit(Collider other)
    {
        Player _player = other.gameObject.GetComponent<Player>();
        if (_player != null)
        {
            colliding = false;
            player = null;
        }
    }

    //private void OnValidate()
    //{
    //    gameObject.name = weaponID.ToString() + "_PickUp";
    //}

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position + Vector3.up, "PickUp.psd");
    }
}