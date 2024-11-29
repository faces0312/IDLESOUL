using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JobType
{
    Attacker,
    Tanker,
    Healer,
    None
}

public abstract class Soul
{
    protected SoulDB tempDB;
    public StatHandler statHandler; // �ӽ÷� public �ٽ� protected �� ��������

    protected string soulName;
    protected string description;

    protected int iD;
    protected int level = 1;
    protected int maxLevel = 100;
    protected int levelUpCost;

    protected int upgradeCount = 1;
    protected int upgradeStack = 0;

    protected JobType job = JobType.None;

    protected Skill[] skills = new Skill[(int)SkillType.Max];
    public Skill[] Skills { get { return skills; } }

    public Soul(int key)
    {
        // TODO : ID�� ��� �˾� �� ���ΰ�?
        // DB�� ��� �˾ƿ� ���ΰ�?

        tempDB = DataManager.Instance.SoulDB.GetByKey(key);
        statHandler = new StatHandler(StatType.Soul, key);

        InitSkills();
    }

    protected abstract void InitSkills();   // ��ų ����

    public void LevelUP(int amount)
    {
        level += amount;
        statHandler.LevelUp(level);
    }

    public void UpgradeSkill(Skill skill, int amount)
    {
        skill.UpgradeSkill(amount);
    }

    public void ApplyPassiveSkill()
    {
        // ��ü ���� �� ���� 1ȸ or �нú� ��ų ���׷��̵� �� ȣ���� �Ǹ� �ȴ�.
        skills[(int)SkillType.Passive].UseSkill(statHandler);
    }

    public void UseSkill(Skill skill)
    {
        skill.UseSkill(statHandler);
    }
}
