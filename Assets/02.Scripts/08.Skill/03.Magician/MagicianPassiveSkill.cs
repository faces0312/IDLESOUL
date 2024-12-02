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
        // TODO : 플레이어 스텟 불러오기 => 임시 값 사용중
        playerStatHandler = TestManager.Instance.playerStatHandler;
    }

    public override void UpgradeSkill(int amount)
    {
        level += amount;

        // 이전 레벨 능력치 해제
        playerStatHandler.UnEquipItem(passiveStat);

        // TODO : 배율 조정
        passiveStat.atk = (int)value * level;
    }

    public override void UseSkill(StatHandler statHandler)
    {
        // TODO : 공격력이 상시로 증가함

        playerStatHandler.EquipItem(passiveStat);
    }
}
