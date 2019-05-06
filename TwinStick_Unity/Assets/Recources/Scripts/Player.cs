using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : Damageable
{

    private Rigidbody rig = null;
    [SerializeField] private float moveSpeed;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    

    
}
