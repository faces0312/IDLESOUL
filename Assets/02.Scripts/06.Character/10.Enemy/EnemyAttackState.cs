using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

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

        if (attackSpeedTmp >= stateMachine.Enemy.StatHandler.CurrentStat.atkSpeed)
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
    }

    public void RangedAttack(int id)
    {
        GameObject rangedAttack = EnemyManager.Instance.EnemyAttackSpawn(id, stateMachine.Enemy.transform.position, Quaternion.Euler(Vector3.zero));

        if (rangedAttack != null)
        {
            // �ʿ��� ��� �߰� ����
            EnemyProjectile projectile = rangedAttack.GetComponent<EnemyProjectile>();
            projectile.attack = BigInteger.ToInt32(stateMachine.Enemy.StatHandler.CurrentStat.atk);
            if (projectile != null)
            {
                Vector3 targetPosition = GameManager.Instance.player.transform.position;
                projectile.dir = (targetPosition - rangedAttack.transform.position).normalized;
            }
        }
    }

    public void MimicAttack(Transform transform)
    {
        Vector3 rotationAngles = new Vector3(180, 0, 0);
        Vector3 enemyPosition = GameManager.Instance.player.transform.position; // ���� ���� ��ġ

        for (int i = 0; i < 6; i++)
        {
            // ������ x�� z ������ ���� (-5���� 5 ����)
            float randomX = Random.Range(-8f, 8f);
            float randomZ = Random.Range(-8f, 8f);

            // ���ο� ���� ��ġ ���
            Vector3 spawnPosition = enemyPosition + new Vector3(randomX, 0, randomZ);

            // ���� ������Ʈ ����
            GameObject attack = EnemyManager.Instance.EnemyAttackSpawn(6008, spawnPosition, Quaternion.Euler(rotationAngles));
            //attack.SetActive(true);
        }
        //GameObject attack = EnemyManager.Instance.EnemyAttackSpawn(6008, GameManager.Instance.player.transform.position, Quaternion.Euler(rotationAngles));
        //attack.SetActive(true);
    }
}
