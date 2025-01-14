using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public Enemy enemy;

    private void Start()
    {
        if (enemy != null)
        {
            GameManager.Instance.OnBossDieEvent += EnemyBossSet;
        }
    }
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

    public void WolfBossSkillCharging()
    {
        enemy.SetSkillCharging(true);
    }

    public void WolfBossSkillStart()
    {
        enemy.stateMachine.SkillState.WolfSkillBossStart();
        if (enemy is Boss3 boss3)
        {
            boss3.isRush = true;
            boss3.rush.SetActive(true);
        }
    }

    public void WolfBossSkillEnd()
    {
        enemy.SetSkillCharging(false);
        if (enemy is Boss3 boss3)
        {
            boss3.isRush = false;
            boss3.rush.SetActive(false);
        }
        enemy.animator.Rebind();
        enemy.stateMachine.ChangeState(enemy.stateMachine.MoveState);
    }

    public void GolemSkillBossCharging()
    {
        enemy.stateMachine.SkillState.bossEnemy.StartSkillCoroutine(enemy.stateMachine.SkillState.PerformSkill());
    }

    public void GolemSkillBossStart()
    {
        enemy.stateMachine.SkillState.GolemSkillBossStart();
    }

    public void GolemSkillBossEnd()
    {
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
    public void GolemAttackStart()
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
            if (bossEnemy.skillZone != null)
            {
                foreach (Transform child in bossEnemy.skillZone.transform)
                    child.gameObject.SetActive(false);
            }
            if (bossEnemy.skillChargingEffect != null)
            {
                bossEnemy.skillChargingEffect.SetActive(false);
            }
        }
        enemy.animator.Rebind();
        enemy.Die();
        GameManager.Instance.GameClear();
    }

    public void EnemyBossSet()
    {
        if (enemy is BossEnemy bossEnemy)
        {
            if (bossEnemy.skillZone != null)
            {
                foreach (Transform child in bossEnemy.skillZone.transform)
                    child.gameObject.SetActive(false);
            }
            if (bossEnemy.skillChargingEffect != null)
            {
                bossEnemy.skillChargingEffect.SetActive(false);
            }
        }
        enemy.animator.Rebind();
        enemy.Die();
    }
}
