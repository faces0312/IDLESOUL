using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightDefaultSkill : Skill
{
    public KnightDefaultSkill(int id, string name, string description, int applyCount, float value, SkillType type) : base(id, name, description, applyCount, value, type)
    {
    }

    public override void UseSkill()
    {
        
    }

    public override void UpgradeSkill(int amount)
    {

    }
}
