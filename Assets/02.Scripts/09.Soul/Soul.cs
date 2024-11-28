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

public abstract class Soul : MonoBehaviour
{
    protected StatHandler statHandler;

    protected string soulName;
    protected string description;

    protected int level = 1;
    protected int maxLevel = 100;
    protected int levelUpCost;

    protected int upgradeCount = 1;
    protected int upgradeStack = 0;

    protected JobType job = JobType.None;

    protected Skill[] skills = new Skill[(int)SkillType.Max];
    public Skill[] Skills { get { return skills; } }

    protected void Awake()
    {
        statHandler = GetComponent<StatHandler>();
    }

    protected abstract void InitSkills();   // ��ų ����

    public void LevelUP(int amount)
    {
        statHandler.LevelUp(amount);
    }

    public void UpgradeSkill(Skill skill, int amount)
    {
        skill.UpgradeSkill(amount);
    }

    public void ApplyPassiveSkill()
    {
        // ��ü ���� �� ���� 1ȸ or �нú� ��ų ���׷��̵� �� ȣ���� �Ǹ� �ȴ�.
        skills[(int)SkillType.Passive].UseSkill(statHandler);
    }

    public void UseSkill(Skill skill)
    {
        skill.UseSkill(statHandler);
    }
}
