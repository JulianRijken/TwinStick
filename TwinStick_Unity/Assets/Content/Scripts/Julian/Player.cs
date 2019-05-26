using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : Damageable
{

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float moveAcceleration = 15f;
    [SerializeField] private float rotationSpeed = 30f;

    [Header("Weapon")]
    [SerializeField] private Transform weaponPivit;
    [SerializeField] private Weapon[] weapons = null;

    [SerializeField] private Weapon[] weaponsInInventory;
    private WeaponSlotType selectedSlot = WeaponSlotType.primary;
    private int weaponSlotCount;


    private Rigidbody rig = null;
    private Vector3 input = Vector3.zero;
    private Camera mainCamera;

    private void Awake()
    {
        weaponSlotCount = Enum.GetValues(typeof(WeaponSlotType)).Length;
        weaponsInInventory = new Weapon[weaponSlotCount];
        weaponSlotCount -= 1;

        for (int i = 0; i < weapons.Length; i++)
        {
            Weapon weapon = Instantiate(weapons[i], weaponPivit.position, weaponPivit.rotation, weaponPivit);
            weapon.gameObject.SetActive(false);
            weapons[i] = weapon;
        }

        weaponsInInventory[(int)WeaponSlotType.empty] = weapons[0];
        selectedSlot = WeaponSlotType.empty;
        weaponsInInventory[(int)WeaponSlotType.empty].gameObject.SetActive(true);
    }

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        InventoryController inventory = GameManager.instance.inventory;
        mainCamera = Camera.main;

        GameManager.instance.notificationCenter.FirePlayerHealthChange(health, maxHealth);
        GameManager.instance.notificationCenter.FireArmorHealthChange(armorHealth, maxArmorHealth);
    }

    private void Update()
    {
        HandelWeaponInput();      
    }

    void FixedUpdate()
    {
        Move();
        Rotate();
    }


    #region Weapon


    /// <summary>
    /// Picks up the weapon and if needed switches it in the invenety
    /// </summary>
    public void PickUpWeapon(WeaponID _weaponID)
    {
        Weapon newWeapon = GetWeapon(_weaponID);
        newWeapon.OnRefresh();
        Weapon oldWeapon = weaponsInInventory[(int)newWeapon.weaponSlotType];

        // Check if you already have the item
        if (oldWeapon == null)
        {
            // So yes, just replace the item
            weaponsInInventory[(int)newWeapon.weaponSlotType] = newWeapon;
        }
        else
        {
            // So no, throw away the old weapon and than switch it
            weaponsInInventory[(int)newWeapon.weaponSlotType] = newWeapon;
            SpawnPickupWeapon(oldWeapon.weaponID);
        }
    }

    /// <summary>
    /// Allws the weapon to shoot and use all of its functions
    /// </summary>
    private void HandelWeaponInput()
    {
        if (GameManager.instance.GetMenuState() == GameMenuState.clear)
        {

            // Switch the weapon
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
                SwitchUp();
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
                SwitchDown();

            // Drop the weapon
            if (Input.GetButton("Drop"))
                DropCurrenWeapon();

            //Todo HAAL WEG DIT IS VOOR TESTING
            if (Input.GetKeyDown(KeyCode.P))
                PickUpWeapon(WeaponID.flameThrower);

            // Give The weapon input
            if (weaponsInInventory[(int)selectedSlot] != null)
            {
                // Als je will toevoegen dat het wapen niet kan schieten als de player niet in stat is zoals als hij van wapen switcht dan kan je dat hier verandern

                if (Input.GetButtonDown("Reload"))
                    weaponsInInventory[(int)selectedSlot].Reload();

                if (weaponsInInventory[(int)selectedSlot].weaponInput == WeaponInputType.hold ? Input.GetButton("Attack") : Input.GetButtonDown("Attack"))
                    weaponsInInventory[(int)selectedSlot].Attack();

                if (Input.GetButtonUp("Attack"))
                    weaponsInInventory[(int)selectedSlot].StopAttack();

                if (Input.GetButtonDown("Gadget"))
                    weaponsInInventory[(int)selectedSlot].UseGadget();

            }
        }

    }

    /// <summary>
    /// Switches to the next above weapon
    /// </summary>
    private void SwitchUp()
    {
        weaponsInInventory[(int)selectedSlot].OnInActive();
        SetCurrenWeaponInActive();

        selectedSlot++;
        if ((int)selectedSlot > weaponSlotCount)
            selectedSlot = 0;

        while (weaponsInInventory[(int)selectedSlot] == null)
        {
            selectedSlot++;
            if ((int)selectedSlot > weaponSlotCount)
                selectedSlot = 0;
        }

        SetCurrenWeaponActive();
        weaponsInInventory[(int)selectedSlot].OnActive();
    }

    /// <summary>
    /// Switches to the next under weapon
    /// </summary>
    private void SwitchDown()
    {
        weaponsInInventory[(int)selectedSlot].OnInActive();
        SetCurrenWeaponInActive();

        selectedSlot--;
        if ((int)selectedSlot < 0)
            selectedSlot = (WeaponSlotType)weaponSlotCount;

        while (weaponsInInventory[(int)selectedSlot] == null)
        {
            selectedSlot--;
            if ((int)selectedSlot < 0)
                selectedSlot = (WeaponSlotType)weaponSlotCount; ;
        }

        SetCurrenWeaponActive();
        weaponsInInventory[(int)selectedSlot].OnActive();

    }

    /// <summary>
    /// Spawns a item witch can be piket up
    /// </summary>
    private void SpawnPickupWeapon(WeaponID _weaponID)
    {
        Debug.Log(_weaponID.ToString() + " Dropped");
    }

    /// <summary>
    /// Drops the current weapon
    /// </summary>
    private void DropCurrenWeapon()
    {
        Weapon dropWeapon = weaponsInInventory[(int)selectedSlot];

        if (dropWeapon.weaponSlotType != WeaponSlotType.empty)
        {
            int oldSelectedSlot = (int)selectedSlot;

            SwitchDown();
            weaponsInInventory[oldSelectedSlot] = null;
            SpawnPickupWeapon(dropWeapon.weaponID);
        }
    }

    /// <summary>
    /// Sets the current holding weapon to Active
    /// </summary>
    private void SetCurrenWeaponActive()
    {
        Weapon oldWeapon = weaponsInInventory[(int)selectedSlot];
        if (oldWeapon != null)
        {
            oldWeapon.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Sets the current holding weapon to InActive
    /// </summary>
    private void SetCurrenWeaponInActive()
    {
        Weapon oldWeapon = weaponsInInventory[(int)selectedSlot];
        if (oldWeapon != null)
        {
            oldWeapon.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Retuns the weapon from the pool by a id
    /// </summary>
    private Weapon GetWeapon(WeaponID _weaponID)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].weaponID == _weaponID)
                return weapons[i];
        }

        return null;
    }


    #endregion


    #region PlayerMovent

    /// <summary>
    /// Moves The Player
    /// </summary>
    private void Move()
    {
        if (GameManager.instance.GetMenuState() == GameMenuState.clear)
        {
            input = Vector3.Lerp(input, GetInput().normalized, Time.deltaTime * moveAcceleration);
            rig.MovePosition(transform.position + input * Time.deltaTime * moveSpeed);
        }
    }

    /// <summary>
    /// Rotates The Player
    /// </summary>
    private void Rotate()
    {
        if (GameManager.instance.GetMenuState() == GameMenuState.clear)
        {
            float yRot = Quaternion.LookRotation(GetMousePos(mainCamera) - transform.position, Vector3.up).eulerAngles.y;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yRot, 0), Time.deltaTime * rotationSpeed);
        }
    }

    /// <summary>
    /// Returns the input
    /// </summary>
    private Vector3 GetInput()
    {
        return new Vector3(Input.GetAxisRaw("Horizontal"), 0,Input.GetAxisRaw("Vertical")); 
    }

    /// <summary>
    /// Returns if the player is moving
    /// </summary>
    private bool IsMoving()
    {
        return (GetInput().magnitude == 0 ? false : true);
    }

    /// <summary>
    /// Returns The Mouse Pos
    /// </summary>
    private Vector3 GetMousePos(Camera cam)
    {
        if (cam != null)
        {
            Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

            float rayLength;
            groundPlane.Raycast(cameraRay, out rayLength);

            return cameraRay.GetPoint(rayLength);
        }

        return Vector3.zero;
    }

    #endregion


    #region Damageble

    protected override void OnHealthLost(float healthLost, string hitBy)
    {
        GameManager.instance.notificationCenter.FirePlayerHealthChange(health, maxHealth);
        GameManager.instance.statsController.AddHealthLost(healthLost);

    }

    protected override void OnArmorHealthLost(float armorHealthLost)
    {
        GameManager.instance.notificationCenter.FireArmorHealthChange(armorHealth, maxArmorHealth);
    }

    protected override void OnDeath(string diedBy)
    {
        GameManager.instance.notificationCenter.FirePlayerDied();
        GameManager.instance.statsController.AddDiedBy(diedBy);
    }

    #endregion
}