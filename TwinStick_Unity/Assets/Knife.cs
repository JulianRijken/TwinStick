using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{
    /// <summary>
    /// Triggers the gun to shoot Once
    /// </summary>
    protected override void OnAttack()
    {
        GameManager.instance.notificationCenter.FirePlayerAnimation(PlayerAnimation.knifeAttack);
    }
}
