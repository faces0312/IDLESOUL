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
        skills[(int)SkillType.Passive] = null;
        skills[(int)SkillType.Default] = null;
        skills[(int)SkillType.Ultimate] = null;
    }
}
