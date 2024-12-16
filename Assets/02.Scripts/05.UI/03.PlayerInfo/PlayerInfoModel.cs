using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInfoModel : UIModel
{
    public event Action OnInfoChanged;
    public event Action OnHpUpgrade;
    public event Action OnAtkUpgrade;
    public event Action OnDefUpgrade;
    public event Action OnReduceDmgUpgrade;
    public event Action OnCritChanceUpgrade;
    public event Action OnCritDmgUpgrade;

    public void HpLevelUp(int amount)
    {
        GameManager.Instance.player.LevelUp(amount, Status.Hp);
        OnHpUpgrade?.Invoke();
    }

    public void AtkLevelUp(int amount)
    {
        GameManager.Instance.player.LevelUp(amount, Status.Atk);
        OnAtkUpgrade?.Invoke();
    }

    public void DefLevelUp(int amount)
    {
        GameManager.Instance.player.LevelUp(amount, Status.Def);
        OnDefUpgrade?.Invoke();
    }

    public void ReduceDmgLevelUp(int amount)
    {
        GameManager.Instance.player.LevelUp(amount, Status.ReduceDmg);
        OnReduceDmgUpgrade?.Invoke();
    }

    public void CritChanceLevelUp(int amount)
    {
        GameManager.Instance.player.LevelUp(amount, Status.CritChance);
        OnCritChanceUpgrade?.Invoke();
    }

    public void CritDmgLevelUp(int amount)
    {
        GameManager.Instance.player.LevelUp(amount, Status.CritDmg);
        OnCritDmgUpgrade?.Invoke();
    }
}
