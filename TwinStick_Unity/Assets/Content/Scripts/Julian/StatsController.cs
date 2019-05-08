using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;


public class StatsController
{
    private StatsData statsData;

    /// <summary>
    /// Stats Constructor
    /// </summary>
    public StatsController()
    {
        statsData = new StatsData()
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
    public StatsData GetData()
    {
        return statsData;
    }

}

public struct StatsData
{
    public float healthLost;
}