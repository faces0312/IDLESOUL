using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherDefaultSkill : Skill
{
    GameObject skillPrefab;
    private float range;
    private float searchRange;
    private float totalValue;
    Transform playerTransform;

    public ArcherDefaultSkill(int id) : base(id)
    {
        // TODO : DB ���� �޾� �ֱ�
        coolTime = 3f;
        skillPrefab = Resources.Load<GameObject>("Prefabs/Skills/ArrowShot");
        range = 10f;
        searchRange = 15f;
        totalValue = value * (level * upgradeValue);
        playerTransform = GameManager.Instance.player.transform;
    }

    public override void UpgradeSkill(int amount)
    {
        level += amount;

        // TODO : ���� ����
        totalValue = value * (level * upgradeValue);
    }

    public override void UseSkill(StatHandler statHandler)
    {
        // ���� ����� Enemy �� ã�� ����
        // Enemy ����Ʈ�� �޾ƿ� �Ÿ� ��� Ž��

        List<GameObject> targets = GameManager.Instance.enemies;    // Enemy ����Ʈ
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

        // Ž�� ������ �Ѿ ����� null ó��
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
                targetPos += playerTransform.right;
            }
            else
            {
                targetPos -= playerTransform.right;
            }
        }


        GameObject arrowShot = Object.Instantiate(skillPrefab, targetPos, Quaternion.LookRotation(skillPrefab.transform.forward));
        //if (arrowShot.TryGetComponent(out Explosion component))
        //{
        //    component.InitSettings(statHandler.CurrentStat.atk * (int)totalValue, range);
        //}
    }
}
