using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public class SpinSword : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float tickTime = 0.5f;

    private float curTime;

    private BigInteger value;
    private float range;

    private Collider myCollider;
    private LayerMask layerMask;

    private Transform playerTransform;

    private Coroutine curCorutine;
    private WaitForSecondsRealtime coroutineTime;

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

            if(curCorutine != null)
                StopCoroutine(curCorutine);
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
            // TODO : Enemy 피격 처리

            //GameManager.Instance.enemies.Remove(collision.gameObject);  // 임시로 제거
            //Destroy(collision.gameObject);
            Debug.LogAssertion("Enemy Destroy");

            if(curCorutine == null)
                curCorutine = StartCoroutine(CoroutineTickDamage());

        }
    }

    private IEnumerator CoroutineTickDamage()
    {
        while (true)
        {
            // TODO : Enemy 피격 처리

            Debug.LogAssertion("Spin Damage!");
            yield return coroutineTime;
        }
    }
}
