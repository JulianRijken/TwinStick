using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MiniEnemys : Damageable
{
    public GameObject Player;
    [SerializeField] private LayerMask hitLayer;
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

    private void OnCollisionEnter(Collision collider_enemy)
    {
        Damageable _damageble = collider_enemy.gameObject.GetComponent<Damageable>();
        Debug.Log((1 << collider_enemy.gameObject.layer) + " " + hitLayer.value);
        if (_damageble != null && 1 << collider_enemy.gameObject.layer == hitLayer.value)
        {
            Debug.Log("DAMAGE");
            _damageble.DoDamage(5, "Rat");
        }
    }
}