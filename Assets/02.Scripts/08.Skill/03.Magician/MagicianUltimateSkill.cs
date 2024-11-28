using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MagicianUltimateSkill : Skill
{
    GameObject skillPrefab;
    private float range;

    public MagicianUltimateSkill(int id, string name, string description, int applyCount, float value, SkillType type) : base(id, name, description, applyCount, value, type)
    {
        skillPrefab = Resources.Load<GameObject>("Prefabs/Skills/Meteor");
        range = 10f;
    }

    public override void UpgradeSkill(int amount)
    {
        level += amount;

        // TODO : amount ��ŭ value ����
    }

    public override void UseSkill(StatHandler statHandler)
    {
        // TODO : �÷��̾� �밢 ��ġ���� �����Ѵ�.

        Vector2 playerPos = TestManager.Instance.TestPlayer.transform.position;   // TODO : �÷��̾� ��ǥ

        playerPos += new Vector2(5, 5); // TODO : ���� �� ��ǥ ��ġ

        GameObject meteor = Object.Instantiate(skillPrefab, playerPos, Quaternion.identity);
        if (meteor.TryGetComponent(out Meteor component))
        {
            component.InitSettings(value, range);
        }
    }
}
