using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] protected float health = 100f;
    [SerializeField] protected float maxHealth = 100f;
    [SerializeField] protected bool invincible = false;

    [Header("Armor")]
    [SerializeField] protected float armor = 100f;
    [SerializeField] protected float armorProtectionProcent = 50f;
    [SerializeField] protected float armorDamageMultiplyer = 0.1f;
    [SerializeField] protected float maxArmor = 100f;
    [SerializeField] protected bool armorEnabled = true;

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
        return armor;
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
        return maxArmor;
    }

    /// <summary>
    /// Removes Health
    /// </summary>
    public void DoDamage(float damage,string removedBy)
    {
        if (!invincible)
        {
            if(armorEnabled)
            {
                if(armor > 0)
                {
                    armor -= damage * armorDamageMultiplyer;
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
    /// Removes the health over time
    /// </summary>
    public void DoDamage(float damage, float overTime, string removedBy)
    {
        // Zorgt dat je een noramale void kan roepen inplaats van start Courutine
        StartCoroutine(IDoDamage(damage, overTime, removedBy));
    }
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
    public void AddArmor(float _armor)
    {
        armor += _armor;
        armor = Mathf.Clamp(armor, 0, maxArmor);
    }



    /// <summary>
    /// Is Called When health Is 0 or lower
    /// </summary>
    protected virtual void OnDeath(string diedBy) { }
    protected virtual void OnDeath() { }

    /// <summary>
    /// Is Called When Health Is Removed
    /// </summary>
    protected virtual void OnRemoveHealth(float healthLost, string hitBy) { }

}
