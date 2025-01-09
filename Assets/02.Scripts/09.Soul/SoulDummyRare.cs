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
        // TODO : SoulDB에서 받아서 넣기
        attackType = AttackType.Ranged;
        skills[(int)SkillType.Passive] = new MagePassiveSkill(12009, statHandler.CurrentStat);
        skills[(int)SkillType.Default] = new MageDefaultSkill(12010);
        skills[(int)SkillType.Ultimate] = new MageUltimateSkill(12011);
    }
}
