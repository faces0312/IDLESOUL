using System.Collections;
using System.Collections.Generic;
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
    void UseSkill();
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

    protected SkillType type;

    // TODO : 생성시 데이터 적용
    public Skill(int id, string name, string description, int applyCount, float value, SkillType type)
    {
        this.id = id;
        this.skillName = name;
        this.description = description;
        this.applyCount = applyCount;
        this.value = value;
        this.type = type;
    }

    public abstract void UseSkill();
    public abstract void UpgradeSkill(int amount);
}
