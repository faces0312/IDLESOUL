using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherPassiveSkill : Skill
{
    Stat passiveStat;
    StatHandler playerStatHandler;

    public float PassiveValue { get; private set; }

    public ArcherPassiveSkill(int id) : base(id)
    {
        passiveStat = new Stat();
        //passiveStat.atk = (int)value * level;
        // TODO : 강화 공격용 수치
        PassiveValue = value * level;
        playerStatHandler = GameManager.Instance.player.StatHandler;
    }

    public override void UpgradeSkill(int amount)
    {
        level += amount;

        // 이전 레벨 능력치 해제
        playerStatHandler.UnEquipItem(passiveStat);

        // TODO : 배율 조정
        //passiveStat.atk = (int)value * level;
        // TODO : 강화 공격용 수치
        //PassiveValue = value * level;
    }

    public override void UseSkill(StatHandler statHandler)
    {
        // 공격력이 상시로 증가함
        //playerStatHandler.EquipItem(passiveStat);
    }
}
