using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ScottGarland;

public class PlayerInfoView : MonoBehaviour, IUIBase
{
    [SerializeField] private GameObject infoPanel;

    [Header("Thumbnail")]
    [SerializeField] private Image thumbnail;

    [Header("Scroll")]
    [SerializeField] private RectTransform statusScroll;
    [SerializeField] private RectTransform upgradeScroll;

    [Header("Status")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject statusBundle;
    [SerializeField] private GameObject statusBaseBundle;
    [SerializeField] private TextMeshProUGUI powerText;
    private TextMeshProUGUI[] statusText;
    private TextMeshProUGUI[] statusBaseText;

    [Header("Upgrade")]
    [SerializeField] private TextMeshProUGUI[] upgradeLevelText;

    [Header("Cost")]
    [SerializeField] private TextMeshProUGUI[] costTexts;

    [Header("Value&NextValue")]
    [SerializeField] private TextMeshProUGUI[] valueTexts;
    [SerializeField] private TextMeshProUGUI[] nextValueTexts;

    private StatHandler playerStatHandler;

    public void Initialize()
    {
        playerStatHandler = GameManager.Instance.player.StatHandler;

        statusText = new TextMeshProUGUI[statusBundle.transform.childCount];
        statusBaseText = new TextMeshProUGUI[statusBundle.transform.childCount];

        for (int i = 0; i < statusBundle.transform.childCount; ++i)
        {
            statusText[i] = statusBundle.transform.GetChild(i).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            statusBaseText[i] = statusBaseBundle.transform.GetChild(i).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }

        nameText.text = GameManager.Instance.player.UserData.NickName;
    }

    public void ShowUI()
    {
        infoPanel.SetActive(true);

        // 스크롤 뷰 포지션 초기화
        statusScroll.position = Vector3.zero;
        upgradeScroll.position = Vector3.zero;

        UpdateUI();
    }

    public void HideUI()
    {
        infoPanel.SetActive(false);
    }

    public void UpdateUI()
    {
        UpdateStatus();
    }

    public void UpdateHp()
    {
        //costTexts[(int)Status.Hp].text = $"{Utils.FormatBigInteger(Utils.UpgradeCost(Status.Hp))}";
        //upgradeLevelText[(int)Status.Hp].text = $"체력 Lv. {playerStatHandler.BaseStat.MaxHealthLevel}";
        //statusText[(int)StatusType.Hp].text = Utils.FormatBigInteger(playerStatHandler.BaseStat.maxHealth);
    }

    public void UpdateAtk()
    {
        //costTexts[(int)Status.Atk].text = $" {Utils.FormatBigInteger(Utils.UpgradeCost(Status.Atk))}";
        //upgradeLevelText[(int)Status.Atk].text = $"공격력 Lv. {playerStatHandler.BaseStat.AtkLevel}";
        //statusText[(int)StatusType.Atk].text = Utils.FormatBigInteger(playerStatHandler.BaseStat.atk);
    }

    public void UpdateDef()
    {
        //costTexts[(int)Status.Def].text = $"{Utils.FormatBigInteger(Utils.UpgradeCost(Status.Def))}";
        //upgradeLevelText[(int)Status.Def].text = $"방어력 Lv. {playerStatHandler.BaseStat.DefLevel}";
        //statusText[(int)StatusType.Def].text = Utils.FormatBigInteger(playerStatHandler.BaseStat.def);
    }

    public void UpdateReduceDmg()
    {
        //costTexts[(int)Status.ReduceDmg].text = $"{Utils.FormatBigInteger(Utils.UpgradeCost(Status.ReduceDmg))}";
        //upgradeLevelText[(int)Status.ReduceDmg].text = $"피해 감소 Lv. {playerStatHandler.BaseStat.ReduceDamageLevel}";
        //statusText[(int)StatusType.ReduceDamage].text = playerStatHandler.BaseStat.reduceDamage.ToString() + "%";
    }

    public void UpdateCritChance()
    {
        //costTexts[(int)Status.CritChance].text = $"{Utils.FormatBigInteger(Utils.UpgradeCost(Status.CritChance))}";
        //upgradeLevelText[(int)Status.CritChance].text = $"치명타 확률 Lv. {playerStatHandler.BaseStat.CriticalRateLevel}";
        //statusText[(int)StatusType.CritChance].text = $"{playerStatHandler.BaseStat.critChance}%";
    }

    public void UpdateCritDmg()
    {
        //costTexts[(int)Status.CritDmg].text = $"{Utils.FormatBigInteger(Utils.UpgradeCost(Status.CritDmg))}";
        //upgradeLevelText[(int)Status.CritDmg].text = $"치명타 피해 Lv. {playerStatHandler.BaseStat.CriticalDamageLevel}";
        //statusText[(int)StatusType.CritDamage].text = $"{playerStatHandler.BaseStat.critDamage}%";
    }
    
    private void UpdateStatus()
    {
        //powerText.text = Utils.FormatBigInteger(playerStatHandler.CurrentStat.totalDamage);
        // TODO : 스탯 핸들러의 TotalDamage가 가지고 있게 수정
        BigInteger power = (playerStatHandler.CurrentStat.maxHealth * 3) + ((playerStatHandler.CurrentStat.atk + playerStatHandler.CurrentStat.def) * 2)
           + ((playerStatHandler.CurrentStat.ReduceDamageLevel * playerStatHandler.CurrentStat.CriticalRateLevel * playerStatHandler.CurrentStat.CriticalDamageLevel) * 10);
        powerText.text = Utils.FormatBigInteger(power);

        statusText[(int)StatusType.Hp].text = $"{Utils.FormatBigInteger(playerStatHandler.CurrentStat.maxHealth)} ";
        statusText[(int)StatusType.Atk].text = Utils.FormatBigInteger(playerStatHandler.CurrentStat.atk);
        statusText[(int)StatusType.Def].text = Utils.FormatBigInteger(playerStatHandler.CurrentStat.def);
        statusText[(int)StatusType.MoveSpeed].text = $"{playerStatHandler.CurrentStat.moveSpeed + 100f}%";
        statusText[(int)StatusType.AtkSpeed].text = $"{playerStatHandler.CurrentStat.atkSpeed + 100f}%";
        statusText[(int)StatusType.ReduceDamage].text = playerStatHandler.CurrentStat.reduceDamage.ToString();
        statusText[(int)StatusType.CritChance].text = $"{playerStatHandler.CurrentStat.critChance}%";
        statusText[(int)StatusType.CritDamage].text = $"{playerStatHandler.CurrentStat.critDamage}%";
        statusText[(int)StatusType.CoolDown].text = $"{playerStatHandler.CurrentStat.coolDown}%";

        statusBaseText[(int)StatusType.Hp].text = $"{Utils.FormatBigInteger(playerStatHandler.BaseStat.maxHealth)} ";
        statusBaseText[(int)StatusType.Atk].text = Utils.FormatBigInteger(playerStatHandler.BaseStat.atk);
        statusBaseText[(int)StatusType.Def].text = Utils.FormatBigInteger(playerStatHandler.BaseStat.def);
        statusBaseText[(int)StatusType.MoveSpeed].text = $"{playerStatHandler.BaseStat.moveSpeed + 100f}%";
        statusBaseText[(int)StatusType.AtkSpeed].text = $"{playerStatHandler.BaseStat.atkSpeed + 100f}%";
        statusBaseText[(int)StatusType.ReduceDamage].text = playerStatHandler.BaseStat.reduceDamage.ToString();
        statusBaseText[(int)StatusType.CritChance].text = $"{playerStatHandler.BaseStat.critChance}%";
        statusBaseText[(int)StatusType.CritDamage].text = $"{playerStatHandler.BaseStat.critDamage}%";
        statusBaseText[(int)StatusType.CoolDown].text = $"{playerStatHandler.BaseStat.coolDown}%";

        upgradeLevelText[(int)Status.Hp].text = $"체력 Lv. {playerStatHandler.BaseStat.MaxHealthLevel}";
        upgradeLevelText[(int)Status.Atk].text = $"공격력 Lv. {playerStatHandler.BaseStat.AtkLevel}";
        upgradeLevelText[(int)Status.Def].text = $"방어력 Lv. {playerStatHandler.BaseStat.DefLevel}";
        upgradeLevelText[(int)Status.ReduceDmg].text = $"피해 감소 Lv. {playerStatHandler.BaseStat.ReduceDamageLevel}";
        upgradeLevelText[(int)Status.CritChance].text = $"치명타 확률 Lv. {playerStatHandler.BaseStat.CriticalRateLevel}";
        upgradeLevelText[(int)Status.CritDmg].text = $"치명타 피해 Lv. {playerStatHandler.BaseStat.CriticalDamageLevel}";

        costTexts[(int)Status.Hp].text = $"{Utils.FormatBigInteger(Utils.UpgradeCost(Status.Hp))}";
        costTexts[(int)Status.Atk].text = $" {Utils.FormatBigInteger(Utils.UpgradeCost(Status.Atk))}";
        costTexts[(int)Status.Def].text = $"{Utils.FormatBigInteger(Utils.UpgradeCost(Status.Def))}";
        costTexts[(int)Status.ReduceDmg].text = $"{Utils.FormatBigInteger(Utils.UpgradeCost(Status.ReduceDmg))}";
        costTexts[(int)Status.CritChance].text = $"{Utils.FormatBigInteger(Utils.UpgradeCost(Status.CritChance))}";
        costTexts[(int)Status.CritDmg].text = $"{Utils.FormatBigInteger(Utils.UpgradeCost(Status.CritDmg))}";

        valueTexts[(int)Status.Hp].text = Utils.FormatBigInteger(playerStatHandler.BaseStat.maxHealth);
        valueTexts[(int)Status.Atk].text = Utils.FormatBigInteger(playerStatHandler.BaseStat.atk);
        valueTexts[(int)Status.Def].text = Utils.FormatBigInteger(playerStatHandler.BaseStat.def);
        valueTexts[(int)Status.ReduceDmg].text = playerStatHandler.BaseStat.reduceDamage.ToString();
        valueTexts[(int)Status.CritChance].text = $"{playerStatHandler.BaseStat.critChance}%";
        valueTexts[(int)Status.CritDmg].text = $"{playerStatHandler.BaseStat.critDamage}%";

        nextValueTexts[(int)Status.Hp].text = $"-> {Utils.FormatBigInteger(playerStatHandler.BaseStat.maxHealth + Utils.UpgradePlayerStatBigInteger(Status.Hp,1))}";
        nextValueTexts[(int)Status.Atk].text = $"-> {Utils.FormatBigInteger(playerStatHandler.BaseStat.atk + Utils.UpgradePlayerStatBigInteger(Status.Atk,1))}";
        nextValueTexts[(int)Status.Def].text = $"-> {Utils.FormatBigInteger(playerStatHandler.BaseStat.def + Utils.UpgradePlayerStatBigInteger(Status.Def,1))}";
        nextValueTexts[(int)Status.ReduceDmg].text = $"-> {playerStatHandler.BaseStat.reduceDamage + Utils.UpgradePlayerStat(Status.ReduceDmg,1)}%";
        nextValueTexts[(int)Status.CritChance].text = $"-> {playerStatHandler.BaseStat.critChance +  Utils.UpgradePlayerStat(Status.CritChance,1)}%";
        nextValueTexts[(int)Status.CritDmg].text = $"-> {playerStatHandler.BaseStat.critDamage + Utils.UpgradePlayerStat(Status.CritDmg,1)}%";
    }
}
