using System.Collections;
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

    protected string skillName;
    protected string description;

    protected int level = 1;
    protected int maxLevel;
    protected int upgradeCost;

    protected int applyCount;
    protected float value;
    protected float upgradeValue;

    protected SkillType type;

    // TODO : 积己矫 单捞磐 利侩
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
    }

    public abstract void UseSkill(StatHandler statHandler);
    public abstract void UpgradeSkill(int amount);
}
