using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : Damageable
{

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float moveAcceleration = 15f;
    [SerializeField] private float rotationSpeed = 30f;
    [SerializeField] private Gun gun = null;

    private Rigidbody rig = null;
    private Vector3 input = Vector3.zero;
    private Camera mainCamera;


    void Start()
    {
        rig = GetComponent<Rigidbody>();
        InventoryController inventory = GameManager.instance.inventory;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        HandleGun();
    }

    private void HandleGun()
    {
        if (gun != null)
        {
            // Hier kan je hem niet laten schieten als je in een animatie zit

            if (Input.GetButtonDown("Reload"))
                gun.ReloadMag();
            else if (Input.GetButtonDown("Shoot"))
                gun.Shoot();
            else if (Input.GetButton("Shoot"))
                gun.ShootAuto();

            if (Input.GetButtonDown("SwitchLazer"))
                gun.SwitchLazer();

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
        Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        float rayLength;
        groundPlane.Raycast(cameraRay, out rayLength);

        return cameraRay.GetPoint(rayLength);
    }


    public Gun GetGun()
    {
        return gun;
    }
}