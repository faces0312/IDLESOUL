using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public class Boss3 : BossEnemy
{
    public AudioSource audio;
    public float skillSpeed;
    public float skillSpeedTmp;

    public GameObject rush;
    public bool isRush;
    private Vector3 rushDirection;
    public override void Initialize()
    {
        base.Initialize();
        healthBar.gameObject.SetActive(true);
        skillDamage = 500;
    }

    public override void TakeDamage(BigInteger damage)
    {
        base.TakeDamage(damage);
        if (statHandler.CurrentStat.health <= 0)
            audio.Play();
    }

    private void OnEnable()
    {
        StartCoroutine(Skill(8));
    }
    IEnumerator Skill(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            // 대기 후 실행할 함수
            stateMachine.ChangeState(stateMachine.SkillState);

            Vector3 start = skillZone.transform.position;
            Vector3 end = target.transform.position;
            Vector3 fin = end - start;
            rushDirection = fin.normalized;

            skillZone.transform.rotation = Quaternion.Euler(skillZone.transform.rotation.x, Quaternion.FromToRotation(Vector3.up, fin).eulerAngles.y, skillZone.transform.rotation.z);


            foreach (Transform child in skillZone.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    public override void Update()
    {
        base.Update();
        if(isRush ==  true)
        {
            if (target != null)
            {
                rushDirection.y = 0; // Y축 이동 방지
                // 돌진 속도로 이동
                transform.position += rushDirection * 25 * Time.deltaTime;
                //transform.rotation = Quaternion.LookRotation(rushDirection);
            }
        }
    }
}
