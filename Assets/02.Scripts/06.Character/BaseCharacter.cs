using System.Collections;
using System.Collections.Generic;
using ScottGarland;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour, ITakeDamageAble
{
    protected StatHandler statHandler; //스텟 관리 클래스
    //protected Animator characterAnimator; //애니메이션 관련 컨트롤러
    //protected CharacterController characterController; //캐릭터 컨트롤러

    public abstract void Attack();
    public abstract void Move();

    public virtual void TakeDamage(float damage)
    {
        int maxHelth = BigInteger.ToInt32(statHandler.CurrentStat.maxHealth);
        int curHelth = BigInteger.ToInt32(statHandler.CurrentStat.health);
        statHandler.CurrentStat.health = Mathf.Clamp(curHelth - (int)damage, 0, maxHelth);

        Debug.Log($"{gameObject.name} 피격됨! 데미지 : {damage} , 체력 상태 : {curHelth}");
    }

    public virtual void TakeKnockBack(Vector3 direction, float force)
    {
    }
}
