using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{
    public override void OnActive()
    {
        base.OnActive();
        GameManager.instance.notificationCenter.FireGunMagAmmoChange(0, 0);
    }


    /// <summary>
    /// Triggers the gun to shoot Once
    /// </summary>
    protected override void OnAttack()
    {
        GameManager.instance.notificationCenter.FirePlayerAnimation(PlayerAnimation.knifeAttack);
    }
}
