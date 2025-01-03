using ScottGarland;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : BossEnemy
{
    public AudioSource audio;

    public float skillSpeed;
    public float skillSpeedTmp;


    public override void Initialize()
    {
        base.Initialize();
        healthBar.gameObject.SetActive(true);
        skillDamage = 500;
        skill = new List<EnemySkillBase>
        {
            new EnemySkill1(this, stateMachine)
        };
    }
    private void OnEnable()
    {
        StartCoroutine(Skill(10f));
    }

    public override void TakeDamage(BigInteger damage)
    {
        base.TakeDamage(damage);
        if (statHandler.CurrentStat.health <= 0)
            audio.Play();
    }

    IEnumerator Skill(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            // ��� �� ������ �Լ�
            stateMachine.ChangeState(stateMachine.SkillState);
        }
    }
}
