
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour, ITakeDamageAble
{
    protected StatHandler statHandler; //스텟 관리 클래스
    //protected Animator characterAnimator; //애니메이션 관련 컨트롤러
    //protected CharacterController characterController; //캐릭터 컨트롤러

    [SerializeField] protected BaseHpSystem baseHpSystem; //체력 계산해주는 클래스 
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

    public virtual void TakeDamage(float damage)
    {
        baseHpSystem.TakeDamage(damage, statHandler);
    }

    public virtual void TakeKnockBack(Vector3 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode.Impulse);
    }
}
