using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;


public class StatsController
{
    private StatsData statsData;

    public StatsController()
    {
        statsData = new StatsData()
        {
            timesPlayed = 0,
            diedBy = new List<string>(),
            score = 0,
        };
    }



    public void AddTimesPlayed()
    {
        statsData.timesPlayed++;
    }


    // !!!!!!!!!!!!!!! IK HEB VAN ENZO EERST DE ENEMY NODIG OM OOK DE NAAM TE KUNNEN CHECKEN
    public void AddDiedBy(string diedBy)
    {
        statsData.diedBy.Add(diedBy);
    }

    public void AddScore(float score)
    {
        statsData.score += score;
    }

    public void AddHealthLost(float healthLost)
    {
        statsData.healthLost += healthLost;
    }


    public StatsData GetData()
    {
        return statsData;
    }

}

public struct StatsData
{
    public int timesPlayed;
    public List<string> diedBy;
    public float score;
    public float healthLost;
}