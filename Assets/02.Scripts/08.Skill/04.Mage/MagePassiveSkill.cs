using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public class MagePassiveSkill : Skill
{
    Stat originPassiveStat;
    Stat passiveStat;
    StatHandler playerStatHandler;

    public float PassiveValue { get; private set; }

    public MagePassiveSkill(int id, Stat stat) : base(id, stat)
    {
        originPassiveStat = new Stat();
        originPassiveStat = stat * (value * level * 0.001f);
        passiveStat = originPassiveStat;
        // TODO : 강화 공격용 수치
        PassiveValue = value * level;
        playerStatHandler = GameManager.Instance.player.StatHandler;
    }

    public override void UpgradeSkill(int amount)
    {
        level += amount;

        // 이전 레벨 능력치 해제
        playerStatHandler.UnEquipItem(passiveStat);

        // 배율 조정
        passiveStat = originPassiveStat * (value * level * 0.03f);
        passiveStat.atk = BigInteger.Divide(BigInteger.Multiply(passiveStat.atk, 150), 100);
        // TODO : 강화 공격용 수치
        PassiveValue = value * level;
    }

    public override void UseSkill(StatHandler statHandler)
    {
        // 공격력이 상시로 증가함
        playerStatHandler.EquipItem(passiveStat);
    }

    public override void ReleaseSkill()
    {
        playerStatHandler.UnEquipItem(passiveStat);
    }
}
