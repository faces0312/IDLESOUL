using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightPassiveSkill : Skill
{
    public KnightPassiveSkill(int id, string name, string description, int applyCount, float value, SkillType type) : base(id, name, description, applyCount, value, type)
    {
    }

    public override void UseSkill(StatHandler statHandler)
    {
        
    }

    public override void UpgradeSkill(int amount)
    {

    }
}
