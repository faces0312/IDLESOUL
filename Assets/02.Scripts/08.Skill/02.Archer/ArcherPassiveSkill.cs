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
        // TODO : ��ȭ ���ݿ� ��ġ
        PassiveValue = value * level;
        playerStatHandler = GameManager.Instance.player.StatHandler;
    }

    public override void UpgradeSkill(int amount)
    {
        level += amount;

        // ���� ���� �ɷ�ġ ����
        playerStatHandler.UnEquipItem(passiveStat);

        // TODO : ���� ����
        //passiveStat.atk = (int)value * level;
        // TODO : ��ȭ ���ݿ� ��ġ
        //PassiveValue = value * level;
    }

    public override void UseSkill(StatHandler statHandler)
    {
        // ���ݷ��� ��÷� ������
        //playerStatHandler.EquipItem(passiveStat);
    }
}
