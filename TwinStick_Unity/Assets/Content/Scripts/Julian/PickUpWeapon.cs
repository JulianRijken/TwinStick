using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PickUpWeapon : MonoBehaviour
{
    [SerializeField] private WeaponID weapon = WeaponID.knife;

    private bool pickUpAllowed = true;
    private bool colliding = false;
    private Player player = null;


    private void Start()
    {
        colliding = false;
    }

    private void Update()
    {
        if (Input.GetButton("Use") && pickUpAllowed && colliding)
        {
            player.PickUpWeapon(weapon);
            Destroy(gameObject);
        }
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

    [ExecuteInEditMode]
    private void OnValidate()
    {
        gameObject.name = weapon.ToString() + "_PickUp";
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position + Vector3.up, "PickUp.psd");
    }
}