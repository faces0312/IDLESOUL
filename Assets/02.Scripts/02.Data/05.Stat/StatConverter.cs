using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public static class StatConverter
{
    public static Stat PlayerStatConvert(int key)
    {
        Stat baseStat = new Stat();

        // TODO : �÷��̾� �⺻ ���� ��ġ ����

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
}
