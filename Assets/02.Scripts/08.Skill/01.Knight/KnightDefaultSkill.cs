using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightDefaultSkill : Skill
{
    GameObject skillPrefab;
    private float range;

    public KnightDefaultSkill(int id) : base(id)
    {
        skillPrefab = Resources.Load<GameObject>("Prefabs/Skills/SpinSword");
        range = 5f;
    }

    public override void UpgradeSkill(int amount)
    {
        level += amount;

        // TODO : amount ��ŭ value ����
    }

    public override void UseSkill(StatHandler statHandler)
    {
        
    }
}
