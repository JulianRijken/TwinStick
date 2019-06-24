using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MiniEnemys : Damageable
{
    public GameObject Player;
    //public float dealdamage;

    // Update is called once per frame
    private void Update()
    {
        GetComponent<NavMeshAgent>().SetDestination(Player.transform.position);

        //Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        //dealdamage = health - 5;
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        Destroy(gameObject);
        //Music.Stop();
    }

    //private void OnTriggerEnter(Collider collider_enemy)
    //{
    //    if (collider_enemy.gameObject.tag == "player")
    //    {
    //        maxHealth -= 5;
    //    }
    //}
}