using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public interface ITakeDamageAble
{
    bool IsInvulnerable { get; set; }

    void TakeDamage(BigInteger damage);
    void TakeKnockBack(Vector3 direction, float force);
}
