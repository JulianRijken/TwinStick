using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;


public class Stats
{
    private CharacterData statsData;

    /// <summary>
    /// Stats Constructor
    /// </summary>
    public Stats()
    {
        statsData = new CharacterData()
        {
            healthLost = 0,
        };
    }

    /// <summary>
    /// Adds the health to the lost health float
    /// </summary>
    public void AddHealthLost(float _healthLost)
    {
        statsData.healthLost += _healthLost;
    }

    
    /// <summary>
    ///  Returns The stats
    /// </summary>
    public CharacterData GetData()
    {
        return statsData;
    }

}

public class CharacterData
{
    public float healthLost;
}