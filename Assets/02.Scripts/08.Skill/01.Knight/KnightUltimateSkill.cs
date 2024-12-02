using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightUltimateSkill : Skill
{
    GameObject skillPrefab;
    private float range;
    private float totalValue;

    public KnightUltimateSkill(int id) : base(id)
    {


        totalValue = value * (level * upgradeValue);
    }

    public override void UpgradeSkill(int amount)
    {
        level += amount;

        // TODO : 배율 조정
        totalValue = value * (level * upgradeValue);
    }

    public override void UseSkill(StatHandler statHandler)
    {

    }
}
