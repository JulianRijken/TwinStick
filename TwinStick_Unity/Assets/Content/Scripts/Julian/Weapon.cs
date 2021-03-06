﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponID {knife = 0, secondaryTest = 1, primaryTest = 2, flameThrower = 3}
public enum WeaponInputType { sigle = 0, hold = 1 }
public enum WeaponSlotType { empty = 0, secondary = 1, primary = 2}

public class Weapon : MonoBehaviour
{
    [Header("Standard")]
    [SerializeField] private float attackDelay = 0.075f;
    public WeaponID weaponID = WeaponID.knife;
    public WeaponInputType weaponInput = WeaponInputType.hold;
    public WeaponSlotType weaponSlotType = WeaponSlotType.primary;

    private bool attackAllowed = true;

    private void Start()
    {
        attackAllowed = true;
    }

    /// <summary>
    /// Makes The weapon Attack
    /// </summary>
    public void Attack()
    {
        if (attackDelay == 0)
        {
            OnAttack();
        }
        else if(attackAllowed == true)
        {
            OnAttack();
            StartCoroutine(IDelayAttack());
        }
    }

    /// <summary>
    /// Stops Attacking
    /// </summary>
    public void StopAttack()
    {
        OnStopAttack();
    }

    /// <summary>
    /// Makes the weapon reload if it can
    /// </summary>
    public void Reload()
    {
        OnReload();
    }

    /// <summary>
    /// Uses the weapon gadget if it has one
    /// </summary>
    public void UseGadget()
    {
        OnUseGadget();
    }

    /// <summary>
    /// Gives The weapon a attack delay
    /// </summary>
    private IEnumerator IDelayAttack()
    {
        attackAllowed = false;
        yield return new WaitForSeconds(attackDelay);
        attackAllowed = true;
    }


    #region OnFunctions

    /// <summary>
    /// Runs On Attack Input
    /// </summary>
    protected virtual void OnAttack()
    {

    }

    /// <summary>
    /// Runs On Attack Input out
    /// </summary>
    protected virtual void OnStopAttack()
    {

    }

    /// <summary>
    /// Refreshes the weapons Stats
    /// </summary>
    public virtual void OnRefresh()
    {
        //xDebug.Log("Refreshed Weapon");
    }


    /// <summary>
    /// Runs On Use Gadget Input
    /// </summary>
    protected virtual void OnUseGadget()
    {

    }

    /// <summary>
    /// Runs On Reload Input
    /// </summary>
    protected virtual void OnReload()
    {

    }

    public virtual void OnInActive()
    {

    }

    public virtual void OnActive()
    {
        StartCoroutine(IDelayAttack());
    }




    #endregion
}

public struct WeaponDefaultData
{
    // if somthing in the weapon has to refresh
}
