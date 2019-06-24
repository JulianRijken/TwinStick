using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MiniEnemys : Damageable
{
    public GameObject Player;

    // Update is called once per frame
    private void Update()
    {
        GetComponent<NavMeshAgent>().SetDestination(Player.transform.position);

        //Instantiate(bullet, shootPoint.position, shootPoint.rotation);
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        Destroy(gameObject);
        //Music.Stop();
    }

    private void OnCollisionStay(Collision collision_enemy)
    {
        if (collision_enemy.collider.CompareTag("Player"))
        {
            health -= 5;
        }
    }
}