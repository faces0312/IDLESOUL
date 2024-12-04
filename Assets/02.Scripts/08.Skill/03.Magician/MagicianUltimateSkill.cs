using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MagicianUltimateSkill : Skill
{
    GameObject skillPrefab;
    private float range;
    private float totalValue;

    public MagicianUltimateSkill(int id) : base(id)
    {
        skillPrefab = Resources.Load<GameObject>("Prefabs/Skills/Meteor");
        range = 10f;
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
        Vector3 playerPos = GameManager.Instance.player.transform.position;

        playerPos += skillPrefab.transform.position;

        GameObject meteor = Object.Instantiate(skillPrefab, playerPos, Quaternion.LookRotation(skillPrefab.transform.forward));
        if (meteor.TryGetComponent(out Meteor component))
        {
            component.InitSettings(statHandler.CurrentStat.atk * (int)totalValue, range);
        }
    }
}
