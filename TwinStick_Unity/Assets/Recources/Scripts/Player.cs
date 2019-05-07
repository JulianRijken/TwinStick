using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : Damageable
{

    private Rigidbody rig = null;

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 30f;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rig.MovePosition(transform.position + GetInput().normalized * Time.deltaTime * moveSpeed);

        float yRot = Quaternion.LookRotation(GetMousePos() - transform.position,Vector3.up).eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(0, yRot,0),Time.deltaTime * rotationSpeed);
    }

    /// <summary>
    /// Returns the input
    /// </summary>
    private Vector3 GetInput()
    {
        return new Vector3(Input.GetAxisRaw("Horizontal"), 0,Input.GetAxisRaw("Vertical")); 
    }
            
    /// <summary>
    /// Returns The Mouse Pos
    /// </summary>
    private Vector3 GetMousePos()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        float rayLength;
        groundPlane.Raycast(cameraRay, out rayLength);

        return cameraRay.GetPoint(rayLength);
    }

}
