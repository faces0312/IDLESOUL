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
        // TODO : SoulDB���� �޾Ƽ� �ֱ�
        attackType = AttackType.Ranged;
        skills[(int)SkillType.Passive] = new MagePassiveSkill(12009, statHandler.CurrentStat);
        skills[(int)SkillType.Default] = new MageDefaultSkill(12010);
        skills[(int)SkillType.Ultimate] = new MageUltimateSkill(12011);
    }
}
