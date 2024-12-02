using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MagicianUltimateSkill : Skill
{
    GameObject skillPrefab;
    private float range;

    public MagicianUltimateSkill(int id) : base(id)
    {
        skillPrefab = Resources.Load<GameObject>("Prefabs/Skills/Meteor");
        range = 10f;
    }

    public override void UpgradeSkill(int amount)
    {
        level += amount;

        // TODO : 배율 조정
        value *= level;
    }

    public override void UseSkill(StatHandler statHandler)
    {
        // TODO : 플레이어 대각 위치에서 생성한다.

        Vector2 playerPos = TestManager.Instance.TestPlayer.transform.position;   // TODO : 플레이어 좌표

        playerPos += new Vector2(5, 5); // TODO : 생성 할 좌표 수치

        GameObject meteor = Object.Instantiate(skillPrefab, playerPos, Quaternion.identity);
        if (meteor.TryGetComponent(out Meteor component))
        {
            component.InitSettings(value, range);
        }
    }
}
