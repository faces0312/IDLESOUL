using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public Enemy enemy;

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

    public void SkeletonBossAttack()
    {
        enemy.stateMachine.AttackState.RangedAttack(6006);
    }
    public void SkeletonBossSkillCharging()
    {
        enemy.stateMachine.SkillState.bossEnemy.StartSkillCoroutine(enemy.stateMachine.SkillState.PerformSkill());
    }

    public void SkeletonBossSkillStart()
    {
        enemy.stateMachine.SkillState.SkeletonSkillBossStart();
    }

    public void SkeletonBossSkillEnd()
    {
        enemy.stateMachine.ChangeState(enemy.stateMachine.MoveState);
    }

    public void EnemyDie()
    {
        enemy.Die();
    }

    public void GoblinAttackStart()
    {
        enemy.stateMachine.AttackState.MeleeAttack(6000);
    }
    public void SkeletonAttackStart()
    {
        enemy.stateMachine.AttackState.MeleeAttack(6004);
    }

    public void MeleeAttackEnd()
    {
        enemy.stateMachine.AttackState.meleeAttack.SetActive(false);
    }

    public void EnergyBoltRangedAttack()
    {
        enemy.stateMachine.AttackState.RangedAttack(6001);
    }

    public void ArrowRangedAttack()
    {
        enemy.stateMachine.AttackState.RangedAttack(6005);
    }

    public void EnemyBossDie()
    {
        if (enemy is BossEnemy bossEnemy)
        {
            // skillZone 비활성화
            foreach (Transform child in bossEnemy.skillZone.transform)
                child.gameObject.SetActive(false);
            bossEnemy.skillChargingEffect.SetActive(false);
        }
        enemy.Die();
        GameManager.Instance.GameClear();
    }
}
