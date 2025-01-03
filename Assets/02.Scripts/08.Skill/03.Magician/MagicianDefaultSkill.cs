using System.Collections;
using System.Collections.Generic;
using ScottGarland;
using UnityEngine;

public class MagicianDefaultSkill : Skill
{
    GameObject skillPrefab;
    private float range;
    private float searchRange;
    private float totalValue;
    Transform playerTransform;

    public MagicianDefaultSkill(int id) : base(id)
    {
        // TODO : DB 에서 받아 넣기
        coolTime = 7f;
        skillPrefab = Resources.Load<GameObject>("Prefabs/Skills/Explosion");
        range = 5f;
        searchRange = 15f;
        totalValue = level * upgradeValue;
        playerTransform = GameManager.Instance.player.transform;
    }

    public override void UpgradeSkill(int amount)
    {
        level += amount;

        // TODO : 배율 조정
        totalValue = level * upgradeValue;
    }

    public override void UseSkill(StatHandler statHandler)
    {
        // 가장 가까운 Enemy 를 찾는 로직
        // Enemy 리스트를 받아와 거리 기반 탐지

        List<GameObject> targets = GameManager.Instance.enemies;    // Enemy 리스트
        GameObject closestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;

        Vector3 playerPos = playerTransform.position;

        foreach (GameObject target in targets)
        {
            float distanceSqr = (playerPos - target.transform.position).sqrMagnitude;

            if (distanceSqr < closestDistanceSqr)
            {
                closestTarget = target;
                closestDistanceSqr = distanceSqr;
            }
        }

        // 탐색 범위를 넘어선 대상은 null 처리
        if (searchRange * searchRange < closestDistanceSqr)
            closestTarget = null;

        Vector3 targetPos = playerPos;

        if (closestTarget != null)
        {
            targetPos = closestTarget.transform.position;
        }
        else
        {
            if (GameManager.Instance.player.PlayerAnimationController.skeleton.ScaleX > 0)
            {
                targetPos += playerTransform.right * 5f;
            }
            else
            {
                targetPos -= playerTransform.right * 5f;
            }
        }
        

        GameObject explosion = Object.Instantiate(skillPrefab, targetPos, Quaternion.LookRotation(skillPrefab.transform.forward));
        if (explosion.TryGetComponent(out Explosion component))
        {
            component.InitSettings((BigInteger.Divide(statHandler.CurrentStat.atk, 10) + (int)totalValue) * (int)value, range);
        }
    }
}
