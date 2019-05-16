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
    [SerializeField] private TrailRenderer smokeTrail;

    private Rigidbody rig;
    private Vector3 lastPos;
    private bool hitWall;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        Destroy(gameObject, destroyTime);
        hitWall = false;
        lastPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (smokeTrail != null)
        {
            Color _smokeTrailColor = smokeTrail.material.color;
            _smokeTrailColor.a -= Time.deltaTime;
            _smokeTrailColor.a = Mathf.Clamp(_smokeTrailColor.a, 0, 255);
            smokeTrail.material.color = _smokeTrailColor;
        }


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
