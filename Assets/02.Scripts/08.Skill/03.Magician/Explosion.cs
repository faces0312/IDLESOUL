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
    private LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        curTime = Time.time;
        myCollider = GetComponent<Collider>();
        layerMask = 1 << LayerMask.NameToLayer("Enemy");
        //transform.localScale = new Vector3(range, range, range);
    }

    // Update is called once per frame
    void Update()
    {
        // ���� �ð� ��, �ݶ��̴� Off
        if(Time.time > curTime + lifeTime)
        {
            myCollider.enabled = false;     // TODO : ������Ʈ Ǯ�� ��� ��, �ٽ� �Ѿ��Ѵ�
            // �ȸ���
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
        }
    }
}
