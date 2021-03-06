﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TongueState
{
    passive = 0,
    aggressive,
    attacking
}

public class Tongue : Damageable
{
    private Animator anim;

    [SerializeField] private TongueState state;

    [SerializeField] private LayerMask hitLayer;

    private Player player;

    [SerializeField] private float aggressiveRange;
    [SerializeField] private float attackRange;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();

        state = TongueState.passive;

        UpdateAnimatorValues();
    }

    public void Update()
    {
        if (player != null)
        {
            float dist = Vector3.Distance(player.gameObject.transform.position, transform.position);

            if (dist <= aggressiveRange && dist > attackRange)
            {
                state = TongueState.aggressive;
            }
            else if (dist <= attackRange)
            {
                state = TongueState.attacking;
            }
            else if (dist > aggressiveRange)
            {
                state = TongueState.passive;
            }
        }

        UpdateAnimatorValues();
    }

    

   

    public void UpdateAnimatorValues()
    {
        anim.SetInteger("State", (int)state);
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
