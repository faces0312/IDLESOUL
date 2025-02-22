﻿using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public enum SkillType
{
    Passive,
    Default,
    Ultimate,
    Max
}

public interface ISkill
{
    void UseSkill(StatHandler statHandler);
    void UpgradeSkill(int amount);
}

public abstract class Skill : ISkill
{
    protected int id;

    public string skillName;
    public string description;

    public int level = 1;
    protected int maxLevel;
    protected int upgradeCost;

    protected int applyCount;
    protected float value;
    protected float upgradeValue;

    protected SkillType type;

    protected float coolTime;

    protected Sprite skillSpr;

    public float CoolTime { get => coolTime; }
    public Sprite SkillSpr { get => skillSpr; }
    public int UpgradeCost { get => upgradeCost; }

    // 생성자에서 데이터 적용
    public Skill(int id)
    {
        SkillDB db = DataManager.Instance.SkillDB.GetByKey(id);
        
        this.id = id;
        this.skillName = db.Name;
        this.description = db.Descripton;
        this.maxLevel = db.MaxLevel;
        this.applyCount = db.ApplyCount;
        this.value = db.Value;
        this.upgradeValue = db.UpgradeValue;
        this.upgradeCost = db.UpgradeCost;
        this.type = db.SkillType;

        skillSpr = Resources.Load<Sprite>(db.SpritePath);
    }

    public Skill(int id, Stat stat)
    {
        SkillDB db = DataManager.Instance.SkillDB.GetByKey(id);

        this.id = id;
        this.skillName = db.Name;
        this.description = db.Descripton;
        this.maxLevel = db.MaxLevel;
        this.applyCount = db.ApplyCount;
        this.value = db.Value;
        this.upgradeValue = db.UpgradeValue;
        this.upgradeCost = db.UpgradeCost;
        this.type = db.SkillType;

        skillSpr = Resources.Load<Sprite>(db.SpritePath);
    }

    public abstract void UseSkill(StatHandler statHandler);
    public abstract void UpgradeSkill(int amount);
    public virtual void ReleaseSkill()
    {

    }

    public virtual void UseSkillSoundPlay()
    {
        //2번 3번 클립에서 랜덤으로 재생되게 하였음 ( 캐릭터의 기합 소리 )
        GameManager.Instance.player.PlayerSFX.PlayClipSFXOneShot((SoundType)Random.Range(2, 4));
    }
}
