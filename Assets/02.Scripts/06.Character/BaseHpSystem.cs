using UnityEngine;
using ScottGarland;
using UnityEngine.UI;
using System;

public class BaseHpSystem : MonoBehaviour
{
    public bool IsDead; 

    void Start()
    {
        HpUpdate();
    }

    public void TakeDamage(BigInteger damage, StatHandler statHandler)
    {
        statHandler.CurrentStat.health = statHandler.CurrentStat.health - damage;
        HpUpdate();
    }
    public void TakeHeal(BigInteger heal, StatHandler statHandler)
    {
        if (statHandler.CurrentStat.health + heal >= statHandler.CurrentStat.maxHealth)
        {
            statHandler.CurrentStat.health = statHandler.CurrentStat.maxHealth;
        }
        else
        {
            statHandler.CurrentStat.health += heal;
        }

        HpUpdate();
    }
    public void TakeKnockBack(Vector3 direction, float force)
    {
    }

    protected virtual void Die()
    {
        gameObject.SetActive(false);
        Debug.Log($"{gameObject.name} 사망!!");
        IsDead = true;
        //TODO :: 점수 증가
    }

    public void HpUpdate()
    {
        UIManager.Instance.ShowUI<UIPlayerHPDisplayController>();
    }
}
