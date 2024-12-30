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
        // TODO : SoulDB���� �޾Ƽ� �ֱ�
        attackType = AttackType.Ranged;
        skills[(int)SkillType.Passive] = new ArcherPassiveSkill(12006, statHandler.CurrentStat);
        skills[(int)SkillType.Default] = new ArcherDefaultSkill(12007);
        skills[(int)SkillType.Ultimate] = new ArcherUltimateSkill(12008);
    }
}
