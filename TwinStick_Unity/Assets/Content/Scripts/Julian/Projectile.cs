using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float damage = 1;
    [SerializeField] private float destroyTime = 5;
    [SerializeField] private string projectileName = "Bullet";
    [SerializeField] private LayerMask collisionLayer = new LayerMask();
    [SerializeField] private TrailRenderer smokeTrail;
    [SerializeField] private GameObject targetHitSound = null;

    private Rigidbody rig;
    private Vector3 lastPos;
    private bool hitWall;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        Destroy(gameObject, destroyTime);
        hitWall = false;
        lastPos = transform.position;

        Quaternion _flatRotation = transform.rotation;
        _flatRotation.x = 0;
        _flatRotation.z = 0;

        transform.rotation = _flatRotation;
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
                switch (_hit.transform.gameObject.layer)
                {
                    case 11:
                        OnHitWall(_hit);
                        break;

                    default:
                        OnHitTarget(_hit);
                        break;
                }

                //Debug.DrawLine(lastPos, transform.position, Color.red, 2f);
            }

            lastPos = transform.position;
        }
    }

    private void OnHitWall(RaycastHit _hit)
    {
        hitWall = true;
        transform.position = _hit.point;
        Destroy(gameObject, 2f);
    }

    private void OnHitTarget(RaycastHit _hit)
    {
        Damageable damageable = _hit.transform.gameObject.GetComponent<Damageable>();
        if (damageable != null)
        {
            if(targetHitSound != null)
                Instantiate(targetHitSound);
            damageable.DoDamage(damage, projectileName);
            hitWall = true;
            transform.position = _hit.point;
            Destroy(gameObject, 2f);
        }
    }
}