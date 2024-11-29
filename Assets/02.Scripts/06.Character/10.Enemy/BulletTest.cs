using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTest : MonoBehaviour
{
    public float attack;//총알 데미지
    public float knockbackPower;//총알 데미지
    [SerializeField] float speed;
    public Vector3 direction;//날라가는 방향
    [SerializeField] private LayerMask targetLayers;

    private void OnEnable()
    {
        Invoke("DestroyBullet", 5f);
    }

    private void FixedUpdate()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((targetLayers & 1 << collision.gameObject.layer) != 0)
        {
            ITakeDamageAble damageable = collision.gameObject.GetComponent<ITakeDamageAble>();
            //TODO :: 무적시간이 아닐때에도 조건에 추가해야됨
            if (damageable != null)
            {
                damageable.TakeDamage(attack);
                Vector3 directionKnockBack = collision.gameObject.transform.position - transform.position;
                damageable.TakeKnockBack(directionKnockBack, knockbackPower);
            }
            DestroyBullet();
        }
    }
}
