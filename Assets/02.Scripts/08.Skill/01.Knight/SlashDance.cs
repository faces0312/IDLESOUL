using System.Collections;
using System.Collections.Generic;
using ScottGarland;
using UnityEngine;

public class SlashDance : MonoBehaviour
{
    [SerializeField] private GameObject hitObj;
    [SerializeField] private float lifeTime;

    private float curTime;

    private BigInteger value;
    private float range;

    void Start()
    {
        curTime = Time.time;

        Invoke("CreateHitObj", 1f);
    }

    void Update()
    {
        // 지속 시간 후 삭제
        if (Time.time > curTime + lifeTime)
        {
            Destroy(gameObject);
        }
    }

    public void InitSettings(BigInteger value, float range)
    {
        this.value = value;
        this.range = range;
    }

    private void CreateHitObj()
    {
        var obj = Instantiate(hitObj, GameManager.Instance.player.transform.position, Quaternion.identity);
        if (obj.TryGetComponent(out SlashDanceHit hit))
        {
            hit.InitSettings(value, lifeTime);
        }
    }
}
