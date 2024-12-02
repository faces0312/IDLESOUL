using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySkillState : EnemyBaseState
{
    private BossEnemy bossEnemy;
    private float skillZoneTime = 1f;

    public EnemySkillState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        bossEnemy = stateMachine.Enemy as BossEnemy;
    }

    public override void Enter()
    {
        base.Enter();
        bossEnemy.StartSkillCoroutine(PerformSkill());
    }

    private IEnumerator PerformSkill()
    {
        Debug.Log("스킬시작");
        Vector3 originalScale = new Vector3(30, 0, 1);

        for (int i = 0; i < 4; i++)
            bossEnemy.skillZone[i].SetActive(true);
        
        float elapsedTime = 0f;
        while (elapsedTime < skillZoneTime)
        {
            float yScale = Mathf.Lerp(0, 1, elapsedTime / skillZoneTime);
            for (int i = 0; i < 4; i++)
                bossEnemy.skillZone[i].transform.localScale = new Vector3(originalScale.x, yScale, originalScale.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < 4; i++)
            bossEnemy.skillZone[i].SetActive(false);

        bossEnemy.SkillAttack();

        stateMachine.ChangeState(stateMachine.MoveState);
        Debug.Log("스킬종료");
    }

}
