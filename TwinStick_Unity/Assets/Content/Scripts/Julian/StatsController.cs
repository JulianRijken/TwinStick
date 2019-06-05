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
        GameManager.instance.notificationCenter.FireStatsChanged();
    }

    public void AddDiedBy(string diedBy)
    {
        statsData.diedBy.Add(diedBy);
        GameManager.instance.notificationCenter.FireStatsChanged();
    }

    public void AddScore(float score)
    {
        statsData.score += score;
        GameManager.instance.notificationCenter.FireStatsChanged();
    }

    public void AddShotsFired(int shots)
    {
        statsData.shotsFired += shots;
        GameManager.instance.notificationCenter.FireStatsChanged();
    }

    public void AddHealthLost(float healthLost)
    {
        statsData.healthLost += healthLost;
        GameManager.instance.notificationCenter.FireStatsChanged();
    }


    public StatsData GetData()
    {
        return statsData;
    }

}

public struct StatsData
{
    public List<string> diedBy;
    public int timesPlayed;
    public float score;
    public float healthLost;
    public int shotsFired;
}