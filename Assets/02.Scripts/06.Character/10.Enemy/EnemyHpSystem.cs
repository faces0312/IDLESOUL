 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyHpSystem : MonoBehaviour, ITakeDamageAble
{
    public Enemy enemy;
    public Slider healthBar;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        HpUpdate();
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("데미지");
        enemy.currentHealth -= damage;
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

        //TODO :: 점수 증가
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
