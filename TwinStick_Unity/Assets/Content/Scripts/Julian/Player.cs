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

    [Header("Items")]
    [SerializeField] private Weapon weapon = null;

    private Rigidbody rig = null;
    private Vector3 input = Vector3.zero;
    private Camera mainCamera;


    void Start()
    {

        rig = GetComponent<Rigidbody>();
        InventoryController inventory = GameManager.instance.inventory;
        mainCamera = Camera.main;

        DoDamage(100, 5f, "Player");

        GameManager.instance.notificationCenter.FirePlayerHealthChange(health, maxHealth);
        GameManager.instance.notificationCenter.FireArmorHealthChange(armorHealth, maxArmorHealth);
    }

    private void Update()
    {
        HandelWeaponInput();

        // Verander dit en zorg voor een input manager
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.instance.gamePaused)
                GameManager.instance.notificationCenter.FireGameUnPaused();
            else
                GameManager.instance.notificationCenter.FireGamePaused();

        }

    }

    private void HandelWeaponInput()
    {
        if (weapon != null)
        {
            // Als je will toevoegen dat het wapen niet kan schieten als de player niet in stat is zoals als hij van wapen switcht dan kan je dat hier verandern

            if (Input.GetButtonDown("Reload"))
                weapon.Reload();

            if (weapon.weaponInput == WeaponInputType.hold ? Input.GetButton("Attack") : Input.GetButtonDown("Attack"))
                weapon.Attack();

            if (Input.GetButtonDown("Gadget"))
                weapon.UseGadget();

        }
    }

    void FixedUpdate()
    {
        Move();
        Rotate();
    }

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

    /// <summary>
    /// Retuns the weapon in hand
    /// </summary>
    public Weapon GetWeapon()
    {
        return weapon;
    }


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
}