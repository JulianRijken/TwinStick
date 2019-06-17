using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PickUpWeapon : MonoBehaviour
{

    [SerializeField] private WeaponID weaponID = WeaponID.knife;

    private bool pickUpAllowed = true;
    private bool colliding = false;
    private Player player = null;
    private float timer;


    private void Start()
    {
        colliding = false;
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Use") && pickUpAllowed && colliding && timer > 1)
        {
            player.PickUpWeapon(weaponID);
            Destroy(gameObject);
        }
    }

    public WeaponID GetWeaponID()
    {
        return weaponID;
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