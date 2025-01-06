using ScottGarland;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicEnemy : BossEnemy
{
    public override void Initialize()
    {
        base.Initialize();
        healthBar.gameObject.SetActive(true);
    }

    public override void TakeDamage(BigInteger damage)
    {
        var dmgFont = ObjectPoolManager.Instance.GetPool(Const.DAMAGE_FONT_KEY, Const.DAMAGE_FONT_POOL_KEY).GetObject();
        dmgFont.SetActive(true);
        dmgFont.transform.position = new Vector3(transform.position.x,transform.position.y +1f, transform.position.z - 1f);
        dmgFont.transform.rotation = Quaternion.identity;
        dmgFont.GetComponent<DamageFont>().SetDamage(Owner.Enemy, damage);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
