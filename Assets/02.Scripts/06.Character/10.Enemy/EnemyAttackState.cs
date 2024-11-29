using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private float attackSpeedTmp;
    public EnemyAttackState(EnemyStateMachine _stateMachine) : base(_stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        /*animator.SetBool(animatorHashData.IdleParameterHash, false);
        animator.SetBool(animatorHashData.WalkParameterHash, false);
        animator.SetTrigger(animatorHashData.AttackParameterHash);
        animator.SetFloat(animatorHashData.AttackSpeedParameterHash, stateMachine.Enemy.enemyDB.AttackSpeed);*/
        attackSpeedTmp = 0f;
    }

    public override void Update()
    {
        base.Update();
        attackSpeedTmp += Time.deltaTime;

        if (attackSpeedTmp >= stateMachine.Enemy.enemyDB.AttackSpeed)
        {
            Attack();
            attackSpeedTmp = 0f;
        }

        float distanceTmp = Vector3.Distance(stateMachine.Enemy.transform.position, stateMachine.Enemy.target.transform.position);
        if (distanceTmp > stateMachine.Enemy.enemyDB.Distance)
        {
            stateMachine.ChangeState(stateMachine.MoveState);
        }
    }

    public void Attack()
    {
        switch (stateMachine.Enemy.attackType)
        {
            case AttackType.Melee:
                MeleeAttack();
                break;
            case AttackType.Ranged:
                RangedAttack();
                break;
        }
    }

    void MeleeAttack()
    {
        Debug.Log("근거리공격");
    }

    void RangedAttack()
    {
        Debug.Log("원거리공격");
        //원거리 적의 경우
        GameObject bulletInstance = Object.Instantiate(stateMachine.Enemy.bulletTest, stateMachine.Enemy.transform.position, Quaternion.Euler(Vector3.zero));
        BulletTest monsterBullet = bulletInstance.GetComponent<BulletTest>();
        monsterBullet.attack = stateMachine.Enemy.enemyDB.Attack;
        monsterBullet.knockbackPower = stateMachine.Enemy.enemyDB.KnockBackPower;

        Vector3 playerProjection = new Vector3(stateMachine.Enemy.target.transform.position.x, stateMachine.Enemy.target.transform.position.y, 0.0f);
        Vector3 selfProjection = new Vector3(stateMachine.Enemy. transform.position.x, stateMachine.Enemy.transform.position.y, 0.0f);

        monsterBullet.direction = (playerProjection - selfProjection).normalized;
    }
}
