using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Damageable : MonoBehaviour
{

    [Header("Health")]
    public float health = 100f;
    public float maxHealth = 100f;
    public bool invincible = false;

    [Header("Armor")]
    public bool armorEnabled = true;
    public float armorHealth = 100f;
    public float armorProtectionProcent = 50f;
    public float armorStrength = 10f;
    public float maxArmorHealth = 100f;

    protected bool died;

    private void Start()
    {
        died = false;
    }

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
                    float _damageProtected = (damage / 100) * armorProtectionProcent;
                    damage -= _damageProtected;
                }
            }

            OnRemoveHealth(damage, removedBy);

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
    protected virtual void OnRemoveHealth(float healthLost, string hitBy) { }

}