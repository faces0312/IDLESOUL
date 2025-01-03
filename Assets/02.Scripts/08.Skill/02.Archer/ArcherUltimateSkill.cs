using ScottGarland;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherUltimateSkill : Skill
{
    GameObject skillPrefab;
    private float range;
    private float totalValue;

    public ArcherUltimateSkill(int id) : base(id)
    {
        // TODO : DB 에서 받아 넣기
        coolTime = 20f;
        skillPrefab = Resources.Load<GameObject>("Prefabs/Skills/ArrowStrike");
        range = 7f;
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
        Vector3 dir = skillPrefab.transform.forward;

        if (GameManager.Instance.player.PlayerAnimationController.skeleton.ScaleX > 0)
        {
            playerPos += skillPrefab.transform.position;
        }
        else
        {
            playerPos += new Vector3(-skillPrefab.transform.position.x, skillPrefab.transform.position.y, skillPrefab.transform.position.z);
            dir *= -1f;
        }

        GameObject arrowStrike = Object.Instantiate(skillPrefab, playerPos, Quaternion.LookRotation(dir));

        if (arrowStrike.TryGetComponent(out ArrowStrike component))
        {
            component.InitSettings((BigInteger.Divide(statHandler.CurrentStat.atk, 10) + (int)totalValue) * (int)value, range);
        }
    }
}
