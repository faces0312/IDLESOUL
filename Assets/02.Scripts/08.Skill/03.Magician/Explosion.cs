using ScottGarland;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private BigInteger value;
    private float range;

    private LayerMask layerMask;


    // Start is called before the first frame update
    void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("Enemy");
        transform.localScale = new Vector3(range, range, range);
    }

    // Update is called once per frame
    void Update()
    {
        // TODO : 폭발 구현
    }

    public void InitSettings(BigInteger value, float range)
    {
        this.value = value;
        this.range = range;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Utils.IsInLayerMask(collision.gameObject.layer, layerMask))
        {
            GameManager.Instance.enemies.Remove(collision.gameObject);  // 임시로 제거
            Destroy(collision.gameObject);
            Debug.LogAssertion("Enemy Destroy");
        }

        Destroy(gameObject);
    }
}
