
using UnityEngine;
using ScottGarland;

public abstract class BaseCharacter : MonoBehaviour, ITakeDamageAble
{
    public bool IsInvulnerable { get; set; }

    protected StatHandler statHandler; //���� ���� Ŭ����
    //protected Animator characterAnimator; //�ִϸ��̼� ���� ��Ʈ�ѷ�
    //protected CharacterController characterController; //ĳ���� ��Ʈ�ѷ�

    [SerializeField] protected BaseHpSystem baseHpSystem; //ü�� ������ִ� Ŭ���� 
    public Rigidbody rb;
    public BaseHpSystem BaseHpSystem { get => baseHpSystem; }
    public StatHandler StatHandler { get => statHandler; set => statHandler = value; }

    protected virtual void Awake()
    {
        if(baseHpSystem == null)
        {
            baseHpSystem = GetComponent<BaseHpSystem>();
        }
    }

    public virtual void TakeDamage(BigInteger damage)
    {
        baseHpSystem.TakeDamage(damage, statHandler);
    }

    public virtual void TakeKnockBack(Vector3 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode.Impulse);
    }
}
