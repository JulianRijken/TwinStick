using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThower : Weapon
{
    [SerializeField] private ParticleSystem FlameThrower;
    [SerializeField] private ParticleSystem WaterGun;

    // Start is called before the first frame update
    private void Start()
    {
        FlameThrower.Stop();
        WaterGun.Stop();
    }

    // Update is called once per frame
    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    FlameThrower.Play();
        //}
        if (Input.GetMouseButtonUp(0))
        {
            FlameThrower.Stop();
        }
        if (Input.GetMouseButtonDown(1))
        {
            WaterGun.Play();
        }
        if (Input.GetMouseButtonUp(1))
        {
            WaterGun.Stop();
        }
    }

    protected override void OnAttack()
    {
        FlameThrower.Play();
    }
}