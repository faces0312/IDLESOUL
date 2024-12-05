using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public class PlayerProjectile : BaseProjectile
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
            int Atk = BigInteger.ToInt32(GameManager.Instance._player.UserData.stat.atk);

            Debug.Log($"공격이 {collision.gameObject.name}에 충돌");
            DamageCaculate(collision.gameObject , Atk * value);

            base.ProjectileCollison(collision);
        }
    }
}