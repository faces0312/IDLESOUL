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

        //skills[(int)SkillType.Passive] = new MagicianPassiveSkill(
        //    0000, "능숙한 마법", "공격력이 증가합니다.", 1, 1f, SkillType.Passive);
        //skills[(int)SkillType.Default] = new MagicianDefaultSkill(
        //    0001, "익스플로전", "범위 내의 적에게 폭발을 일으켜 데미지를 입힌다.", 1, 1f, SkillType.Default);
        //skills[(int)SkillType.Ultimate] = new MagicianUltimateSkill(
        //    0002, "메테오", "범위 내에 거대한 폭발을 일으켜 데미지를 입힌다.", 1, 1f, SkillType.Ultimate);
    }
}
