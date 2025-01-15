using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public class EnemySkill4 : EnemySkillBase
{
    public GameObject bulletInstances;
    private float skillZoneTime = 2f;

    public EnemySkill4(BossEnemy bossEnemy, EnemyStateMachine stateMachine) : base(bossEnemy, stateMachine) { }

    public override IEnumerator PerformSkill()
    {
        Transform skillZoneTransform = bossEnemy.skillZone.transform;
        int childCount = skillZoneTransform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            skillZoneTransform.GetChild(i).gameObject.SetActive(true);
        }

        float elapsedTime = 0f;
        while (elapsedTime < skillZoneTime)
        {
            yield return null;
        }
    }

    public void SkillAttack4()
    {
        if (bossEnemy.transform.localScale.x > 0)
            bulletInstances = EnemyManager.Instance.EnemyAttackSpawn(6010, bossEnemy.transform.position, Quaternion.Euler(0, 0, 0));
        else
            bulletInstances = EnemyManager.Instance.EnemyAttackSpawn(6010, bossEnemy.transform.position, Quaternion.Euler(0, 180, 0));
    }
}
