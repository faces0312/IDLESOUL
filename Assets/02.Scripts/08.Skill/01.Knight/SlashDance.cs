using System.Collections;
using System.Collections.Generic;
using ScottGarland;
using UnityEngine;

public class SlashDance : MonoBehaviour
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
        // TODO : �÷��̾ ��� ����ٳ����
        transform.position = OriginPos + playerTransform.position;

        // ���� �ð� ��, �ݶ��̴� Off
        if (Time.time > curTime + lifeTime)
        {
            myCollider.enabled = false;     // TODO : ������Ʈ Ǯ�� ��� ��, �ٽ� �Ѿ��Ѵ�

            if (curCorutine != null)
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
            // TODO : Enemy �ǰ� ó��

            //GameManager.Instance.enemies.Remove(collision.gameObject);  // �ӽ÷� ����
            //Destroy(collision.gameObject);
            Debug.LogAssertion("Enemy Destroy");

            if (curCorutine == null)
                curCorutine = StartCoroutine(CoroutineTickDamage());

        }
    }

    private IEnumerator CoroutineTickDamage()
    {
        while (true)
        {
            // TODO : Enemy �ǰ� ó��

            Debug.LogAssertion("Spin Damage!");
            yield return coroutineTime;
        }
    }
}
