using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulDummyRare : Soul
{
    public SoulDummyRare(int key) : base(key)
    {
    }

    protected override void InitSkills()
    {
        attackType = AttackType.Melee;
        skills[(int)SkillType.Passive] = new MagicianPassiveSkill(12000);
        skills[(int)SkillType.Default] = new MagicianDefaultSkill(12001);
        skills[(int)SkillType.Ultimate] = new MagicianUltimateSkill(12002);
    }
}
