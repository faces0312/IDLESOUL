using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulDummyEpic : Soul
{
    public SoulDummyEpic(int key) : base(key)
    {
    }

    protected override void InitSkills()
    {
        attackType = AttackType.Melee;
        skills[(int)SkillType.Passive] = new KnightPassiveSkill(12003, statHandler.CurrentStat);
        skills[(int)SkillType.Default] = new KnightDefaultSkill(12004);
        skills[(int)SkillType.Ultimate] = new KnightUltimateSkill(12005);
    }
}
