using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public static class StatConverter
{
    public static Stat PlayerStatConvert(int key)
    {
        Stat baseStat = new Stat();

        // TODO : 플레이어 기본 스텟 수치 정보
        // 현재 Temp

        baseStat.iD = 0;

        baseStat.health = new BigInteger(10);
        baseStat.maxHealth = baseStat.health;
        baseStat.atk = new BigInteger(10);
        baseStat.def = new BigInteger(5);

        baseStat.moveSpeed = 5f;
        baseStat.atkSpeed = 5f;

        baseStat.reduceDamage = 5f;

        baseStat.critChance = 5f;
        baseStat.critDamage = 5f;
        baseStat.coolDown = 5f;

        return baseStat;
    }

    public static Stat PlayerStatConvert(UserData userData)
    {
        Stat baseStat = new Stat();

        // TODO : 플레이어 기본 스텟 수치 정보
        // 현재 Temp

        baseStat.iD = userData.UID;

        baseStat.health = userData.stat.health;
        baseStat.maxHealth = userData.stat.maxHealth;
        baseStat.atk = userData.stat.atk;
        baseStat.def = userData.stat.def;

        baseStat.moveSpeed = userData.stat.moveSpeed;
        baseStat.atkSpeed = userData.stat.atkSpeed;

        baseStat.reduceDamage = userData.stat.reduceDamage;

        baseStat.critChance = userData.stat.critChance;
        baseStat.critDamage = userData.stat.critDamage;
        baseStat.coolDown = userData.stat.coolDown;

        baseStat.MaxHealthLevel = userData.stat.MaxHealthLevel;
        baseStat.AtkLevel = userData.stat.AtkLevel;
        baseStat.DefLevel = userData.stat.DefLevel;
        baseStat.ReduceDamageLevel = userData.stat.ReduceDamageLevel;
        baseStat.CriticalRateLevel = userData.stat.CriticalRateLevel;
        baseStat.CriticalDamageLevel = userData.stat.CriticalDamageLevel;

        return baseStat;
    }

    public static Stat SoulStatConvert(int key)
    {
        Stat baseStat = new Stat();

        SoulDB db = DataManager.Instance.SoulDB.GetByKey(key);

        baseStat.iD = db.key;

        baseStat.health = new BigInteger(db.Health.ToString());
        baseStat.maxHealth = baseStat.health;
        baseStat.atk = new BigInteger(db.Attack.ToString());
        baseStat.def = new BigInteger(db.Defence.ToString());

        baseStat.moveSpeed = db.MoveSpeed;
        baseStat.atkSpeed = db.AttackSpeed;

        baseStat.reduceDamage = db.ReduceDamage;

        baseStat.critChance = db.CriticalRate;
        baseStat.critDamage = db.CriticalDamage;
        baseStat.coolDown = db.CoolDown;

        return baseStat;
    }

    public static Stat EnemyStatConvert(int key)
    {
        Stat baseStat = new Stat();

        EnemyDB db = DataManager.Instance.EnemyDB.GetByKey(key);

        baseStat.iD = db.key;

        baseStat.health = new BigInteger(db.Health.ToString());
        baseStat.maxHealth = baseStat.health;
        baseStat.atk = new BigInteger(db.Attack.ToString());
        baseStat.def = new BigInteger(db.Defence.ToString());

        baseStat.moveSpeed = db.MoveSpeed;
        baseStat.atkSpeed = db.AttackSpeed;

        baseStat.critChance = db.CritChance;
        baseStat.critDamage = db.CritDamage;

        return baseStat;
    }
}
