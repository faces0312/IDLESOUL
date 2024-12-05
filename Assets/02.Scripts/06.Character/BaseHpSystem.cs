using UnityEngine;
using ScottGarland;
using UnityEngine.UI;

public class BaseHpSystem : MonoBehaviour
{
    void Start()
    {
        HpUpdate();
    }

    public void TakeDamage(float damage, StatHandler statHandler)
    {
        int maxHelth = BigInteger.ToInt32(statHandler.CurrentStat.maxHealth);
        int curHelth = BigInteger.ToInt32(statHandler.CurrentStat.health);
        statHandler.CurrentStat.health = Mathf.Clamp(curHelth - (int)damage, 0, maxHelth);
        Debug.Log($"{gameObject.name} 피격됨! 데미지 : {damage} , 체력 상태 : {curHelth}");

        HpUpdate();
        if (statHandler.CurrentStat.health <= 0)
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
        Debug.Log($"{gameObject.name} 사망!!");
        //TODO :: 점수 증가
    }

    public void HpUpdate()
    {

    }
}
