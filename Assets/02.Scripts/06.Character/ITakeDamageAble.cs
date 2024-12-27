using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakeDamageAble
{
    bool IsInvulnerable { get; set; }

    void TakeDamage(float damage);
    void TakeKnockBack(Vector3 direction, float force);
}
