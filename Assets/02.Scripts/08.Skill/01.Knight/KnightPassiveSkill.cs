using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightPassiveSkill : Skill
{
    Stat passiveStat;
    StatHandler playerStatHandler;

    public KnightPassiveSkill(int id) : base(id)
    {
        passiveStat = new Stat();
        passiveStat.def = (int)value * level;
        playerStatHandler = GameManager.Instance.player.StatHandler;
    }

    public override void UpgradeSkill(int amount)
    {
        level += amount;

        // 이전 레벨 능력치 해제
        playerStatHandler.UnEquipItem(passiveStat);

        // TODO : 배율 조정
        passiveStat.def = (int)value * level;
    }

    public override void UseSkill(StatHandler statHandler)
    {
        // 방어력이 상시로 증가함
        playerStatHandler.EquipItem(passiveStat);
    }
}
