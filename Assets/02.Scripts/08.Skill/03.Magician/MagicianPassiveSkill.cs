using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianPassiveSkill : Skill
{
    public MagicianPassiveSkill(int id) : base(id)
    {
    }

    public override void UpgradeSkill(int amount)
    {
        level += amount;

        // TODO : amount ��ŭ value ����
    }

    public override void UseSkill(StatHandler statHandler)
    {
        // TODO : ���ݷ��� ��÷� ������
    }
}
