using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightDefaultSkill : Skill
{
    GameObject skillPrefab;
    private float range;

    public KnightDefaultSkill(int id, string name, string description, int applyCount, float value, SkillType type) : base(id, name, description, applyCount, value, type)
    {
        skillPrefab = Resources.Load<GameObject>("Prefabs/Skills/SpinSword");
        range = 5f;
    }

    public override void UpgradeSkill(int amount)
    {
        level += amount;

        // TODO : amount 만큼 value 증가
    }

    public override void UseSkill(StatHandler statHandler)
    {
        
    }
}
