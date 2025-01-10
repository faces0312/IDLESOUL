using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public class WindStorm : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float tickTime = 0.1f;
    [SerializeField] private float moveSpeed = 5f;

    private float curTime;

    private BigInteger value;
    private float searchRange;
    private int atkAcount = 15; //���� Ƚ��

    private Collider myCollider;
    private LayerMask layerMask;

    private WaitForSecondsRealtime coroutineTime;

    private Dictionary<int, Coroutine> enemyDic = new Dictionary<int, Coroutine>();

    private GameObject targetEnemy;

    public Vector3 OriginPos { get; set; }

    void Start()
    {
        curTime = Time.time;
        myCollider = GetComponent<Collider>();
        layerMask = 1 << LayerMask.NameToLayer("Enemy");
        coroutineTime = new WaitForSecondsRealtime(tickTime);
    }

    void Update()
    {
        if(targetEnemy == null)
            UpdateEnemies();

        if (targetEnemy != null)
            ChaseEnemy();

        // ���� �ð� ��, �ݶ��̴� Off
        if (Time.time > curTime + lifeTime)
        {
            myCollider.enabled = false;     // TODO : ������Ʈ Ǯ�� ��� ��, �ٽ� �Ѿ��Ѵ�

            Destroy(gameObject);
        }
    }

    public void InitSettings(BigInteger value, float range)
    {
        this.value = value;
        this.searchRange = range;
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
                //var audioSource = ObjectPoolManager.Instance.GetPool(Const.AUDIO_SOURCE_KEY, Const.AUDIO_SOURCE_POOL_KEY).GetObject();
                //audioSource.SetActive(true);
                //AudioSource audio = audioSource.GetComponent<AudioSource>();
                //audio.Play();

                for (int i = 0; i < atkAcount; i++)
                {
                    damageable.TakeDamage(Utils.CriticalCaculate(GameManager.Instance.player.StatHandler, value));
                }
            }

            yield return coroutineTime;
        }
    }

    private void UpdateEnemies()
    {
        // ���� ����� Enemy �� ã�� ����
        // Enemy ����Ʈ�� �޾ƿ� �Ÿ� ��� Ž��

        List<GameObject> targets = GameManager.Instance.enemies;    // Enemy ����Ʈ
        GameObject closestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;

        Vector3 thisPos = transform.position;

        foreach (GameObject target in targets)
        {
            float distanceSqr = (thisPos - target.transform.position).sqrMagnitude;

            if (distanceSqr < closestDistanceSqr)
            {
                closestTarget = target;
                closestDistanceSqr = distanceSqr;
            }
        }

        if (closestTarget != null)
        {
            targetEnemy = closestTarget;

            if (targetEnemy.TryGetComponent(out Enemy enemy))
            {
                enemy.OnDieEvent += DieEnemy;
            }
        }
    }
    
    private void ChaseEnemy()
    {
        Vector3 dir = (targetEnemy.transform.position - transform.position).normalized;
        dir.y = 0;

        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    private void DieEnemy()
    {
        targetEnemy = null;
    }
}
