using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum PlayerAnimation { rifleAttack = 0, knifeAttack = 1 , rifleReload = 3}
public enum PlayerMovementState { walking = 0, rolling = 1}

[RequireComponent(typeof(Rigidbody))]
public class Player : Damageable
{

    [Header("Movement")]
    [SerializeField] private float defaultMoveSpeed = 5f;
    [SerializeField] private float moveAcceleration = 15f;
    [SerializeField] private float rotationSpeed = 30f;
    [SerializeField] private float rollDistance = 5f;
    [SerializeField] private float rollTime = 5f;
    [SerializeField] private AnimationCurve rollCurve = null;

    [Header("Weapon")]
    [SerializeField] private Transform weaponPivit;
    [SerializeField] private float throwForce = 5f;
    [SerializeField] private Vector2 torqueForce = new Vector2(-1,1);
    [SerializeField] private Weapon[] weapons = null;
    [SerializeField] private Weapon[] weaponsInInventory = null;
    [SerializeField] private PickUp[] weaponPickups = null;

    [Header("Animations")]
    [SerializeField] private Animator animator;

    private WeaponSlotType selectedSlot = WeaponSlotType.primary;
    private int weaponSlotCount;

    private Rigidbody rig = null;
    private Vector3 input = Vector3.zero;
    private Camera mainCamera;
    private float moveSpeed = 5f;
    private PlayerMovementState playerMovementState;

    private void Awake()
    {
        GameManager.instance.notificationCenter.OnPlayerAnimation += HandleAnimations;


        weaponSlotCount = Enum.GetValues(typeof(WeaponSlotType)).Length;
        weaponsInInventory = new Weapon[weaponSlotCount];
        weaponSlotCount -= 1;

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(false);
        }



        selectedSlot = WeaponSlotType.empty;
        PickUpWeapon(WeaponID.knife);


