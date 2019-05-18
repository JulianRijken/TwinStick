using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Damageable : MonoBehaviour
{

    [Header("Health")]
    [SerializeField] protected bool invincible = false;
    [SerializeField] protected float health = 100f;
    [SerializeField] protected float maxHealth = 100f;
    protected bool died;

    [Header("Armor")]
    [SerializeField] protected bool armorEnabled = true;
    [SerializeField] protected float armorHealth = 100f;
    [SerializeField] protected float armorProtectionProcent = 50f;
    [SerializeField] protected float armorStrength = 10f;
    [SerializeField] protected float maxArmorHealth = 100f;

    private void Start()
    {
        died = false;
    }

    #region GetFunctions

    /// <summary>
    /// Returns The current health
    /// </summary>
    public float GetHealth()
    {
        return health;
    }

    /// <summary>
    /// Returns The current armor
    /// </summary>
    public float GetArmor()
    {
        return armorHealth;
    }

    /// <summary>
    /// Returns The Max Health
    /// </summary>
    public float GetMaxHealth()
    {
        return maxHealth;
    }

    /// <summary>
    /// Returns The Max Armor
    /// </summary>
    public float GetMaxArmor()
    {
        return maxArmorHealth;
    }

    #endregion


    #region DoFunctions

    /// <summary>
    /// Removes Health
    /// </summary>
    public void DoDamage(float damage, string removedBy)
    {
        if (!invincible)
        {
            if (armorEnabled)
            {
                if (armorHealth > 0)
                {
                    armorHealth -= damage / armorStrength;
                    OnArmorHealthLost(damage / armorStrength);
                    float _damageProtected = (damage / 100) * armorProtectionProcent;
                    damage -= _damageProtected;
                }
            }

            OnHealthLost(damage, removedBy);

            health -= damage;
            health = Mathf.Clamp(health, 0, maxHealth);

            if (health <= 0 && died == false)
            {
                OnDeath();
                OnDeath(removedBy);
                died = true;
            }
        }
    }

    /// <summary>
    /// Does the damage over the time
    /// </summary>
    public void DoDamage(float damage, float overTime, string removedBy)
    {
        // Zorgt dat je een noramale void kan roepen inplaats van start Courutine
        StartCoroutine(IDoDamage(damage, overTime, removedBy));
    }

    /// <summary>
    ///  Does the damage over the time
    /// </summary>
    private IEnumerator IDoDamage(float damage, float overTime, string removedBy)
    {
        float timer = 0;
        float startHealth = this.health;

        while (timer < overTime)
        {
            DoDamage((Time.deltaTime / overTime) * damage, removedBy);

            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        health = startHealth = damage;
    }

    #endregion


    #region AddFunctions

    /// <summary>
    /// Returns The current health
    /// </summary>
    public void AddHealth(float _health)
    {
        health += _health;
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    /// <summary>
    /// Returns The current armor
    /// </summary>
    public void AddArmorHealth(float _armorHealth)
    {
        armorHealth += _armorHealth;
        armorHealth = Mathf.Clamp(armorHealth, 0, maxArmorHealth);
    }

    #endregion


    #region OnFuncitons
    /// <summary>
    /// Is Called When health Is 0 or lower with a name
    /// </summary>
    protected virtual void OnDeath(string diedBy) { }

    /// <summary>
    /// Is Called When health Is 0 or lower
    /// </summary>
    protected virtual void OnDeath() { }

    /// <summary>
    /// Is Called When Health Is Removed
    /// </summary>
    protected virtual void OnHealthLost(float healthLost, string hitBy) { }

    /// <summary>
    /// Is Called When Health Is Removed
    /// </summary>
    protected virtual void OnArmorHealthLost(float armorHealthLost) { }

    #endregion
}