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
        // 플레이어 대각 위치에서 생성한다.

        Vector2 playerPos = GameManager.Instance.player.transform.position;   // TODO : 플레이어 좌표 => 적용 확인 시 주석 삭제

        playerPos += new Vector2(5, 5); // TODO : 생성 할 좌표 수치

        GameObject meteor = Object.Instantiate(skillPrefab, playerPos, Quaternion.identity);
        if (meteor.TryGetComponent(out Meteor component))
        {
            component.InitSettings(statHandler.CurrentStat.atk * (int)totalValue, range);
        }
    }
}
