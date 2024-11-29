using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMagician : Soul
{
    public SoulMagician(int key) : base(key)
    {
    }

    protected override void InitSkills()
    {
        skills[(int)SkillType.Passive] = new MagicianPassiveSkill(12000);
        skills[(int)SkillType.Default] = new MagicianDefaultSkill(12001);
        skills[(int)SkillType.Ultimate] = new MagicianUltimateSkill(12002);
    }
}
