using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinSword : MonoBehaviour
{
    private float value;
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
        
    }

    public void InitSettings(float value, float range)
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
    }
}
