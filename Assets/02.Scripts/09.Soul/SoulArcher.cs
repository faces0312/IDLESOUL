using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulArcher : Soul
{
    public SoulArcher(int key) : base(key)
    {
    }

    protected override void InitSkills()
    {
        // TODO : SoulDB에서 받아서 넣기
        attackType = AttackType.Ranged;
        skills[(int)SkillType.Passive] = new MagicianPassiveSkill(12000);
        skills[(int)SkillType.Default] = new MagicianDefaultSkill(12001);
        skills[(int)SkillType.Ultimate] = new MagicianUltimateSkill(12002);
    }
}
