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
        // Make Smoke dissapere
        if (smokeTrail != null)
        {
            Color _smokeTrailColor = smokeTrail.material.color;
            _smokeTrailColor.a -= Time.deltaTime;
            _smokeTrailColor.a = Mathf.Clamp(_smokeTrailColor.a, 0, 255);
            smokeTrail.material.color = _smokeTrailColor;
        }

        // Move the projectile
        if (!hitWall)
        {
            transform.position += transform.forward * Time.deltaTime * moveSpeed;

            RaycastHit _hit;
            if (Physics.Linecast(lastPos, transform.position, out _hit, collisionLayer))
            {
                // !!!!!!!!!! Zorg dat je iets van een static layer manager heb zodat je niet errors krijgt als je een layer van plek veranderd
                switch(_hit.transform.gameObject.layer)
                {
                    case 9:
                        OnHitEnemy(_hit);
                        break;
                    case 11:
                        OnHitWall(_hit);
                        break;
                }

                //Debug.DrawLine(lastPos, transform.position, Color.red, 2f);
            }

            lastPos = transform.position;
        }

    }

    void OnHitWall(RaycastHit _hit)
    {
        hitWall = true;
        transform.position = _hit.point;
        Destroy(gameObject, 2f);
    }

    void OnHitEnemy(RaycastHit _hit)
    {
        Damageable damageable = _hit.transform.gameObject.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.RemoveHealth(damage, projectileName);
            hitWall = true;
            transform.position = _hit.point;
            Destroy(gameObject, 2f);
        }
    }

}
