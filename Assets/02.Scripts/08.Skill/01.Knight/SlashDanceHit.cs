using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public class SlashDanceHit : MonoBehaviour
{
    [SerializeField] private float tickTime = 0.05f;
    private float lifeTime;

    private float curTime;

    private BigInteger value; // 데미지 계수
    private float time;
    private int atkAcount = 10; //공격 횟수

    private Collider myCollider;
    private LayerMask layerMask;

    private WaitForSecondsRealtime coroutineTime;

    private Dictionary<int, Coroutine> enemyDic = new Dictionary<int, Coroutine>();

    void Start()
    {
        lifeTime = time - 1f;
        curTime = Time.time;
        myCollider = GetComponent<Collider>();
        layerMask = 1 << LayerMask.NameToLayer("Enemy");
        coroutineTime = new WaitForSecondsRealtime(tickTime);
    }

    void Update()
    {
        // 지속 시간 후 삭제
        if (Time.time > curTime + lifeTime)
        {
            Destroy(gameObject);
        }
    }

    public void InitSettings(BigInteger value, float time)
    {
        this.value = value;
        this.time = time;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Utils.IsInLayerMask(other.gameObject.layer, layerMask))
        {
            enemyDic.Add(other.GetInstanceID(),
                StartCoroutine(CoroutineTickDamage(other.gameObject)));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Utils.IsInLayerMask(other.gameObject.layer, layerMask))
        {
            if (enemyDic.TryGetValue(other.GetInstanceID(), out Coroutine coroutine))
            {
                StopCoroutine(coroutine);
            }
        }
    }

    private IEnumerator CoroutineTickDamage(GameObject hitObj)
    {
        while (true)
        {
            if (hitObj.TryGetComponent(out ITakeDamageAble damageable) && !damageable.IsInvulnerable)
            {
                for(int i =  0; i < atkAcount; i++)
                {
                    damageable.TakeDamage(Utils.CriticalCaculate(GameManager.Instance.player.StatHandler, value));
                }   
            }

            yield return coroutineTime;
        }
    }
}
