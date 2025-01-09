using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public class MageDefaultSkill : Skill
{
    GameObject skillPrefab;
    private float range;
    private float searchRange;
    private float totalValue;
    Transform playerTransform;

    public MageDefaultSkill(int id) : base(id)
    {
        // TODO : DB 에서 받아 넣기
        coolTime = 7f;
        skillPrefab = Resources.Load<GameObject>("Prefabs/Skills/WindStorm");
        range = 5f;
        searchRange = 15f;
        totalValue = level * upgradeValue;
        playerTransform = GameManager.Instance.player.transform;
    }

    public override void UpgradeSkill(int amount)
    {
        level += amount;

        // TODO : 배율 조정
        totalValue = level * upgradeValue;
    }

    public override void UseSkill(StatHandler statHandler)
    {
        Vector3 playerPos = playerTransform.position;

        GameObject windStorm = Object.Instantiate(skillPrefab, playerPos, Quaternion.LookRotation(skillPrefab.transform.forward));
        if (windStorm.TryGetComponent(out WindStorm component))
        {
            component.InitSettings((BigInteger.Divide(statHandler.CurrentStat.atk, 10) + (int)totalValue) * (int)value, range);
        }
    }
}
