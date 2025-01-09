using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public class WindStorm : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float tickTime = 0.1f;

    private float curTime;

    private BigInteger value;
    private float range;
    private int atkAcount = 15; //공격 횟수

    private Collider myCollider;
    private LayerMask layerMask;

    private Transform playerTransform;

    private WaitForSecondsRealtime coroutineTime;

    private Dictionary<int, Coroutine> enemyDic = new Dictionary<int, Coroutine>();

    public Vector3 OriginPos { get; set; }

    void Start()
    {
        curTime = Time.time;
        myCollider = GetComponent<Collider>();
        layerMask = 1 << LayerMask.NameToLayer("Enemy");
        //transform.localScale = new Vector3(range, range, range);
        playerTransform = GameManager.Instance.player.transform;
        coroutineTime = new WaitForSecondsRealtime(tickTime);
    }

    void Update()
    {
        // TODO : 적을 계속 추적해서 따라다녀야 함
        //UpdateEnemies();

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
            if (!enemyDic.ContainsKey(other.GetInstanceID()))
            {
                enemyDic.Add(other.GetInstanceID(), StartCoroutine(CoroutineTickDamage(other.gameObject)));
            }
            /*enemyDic.Add(other.GetInstanceID(),
                StartCoroutine(CoroutineTickDamage(other.gameObject)));*/
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Utils.IsInLayerMask(other.gameObject.layer, layerMask))
        {
            if (enemyDic.TryGetValue(other.GetInstanceID(), out Coroutine coroutine))
            {
                StopCoroutine(coroutine);
                enemyDic.Remove(other.GetInstanceID());
            }
        }
    }

    private IEnumerator CoroutineTickDamage(GameObject hitObj)
    {
        while (true)
        {
            if (hitObj.TryGetComponent(out ITakeDamageAble damageable) && !damageable.IsInvulnerable)
            {
                var audioSource = ObjectPoolManager.Instance.GetPool(Const.AUDIO_SOURCE_KEY, Const.AUDIO_SOURCE_POOL_KEY).GetObject();
                audioSource.SetActive(true);
                AudioSource audio = audioSource.GetComponent<AudioSource>();
                audio.Play();

                for (int i = 0; i < atkAcount; i++)
                {
                    damageable.TakeDamage(Utils.CriticalCaculate(GameManager.Instance.player.StatHandler, value));
                }
            }

            yield return coroutineTime;
        }
    }

    //private void UpdateEnemies()
    //{
    //    // 가장 가까운 Enemy 를 찾는 로직
    //    // Enemy 리스트를 받아와 거리 기반 탐지

    //    List<GameObject> targets = GameManager.Instance.enemies;    // Enemy 리스트
    //    GameObject closestTarget = null;
    //    float closestDistanceSqr = Mathf.Infinity;

    //    Vector3 playerPos = playerTransform.position;

    //    foreach (GameObject target in targets)
    //    {
    //        float distanceSqr = (playerPos - target.transform.position).sqrMagnitude;

    //        if (distanceSqr < closestDistanceSqr)
    //        {
    //            closestTarget = target;
    //            closestDistanceSqr = distanceSqr;
    //        }
    //    }

    //    // 탐색 범위를 넘어선 대상은 null 처리
    //    if (searchRange * searchRange < closestDistanceSqr)
    //        closestTarget = null;

    //    Vector3 targetPos = playerPos;

    //    if (closestTarget != null)
    //    {
    //        targetPos = closestTarget.transform.position;
    //    }
    //    else
    //    {
    //        if (GameManager.Instance.player.PlayerAnimationController.skeleton.ScaleX > 0)
    //        {
    //            targetPos += playerTransform.right * 5f;
    //        }
    //        else
    //        {
    //            targetPos -= playerTransform.right * 5f;
    //        }
    //    }
    //}
}
