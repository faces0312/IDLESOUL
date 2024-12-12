using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill2 : EnemySkillBase
{
    public GameObject bulletInstances;
    private float skillZoneTime = 2f;

    public EnemySkill2(BossEnemy bossEnemy, EnemyStateMachine stateMachine) : base(bossEnemy, stateMachine) { }

    public override IEnumerator PerformSkill()
    {
        bossEnemy.skillChargingEffect.SetActive(true);
        Debug.Log("��ų2����");

        foreach (Transform child in bossEnemy.skillZone.transform)
        {
            child.position = new Vector3(GameManager.Instance.player.transform.position.x, GameManager.Instance.player.transform.position.y - 0.5f, GameManager.Instance.player.transform.position.z);
            child.gameObject.SetActive(true);
        }

        yield return null;
    }

    public void SkillAttack2()
    {
        bulletInstances = EnemyManager.Instance.EnemyAttackSpawn(6007, new Vector3(GameManager.Instance.player.transform.position.x, GameManager.Instance.player.transform.position.y + 8f, GameManager.Instance.player.transform.position.z), Quaternion.Euler(90, 0, 0));

        EnemyProjectile projectile = bulletInstances.GetComponent<EnemyProjectile>();
        //skill �������� ����
        projectile.attack = stateMachine.Enemy.enemyDB.Attack;
        projectile.knockbackPower = stateMachine.Enemy.enemyDB.KnockBackPower;

        if (projectile != null)
        {
            projectile.dir = Vector3.down;//rotation.eulerAngles;
        }
        /*int bulletCount = 8;
        float angleStep = 45f; // 45�� ����
        GameObject[] bulletInstances = new GameObject[bulletCount];

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep; // 0������ �����Ͽ� 45���� ����
            Quaternion rotation = Quaternion.Euler(0, angle, 0);

            bulletInstances[i] = EnemyManager.Instance.EnemyAttackSpawn(6003, new Vector3(stateMachine.Enemy.transform.position.x, stateMachine.Enemy.transform.position.y, stateMachine.Enemy.transform.position.z), rotation);

            EnemyProjectile projectile = bulletInstances[i].GetComponent<EnemyProjectile>();
            //skill �������� ����
            projectile.attack = stateMachine.Enemy.enemyDB.Attack;
            projectile.knockbackPower = stateMachine.Enemy.enemyDB.KnockBackPower;

            if (projectile != null)
            {
                projectile.dir = rotation * Vector3.right;//rotation.eulerAngles;
            }
            *//*// �� �Ѿ��� ������ ���
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.right;
            monsterBullet.Initialize(direction);*//*
        }*/
    }
}
