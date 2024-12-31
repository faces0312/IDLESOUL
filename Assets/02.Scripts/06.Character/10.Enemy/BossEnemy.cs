using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossEnemy : Enemy
{
    public List<EnemySkillBase> skill;//������ ����� ��ų��
    public bool isSkillCharging;//��ų ��¡������
    public GameObject skillZone;
    public GameObject skillChargingEffect;
    public static float skillDamage;
    public Vector3 originalScale;

    public void StartSkillCoroutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }

    public override void BossAppear()
    {
        IsInvulnerable = true;
        StartCoroutine(BossAppearCoroutine());
    }


    public override void SetSkillCharging(bool charging)
    {
        isSkillCharging = charging;
        if (charging)
        {
            originalScale = transform.localScale; // ��ų ��� ���� ���� scale ����
        }
    }


    IEnumerator BossAppearCoroutine()
    {
        yield return new WaitForSeconds(2.5f);
        IsInvulnerable = false;
    }

    public override void LateUpdate()
    {
        if (!isSkillCharging)
        {
            base.LateUpdate(); // �⺻ ���� ��ȯ ����
        }
        else
        {
            transform.localScale = originalScale; // ��ų ��� ���� ���� scale�� ����
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
        isSkillCharging = false;
    }

}