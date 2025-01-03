using ScottGarland;
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
        // TODO : DB 에서 받아 넣기
        coolTime = 6f;
        skillPrefab = Resources.Load<GameObject>("Prefabs/Skills/SpinSword");
        range = 4f;
        totalValue = level * upgradeValue;
    }

    public override void UpgradeSkill(int amount)
    {
        level += amount;

        // TODO : 배율 조정
        totalValue = level * upgradeValue;
    }

    public override void UseSkill(StatHandler statHandler)
    {
        

        Vector3 playerPos = GameManager.Instance.player.transform.position;

        playerPos += skillPrefab.transform.position;

        GameObject spinSword = Object.Instantiate(skillPrefab, playerPos, Quaternion.LookRotation(skillPrefab.transform.forward));
        if (spinSword.TryGetComponent(out SpinSword component))
        {
            component.InitSettings((BigInteger.Divide(statHandler.CurrentStat.atk, 10) + (int)totalValue) * (int)value, range);
            component.OriginPos = skillPrefab.transform.position;
        }
    }
}
