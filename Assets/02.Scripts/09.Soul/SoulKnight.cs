using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulKnight : Soul
{
    public SoulKnight(int key) : base(key)
    {
    }

    protected override void InitSkills()
    {
        // TODO : SoulDB에서 받아서 넣기
        attackType = AttackType.Melee;
        skills[(int)SkillType.Passive] = new KnightPassiveSkill(12003, statHandler.CurrentStat);
        skills[(int)SkillType.Default] = new KnightDefaultSkill(12004);
        skills[(int)SkillType.Ultimate] = new KnightUltimateSkill(12005);
    }
}
