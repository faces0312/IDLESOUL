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
        //플레이어가 가지고 있는 골드가 업그레이드 코스트보다 높으면 true
        if (GameManager.Instance.player.UserData.Gold >= Utils.UpgradeCost(Status.Hp))
        {
            //플레이어 골드 - 업그레이드 코스트 비용 적용
            GameManager.Instance.player.UserData.Gold =
                Mathf.Max(0, GameManager.Instance.player.UserData.Gold - BigInteger.ToInt32(Utils.UpgradeCost(Status.Hp)));
            //플레이어 레벨업 => 스텟 증가량 및 스테이터스 타입을 전달하여 스탯 증가
            GameManager.Instance.player.LevelUp(amount, Status.Hp);
            OnHpUpgrade?.Invoke(); //View에 데이터를 전달하여 출력을 갱신함
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
