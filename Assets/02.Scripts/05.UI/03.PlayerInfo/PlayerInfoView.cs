using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoView : MonoBehaviour, IUIBase
{
    [SerializeField] private GameObject infoPanel;

    [Header("Thumbnail")]
    [SerializeField] private Image thumbnail;

    [Header("Scroll")]
    [SerializeField] private RectTransform statusScroll;
    [SerializeField] private RectTransform upgradeScroll;

    [Header("Status")]
    [SerializeField] private GameObject statusBundle;
    [SerializeField] private TextMeshProUGUI powerText;
    private TextMeshProUGUI[] statusText;

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

        for (int i = 0; i < statusBundle.transform.childCount; ++i)
        {
            statusText[i] = statusBundle.transform.GetChild(i).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }
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
        costTexts[(int)Status.Hp].text = $"{Utils.FormatBigInteger(Utils.UpgradeCost(Status.Hp))}";
        upgradeLevelText[(int)Status.Hp].text = $"체력 Lv. {playerStatHandler.CurrentStat.MaxHealthLevel}";
        statusText[(int)StatusType.Hp].text = Utils.FormatBigInteger(playerStatHandler.CurrentStat.maxHealth);

        valueTexts[(int)Status.Hp].text = Utils.FormatBigInteger(playerStatHandler.CurrentStat.maxHealth);
        nextValueTexts[(int)Status.Hp].text = $"-> {Utils.FormatBigInteger(Utils.UpgradePlayerStatBigInteger(Status.Hp))}";
    }

    public void UpdateAtk()
    {
        costTexts[(int)Status.Atk].text = $" {Utils.FormatBigInteger(Utils.UpgradeCost(Status.Atk))}";
        upgradeLevelText[(int)Status.Atk].text = $"공격력 Lv. {playerStatHandler.CurrentStat.AtkLevel}";
        statusText[(int)StatusType.Atk].text = Utils.FormatBigInteger(playerStatHandler.CurrentStat.atk);

        valueTexts[(int)Status.Atk].text = Utils.FormatBigInteger(playerStatHandler.CurrentStat.atk);
        nextValueTexts[(int)Status.Atk].text = $"-> {Utils.FormatBigInteger(Utils.UpgradePlayerStatBigInteger(Status.Atk))}";
    }

    public void UpdateDef()
    {
        costTexts[(int)Status.Def].text = $"{Utils.FormatBigInteger(Utils.UpgradeCost(Status.Def))}";
        upgradeLevelText[(int)Status.Def].text = $"방어력 Lv. {playerStatHandler.CurrentStat.DefLevel}";
        statusText[(int)StatusType.Def].text = Utils.FormatBigInteger(playerStatHandler.CurrentStat.def);

        valueTexts[(int)Status.Def].text = Utils.FormatBigInteger(playerStatHandler.CurrentStat.def);
        nextValueTexts[(int)Status.Def].text = $"-> {Utils.FormatBigInteger(Utils.UpgradePlayerStatBigInteger(Status.Def))}";
    }

    public void UpdateReduceDmg()
    {
        costTexts[(int)Status.ReduceDmg].text = $"{Utils.FormatBigInteger(Utils.UpgradeCost(Status.ReduceDmg))}";
        upgradeLevelText[(int)Status.ReduceDmg].text = $"피해 감소 Lv. {playerStatHandler.CurrentStat.ReduceDamageLevel}";
        statusText[(int)StatusType.ReduceDamage].text = playerStatHandler.CurrentStat.reduceDamage.ToString() + "%";

        valueTexts[(int)Status.ReduceDmg].text = playerStatHandler.CurrentStat.reduceDamage.ToString();
        nextValueTexts[(int)Status.ReduceDmg].text = $"-> {Utils.UpgradePlayerStat(Status.ReduceDmg)}%";
    }

    public void UpdateCritChance()
    {
        costTexts[(int)Status.CritChance].text = $"{Utils.FormatBigInteger(Utils.UpgradeCost(Status.CritChance))}";
        upgradeLevelText[(int)Status.CritChance].text = $"치명타 확률 Lv. {playerStatHandler.CurrentStat.CriticalRateLevel}";
        statusText[(int)StatusType.CritChance].text = $"{playerStatHandler.CurrentStat.critChance}%";

        valueTexts[(int)Status.CritChance].text = $"{playerStatHandler.CurrentStat.critChance}%";
        nextValueTexts[(int)Status.CritChance].text = $"-> {Utils.UpgradePlayerStat(Status.CritChance)}%";
    }

    public void UpdateCritDmg()
    {
        costTexts[(int)Status.CritDmg].text = $"{Utils.FormatBigInteger(Utils.UpgradeCost(Status.CritDmg))}";
        upgradeLevelText[(int)Status.CritDmg].text = $"치명타 피해 Lv. {playerStatHandler.CurrentStat.CriticalDamageLevel}";
        statusText[(int)StatusType.CritDamage].text = $"{playerStatHandler.CurrentStat.critDamage}%";

        valueTexts[(int)Status.CritDmg].text = $"{playerStatHandler.CurrentStat.critDamage}%";
        nextValueTexts[(int)Status.CritDmg].text = $"-> {Utils.UpgradePlayerStat(Status.CritDmg)}%";
    }
    
    private void UpdateStatus()
    {
        powerText.text = Utils.FormatBigInteger(playerStatHandler.CurrentStat.totalDamage);
        statusText[(int)StatusType.Hp].text = Utils.FormatBigInteger(playerStatHandler.CurrentStat.maxHealth);
        statusText[(int)StatusType.Atk].text = Utils.FormatBigInteger(playerStatHandler.CurrentStat.atk);
        statusText[(int)StatusType.Def].text = Utils.FormatBigInteger(playerStatHandler.CurrentStat.def);
        statusText[(int)StatusType.MoveSpeed].text = $"{playerStatHandler.CurrentStat.moveSpeed + 100f}%";
        statusText[(int)StatusType.AtkSpeed].text = $"{playerStatHandler.CurrentStat.atkSpeed + 100f}%";
        statusText[(int)StatusType.ReduceDamage].text = playerStatHandler.CurrentStat.reduceDamage.ToString();
        statusText[(int)StatusType.CritChance].text = $"{playerStatHandler.CurrentStat.critChance}%";
        statusText[(int)StatusType.CritDamage].text = $"{playerStatHandler.CurrentStat.critDamage}%";
        statusText[(int)StatusType.CoolDown].text = $"{playerStatHandler.CurrentStat.coolDown}%";

        upgradeLevelText[(int)Status.Hp].text = $"체력 Lv. {playerStatHandler.CurrentStat.MaxHealthLevel}";
        upgradeLevelText[(int)Status.Atk].text = $"공격력 Lv. {playerStatHandler.CurrentStat.AtkLevel}";
        upgradeLevelText[(int)Status.Def].text = $"방어력 Lv. {playerStatHandler.CurrentStat.DefLevel}";
        upgradeLevelText[(int)Status.ReduceDmg].text = $"피해 감소 Lv. {playerStatHandler.CurrentStat.ReduceDamageLevel}";
        upgradeLevelText[(int)Status.CritChance].text = $"치명타 확률 Lv. {playerStatHandler.CurrentStat.CriticalRateLevel}";
        upgradeLevelText[(int)Status.CritDmg].text = $"치명타 피해 Lv. {playerStatHandler.CurrentStat.CriticalDamageLevel}";

        costTexts[(int)Status.Hp].text = $"{Utils.FormatBigInteger(Utils.UpgradeCost(Status.Hp))}";
        costTexts[(int)Status.Atk].text = $" {Utils.FormatBigInteger(Utils.UpgradeCost(Status.Atk))}";
        costTexts[(int)Status.Def].text = $"{Utils.FormatBigInteger(Utils.UpgradeCost(Status.Def))}";
        costTexts[(int)Status.ReduceDmg].text = $"{Utils.FormatBigInteger(Utils.UpgradeCost(Status.ReduceDmg))}";
        costTexts[(int)Status.CritChance].text = $"{Utils.FormatBigInteger(Utils.UpgradeCost(Status.CritChance))}";
        costTexts[(int)Status.CritDmg].text = $"{Utils.FormatBigInteger(Utils.UpgradeCost(Status.CritDmg))}";

        valueTexts[(int)Status.Hp].text = Utils.FormatBigInteger(playerStatHandler.CurrentStat.maxHealth);
        valueTexts[(int)Status.Atk].text = Utils.FormatBigInteger(playerStatHandler.CurrentStat.atk);
        valueTexts[(int)Status.Def].text = Utils.FormatBigInteger(playerStatHandler.CurrentStat.def);
        valueTexts[(int)Status.ReduceDmg].text = playerStatHandler.CurrentStat.reduceDamage.ToString();
        valueTexts[(int)Status.CritChance].text = $"{playerStatHandler.CurrentStat.critChance}%";
        valueTexts[(int)Status.CritDmg].text = $"{playerStatHandler.CurrentStat.critDamage}%";

        nextValueTexts[(int)Status.Hp].text = $"-> {Utils.FormatBigInteger(Utils.UpgradePlayerStatBigInteger(Status.Hp))}";
        nextValueTexts[(int)Status.Atk].text = $"-> {Utils.FormatBigInteger(Utils.UpgradePlayerStatBigInteger(Status.Atk))}";
        nextValueTexts[(int)Status.Def].text = $"-> {Utils.FormatBigInteger(Utils.UpgradePlayerStatBigInteger(Status.Def))}";
        nextValueTexts[(int)Status.ReduceDmg].text = $"-> {Utils.UpgradePlayerStat(Status.ReduceDmg)}%";
        nextValueTexts[(int)Status.CritChance].text = $"-> {Utils.UpgradePlayerStat(Status.CritChance)}%";
        nextValueTexts[(int)Status.CritDmg].text = $"-> {Utils.UpgradePlayerStat(Status.CritDmg)}%";
    }
}
