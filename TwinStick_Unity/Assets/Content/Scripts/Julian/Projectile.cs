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
    [SerializeField] private LayerMask collisionLayer = new LayerMask();
    [SerializeField] private TrailRenderer testLine;

    private Rigidbody rig;
    private Vector3 lastPos;
    private bool hitWall;


    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        Destroy(gameObject, destroyTime);
        hitWall = false;
    }

    // Gebruik linecast
    // gebruik layer checkn voor wat te doen

    private void FixedUpdate()
    {
        Color _col = testLine.material.color;
        _col.a -= Time.deltaTime;
        _col.a = Mathf.Clamp(_col.a, 0, 255);
        testLine.material.color = _col;


        if (!hitWall)
        rig.MovePosition(transform.position + transform.forward * Time.deltaTime * moveSpeed);


        RaycastHit _hit;
        if (Physics.Linecast(lastPos, transform.position, out _hit,collisionLayer))
        {
            hitWall = true;
            transform.position = _hit.point;
            Debug.DrawLine(lastPos,transform.position,Color.red,2f);
        }

        lastPos = transform.position;

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
