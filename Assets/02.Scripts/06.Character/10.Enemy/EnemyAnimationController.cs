using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public Enemy enemy;

    public void RangedAttack()
    {
        enemy.stateMachine.AttackState.RangedAttack();
    }

    public void MeleeAttackStart()
    {
        enemy.stateMachine.AttackState.MeleeAttack();
    }

    public void MeleeAttackBoss()
    {
        enemy.stateMachine.AttackState.MeleeAttackBoss();
    }
    public void MeleeSkillBossCharging()
    {
        enemy.stateMachine.SkillState.bossEnemy.StartSkillCoroutine(enemy.stateMachine.SkillState.PerformSkill());
    }

    public void MeleeSkillBossStart()
    {
        enemy.stateMachine.SkillState.MeleeSkillBossStart();
    }

    public void MeleeSkillBossEnd()
    {
        enemy.stateMachine.AttackState.meleeAttack.SetActive(false);
        enemy.stateMachine.ChangeState(enemy.stateMachine.MoveState);
    }

    public void MeleeAttackEnd()
    {
        enemy.stateMachine.AttackState.meleeAttack.SetActive(false);
    }
}
