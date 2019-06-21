using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{

    private enum PickupType { item = 1, weapon = 2 }


    [SerializeField] private PickupType pickupType = PickupType.item;
    [SerializeField] private float activeDelay = 1;

    [Header("Item")]
    [SerializeField] private ItemID item = ItemID.keyCardA;
    [SerializeField] private int count = 30;
    [SerializeField] private float pickUpDelay = 0.3f;
    [SerializeField] private float hightOffset = 1;
    [SerializeField] private int givePerPickUp = 5;
    private float timer;


    [Header("Weapon")]
    [SerializeField] private WeaponID weapon = WeaponID.knife;
    [SerializeField] private int startingAmmo = 30;
    private Player player = null;
    private int ammo;

    private float timeAlive;
    private Slider slider = null;
    private Animator animator = null;
    private bool pickUpAllowed = true;
    private bool colliding = false;


    private void Start()
    {
        colliding = false;
        player = null;

        animator = GetComponentInChildren<Animator>();
        slider = GetComponentInChildren<Slider>();


        if (pickupType == PickupType.item)
        {
            slider.maxValue = count;
            slider.minValue = 0;
            slider.value = slider.maxValue;
        }

        if (count == 1 || pickupType == PickupType.weapon)
            slider.gameObject.SetActive(false);


        Vector3 scaleTmp = animator.transform.localScale;
        scaleTmp.x /= transform.localScale.x;
        scaleTmp.y /= transform.localScale.y;
        scaleTmp.z /= transform.localScale.z;
        animator.transform.localScale = scaleTmp;

    }

    private void Update()
    {
        timeAlive += Time.deltaTime;

        if (Input.GetButton("Use") && colliding && timeAlive > activeDelay)
        {

            if(pickupType == PickupType.item)
            {
                if(pickUpAllowed)
                {
                    // Adds and removes the items
                    if (count >= givePerPickUp)
                    {
                        GameManager.instance.inventory.AddItem(item, givePerPickUp);
                        count -= givePerPickUp;
                    }
                    else if (count > 0)
                    {
                        GameManager.instance.inventory.AddItem(item, count);
                        count = 0;
                    }

                    // Checks if done and destroy
                    if (count <= 0)
                    {
                        Destroy(gameObject);
                    }

                    // Update Slider
                    if (slider != null)
                        slider.value = count;

                    // Create a delay
                    StartCoroutine(AllowDelayCoroutine());
                }
            }
            else
            {
                if (player != null)
                {
                    player.PickUpWeapon(weapon, ammo);
                    Destroy(gameObject);
                }
            }
        }
    }

    // Resets the delay
    private IEnumerator AllowDelayCoroutine()
    {
        pickUpAllowed = false;
        yield return new WaitForSeconds(pickUpDelay);
        pickUpAllowed = true;
    }

    public void SetWeaponAmmo(int _ammo)
    {
        ammo = _ammo;
    }

    public int GetWeaponAmmo()
    {
        return ammo;
    }


    public WeaponID GetWeaponID()
    {
        return weapon;
    }


    // Checks Collition
    private void OnTriggerEnter(Collider other)
    {
        if (timeAlive > activeDelay)
        {
            player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                colliding = true;

                if (animator != null)
                    animator.SetBool("colliding", true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (timeAlive > activeDelay)
        {
            player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                colliding = false;

                if (animator != null)
                    animator.SetBool("colliding", false);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position + Vector3.up, "PickUp.psd");
    }
}