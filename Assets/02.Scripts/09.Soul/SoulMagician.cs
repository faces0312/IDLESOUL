using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMagician : Soul
{
    private void Start()
    {
        InitSkills();
    }

    protected override void InitSkills()
    {
        // TODO : �����͸� �޾ƿ� �ֱ�

        //skills[(int)SkillType.Passive] = new MagicianPassiveSkill();
        skills[(int)SkillType.Default] = new MagicianDefaultSkill(
            0001, "�ͽ��÷���", "���� ���� ������ ������ ������ �������� ������.", 1, 1f, SkillType.Default);
        //skills[(int)SkillType.Ultimate] = new MagicianUltimateSkill();
    }
}
