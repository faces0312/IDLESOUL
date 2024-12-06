﻿using Enums;
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

public abstract class Soul : IGachable
{
    protected SoulDB tempDB;
    protected StatHandler statHandler;

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
    public StatHandler StatHandler { get { return statHandler; } }

    // TODO : 소환 중인지 확인 여부의 bool 변수가 필요할 수도 있음

    public Soul(int key)
    {
        SoulDB db = DataManager.Instance.SoulDB.GetByKey(key);
        statHandler = new StatHandler(StatType.Soul, key);

        soulName = db.Name;
        description = db.Descripton;

        iD = db.key;
        maxLevel = db.MaxExp;
        //levelUpCost = db.

        //upgradeCount = db.
        //upgradeStack = db.

        job = db.JobType;
        //attackType = db.AttackType;

        // TODO : 스킬 데이터 넘겨주기
        InitSkills();
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
        // 소울 장착 시 && 패시브 스킬 업그레이드 시 호출
        skills[(int)SkillType.Passive].UseSkill(statHandler);
    }

    public void UseSkill(Skill skill)
    {
        skill.UseSkill(statHandler);
    }

    public int GetID()
    {
        return this.iD;
    }

    public string GetName()
    {
        return this.soulName;
    }

    public string GetDescription()
    {
        return this.description;
    }
}
