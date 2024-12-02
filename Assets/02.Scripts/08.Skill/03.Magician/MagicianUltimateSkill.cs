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

        // TODO : ���� ����
        totalValue = value * (level * upgradeValue);
    }

    public override void UseSkill(StatHandler statHandler)
    {
        // �÷��̾� �밢 ��ġ���� �����Ѵ�.

        Vector2 playerPos = GameManager.Instance.player.transform.position;   // TODO : �÷��̾� ��ǥ => ���� Ȯ�� �� �ּ� ����

        playerPos += new Vector2(5, 5); // TODO : ���� �� ��ǥ ��ġ

        GameObject meteor = Object.Instantiate(skillPrefab, playerPos, Quaternion.identity);
        if (meteor.TryGetComponent(out Meteor component))
        {
            component.InitSettings(statHandler.CurrentStat.atk * (int)totalValue, range);
        }
    }
}
