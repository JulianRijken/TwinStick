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


    //xtodo Refresh het wapen zodra je een niewe op pakt 
    //todo Sla de refresh dingen op in een struct bij het wapen
    //todo ui gewoon updaten zodra je van wapen switched
    //todo fix het dropen van het wapen

    private void Awake()
    {
        weaponSlotCount = Enum.GetValues(typeof(WeaponSlotType)).Length;
        weaponsInInventory = new Weapon[weaponSlotCount];

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

    private void SwitchUp()
    {
        weaponsInInventory[(int)selectedSlot].gameObject.SetActive(false);

        selectedSlot++;
        if ((int)selectedSlot >= weaponSlotCount)
            selectedSlot = 0;

        while (weaponsInInventory[(int)selectedSlot] == null)
        {
            selectedSlot++;
            if ((int)selectedSlot >= weaponSlotCount)
                selectedSlot = 0;
        }

        weaponsInInventory[(int)selectedSlot].gameObject.SetActive(true);
    }

    private void SwitchDown()
    {
        weaponsInInventory[(int)selectedSlot].gameObject.SetActive(false);

        selectedSlot--;
        if ((int)selectedSlot < 0)
            selectedSlot = (WeaponSlotType)weaponSlotCount - 1;

        while (weaponsInInventory[(int)selectedSlot] == null)
        {
            selectedSlot--;
            if ((int)selectedSlot < 0)
                selectedSlot = (WeaponSlotType)weaponSlotCount - 1; ;
        }

        weaponsInInventory[(int)selectedSlot].gameObject.SetActive(true);

    }

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
    /// Spawns a item witch can be piket up
    /// </summary>
    private void SpawnPickupWeapon(WeaponID _weaponID)
    {
        Debug.Log(_weaponID.ToString() + " Dropped");
    }

    /// <summary>
    /// Allws the weapon to shoot and use all of its functions
    /// </summary>
    private void HandelWeaponInput()
    {

        float _input = Input.GetAxis("Mouse ScrollWheel");
        if (_input > 0)
            SwitchUp();
        else if (_input < 0)
            SwitchDown();

        if (Input.GetButton("Drop"))
            DropCurrenWeapon();

        if (Input.GetKeyDown(KeyCode.P))
            PickUpWeapon(WeaponID.primaryTest);

        if (weaponsInInventory[(int)selectedSlot] != null)
        {
            // Als je will toevoegen dat het wapen niet kan schieten als de player niet in stat is zoals als hij van wapen switcht dan kan je dat hier verandern

            if (Input.GetButtonDown("Reload"))
                weaponsInInventory[(int)selectedSlot].Reload();

            if (weaponsInInventory[(int)selectedSlot].weaponInput == WeaponInputType.hold ? Input.GetButton("Attack") : Input.GetButtonDown("Attack"))
                weaponsInInventory[(int)selectedSlot].Attack();

            if (Input.GetButtonDown("Gadget"))
                weaponsInInventory[(int)selectedSlot].UseGadget();

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

    /// <summary>
    /// Drops the current weapon
    /// </summary>
    private void DropCurrenWeapon()
    {
        //Weapon dropWeapon = weaponsInInventory[(int)selectedSlot];

        //if (dropWeapon.weaponSlotType != WeaponSlotType.empty)
        //{
        //    weaponsInInventory[(int)selectedSlot] = null;
        //    SwitchDown();

        //    SpawnPickupWeapon(dropWeapon.weaponID);
        //}
    }


    #endregion


    #region PlayerMovent

    /// <summary>
    /// Moves The Player
    /// </summary>
    private void Move()
    {
        input = Vector3.Lerp(input, GetInput().normalized,Time.deltaTime * moveAcceleration);
        rig.MovePosition(transform.position + input * Time.deltaTime * moveSpeed);
    }

    /// <summary>
    /// Rotates The Player
    /// </summary>
    private void Rotate()
    {
        float yRot = Quaternion.LookRotation(GetMousePos(mainCamera) - transform.position, Vector3.up).eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yRot, 0), Time.deltaTime * rotationSpeed);
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



//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
//public class Player : Damageable
//{

//    [Header("Movement")]
//    [SerializeField] private float moveSpeed = 10f;
//    [SerializeField] private float moveAcceleration = 15f;
//    [SerializeField] private float rotationSpeed = 30f;

//    [Header("Weapon")]
//    [SerializeField] private Transform weaponPivit;
//    [SerializeField] private Weapon[] weapons = null;

//    private Weapon[] weaponsInInventory;
//    private WeaponSlotType selectedSlot = WeaponSlotType.primary;
//    private int weaponSlotCount;

//    private Rigidbody rig = null;
//    private Vector3 input = Vector3.zero;
//    private Camera mainCamera;

//    private void Awake()
//    {
//        weaponSlotCount = Enum.GetValues(typeof(WeaponSlotType)).Length;
//        weaponsInInventory = new Weapon[weaponSlotCount];

//        for (int i = 0; i < weapons.Length; i++)
//        {
//            weapons[i].gameObject.SetActive(false);
//        }
//    }

//    void Start()
//    {
//        rig = GetComponent<Rigidbody>();
//        InventoryController inventory = GameManager.instance.inventory;
//        mainCamera = Camera.main;

//        // Remove
//        DoDamage(100, 5f, "Player");

//        GameManager.instance.notificationCenter.FirePlayerHealthChange(health, maxHealth);
//        GameManager.instance.notificationCenter.FireArmorHealthChange(armorHealth, maxArmorHealth);
//    }

//    private void Update()
//    {
//        HandelWeaponInput();

//        CheckWeaponInput();

//        // Verander dit en zorg voor een input manager
//        if (Input.GetKeyDown(KeyCode.Escape))
//        {
//            if (GameManager.instance.gamePaused)
//                GameManager.instance.notificationCenter.FireGameUnPaused();
//            else
//                GameManager.instance.notificationCenter.FireGamePaused();
//        }


//    }

//    private void CheckWeaponInput()
//    {
//        float _input = Input.GetAxis("Mouse ScrollWheel");
//        if (_input > 0)
//        {
//            int newSLot;
//            newSLot = (int)selectedSlot;
//            newSLot++;

//            if (newSLot >= weaponSlotCount)
//                newSLot = 0;

//            selectedSlot = (WeaponSlotType)newSLot;



//        }
//        else if (_input < 0)
//        {
//            int newSLot;
//            newSLot = (int)selectedSlot;
//            newSLot--;

//            if (newSLot < 0)
//                newSLot = weaponSlotCount - 1;

//            selectedSlot = (WeaponSlotType)newSLot;

//        }


//    }

//    private void SwapWeapon()
//    {
//        if (selectedSlot == WeaponSlotType.primary && weaponsInInventory[1] != null)
//        {
//            selectedSlot = WeaponSlotType.secondary;
//        }
//        else if (selectedSlot == WeaponSlotType.secondary && weaponsInInventory[0] != null)
//        {
//            selectedSlot = WeaponSlotType.primary;
//        }

//    }

//    private void PickupWeapon(WeaponID weaponID)
//    {

//        for (int i = 0; i < weapons.Length; i++)
//        {
//            if (weapons[i].weaponID == weaponID)
//            {
//                if (weapons[i].weaponSlotType == WeaponSlotType.primary)
//                {
//                    weaponsInInventory[0] = weapons[i];
//                }
//                else if (weapons[i].weaponSlotType == WeaponSlotType.secondary)
//                {
//                    weaponsInInventory[1] = weapons[i];
//                }
//            }
//        }
//    }

//    private void HandelWeaponInput()
//    {
//        if (weaponsInInventory[(int)selectedSlot] != null)
//        {
//            // Als je will toevoegen dat het wapen niet kan schieten als de player niet in stat is zoals als hij van wapen switcht dan kan je dat hier verandern

//            if (Input.GetButtonDown("Reload"))
//                weaponsInInventory[(int)selectedSlot].Reload();

//            if (weaponsInInventory[(int)selectedSlot].weaponInput == WeaponInputType.hold ? Input.GetButton("Attack") : Input.GetButtonDown("Attack"))
//                weaponsInInventory[(int)selectedSlot].Attack();

//            if (Input.GetButtonDown("Gadget"))
//                weaponsInInventory[(int)selectedSlot].UseGadget();

//        }
//    }

//    void FixedUpdate()
//    {
//        Move();
//        Rotate();
//    }

//    /// <summary>
//    /// Moves The Player
//    /// </summary>
//    private void Move()
//    {
//        input = Vector3.Lerp(input, GetInput().normalized, Time.deltaTime * moveAcceleration);
//        rig.MovePosition(transform.position + input * Time.deltaTime * moveSpeed);
//    }

//    /// <summary>
//    /// Rotates The Player
//    /// </summary>
//    private void Rotate()
//    {
//        float yRot = Quaternion.LookRotation(GetMousePos(mainCamera) - transform.position, Vector3.up).eulerAngles.y;
//        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yRot, 0), Time.deltaTime * rotationSpeed);
//    }

//    /// <summary>
//    /// Returns the input
//    /// </summary>
//    private Vector3 GetInput()
//    {
//        return new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
//    }

//    /// <summary>
//    /// Returns if the player is moving
//    /// </summary>
//    private bool IsMoving()
//    {
//        return (GetInput().magnitude == 0 ? false : true);
//    }

//    /// <summary>
//    /// Returns The Mouse Pos
//    /// </summary>
//    private Vector3 GetMousePos(Camera cam)
//    {
//        if (cam != null)
//        {
//            Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
//            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

//            float rayLength;
//            groundPlane.Raycast(cameraRay, out rayLength);

//            return cameraRay.GetPoint(rayLength);
//        }

//        return Vector3.zero;
//    }



//    protected override void OnHealthLost(float healthLost, string hitBy)
//    {
//        GameManager.instance.notificationCenter.FirePlayerHealthChange(health, maxHealth);
//        GameManager.instance.statsController.AddHealthLost(healthLost);

//    }

//    protected override void OnArmorHealthLost(float armorHealthLost)
//    {
//        GameManager.instance.notificationCenter.FireArmorHealthChange(armorHealth, maxArmorHealth);
//    }

//    protected override void OnDeath(string diedBy)
//    {
//        GameManager.instance.notificationCenter.FirePlayerDied();
//        GameManager.instance.statsController.AddDiedBy(diedBy);
//    }
//}