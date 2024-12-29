using UnityEngine;
using ScottGarland;
using UnityEngine.UI;

public class BaseHpSystem : MonoBehaviour
{
    public bool IsDead; 

    void Start()
    {
        HpUpdate();
    }

    public void TakeDamage(BigInteger damage, StatHandler statHandler)
    {
        int maxHelth = BigInteger.ToInt32(statHandler.CurrentStat.maxHealth);
        int curHelth = BigInteger.ToInt32(statHandler.CurrentStat.health);
        statHandler.CurrentStat.health = Mathf.Clamp(curHelth - BigInteger.ToInt32(damage), 0, maxHelth);
        //Debug.Log($"{gameObject.name} 피격됨! 데미지 : {damage} , 체력 상태 : {curHelth}");

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

    }
}
