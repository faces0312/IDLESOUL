using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianDefaultSkill : Skill
{
    GameObject skillPrefab;
    private float range;

    public MagicianDefaultSkill(int id, string name, string description, int applyCount, float value, SkillType type) : base(id, name, description, applyCount, value, type)
    {
        skillPrefab = Resources.Load<GameObject>("Prefabs/Skills/Explosion");
        range = 5f;
    }

    public override void UpgradeSkill(int amount)
    {
        level += amount;

        // TODO : amount 만큼 value 증가
    }

    public override void UseSkill()
    {
        // TODO : 가장 가까운 Enemy 를 찾는 로직 구현
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

        Vector3 targetPos = Vector3.zero;

        if (closestTarget == null)
            Debug.LogAssertion("Target is null!");
        else
            targetPos = closestTarget.transform.position;

        GameObject explosion = Object.Instantiate(skillPrefab, targetPos, Quaternion.identity);
        if(explosion.TryGetComponent(out Explosion component))
        {
            component.InitSettings(value, range);
        }
    }
}
