using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoulInfoModel : UIModel
{
    public event Action OnInfoChanged;
    public event Action OnDefaultSkillChanged;
    public event Action OnUltimateSkillChanged;
    public event Action OnPassiveSkillChanged;

    public Soul soul;

    public void SoulLevelUp(int amount)
    {
        soul.LevelUP(amount);
        OnInfoChanged?.Invoke();
    }

    public void DefaultLevelUp(int amount)
    {
        soul.UpgradeSkill(SkillType.Default, amount);
        OnDefaultSkillChanged?.Invoke();
    }

    public void UltimateLevelUp(int amount)
    {
        soul.UpgradeSkill(SkillType.Ultimate, amount);
        OnUltimateSkillChanged?.Invoke();
    }

    public void PassiveLevelUp(int amount)
    {
        soul.UpgradeSkill(SkillType.Passive, amount);
        soul.ApplyPassiveSkill();
        OnPassiveSkillChanged?.Invoke();
    }
}
