 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using ScottGarland;

public class EnemyHpSystem : MonoBehaviour, ITakeDamageAble
{
    public bool IsInvulnerable { get; set; }
    public Enemy enemy;
    public Slider healthBar;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        HpUpdate();
    }

    public void TakeDamage(BigInteger damage)
    {
        Debug.Log("������");
        enemy.currentHealth -= BigInteger.ToInt32(damage);
        HpUpdate();
        if (enemy.currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeKnockBack(Vector3 direction, float force)
    {
    }

    public void Die()
    {
        gameObject.SetActive(false);

        //TODO :: ���� ����
    }

    public void HpUpdate()
    {
        healthBar.value = enemy.currentHealth / enemy.enemyDB.Health;
        if (healthBar.value < 1 && healthBar.value > 0)
            healthBar.gameObject.SetActive(true);
        else
            healthBar.gameObject.SetActive(false);
    }
}
