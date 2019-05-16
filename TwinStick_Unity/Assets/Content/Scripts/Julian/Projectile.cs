using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5;
    [SerializeField] float damage = 1;
    [SerializeField] float destroyTime = 5;
    [SerializeField] private string projectileName = "Bullet";

    private Rigidbody rig;
    private Vector3 lastPos;


    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        Destroy(gameObject, destroyTime);
    }

    // Gebruik linecast
    // gebruik layer checkn voor wat te doen

    private void FixedUpdate()
    {
        RaycastHit _hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out _hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * _hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {

            rig.MovePosition(transform.position + transform.forward * Time.deltaTime * moveSpeed);
            lastPos = transform.position;
        }
    }

    void OnHit(GameObject hitObject)
    {
        Damageable damageable = hitObject.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.RemoveHealth(damage, projectileName);

            Destroy(gameObject, 0.5f);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
