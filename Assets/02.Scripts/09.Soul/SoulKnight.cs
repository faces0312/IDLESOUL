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
        //skills[(int)SkillType.Passive] = new KnightPassiveSkill(12003);
        skills[(int)SkillType.Default] = new KnightDefaultSkill(12004);
        //skills[(int)SkillType.Ultimate] = new KnightUltimateSkill(12005);
    }
}
