using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ScottGarland;

public class SoulInfoModel : UIModel
{
    public event Action OnInfoChanged;
    public event Action OnDefaultSkillChanged;
    public event Action OnUltimateSkillChanged;
    public event Action OnPassiveSkillChanged;

    public Soul soul;

    public void SoulLevelUp(int amount)
    {
        //플레이어가 가지고 있는 골드가 업그레이드 코스트보다 높으면 true
        if (GameManager.Instance.player.UserData.Gold >= Utils.SoulUpgradeCost(LevelType.Soul, soul))
        {
            //플레이어 골드 - 업그레이드 코스트 비용 적용
            GameManager.Instance.player.UserData.Gold =
                Math.Max(0, GameManager.Instance.player.UserData.Gold - BigInteger.ToInt32(Utils.SoulUpgradeCost(LevelType.Soul, soul)));

            soul.LevelUP(amount);
            OnInfoChanged?.Invoke();
        }
    }

    public void DefaultLevelUp(int amount)
    {

        //플레이어가 가지고 있는 골드가 업그레이드 코스트보다 높으면 true
        if (GameManager.Instance.player.UserData.Gold >= Utils.SoulUpgradeCost(LevelType.Default, soul))
        {
            //플레이어 골드 - 업그레이드 코스트 비용 적용
            GameManager.Instance.player.UserData.Gold =
                Mathf.Max(0, GameManager.Instance.player.UserData.Gold - BigInteger.ToInt32(Utils.SoulUpgradeCost(LevelType.Default, soul)));

            soul.UpgradeSkill(SkillType.Default, amount);
            OnDefaultSkillChanged?.Invoke();
        }
    }

    public void UltimateLevelUp(int amount)
    {
        //플레이어가 가지고 있는 골드가 업그레이드 코스트보다 높으면 true
        if (GameManager.Instance.player.UserData.Gold >= Utils.SoulUpgradeCost(LevelType.Ultimate, soul))
        {
            //플레이어 골드 - 업그레이드 코스트 비용 적용
            GameManager.Instance.player.UserData.Gold =
                Mathf.Max(0, GameManager.Instance.player.UserData.Gold - BigInteger.ToInt32(Utils.SoulUpgradeCost(LevelType.Ultimate, soul)));

            soul.UpgradeSkill(SkillType.Ultimate, amount);
            OnUltimateSkillChanged?.Invoke();
        }
    }

    public void PassiveLevelUp(int amount)
    {
        //플레이어가 가지고 있는 골드가 업그레이드 코스트보다 높으면 true
        if (GameManager.Instance.player.UserData.Gold >= Utils.SoulUpgradeCost(LevelType.Passive, soul))
        {
            //플레이어 골드 - 업그레이드 코스트 비용 적용
            GameManager.Instance.player.UserData.Gold =
                Mathf.Max(0, GameManager.Instance.player.UserData.Gold - BigInteger.ToInt32(Utils.SoulUpgradeCost(LevelType.Passive, soul)));

            soul.UpgradeSkill(SkillType.Passive, amount);
            soul.ApplyPassiveSkill();
            OnPassiveSkillChanged?.Invoke();
        }
    }
}
