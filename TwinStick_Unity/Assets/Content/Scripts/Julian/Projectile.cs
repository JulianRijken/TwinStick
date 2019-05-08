using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float speed = 5;
    [SerializeField] float damage = 1;

    private Rigidbody2D rig;
    private Vector2 direction;


    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rig.MovePosition(transform.position + new Vector3(direction.x,direction.y,0) * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
            damageable.RemoveHealth(damage);

        Destroy(gameObject);
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }
}
