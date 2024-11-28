using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulKnight : Soul
{
    protected override void InitSkills()
    {
        skills[(int)SkillType.Passive] = new KnightPassiveSkill();
        skills[(int)SkillType.Default] = new KnightDefaultSkill();
        skills[(int)SkillType.Ultimate] = new KnightUltimateSkill();
    }
}
