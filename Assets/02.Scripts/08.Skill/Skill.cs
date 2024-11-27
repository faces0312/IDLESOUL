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
}

public abstract class Skill : MonoBehaviour, ISkill
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
}
