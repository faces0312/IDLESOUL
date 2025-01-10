using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public class MultipleStorm : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3f;

    private float curTime;

    private float range;
    private BigInteger value;

    public Vector3 OriginPos { get; set; }

    void Start()
    {
        curTime = Time.time;
    }

    void Update()
    {
        // ���� �ð� ��, �ݶ��̴� Off
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

    public float InitSettings(out BigInteger value)
    {
        value = this.value;

        return this.range;
    }
}
