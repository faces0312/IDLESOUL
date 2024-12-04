using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySkillState : EnemyBaseState
{
    private BossEnemy bossEnemy; 
    private int currentSkillIndex = 0;

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
        //보스가 스킬을 가지고 있다면 스킬 발동
        if (bossEnemy.skill.Count > 0)
        {
            yield return bossEnemy.skill[currentSkillIndex].PerformSkill();
            //다음 스킬 인덱스 이동.
            currentSkillIndex = (currentSkillIndex + 1) % bossEnemy.skill.Count;
        }
        else//스킬이 없다면 이동 상태로 전환
        {
            stateMachine.ChangeState(stateMachine.MoveState);
        }
    }
}
