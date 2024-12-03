using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightDefaultSkill : Skill
{
    GameObject skillPrefab;
    private float range;
    private float totalValue;

    public KnightDefaultSkill(int id) : base(id)
    {
        skillPrefab = Resources.Load<GameObject>("Prefabs/Skills/SpinSword");
        range = 5f;
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
