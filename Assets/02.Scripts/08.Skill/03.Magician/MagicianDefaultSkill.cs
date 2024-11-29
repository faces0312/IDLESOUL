using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianDefaultSkill : Skill
{
    GameObject skillPrefab;
    private float range;
    private float searchRange;

    public MagicianDefaultSkill(int id, string name, string description, int applyCount, float value, SkillType type) : base(id, name, description, applyCount, value, type)
    {
        skillPrefab = Resources.Load<GameObject>("Prefabs/Skills/Explosion");
        range = 5f;
        searchRange = 10f;
    }

    public override void UpgradeSkill(int amount)
    {
        level += amount;

        // TODO : amount 만큼 value 증가
    }

    public override void UseSkill(StatHandler statHandler)
    {
        // 가장 가까운 Enemy 를 찾는 로직 구현
        // Enemy 리스트를 받아와 거리 기반 탐지

        List<GameObject> targets = GameManager.Instance.enemies;    // Enemy 리스트
        GameObject closestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;

        Vector2 playerPos = TestManager.Instance.TestPlayer.transform.position;   // TODO : 플레이어 좌표

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

        Vector3 targetPos = Vector3.zero;   // TODO : Default값 -> 플레이어 앞 좌표

        if (closestTarget != null)
            targetPos = closestTarget.transform.position;
        else
            Debug.LogAssertion("Target is null!");

        GameObject explosion = Object.Instantiate(skillPrefab, targetPos, Quaternion.identity);
        if(explosion.TryGetComponent(out Explosion component))
        {
            component.InitSettings(value, range);
        }
    }
}
