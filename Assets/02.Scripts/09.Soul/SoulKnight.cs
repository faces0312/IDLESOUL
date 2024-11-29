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
        // TODO : 데이터를 받아와 넣기

        //skills[(int)SkillType.Passive] = new KnightPassiveSkill(
        //    0000, "능숙한 마법", "공격력이 증가합니다.", 1, 1f, SkillType.Passive);
        skills[(int)SkillType.Default] = new KnightDefaultSkill(
            0011, "스핀 소드", "검을 들고 회전하여 여러 차례 피해를 입힌다.", 1, 1f, SkillType.Default);
        //skills[(int)SkillType.Ultimate] = new KnightUltimateSkill(
        //    0002, "메테오", "범위 내에 거대한 폭발을 일으켜 데미지를 입힌다.", 1, 1f, SkillType.Ultimate);
    }
}
