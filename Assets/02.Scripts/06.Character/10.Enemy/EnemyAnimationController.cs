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
}
