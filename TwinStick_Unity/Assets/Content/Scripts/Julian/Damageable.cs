using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] protected float health = 100;
    [SerializeField] protected float maxHealth = 100;
    [SerializeField] protected bool invincible = false;

    protected bool death;

    private void Awake()
    {
        death = false;

        if (health == 0)
            Debug.LogError("Health is not set");
    }

    private void Update()
    {
        health = Mathf.Clamp(health,0, maxHealth);
    }

    /// <summary>
    /// Is Called When health Is 0 or lower
    /// </summary>
    /// 

    protected virtual void OnDeath(string diedBy) {}

    protected virtual void OnDeath() { }

    /// <summary>
    /// Is Called When Health Is Removed
    /// </summary>
    protected virtual void OnHit(float healthLost,string hitBy) {}


    /// <summary>
    /// Returns The health
    /// </summary>
    /// <returns></returns>
    public float GetHealth()
    {
        return health;
    }

    /// <summary>
    /// Returns The Max Health
    /// </summary>
    /// <returns></returns>
    public float GetMaxHealth()
    {
        return maxHealth;
    }

    /// <summary>
    /// Removes Health
    /// </summary>
    public void RemoveHealth(float damage,string removedBy)
    {
        // Calls the hit function
        OnHit(damage, removedBy);
        // Removes The health
        health -= damage;
        // Cals the check death function
        if (health <= 0 && death == false && !invincible)
        {
            OnDeath();
            OnDeath(removedBy);
            death = true;
        }
    }

    /// <summary>
    /// Removes the health over time
    /// </summary>
    public void RemoveHealth(float damage, float overTime, string removedBy)
    {
        // Zorgt dat je een noramale void kan roepen inplaats van start Courutine
        StartCoroutine(IRemoveHealth(damage, overTime, removedBy));
    }
    IEnumerator IRemoveHealth(float damage, float overTime, string removedBy)
    {
        float timer = 0;
        float startHealth = this.health;

        while (timer < overTime)
        {
            RemoveHealth((Time.deltaTime / overTime) * damage, removedBy);

            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        health = startHealth = damage;
    }

}
