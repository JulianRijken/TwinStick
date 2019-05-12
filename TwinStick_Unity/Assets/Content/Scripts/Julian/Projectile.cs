using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5;
    [SerializeField] float damage = 1;
    [SerializeField] float destroyTime = 5;

    private Rigidbody rig;


    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        Destroy(gameObject, destroyTime);
    }

    private void FixedUpdate()
    {
        rig.MovePosition(transform.position + transform.forward * Time.deltaTime * moveSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Damageable damageable = collision.transform.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.RemoveHealth(damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
