﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MiniEnemys : Damageable
{
    [SerializeField] public GameObject itemdrop;
    public GameObject Player;
    [SerializeField] private LayerMask hitLayer;

    // Update is called once per frame
    private void Update()
    {
        GetComponent<NavMeshAgent>().SetDestination(Player.transform.position);

       
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        Destroy(gameObject);
        if(itemdrop != null)
        Instantiate(itemdrop);
    }

    private void OnCollisionEnter(Collision collider_enemy)
    {
        Damageable _damageble = collider_enemy.gameObject.GetComponent<Damageable>();
        Debug.Log((1 << collider_enemy.gameObject.layer) + " " + hitLayer.value);
        if (_damageble != null && 1 << collider_enemy.gameObject.layer == hitLayer.value)
        {
            Debug.Log("DAMAGE");
            _damageble.DoDamage(20, "Rat");
        }
    }
}
