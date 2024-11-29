using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour, ITakeDamageAble
{
    protected StatHandler statHandler; //스텟 관리 클래스
    //protected Animator characterAnimator; //애니메이션 관련 컨트롤러
    //Controller CharacterController; //캐릭터 컨트롤러

    public abstract void Attack();
    public abstract void Move();

    public virtual void TakeDamage(float damage)
    {
    }

    public virtual void TakeKnockBack(Vector3 direction, float force)
    {
    }
}
