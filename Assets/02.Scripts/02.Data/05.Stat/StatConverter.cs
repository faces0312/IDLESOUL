using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StatConverter
{
    public static Stat PlayerStatConvert(int key)
    {
        Stat baseStat = new Stat();

        return baseStat;
    }

    public static Stat EnemyStatConvert(int key)
    {
        Stat baseStat = new Stat();

        return baseStat;
    }

    public static Stat SoulStatConvert(int key)
    {
        Stat baseStat = new Stat();

        SoulDB db = DataManager.Instance.SoulDB.GetByKey(key);

        return baseStat;
    }
}
