using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill1 : EnemySkillBase
{
    private float skillZoneTime = 2f;

    public EnemySkill1(BossEnemy bossEnemy, EnemyStateMachine stateMachine) : base(bossEnemy, stateMachine) { }

    public override IEnumerator PerformSkill()
    {
        Debug.Log("스킬1시작");
        Vector3 originalScale = new Vector3(30, 0, 1);

        foreach (Transform child in bossEnemy.skillZone.transform)
            child.gameObject.SetActive(true);

        float elapsedTime = 0f;
        while (elapsedTime < skillZoneTime)
        {
            float yScale = Mathf.Lerp(0, 1, elapsedTime / skillZoneTime);
            foreach (Transform child in bossEnemy.skillZone.transform)
                child.transform.localScale = new Vector3(originalScale.x, yScale, originalScale.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foreach (Transform child in bossEnemy.skillZone.transform)
            child.gameObject.SetActive(false);

        SkillAttack1();

        stateMachine.ChangeState(stateMachine.MoveState);
        Debug.Log("스킬1종료");
    }

    public void SkillAttack1()
    {
        int bulletCount = 8;
        float angleStep = 45f; // 45도 간격
        GameObject[] bulletInstances = new GameObject[bulletCount];

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep; // 0도부터 시작하여 45도씩 증가
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            bulletInstances[i] = Object.Instantiate(stateMachine.Enemy.bulletTest, stateMachine.Enemy.transform.position, rotation);

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
