using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public enum StatType
{
    Player,
    Soul,
    Enemy,
}

public enum TestSkillType
{
    Passive,
    Default,
    Ultimate,
    Max
}

[CreateAssetMenu(fileName = "Stat", menuName = "Stat/Defalut", order = 0)]
public class StatSO : ScriptableObject
{
    public string combatPower;
    public int level;

    public string health;
    public string maxHealth;
    public string atk;
    public string def;

    public float reduceDamage;
    public float criticalRate;
    public float criticalDamage;

    public float atkSpeed;
    public float moveSpeed;
    public float coolDown;

    public string exp;
    public string MaxExp;

    public TestSkill[] skills = new TestSkill[(int)TestSkillType.Max];
}
public class TestSkill
{

}
