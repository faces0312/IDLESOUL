using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInfoModel : UIModel
{
    public event Action OnInfoChanged;

    public void HpLevelUp(int amount)
    {
        OnInfoChanged?.Invoke();
    }

    public void AtkLevelUp(int amount)
    {
        OnInfoChanged?.Invoke();
    }

    public void DefLevelUp(int amount)
    {
        OnInfoChanged?.Invoke();
    }

    public void ReduceDmgLevelUp(int amount)
    {
        OnInfoChanged?.Invoke();
    }

    public void CritChanceLevelUp(int amount)
    {
        OnInfoChanged?.Invoke();
    }

    public void CritDmgLevelUp(int amount)
    {
        OnInfoChanged?.Invoke();
    }
}
