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

    public void MeleeAttackEnd()
    {
        enemy.stateMachine.AttackState.meleeAttack.SetActive(false);
    }
}
