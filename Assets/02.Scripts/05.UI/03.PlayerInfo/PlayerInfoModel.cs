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

    public void GoldCheck(Status buyType)
    {
        long result = GameManager.Instance.player.UserData.Gold - BigInteger.ToInt64(Utils.UpgradeCost(buyType));

        if ( result >= 0 )
        {
            GameManager.Instance.player.UserData.Gold = result;
        }
        else
        {
            GameManager.Instance.player.UserData.Gold = 0;
        }
    }

    public void HpLevelUp(int amount)
    {
        //�÷��̾ ������ �ִ� ��尡 ���׷��̵� �ڽ�Ʈ���� ������ true
        if (GameManager.Instance.player.UserData.Gold >= Utils.UpgradeCost(Status.Hp))
        {
            //�÷��̾� ��� - ���׷��̵� �ڽ�Ʈ ��� ����
            GoldCheck(Status.Hp);
            //�÷��̾� ������ => ���� ������ �� �������ͽ� Ÿ���� �����Ͽ� ���� ����
            GameManager.Instance.player.LevelUp(amount, Status.Hp);
            OnHpUpgrade?.Invoke(); //View�� �����͸� �����Ͽ� ����� ������
        }
    }

    public void AtkLevelUp(int amount)
    {
        if (GameManager.Instance.player.UserData.Gold >= Utils.UpgradeCost(Status.Atk))
        {
            GoldCheck(Status.Atk);
            GameManager.Instance.player.LevelUp(amount, Status.Atk);
            OnAtkUpgrade?.Invoke();
        }
    }

    public void DefLevelUp(int amount)
    {
        if (GameManager.Instance.player.UserData.Gold >= Utils.UpgradeCost(Status.Def))
        {
            GoldCheck(Status.Def);
            GameManager.Instance.player.LevelUp(amount, Status.Def);
            OnDefUpgrade?.Invoke();
        }
    }

    public void ReduceDmgLevelUp(int amount)
    {
        if (GameManager.Instance.player.UserData.Gold >= Utils.UpgradeCost(Status.ReduceDmg))
        {
            GoldCheck(Status.ReduceDmg);
            GameManager.Instance.player.LevelUp(amount, Status.ReduceDmg);
            OnReduceDmgUpgrade?.Invoke();
        }
    }

    public void CritChanceLevelUp(int amount)
    {
        if (GameManager.Instance.player.UserData.Gold >= Utils.UpgradeCost(Status.CritChance))
        {
            GoldCheck(Status.CritChance);
            GameManager.Instance.player.LevelUp(amount, Status.CritChance);
            OnCritChanceUpgrade?.Invoke();
        }
    }

    public void CritDmgLevelUp(int amount)
    {
        if (GameManager.Instance.player.UserData.Gold >= Utils.UpgradeCost(Status.CritDmg))
        {
            GoldCheck(Status.CritDmg);
            GameManager.Instance.player.LevelUp(amount, Status.CritDmg);
            OnCritDmgUpgrade?.Invoke();
        }
    }
}
