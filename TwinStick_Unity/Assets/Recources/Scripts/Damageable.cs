using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] protected float health = 100;
    [SerializeField] protected bool invincible = false;

    protected bool death;

    private void Awake()
    {
        death = false;

        if (health == 0)
            Debug.LogError("Health is not set");

    }

    /// <summary>
    /// Is Called When health Is 0 or lower
    /// </summary>
    /// 

    protected virtual void OnDeath(){}

    /// <summary>
    /// Is Called When Health Is Removed
    /// </summary>
    protected virtual void OnHit()
    {

    }


    /// <summary>
    /// CHecks If the health is 0 or lower and turns the death to true
    /// </summary>
    private void CheckDeath()
    {
        if (health <= 0 && death == false && !invincible)
        {
            OnDeath();
            death = true;
        }
    }


    /// <summary>
    /// Returns The health
    /// </summary>
    /// <returns></returns>
    public float GetHealth()
    {
        return health;
    }

    /// <summary>
    /// Removes Health
    /// </summary>
    public void RemoveHealth(float damage)
    {
        // Calls the hit function
        OnHit();
        // Removes The health
        health -= damage;
        // Cals the check death function
        CheckDeath();
    }

    /// <summary>
    /// Removes the health over time
    /// </summary>
    public void RemoveHealth(float damage, float overTime)
    {
        // Zorgt dat je een noramale void kan roepen inplaats van start Courutine
        StartCoroutine(IRemoveHealth(damage, overTime));
    }
    IEnumerator IRemoveHealth(float damage, float overTime)
    {
        float timer = 0;
        float startHealth = this.health;

        while (timer < 1)
        {
            timer += Time.deltaTime / overTime;
            RemoveHealth(damage * timer * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        health = startHealth = damage;
    }

}
