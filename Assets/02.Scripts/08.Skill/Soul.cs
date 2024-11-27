using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JobType
{
    Attacker,
    Tanker,
    Healer
}

public abstract class Soul : MonoBehaviour
{
    protected StatHandler statHandler;

    protected string soulName;
    protected string description;

    protected int level;
    protected int maxLevel;
    protected int levelUpCost;

    protected int upgradeCount = 1;
    protected int upgradeStack = 0;

    protected JobType job;

    protected Skill[] skills = new Skill[(int)SkillType.Max];

    protected void LevelUP(int levelAmount)
    {

    }

    protected void ApplyPassiveSkill(Skill skill)
    {

    }

    protected void UseDefaultSkill(Skill skill)
    {

    }

    protected void UseUltimateSkill(Skill skill)
    {

    }
}
