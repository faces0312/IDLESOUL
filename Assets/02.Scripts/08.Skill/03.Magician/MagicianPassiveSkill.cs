using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianPassiveSkill : Skill
{
    Stat passiveStat;
    StatHandler playerStatHandler;

    public MagicianPassiveSkill(int id) : base(id)
    {
        passiveStat = new Stat();
        passiveStat.atk = (int)value * level;
        // TODO : 플레이어 스텟 불러오기 => 적용 확인 시 주석 삭제
        playerStatHandler = GameManager.Instance.player.StatHandler;
    }

    public override void UpgradeSkill(int amount)
    {
        level += amount;

        // TODO : amount 만큼 value 증가

        playerStatHandler.UnEquipItem(passiveStat);

        passiveStat.atk = (int)value * level;
    }

    public override void UseSkill(StatHandler statHandler)
    {
        // 공격력이 상시로 증가함
        playerStatHandler.EquipItem(passiveStat);
    }
}
