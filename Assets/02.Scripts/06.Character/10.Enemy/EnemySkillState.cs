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
        if (bossEnemy.skill.Count > 0)
        {
            yield return bossEnemy.skill[currentSkillIndex].PerformSkill();
            currentSkillIndex = (currentSkillIndex + 1) % bossEnemy.skill.Count;
        }
        else
        {
            stateMachine.ChangeState(stateMachine.MoveState);
        }
    }
}
