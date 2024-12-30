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
    [SerializeField] private GameObject statusBundle;
    [SerializeField] private TextMeshProUGUI powerText;
    private TextMeshProUGUI[] statusText;

    [Header("Upgrade")]
    [SerializeField] private TextMeshProUGUI[] upgradeLevelText;

    [Header("Level")]
    [SerializeField] private TextMeshProUGUI[] levelTexts;

    [Header("Cost")]
    [SerializeField] private TextMeshProUGUI[] costTexts;

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
    }

    public void UpdateAtk()
    {
        costTexts[(int)Status.Atk].text = $" {Utils.FormatBigInteger(Utils.UpgradeCost(Status.Atk))}";
        upgradeLevelText[(int)Status.Atk].text = $"공격력 Lv. {playerStatHandler.CurrentStat.AtkLevel}";
        statusText[(int)StatusType.Atk].text = Utils.FormatBigInteger(playerStatHandler.CurrentStat.atk);
    }

    public void UpdateDef()
    {
        costTexts[(int)Status.Def].text = $"{Utils.FormatBigInteger(Utils.UpgradeCost(Status.Def))}";
        upgradeLevelText[(int)Status.Def].text = $"방어력 Lv. {playerStatHandler.CurrentStat.DefLevel}";
        statusText[(int)StatusType.Def].text = Utils.FormatBigInteger(playerStatHandler.CurrentStat.def);
    }

    public void UpdateReduceDmg()
    {
        costTexts[(int)Status.ReduceDmg].text = $"{Utils.FormatBigInteger(Utils.UpgradeCost(Status.ReduceDmg))}";
        upgradeLevelText[(int)Status.ReduceDmg].text = $"피해 감소 Lv. {playerStatHandler.CurrentStat.ReduceDamageLevel}";
        statusText[(int)StatusType.ReduceDamage].text = playerStatHandler.CurrentStat.reduceDamage.ToString() + "%";
    }

    public void UpdateCritChance()
    {
        costTexts[(int)Status.CritChance].text = $"{Utils.FormatBigInteger(Utils.UpgradeCost(Status.CritChance))}";
        upgradeLevelText[(int)Status.CritChance].text = $"치명타 확률 Lv. {playerStatHandler.CurrentStat.CriticalRateLevel}";
        statusText[(int)StatusType.CritChance].text = $"{playerStatHandler.CurrentStat.critChance}%";
    }

    public void UpdateCritDmg()
    {
        costTexts[(int)Status.CritDmg].text = $"{Utils.FormatBigInteger(Utils.UpgradeCost(Status.CritDmg))}";
        upgradeLevelText[(int)Status.CritDmg].text = $"치명타 피해 Lv. {playerStatHandler.CurrentStat.CriticalDamageLevel}";
        statusText[(int)StatusType.CritDamage].text = $"{playerStatHandler.CurrentStat.critDamage}%";
    }
    
    private void UpdateStatus()
    {
        //powerText.text = Utils.FormatBigInteger(playerStatHandler.CurrentStat.totalDamage);
        // TODO : 스탯 핸들러의 TotalDamage가 가지고 있게 수정
        BigInteger power = (playerStatHandler.CurrentStat.maxHealth * 3) + ((playerStatHandler.CurrentStat.atk + playerStatHandler.CurrentStat.def) * 2)
           + ((playerStatHandler.CurrentStat.ReduceDamageLevel * playerStatHandler.CurrentStat.CriticalRateLevel * playerStatHandler.CurrentStat.CriticalDamageLevel) * 10);
        powerText.text = Utils.FormatBigInteger(power);
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
    }
}
