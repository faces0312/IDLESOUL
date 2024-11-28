using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private BoxCollider2D myCollider;
    private float value;
    private float range;

    private void Awake()
    {
        myCollider = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        myCollider.size = new Vector2(range, range);
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
        // TODO : 단순 이름 비교가 아닌 LayerMask로 변경

        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            GameManager.Instance.enemies.Remove(collision.gameObject);  // 임시로 제거
            //Destroy(collision.gameObject);
            Debug.LogAssertion("Enemy Destroy");
        }

        //Destroy(gameObject);
    }
}
