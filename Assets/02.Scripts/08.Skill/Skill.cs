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
    protected int iD;

    protected string skillName;
    protected string description;

    protected int level;
    protected int maxLevel;
    protected int upgradeCost;

    protected int applyCount;
    protected float value;

    protected SkillType type;

    public abstract void UseSkill();
    public abstract void UpgradeSkill(int amount);
}
