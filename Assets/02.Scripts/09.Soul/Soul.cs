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
    protected AttackType attackType;

    protected Skill[] skills = new Skill[(int)SkillType.Max];
    public Skill[] Skills { get { return skills; } }

    public Soul(int key)
    {
        // TODO : DB를 들고 있을지, 데이터를 추출해서 개별적으로 들고 있을지
        // TODO : 기본 공격 타입 : 근거리 원거리
        tempDB = DataManager.Instance.SoulDB.GetByKey(key);
        statHandler = new StatHandler(StatType.Soul, key);

        InitSkills();
        ApplyPassiveSkill();
    }

    protected abstract void InitSkills();   // 스킬 생성

    public void LevelUP(int amount)
    {
        level += amount;
        statHandler.LevelUp(level);
    }

    public void UpgradeSkill(SkillType type, int amount)
    {
        skills[(int)type].UpgradeSkill(amount);
    }

    public void ApplyPassiveSkill()
    {
        // 객체 생성 시 최초 1회 && 패시브 스킬 업그레이드 시 호출
        skills[(int)SkillType.Passive].UseSkill(statHandler);
    }

    public void UseSkill(Skill skill)
    {
        skill.UseSkill(statHandler);
    }
}