        moveSpeed = defaultMoveSpeed;
        playerMovementState = PlayerMovementState.walking;
    }

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        mainCamera = Camera.main;

        GameManager.instance.notificationCenter.FirePlayerHealthChange(health, maxHealth);
        GameManager.instance.notificationCenter.FireArmorHealthChange(armorHealth, maxArmorHealth);

    }

    private void Update()
    {
        HandelMoveInput();
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
    public void PickUpWeapon(WeaponID _weaponID,int _ammo = 0)
    {
        Weapon newWeapon = GetWeapon(_weaponID);
        newWeapon.OnRefresh();

        Gun _gun = newWeapon.GetComponent<Gun>();
        if (_gun != null)
            _gun.SetAmmoInMag(_ammo);

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
            GetWeapon(oldWeapon.weaponID).gameObject.SetActive(false);

            SpawnPickupWeapon(oldWeapon.weaponID);
        }


        if ((int)newWeapon.weaponSlotType > (int)selectedSlot)
            SwitchUp();
        else if ((int)newWeapon.weaponSlotType == (int)selectedSlot)
        {
            weaponsInInventory[(int)selectedSlot].gameObject.SetActive(true);
            weaponsInInventory[(int)selectedSlot].OnActive();
        }
        else if ((int)newWeapon.weaponSlotType < (int)selectedSlot)
            SwitchDown();

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

            if (playerMovementState != PlayerMovementState.rolling)
            {
                // Drop the weapon
                if (Input.GetButtonDown("Drop"))
                    DropCurrenWeapon();

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

    }

    /// <summary>
    /// Switches to the next above weapon
    /// </summary>
    private void SwitchUp()
    {
        weaponsInInventory[(int)selectedSlot].OnInActive();
        SetCurrenWeaponInActive();

        do
        {
            selectedSlot++;
            if ((int)selectedSlot > weaponSlotCount)
                selectedSlot = 0;
        }
        while (weaponsInInventory[(int)selectedSlot] == null);

        animator.SetFloat("currentSlotType", (float)selectedSlot);
        animator.SetTrigger("switchWeapon");

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

        do
        {
            selectedSlot--;
            if ((int)selectedSlot < 0)
                selectedSlot = (WeaponSlotType)weaponSlotCount;
        }
        while (weaponsInInventory[(int)selectedSlot] == null);

        animator.SetFloat("currentSlotType", (float)selectedSlot);
        animator.SetTrigger("switchWeapon");

        SetCurrenWeaponActive();
        weaponsInInventory[(int)selectedSlot].OnActive();

    }

    /// <summary>
    /// Spawns a item witch can be piket up
    /// </summary>
    private void SpawnPickupWeapon(WeaponID _weaponID)
    {
        //Debug.Log(_weaponID.ToString() + " Dropped");

        for (int i = 0; i < weaponPickups.Length; i++)
        {
            if(weaponPickups[i].GetWeaponID() == _weaponID)
            {
                PickUp _spawntWeapon = Instantiate(weaponPickups[i],transform.position + transform.forward,transform.rotation);

                Rigidbody _rb = _spawntWeapon.GetComponent<Rigidbody>();
                if(_rb != null)
                {
                    _rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
                    _rb.AddTorque(new Vector3(Random.Range(torqueForce.x,torqueForce.y), Random.Range(torqueForce.x, torqueForce.y), Random.Range(torqueForce.x, torqueForce.y)),ForceMode.Impulse);
                }

                Gun _gun = GetWeapon(_weaponID).GetComponent<Gun>();
                if (_gun != null)
                    _spawntWeapon.SetWeaponAmmo(_gun.GetAmmoInMag());

                //Debug.Log(_spawntWeapon.GetWeaponAmmo());

                return;
            }
        }
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

    /// <summary>
    /// Retuns the weapon from the pool by a id
    /// </summary>
    private Weapon GetWeaponInInventory(WeaponID _weaponID)
    {
        for (int i = 0; i < weaponsInInventory.Length; i++)
        {
            if (weaponsInInventory[i].weaponID == _weaponID)
                return weaponsInInventory[i];
        }

        return null;
    }


    #endregion



    #region PlayerMovent

    private void HandelMoveInput()
    {
        if (Input.GetKey(KeyCode.Space) && playerMovementState != PlayerMovementState.rolling )
        {
            Roll();
        }
    }

    private void Roll()
    {
        StartCoroutine(RollCoroutine());
    }

    private IEnumerator RollCoroutine()
    {
        SetPlayerState(PlayerMovementState.rolling);
        animator.SetTrigger("Roll");

        float timer = 0;

        while (timer < rollTime)
        {
            timer += Time.deltaTime;

            Vector3 _moveDir = transform.TransformDirection(Vector3.forward);
            rig.MovePosition(transform.position + _moveDir * Time.deltaTime * rollCurve.Evaluate(timer / rollTime) * rollDistance);

            yield return new WaitForFixedUpdate();
        }

        SetPlayerState(PlayerMovementState.walking);

    }

    /// <summary>
    /// Moves The Player
    /// </summary>
    private void Move()
    {
        if (playerMovementState == PlayerMovementState.walking)
        {
            if (GameManager.instance.GetMenuState() == GameMenuState.clear)
            {
                input = Vector3.Lerp(input, GetInput().normalized, Time.deltaTime * moveAcceleration);
                rig.velocity = input * moveSpeed;

                SetAnimatorVeloctiy(input.magnitude * moveSpeed);

                SetAnimatorInput(transform.InverseTransformDirection(input));
            }
        }
    }

    /// <summary>
    /// Rotates The Player
    /// </summary>
    private void Rotate()
    {
       // if (playerMovementState == PlayerMovementState.walking)
       //{
            if (GameManager.instance.GetMenuState() == GameMenuState.clear)
            {
                float yRot = Quaternion.LookRotation(GetMousePos(mainCamera) - transform.position, Vector3.up).eulerAngles.y;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yRot, 0), Time.deltaTime * rotationSpeed);
            }
        //}
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



    #region Animations

    void HandleAnimations(PlayerAnimation _animation)
    {
        switch(_animation)
        {
            case PlayerAnimation.knifeAttack:
                animator.SetTrigger("knifeAttack");
                break;

            case PlayerAnimation.rifleAttack:
                animator.SetTrigger("rifleAttack");
                break;

            case PlayerAnimation.rifleReload:
                animator.SetTrigger("rifleReload");
                break;
        }
    }


    /// <summary>
    /// Sets the animatior input float
    /// </summary>
    void SetAnimatorInput(Vector3 _input)
    {
        animator.SetFloat("InputX", _input.x);
        animator.SetFloat("InputY", _input.z);
    }

    /// <summary>
    /// Sets the animatior velocity
    /// </summary>
    void SetAnimatorVeloctiy(float _velocity)
    {
        animator.SetFloat("Velocity", _velocity);
    }

    #endregion


    /// <summary>
    /// Setst the player state
    /// </summary>
    public void SetPlayerState(PlayerMovementState _playerState)
    {
        playerMovementState = _playerState;
    }

    private void OnDestroy()
    {
        GameManager.instance.notificationCenter.OnPlayerAnimation -= HandleAnimations;
    }
}