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
        StopAnimation(animatorHashData.WalkParameterHash);
        StartAnimation(animatorHashData.IdleParameterHash);
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
        StartAnimationTrigger(animatorHashData.AttackParameterHash);
        switch (stateMachine.Enemy.attackType)
        {
            case AttackType.Melee:
                //MeleeAttack();
                break;
            case AttackType.Ranged:
                //Invoke("RangedAttack", 0.45f);
                break;
        }
    }

    public void MeleeAttack()
    {
        GameObject meleeAttack;
        if (stateMachine.Enemy.transform.localScale.x > 0)
            meleeAttack = EnemyManager.Instance.EnemyAttackSpawn(6000, new Vector3(stateMachine.Enemy.transform.position.x - 0.5f, stateMachine.Enemy.transform.position.y, stateMachine.Enemy.transform.position.z), Quaternion.Euler(90, 0, 90));
        else
            meleeAttack = EnemyManager.Instance.EnemyAttackSpawn(6000, new Vector3(stateMachine.Enemy.transform.position.x + 0.5f, stateMachine.Enemy.transform.position.y, stateMachine.Enemy.transform.position.z), Quaternion.Euler(90, 180, 90));

    }

    public void RangedAttack()
    {
        GameObject rangedAttack = EnemyManager.Instance.EnemyAttackSpawn(6001, stateMachine.Enemy.transform.position, Quaternion.Euler(Vector3.zero));

        if (rangedAttack != null)
        {
            // 필요한 경우 추가 설정
            EnemyProjectile projectile = rangedAttack.GetComponent<EnemyProjectile>();
            if (projectile != null)
            {
                Vector3 targetPosition = GameManager.Instance._player.transform.position;
                projectile.dir = (targetPosition - rangedAttack.transform.position).normalized;
            }
        }
        /*//Debug.Log("원거리공격");
        //원거리 적의 경우
        GameObject bulletInstance = 
        Object.Instantiate(stateMachine.Enemy.bulletTest,new Vector3(stateMachine.Enemy.transform.position.x, stateMachine.Enemy.transform.position.y, stateMachine.Enemy.transform.position.z), Quaternion.Euler(Vector3.zero));
        //EnemyProjectile monsterBullet = bulletInstance.GetComponent<EnemyProjectile>();
        //monsterBullet.attack = stateMachine.Enemy.enemyDB.Attack;
        //monsterBullet.knockbackPower = stateMachine.Enemy.enemyDB.KnockBackPower;


        Vector3 playerProjection = new Vector3(stateMachine.Enemy.target.transform.position.x, stateMachine.Enemy.target.transform.position.y, stateMachine.Enemy.target.transform.position.z);
        Vector3 selfProjection = new Vector3(stateMachine.Enemy. transform.position.x, stateMachine.Enemy.transform.position.y, stateMachine.Enemy.transform.position.z);

        bulletInstance.GetComponent<EnemyProjectile>().dir = (playerProjection - selfProjection).normalized;
        //monsterBullet.dir = (playerProjection - selfProjection).normalized;*/
    }
}
