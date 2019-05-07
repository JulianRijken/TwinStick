using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;


public class Stats
{
    private CharacterData m_Data;

    public Stats()
    {
        m_Data = new CharacterData()
        {
            healthGaind = 0,
            healthLost = 0
        };
    }

    public void Damage(int _damage)
    {
        m_Data.healthLost = _damage;
    }

    public void Heal(int _health)
    {
        m_Data.healthGaind = _health;
    }
}

public class CharacterData
{
    public float healthLost;
    public int healthGaind;
}