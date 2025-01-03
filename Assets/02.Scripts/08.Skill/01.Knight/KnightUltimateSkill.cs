using ScottGarland;
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
        // TODO : DB ���� �޾� �ֱ�
        coolTime = 25f;
        skillPrefab = Resources.Load<GameObject>("Prefabs/Skills/SlashDance");
        range = 10f;
        totalValue = level * upgradeValue;
    }

    public override void UpgradeSkill(int amount)
    {
        level += amount;

        // TODO : ���� ����
        totalValue = level * upgradeValue;
    }

    public override void UseSkill(StatHandler statHandler)
    {
        Vector3 playerPos = GameManager.Instance.player.transform.position;

        playerPos += skillPrefab.transform.position;

        GameObject slashDance = Object.Instantiate(skillPrefab, playerPos, Quaternion.LookRotation(skillPrefab.transform.forward));
        if (slashDance.TryGetComponent(out SlashDance component))
        {
            component.InitSettings((BigInteger.Divide(statHandler.CurrentStat.atk, 10) + (int)totalValue) * (int)value, range);
        }

        GameManager.Instance.cameraController.SwordSlashEffect();
    }
}
