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
        // TODO : �÷��̾� ���� �ҷ����� => ���� Ȯ�� �� �ּ� ����
        playerStatHandler = GameManager.Instance.player.StatHandler;
    }

    public override void UpgradeSkill(int amount)
    {
        level += amount;

        // TODO : amount ��ŭ value ����

        playerStatHandler.UnEquipItem(passiveStat);

        passiveStat.atk = (int)value * level;
    }

    public override void UseSkill(StatHandler statHandler)
    {
        // ���ݷ��� ��÷� ������
        playerStatHandler.EquipItem(passiveStat);
    }
}
