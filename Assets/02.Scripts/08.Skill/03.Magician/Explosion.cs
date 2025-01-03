using Cinemachine;
using ScottGarland;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    private float curTime;

    private BigInteger value;
    private float range;

    private Collider myCollider;
    private CinemachineImpulseSource impulseSource;
    private LayerMask layerMask;

    void Start()
    {
        curTime = Time.time;
        myCollider = GetComponent<Collider>();
        layerMask = 1 << LayerMask.NameToLayer("Enemy");
        //transform.localScale = new Vector3(range, range, range);

        impulseSource = GetComponent<CinemachineImpulseSource>();
        GameManager.Instance.cameraController.ShakeCamera(impulseSource);
    }

    void Update()
    {
        // 지속 시간 후, 콜라이더 Off
        if(Time.time > curTime + lifeTime)
        {
            myCollider.enabled = false;     // TODO : 오브젝트 풀링 사용 시, 다시 켜야한다
        }
    }

    public void InitSettings(BigInteger value, float range)
    {
        this.value = value;
        this.range = range;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Utils.IsInLayerMask(other.gameObject.layer, layerMask))
        {
            if (other.gameObject.TryGetComponent(out ITakeDamageAble damageable) && !damageable.IsInvulnerable)
            {
                damageable.TakeDamage(Utils.CriticalCaculate(GameManager.Instance.player.StatHandler, value));
                //damageable.TakeDamage(value);
                damageable.TakeDamage(value);
            }
        }
    }
}
