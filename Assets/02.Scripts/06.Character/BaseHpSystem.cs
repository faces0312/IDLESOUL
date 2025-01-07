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
        //var maxHelth = BigInteger.ToUInt64(statHandler.CurrentStat.maxHealth);
        var curHelth = BigInteger.ToUInt64(statHandler.CurrentStat.health);

        var result = BigInteger.ToUInt64(damage);
        if(curHelth - result > result) //unsinged 자료형이기에 
        {
            statHandler.CurrentStat.health = 0;
        }
        else
        {
            //statHandler.CurrentStat.health = Math.Clamp(curHelth - result, 0, maxHelth);
            statHandler.CurrentStat.health = curHelth - result;
        }
       

        HpUpdate();
    }
    public void TakeHeal(BigInteger heal, StatHandler statHandler)
    {
        var maxHelth = BigInteger.ToUInt64(statHandler.CurrentStat.maxHealth);
        var curHelth = BigInteger.ToUInt64(statHandler.CurrentStat.health);
        statHandler.CurrentStat.health = Math.Clamp(curHelth + BigInteger.ToUInt64(heal), 0, maxHelth);

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
