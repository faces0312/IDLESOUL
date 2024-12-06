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
        // TODO : DB 에서 받아 넣기
        coolTime = 10f;
        skillPrefab = Resources.Load<GameObject>("Prefabs/Skills/SlashDance");
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
        Vector3 playerPos = GameManager.Instance.player.transform.position;

        playerPos += skillPrefab.transform.position;

        GameObject slashDance = Object.Instantiate(skillPrefab, playerPos, Quaternion.LookRotation(skillPrefab.transform.forward));
        //if (spinSword.TryGetComponent(out SpinSword component))
        //{
        //    component.InitSettings(statHandler.CurrentStat.atk * (int)totalValue, range);
        //    component.OriginPos = skillPrefab.transform.position;
        //}
    }
}
