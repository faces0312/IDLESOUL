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
        // TODO : �÷��̾� ���� �ҷ����� => �ӽ� �� �����
        playerStatHandler = TestManager.Instance.playerStatHandler;
    }

    public override void UpgradeSkill(int amount)
    {
        level += amount;

        // ���� ���� �ɷ�ġ ����
        playerStatHandler.UnEquipItem(passiveStat);

        // TODO : ���� ����
        passiveStat.atk = (int)value * level;
    }

    public override void UseSkill(StatHandler statHandler)
    {
        // TODO : ���ݷ��� ��÷� ������

        playerStatHandler.EquipItem(passiveStat);
    }
}
