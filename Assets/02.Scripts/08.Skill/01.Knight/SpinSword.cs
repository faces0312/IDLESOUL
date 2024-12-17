using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public class SpinSword : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float tickTime = 0.1f;

    private float curTime;

    private BigInteger value;
    private float range;

    private Collider myCollider;
    private LayerMask layerMask;

    private Transform playerTransform;

    private WaitForSecondsRealtime coroutineTime;

    private Dictionary<int, Coroutine> enemyDic = new Dictionary<int, Coroutine>();

    public Vector3 OriginPos { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        curTime = Time.time;
        myCollider = GetComponent<Collider>();
        layerMask = 1 << LayerMask.NameToLayer("Enemy");
        //transform.localScale = new Vector3(range, range, range);
        playerTransform = GameManager.Instance.player.transform;
        coroutineTime = new WaitForSecondsRealtime(tickTime);
    }

    // Update is called once per frame
    void Update()
    {
        // TODO : 플레이어를 계속 따라다녀야함
        transform.position = OriginPos + playerTransform.position;

        // 지속 시간 후, 콜라이더 Off
        if (Time.time > curTime + lifeTime)
        {
            myCollider.enabled = false;     // TODO : 오브젝트 풀링 사용 시, 다시 켜야한다

            Destroy(gameObject);
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
            // TODO : Enemy 피격 처리
            ITakeDamageAble damageable = hitObj.GetComponent<ITakeDamageAble>();
            //TODO :: 무적시간이 아닐때에도 조건에 추가해야됨
            if (damageable != null)
            {
                damageable.TakeDamage(3);
            }

            yield return coroutineTime;
        }
    }
}
