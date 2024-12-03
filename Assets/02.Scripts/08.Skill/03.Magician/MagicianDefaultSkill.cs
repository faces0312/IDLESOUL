using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianDefaultSkill : Skill
{
    GameObject skillPrefab;
    private float range;
    private float searchRange;
    private float totalValue;

    public MagicianDefaultSkill(int id) : base(id)
    {
        skillPrefab = Resources.Load<GameObject>("Prefabs/Skills/Explosion");
        range = 5f;
        searchRange = 10f;
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
        // 가장 가까운 Enemy 를 찾는 로직
        // Enemy 리스트를 받아와 거리 기반 탐지

        List<GameObject> targets = GameManager.Instance.enemies;    // Enemy 리스트
        GameObject closestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;

        Vector2 playerPos = GameManager.Instance.player.transform.position;   // TODO : 플레이어 좌표 => 적용 확인 시 주석 삭제

        foreach (GameObject target in targets)
        {
            float distanceSqr = (playerPos - (Vector2)target.transform.position).sqrMagnitude;

            if (distanceSqr < closestDistanceSqr)
            {
                closestTarget = target;
                closestDistanceSqr = distanceSqr;
            }
        }

        // 탐색 범위를 넘어선 대상은 null 처리
        if (searchRange * searchRange < closestDistanceSqr)
            closestTarget = null;

        Vector3 targetPos = playerPos;  // 플레이어 상 하 좌 우 에 배치할 것인가?

        if (closestTarget != null)
            targetPos = closestTarget.transform.position;

        GameObject explosion = Object.Instantiate(skillPrefab, targetPos, Quaternion.identity);
        if (explosion.TryGetComponent(out Explosion component))
        {
            component.InitSettings(statHandler.CurrentStat.atk * (int)totalValue, range);
        }
    }
}
