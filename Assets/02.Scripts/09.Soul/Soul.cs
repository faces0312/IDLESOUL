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
    public StatHandler statHandler; // 임시로 public 다시 protected 로 돌려놓기

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
        // TODO : ID는 어떻게 알아 올 것인가?
        // DB는 어떻게 알아올 것인가?

        tempDB = DataManager.Instance.SoulDB.GetByKey(key);
        statHandler = new StatHandler(StatType.Soul, key);

        InitSkills();
    }

    protected abstract void InitSkills();   // 스킬 생성

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
        // 객체 생성 시 최초 1회 or 패시브 스킬 업그레이드 시 호출이 되면 된다.
        skills[(int)SkillType.Passive].UseSkill(statHandler);
    }

    public void UseSkill(Skill skill)
    {
        skill.UseSkill(statHandler);
    }
}
