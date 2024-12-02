using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    public GameObject[] skillZone;

    public float skillSpeed;
    public float skillSpeedTmp;

    public override void Update()
    {
        base.Update();

        skillSpeedTmp += Time.deltaTime;
        if (skillSpeedTmp >= 10f)
        {
            stateMachine.ChangeState(stateMachine.SkillState);
            skillSpeedTmp = 0f;
        }
    }

    public void StartSkillCoroutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }

    public void SkillAttack()
    {
        int bulletCount = 8;
        float angleStep = 45f; // 45도 간격
        GameObject[] bulletInstances = new GameObject[bulletCount];

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep; // 0도부터 시작하여 45도씩 증가
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            bulletInstances[i] = Instantiate(stateMachine.Enemy.bulletTest, stateMachine.Enemy.transform.position, rotation);

            BulletTest monsterBullet = bulletInstances[i].GetComponent<BulletTest>();
            //skill 데미지로 설정
            monsterBullet.attack = stateMachine.Enemy.enemyDB.Attack;
            monsterBullet.knockbackPower = stateMachine.Enemy.enemyDB.KnockBackPower;

            // 각 총알의 방향을 계산
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.right;
            monsterBullet.Initialize(direction);
        }
    }
}
