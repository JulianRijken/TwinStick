using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThower : Weapon
{
    [SerializeField] private AudioSource sound = null;
    [SerializeField] private Transform emitterTransform = null;

    private ParticleSystem flameParticle = null;

    private void Start()
    {    
        flameParticle = FindObjectOfType<ParticleEmitters>().flameThrowerFlame;
    }

    private void LateUpdate()
    {
        if (flameParticle != null && emitterTransform != null)
        {
            flameParticle.transform.position = emitterTransform.position;
            flameParticle.transform.rotation = emitterTransform.rotation;
        }
    }

    public override void OnActive()
    {
        base.OnActive();
        sound.Pause();
    }

    protected override void OnAttack()
    {       
        sound.UnPause();
        flameParticle.Play();
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