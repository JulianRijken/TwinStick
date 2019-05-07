using System;
using System.Linq.Expressions;
using UnityEngine;


public static class Stats
{
    static public void SaveStats(CharacterData data)
    {
        // add een character slot voor de string naam voor meer players

        PlayerPrefs.SetFloat(MemberInfoGetting.GetMemberName(() => data.healthGaind).ToString(), data.healthGaind);
        PlayerPrefs.Save();
    }

    static public CharacterData LoadStats()
    {
        CharacterData data = new CharacterData();
        data.healthGaind = PlayerPrefs.GetFloat(MemberInfoGetting.GetMemberName(() => data.healthGaind).ToString());
        return data;
    }


}

public class CharacterData
{
    public float healthLost;
    public float healthGaind;
}

public static class MemberInfoGetting
{
    public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
    {
        MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
        return expressionBody.Member.Name;
    }
}