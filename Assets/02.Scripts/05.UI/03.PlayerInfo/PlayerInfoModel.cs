using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ScottGarland;

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
        if (GameManager.Instance.player.UserData.Gold >= Utils.UpgradeCost(Status.Hp))
        {
            GameManager.Instance.player.UserData.Gold = 
                Mathf.Max(0, GameManager.Instance.player.UserData.Gold - BigInteger.ToInt32(Utils.UpgradeCost(Status.Hp)));
            GameManager.Instance.player.LevelUp(amount, Status.Hp);
            OnHpUpgrade?.Invoke();
        }
    }

    public void AtkLevelUp(int amount)
    {
        if (GameManager.Instance.player.UserData.Gold >= Utils.UpgradeCost(Status.Atk))
        {
            GameManager.Instance.player.UserData.Gold = 
                Mathf.Max(0, GameManager.Instance.player.UserData.Gold - BigInteger.ToInt32(Utils.UpgradeCost(Status.Atk)));
            GameManager.Instance.player.LevelUp(amount, Status.Atk);
            OnAtkUpgrade?.Invoke();
        }
    }

    public void DefLevelUp(int amount)
    {
        if (GameManager.Instance.player.UserData.Gold >= Utils.UpgradeCost(Status.Def))
        {
            GameManager.Instance.player.UserData.Gold = 
                Mathf.Max(0, GameManager.Instance.player.UserData.Gold - BigInteger.ToInt32(Utils.UpgradeCost(Status.Def)));
            GameManager.Instance.player.LevelUp(amount, Status.Def);
            OnDefUpgrade?.Invoke();
        }
    }

    public void ReduceDmgLevelUp(int amount)
    {
        if (GameManager.Instance.player.UserData.Gold >= Utils.UpgradeCost(Status.ReduceDmg))
        {
            GameManager.Instance.player.UserData.Gold = 
                Mathf.Max(0, GameManager.Instance.player.UserData.Gold - BigInteger.ToInt32(Utils.UpgradeCost(Status.ReduceDmg)));
            GameManager.Instance.player.LevelUp(amount, Status.ReduceDmg);
            OnReduceDmgUpgrade?.Invoke();
        }
    }

    public void CritChanceLevelUp(int amount)
    {
        if (GameManager.Instance.player.UserData.Gold >= Utils.UpgradeCost(Status.CritChance))
        {
            GameManager.Instance.player.UserData.Gold = 
                Mathf.Max(0, GameManager.Instance.player.UserData.Gold - BigInteger.ToInt32(Utils.UpgradeCost(Status.CritChance)));
            GameManager.Instance.player.LevelUp(amount, Status.CritChance);
            OnCritChanceUpgrade?.Invoke();
        }
    }

    public void CritDmgLevelUp(int amount)
    {
        if (GameManager.Instance.player.UserData.Gold >= Utils.UpgradeCost(Status.CritDmg))
        {
            GameManager.Instance.player.UserData.Gold = 
                Mathf.Max(0, GameManager.Instance.player.UserData.Gold - BigInteger.ToInt32(Utils.UpgradeCost(Status.CritDmg)));
            GameManager.Instance.player.LevelUp(amount, Status.CritDmg);
            OnCritDmgUpgrade?.Invoke();
        }
    }
}
