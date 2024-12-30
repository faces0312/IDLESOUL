using ScottGarland;
using System;

public enum StatType
{
    Player,
    Soul,
    Enemy,
}

[System.Serializable]
public class Stat
{
    public BigInteger totalDamage = 0;

    public int iD;

    public BigInteger health;
    public BigInteger maxHealth;
    public BigInteger atk;
    public BigInteger def;

    public float moveSpeed;
    public float atkSpeed;

    public float reduceDamage;

    public float critChance;
    public float critDamage;
    public float coolDown;

    public int MaxHealthLevel;
    public int AtkLevel;
    public int DefLevel;
    public int ReduceDamageLevel;
    public int CriticalRateLevel;
    public int CriticalDamageLevel;

    public Stat()
    {
        this.health = 0;
        this.maxHealth = 0;
        this.atk = 0;
        this.def = 0;

        this.moveSpeed = 0;
        this.atkSpeed = 0;

        this.reduceDamage = 0;

        this.critChance = 0;
        this.critDamage = 0;
        this.coolDown = 0;

        this.MaxHealthLevel = 0;
        this.AtkLevel = 0;
        this.DefLevel = 0;
        this.ReduceDamageLevel = 0;
        this.CriticalRateLevel = 0;
        this.CriticalDamageLevel = 0;
    }

    public Stat(Stat stat)
    {
        this.health = stat.health;
        this.maxHealth = stat.maxHealth;
        this.atk = stat.atk;
        this.def = stat.def;

        this.moveSpeed = stat.moveSpeed;
        this.atkSpeed = stat.atkSpeed;

        this.reduceDamage = stat.reduceDamage;

        this.critChance = stat.critChance;
        this.critDamage = stat.critDamage;
        this.coolDown = stat.coolDown;

        this.MaxHealthLevel = stat.MaxHealthLevel;
        this.AtkLevel = stat.AtkLevel;
        this.DefLevel = stat.DefLevel;
        this.ReduceDamageLevel = stat.ReduceDamageLevel;
        this.CriticalRateLevel = stat.CriticalRateLevel;
        this.CriticalDamageLevel = stat.CriticalDamageLevel;

    }

    public static Stat operator +(Stat stat1, Stat stat2)
    {
        Stat clacStat = stat1;

        clacStat.health = stat1.health + stat2.health;
        clacStat.maxHealth = stat1.maxHealth + stat2.maxHealth;
        clacStat.atk = stat1.atk + stat2.atk;
        clacStat.def = stat1.def + stat2.def;

        clacStat.moveSpeed = (float)Math.Round(stat1.moveSpeed + stat2.moveSpeed, 1);
        clacStat.atkSpeed = (float)Math.Round(stat1.atkSpeed + stat2.atkSpeed, 1);

        clacStat.reduceDamage = (float)Math.Round(stat1.reduceDamage + stat2.reduceDamage, 1);

        clacStat.critChance = (float)Math.Round(stat1.critChance + stat2.critChance, 1);
        clacStat.critDamage = (float)Math.Round(stat1.critDamage + stat2.critDamage, 1);
        clacStat.coolDown = (float)Math.Round(stat1.coolDown + stat2.coolDown, 1);

        return clacStat;
    }

    public static Stat operator -(Stat stat1, Stat stat2)
    {
        Stat clacStat = stat1;

        clacStat.health = stat1.health - stat2.health;
        clacStat.maxHealth = stat1.maxHealth - stat2.maxHealth;
        clacStat.atk = stat1.atk - stat2.atk;
        clacStat.def = stat1.def - stat2.def;

        clacStat.moveSpeed = (float)Math.Round(stat1.moveSpeed - stat2.moveSpeed, 1);
        clacStat.atkSpeed = (float)Math.Round(stat1.atkSpeed - stat2.atkSpeed, 1);

        clacStat.reduceDamage = (float)Math.Round(stat1.reduceDamage - stat2.reduceDamage, 1);

        clacStat.critChance = (float)Math.Round(stat1.critChance - stat2.critChance, 1);
        clacStat.critDamage = (float)Math.Round(stat1.critDamage - stat2.critDamage, 1);
        clacStat.coolDown = (float)Math.Round(stat1.coolDown - stat2.coolDown, 1);

        return clacStat;
    }

    public static Stat operator *(Stat stat, int amount)
    {
        Stat clacStat = stat;

        clacStat.health = stat.health * amount;
        clacStat.maxHealth = stat.maxHealth * amount;
        clacStat.atk = stat.atk * amount;
        clacStat.def = stat.def * amount;

        clacStat.moveSpeed = (float)Math.Round(stat.moveSpeed * amount, 1);
        clacStat.atkSpeed = (float)Math.Round(stat.atkSpeed * amount, 1);

        clacStat.reduceDamage = (float)Math.Round(stat.reduceDamage * amount, 1);

        clacStat.critChance = (float)Math.Round(stat.critChance * amount, 1);
        clacStat.critDamage = (float)Math.Round(stat.critDamage * amount, 1);
        clacStat.coolDown = (float)Math.Round(stat.coolDown * amount, 1);

        return clacStat;
    }

    public static Stat operator /(Stat stat, int amount)
    {
        Stat clacStat = stat;

        clacStat.health = stat.health / amount;
        clacStat.maxHealth = stat.maxHealth / amount;
        clacStat.atk = stat.atk / amount;
        clacStat.def = stat.def / amount;

        clacStat.moveSpeed = (float)Math.Round(stat.moveSpeed / amount, 1);
        clacStat.atkSpeed = (float)Math.Round(stat.atkSpeed / amount, 1);

        clacStat.reduceDamage = (float)Math.Round(stat.reduceDamage / amount, 1);

        clacStat.critChance = (float)Math.Round(stat.critChance * amount, 1);
        clacStat.critDamage = (float)Math.Round(stat.critDamage * amount, 1);
        clacStat.coolDown = (float)Math.Round(stat.coolDown * amount, 1);

        return clacStat;
    }
}
