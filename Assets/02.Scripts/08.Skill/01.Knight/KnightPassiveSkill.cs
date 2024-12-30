using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public class KnightPassiveSkill : Skill
{
    Stat originPassiveStat;
    Stat passiveStat;
    StatHandler playerStatHandler;

    public KnightPassiveSkill(int id, Stat stat) : base(id, stat)
    {
        originPassiveStat = new Stat();
        originPassiveStat = stat * (value * level * 0.001f);
        passiveStat = originPassiveStat;
        playerStatHandler = GameManager.Instance.player.StatHandler;
    }

    public override void UpgradeSkill(int amount)
    {
        level += amount;

        // 이전 레벨 능력치 해제
        playerStatHandler.UnEquipItem(passiveStat);

        // 배율 조정
        passiveStat = originPassiveStat * (value * level * 0.03f);
        passiveStat.atk = BigInteger.Divide(BigInteger.Multiply(passiveStat.def, 150), 100);
    }

    public override void UseSkill(StatHandler statHandler)
    {
        // 방어력이 상시로 증가함
        playerStatHandler.EquipItem(passiveStat);
    }

    public override void ReleaseSkill()
    {
        playerStatHandler.UnEquipItem(passiveStat);
    }
}
