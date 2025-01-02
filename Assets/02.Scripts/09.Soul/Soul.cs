using Enums;
using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JobType
{
    None,
    Attacker,
    Tanker,
    Healer,
}

public enum SoulType
{
    None,
    Magician,
    Knight,
    Archer,
    DummyRare,
    DummyEpic
}

public abstract class Soul
{
    protected SoulDB tempDB;
    public StatHandler statHandler;

    public string soulName;
    public string description;

    protected int iD;
    public int level = 1;
    protected int maxLevel = 100;
    protected int levelUpCost;

    protected int upgradeCount = 1;
    protected int upgradeStack = 0;

    protected int ownStack = 0; // TODO : DB 추가

    protected JobType job = JobType.None;
    protected AttackType attackType;

    protected SoulType soulType;

    public Sprite icon;
    public Sprite sprite;

    protected Skill[] skills = new Skill[(int)SkillType.Max];
    public Skill[] Skills { get { return skills; } }
    public StatHandler StatHandler { get { return statHandler; } }
    public int OwnStack { get { return ownStack; } }

    public int ID { get { return iD; } }

    public SoulType SoulType { get { return soulType; } }
    public JobType Job { get { return job; } }

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
        attackType = db.AttackType;

        icon = Resources.Load<Sprite>(db.IconPath);
        sprite = Resources.Load<Sprite>(db.SpritePath);

        soulType = (SoulType)db.SoulType;

        // TODO : 스킬 데이터 넘겨주기
        InitSkills();
    }

    protected abstract void InitSkills();   // 스킬 생성

    public void LevelUP(int amount)
    {
        level += amount;
        statHandler.LevelUp(level);

        //UserSoulData에서 해당 Soul 데이터를 불러와서 해당 상승된 레벨을 저장 후 세이브
        UserSoulData soulData = GameManager.Instance.player.UserData.GainSoul.Find(x => x.ID == iD);
        soulData.Level = level;
        DataManager.Instance.SaveUserData(GameManager.Instance.player.UserData);
    }

    public void UpgradeSkill(SkillType type, int amount)
    {
        skills[(int)type].UpgradeSkill(amount);

        //UserSoulData에서 해당 Soul 데이터를 불러와서 해당 상승된 레벨을 저장 후 세이브
        UserSoulData soulData = GameManager.Instance.player.UserData.GainSoul.Find(x => x.ID == iD);

        switch (type)
        {
            case SkillType.Passive:
                soulData.PassiveSkillLevel = Skills[(int)type].level;
                break;
            case SkillType.Default:
                soulData.DefaultSkillLevel = Skills[(int)type].level;
                break;
            case SkillType.Ultimate:
                soulData.UltimateSkillLevel = Skills[(int)type].level;
                break;
        }

        DataManager.Instance.SaveUserData(GameManager.Instance.player.UserData);
    }

    public void ApplyPassiveSkill()
    {
        // 소울 장착 시 && 패시브 스킬 업그레이드 시 호출
        skills[(int)SkillType.Passive].UseSkill(statHandler);
    }

    public void ReleasePassiveSkill()
    {
        skills[(int)SkillType.Passive].ReleaseSkill();
    }

    public void UseSkill(Skill skill)
    {
        skill.UseSkill(GameManager.Instance.player.StatHandler);
    }

    public void CollectSoul(int amount)
    {
        ownStack += amount;
    }
}
