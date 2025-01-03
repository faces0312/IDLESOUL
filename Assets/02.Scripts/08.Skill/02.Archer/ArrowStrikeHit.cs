using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public class ArrowStrikeHit : MonoBehaviour
{
    [SerializeField] private float tickTime = 0.05f;
    private float lifeTime;

    private float curTime;

    private BigInteger value;
    private float time;
    private int atkAccount = 10;

    private Collider myCollider;
    private LayerMask layerMask;

    private WaitForSecondsRealtime coroutineTime;

    private Dictionary<int, Coroutine> enemyDic = new Dictionary<int, Coroutine>();

    void Start()
    {
        lifeTime = time - 2.6f;
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
                for (int i = 0; i < atkAccount; i++)
                {
                    damageable.TakeDamage(Utils.CriticalCaculate(GameManager.Instance.player.StatHandler, value));
                }
            }

            yield return coroutineTime;
        }
    }
}
