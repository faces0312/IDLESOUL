using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class EnemyProjectile : BaseProjectile
{
    [SerializeField] private float value = 0.1f; // 해당 투사체의 계수

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (TargetLayer == ((1 << collision.gameObject.layer) | TargetLayer))
        {


            Debug.Log($"공격이 {collision.gameObject.name}에 충돌");
            DamageCaculate(collision.gameObject, value);

            base.ProjectileCollison(collision);
        }
    }
}