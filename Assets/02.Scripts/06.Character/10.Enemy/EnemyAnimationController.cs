using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public Enemy enemy;

    public void MeleeAttackBoss()
    {
        enemy.slash.SetActive(true);
    }
    public void MeleeSkillBossCharging()
    {
        enemy.SetSkillCharging(true);
        enemy.stateMachine.SkillState.bossEnemy.StartSkillCoroutine(enemy.stateMachine.SkillState.PerformSkill());
    }

    public void MeleeSkillBossStart()
    {
        enemy.stateMachine.SkillState.MeleeSkillBossStart();
    }

    public void MeleeSkillBossEnd()
    {
        enemy.SetSkillCharging(false);
        enemy.slash.SetActive(false);
        enemy.stateMachine.ChangeState(enemy.stateMachine.MoveState);
    }

    public void SkeletonBossAttack()
    {
        enemy.stateMachine.AttackState.RangedAttack(6006);
    }
    public void SkeletonBossSkillCharging()
    {
        enemy.SetSkillCharging(true);
        enemy.stateMachine.SkillState.bossEnemy.StartSkillCoroutine(enemy.stateMachine.SkillState.PerformSkill());
    }

    public void SkeletonBossSkillStart()
    {
        enemy.stateMachine.SkillState.SkeletonSkillBossStart();
    }

    public void SkeletonBossSkillEnd()
    {
        enemy.SetSkillCharging(false);
        enemy.stateMachine.ChangeState(enemy.stateMachine.MoveState);
    }

    public void EnemyDie()
    {
        enemy.animator.Rebind();
        enemy.Die();
    }

    public void GoblinAttackStart()
    {
        enemy.slash.SetActive(true);
    }
    public void SkeletonAttackStart()
    {
        enemy.slash.SetActive(true);
    }

    public void WolfAttackStart()
    {
        enemy.slash.SetActive(true);
    }

    public void MeleeAttackEnd()
    {
        enemy.slash.SetActive(false);
    }

    public void EnergyBoltRangedAttack()
    {
        enemy.stateMachine.AttackState.RangedAttack(6001);
    }

    public void ArrowRangedAttack()
    {
        enemy.stateMachine.AttackState.RangedAttack(6005);
    }

    public void WolfRangedAttack()
    {
        enemy.stateMachine.AttackState.RangedAttack(6009);
    }

    public void MimicAttack()
    {
        enemy.stateMachine.AttackState.MimicAttack(enemy.transform);
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
        enemy.animator.Rebind();
        enemy.Die();
        GameManager.Instance.GameClear();
    }
}
