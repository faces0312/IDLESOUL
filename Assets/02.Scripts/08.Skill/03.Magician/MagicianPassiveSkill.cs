using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianPassiveSkill : Skill
{
    public MagicianPassiveSkill(int id, string name, string description, int applyCount, float value, SkillType type) : base(id, name, description, applyCount, value, type)
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
