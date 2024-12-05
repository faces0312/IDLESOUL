using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public class SlashDanceHit : MonoBehaviour
{
    [SerializeField] private float tickTime = 0.05f;
    private float lifeTime;

    private float curTime;

    private BigInteger value;
    private float time;

    private Collider myCollider;
    private LayerMask layerMask;

    private Coroutine curCorutine;
    private WaitForSecondsRealtime coroutineTime;

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
            if (curCorutine != null)
                StopCoroutine(curCorutine);
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
            if (curCorutine == null)
                curCorutine = StartCoroutine(CoroutineTickDamage());
        }
    }

    private IEnumerator CoroutineTickDamage()
    {
        while (true)
        {
            // TODO : Enemy 피격 처리

            Debug.LogAssertion("Slash Damage!");
            yield return coroutineTime;
        }
    }
}
