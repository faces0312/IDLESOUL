
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour, ITakeDamageAble
{
    protected StatHandler statHandler; //���� ���� Ŭ����
    //protected Animator characterAnimator; //�ִϸ��̼� ���� ��Ʈ�ѷ�
    //protected CharacterController characterController; //ĳ���� ��Ʈ�ѷ�

    [SerializeField]protected BaseHpSystem baseHpSystem; //ü�� ������ִ� Ŭ���� 

    public abstract void Attack();
    public abstract void Move();

    protected virtual void Awake()
    {
        if(baseHpSystem == null)
        {
            baseHpSystem = GetComponent<BaseHpSystem>();
        }
    }

    public virtual void TakeDamage(float damage)
    {
        baseHpSystem.TakeDamage(damage, statHandler);
    }

    public virtual void TakeKnockBack(Vector3 direction, float force)
    {
        
    }
}
