using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThower : Weapon
{
    [SerializeField] private ParticleSystem[] particleSystems = null;
    [SerializeField] private AudioSource sound = null;

    public override void OnActive()
    {
        base.OnActive();
        sound.Pause();
    }

    protected override void OnAttack()
    {
        
        sound.UnPause();

        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Play();
        }
    }

    protected override void OnStopAttack()
    {
        sound.Pause();
    }

    public override void OnInActive()
    {
        base.OnInActive();
        sound.Pause();
    }
}