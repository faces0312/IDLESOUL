using ScottGarland;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum LevelType
{
    Default,
    Ultimate,
    Passive,
    Soul
}

public enum StatusType
{
    Hp,
    Atk,
    Def,
    MoveSpeed,
    AtkSpeed,
    ReduceDamage,
    CritChance,
    CritDamage,
    CoolDown
}

public class SoulInfoView : MonoBehaviour, IUIBase
{
    [SerializeField] private GameObject invenPanel;

    [Header("Thumbnail")]
    [SerializeField] private Image thumbnail;

    [Header("Scroll")]
    [SerializeField] private RectTransform scrollTransform;

    [Header("Status")]
    [SerializeField] private GameObject statusBundle;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI powerText;
    private TextMeshProUGUI[] statusText;

    [Header("Skills")]
    [SerializeField] private GameObject defaultSkill;
    [SerializeField] private GameObject ultimateSkill;
    [SerializeField] private GameObject passiveSkill;

    [Header("Level")]
    [SerializeField] private TextMeshProUGUI[] levelTexts;

    [Header("Icon")]
    [SerializeField] private Image[] icons;

    [Header("Name")]
    [SerializeField] private TextMeshProUGUI[] skillNameTexts;

    [Header("Description")]
    [SerializeField] private TextMeshProUGUI[] skillDescriptionTexts;

    [Header("Cost")]
    [SerializeField] private TextMeshProUGUI[] costTexts;

    public Soul soul;

    public void Initialize()
    {
        statusText = new TextMeshProUGUI[statusBundle.transform.childCount];

        for (int i = 0; i < statusBundle.transform.childCount; ++i)
        {
            statusText[i] = statusBundle.transform.GetChild(i).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }
    }

    public void ShowUI()
    {
        invenPanel.SetActive(true);

        // 스크롤 뷰 포지션 초기화
        scrollTransform.position = Vector3.zero;

        ConnectSoul();

        UpdateUI();
        UpdateDefault();
        UpdateUltimate();
        UpdatePassive();
    }

    public void HideUI()
    {
        invenPanel.SetActive(false);
    }

    public void UpdateUI()
    {
        levelTexts[(int)LevelType.Soul].text = $"Lv. {soul.level}";
        costTexts[(int)LevelType.Soul].text = $"{Utils.FormatBigInteger(Utils.SoulUpgradeCost(LevelType.Soul, soul))}";

        UpdateStatus();
    }

    public void UpdateDefault()
    {
        levelTexts[(int)LevelType.Default].text = $"Lv. {soul.Skills[(int)SkillType.Default].level}";
        costTexts[(int)LevelType.Default].text = $"{Utils.FormatBigInteger(Utils.SoulUpgradeCost(LevelType.Default, soul))}";

        UpdateStatus();
    }

    public void UpdateUltimate()
    {
        levelTexts[(int)LevelType.Ultimate].text = $"Lv. {soul.Skills[(int)SkillType.Ultimate].level}";
        costTexts[(int)LevelType.Ultimate].text = $" {Utils.FormatBigInteger(Utils.SoulUpgradeCost(LevelType.Ultimate, soul))}";

        UpdateStatus();
    }

    public void UpdatePassive()
    {
        levelTexts[(int)LevelType.Passive].text = $"Lv. {soul.Skills[(int)SkillType.Passive].level}";
        costTexts[(int)LevelType.Passive].text = $"{Utils.FormatBigInteger(Utils.SoulUpgradeCost(LevelType.Passive, soul))}";

        UpdateStatus();
    }

    private void ConnectSoul()
    {
        SoulInfoController controller = UIManager.Instance.GetController("SoulInfo") as SoulInfoController;

        if(controller.SoulInfoModel.soul != soul)
        {
            soul = controller.SoulInfoModel.soul;

            InitUI();
        }
    }

    private void InitUI()
    {
        thumbnail.sprite = soul.icon;

        icons[(int)LevelType.Default].sprite = soul.Skills[(int)SkillType.Default].SkillSpr;
        icons[(int)LevelType.Ultimate].sprite = soul.Skills[(int)SkillType.Ultimate].SkillSpr;
        icons[(int)LevelType.Passive].sprite = soul.Skills[(int)SkillType.Passive].SkillSpr;

        skillNameTexts[(int)LevelType.Default].text = $"{soul.Skills[(int)SkillType.Default].skillName}";
        skillNameTexts[(int)LevelType.Ultimate].text = $"{soul.Skills[(int)SkillType.Ultimate].skillName}";
        skillNameTexts[(int)LevelType.Passive].text = $"{soul.Skills[(int)SkillType.Passive].skillName}";

        skillDescriptionTexts[(int)LevelType.Default].text = $"{soul.Skills[(int)SkillType.Default].description}";
        skillDescriptionTexts[(int)LevelType.Ultimate].text = $"{soul.Skills[(int)SkillType.Ultimate].description}";
        skillDescriptionTexts[(int)LevelType.Passive].text = $"{soul.Skills[(int)SkillType.Passive].description}";
    }

    private void UpdateStatus()
    {
        levelText.text = $"Lv. {soul.level}";
        //powerText.text = Utils.FormatBigInteger(soul.statHandler.CurrentStat.totalDamage);
        // TODO : 스탯 핸들러의 TotalDamage가 가지고 있게 수정
        BigInteger power = (soul.statHandler.CurrentStat.maxHealth * 15) + ((soul.statHandler.CurrentStat.atk + soul.statHandler.CurrentStat.def) * 9)
            + ((soul.Skills[(int)SkillType.Passive].level * soul.Skills[(int)SkillType.Default].level * soul.Skills[(int)SkillType.Ultimate].level) * 30);
        powerText.text = Utils.FormatBigInteger(power);
        statusText[(int)StatusType.Hp].text = Utils.FormatBigInteger(soul.statHandler.CurrentStat.maxHealth);
        statusText[(int)StatusType.Atk].text = Utils.FormatBigInteger(soul.statHandler.CurrentStat.atk);
        statusText[(int)StatusType.Def].text = Utils.FormatBigInteger(soul.statHandler.CurrentStat.def);
        statusText[(int)StatusType.MoveSpeed].text = $"{soul.statHandler.CurrentStat.moveSpeed + 100f}%";
        statusText[(int)StatusType.AtkSpeed].text = $"{soul.statHandler.CurrentStat.atkSpeed + 100f}%";
        statusText[(int)StatusType.ReduceDamage].text = soul.statHandler.CurrentStat.reduceDamage.ToString();
        statusText[(int)StatusType.CritChance].text = $"{soul.statHandler.CurrentStat.critChance}%";
        statusText[(int)StatusType.CritDamage].text = $"{soul.statHandler.CurrentStat.critDamage}%";
        statusText[(int)StatusType.CoolDown].text = $"{soul.statHandler.CurrentStat.coolDown}%";

        costTexts[(int)LevelType.Default].text = $"{Utils.FormatBigInteger(Utils.SoulUpgradeCost(LevelType.Default, soul))}";
        costTexts[(int)LevelType.Ultimate].text = $"{Utils.FormatBigInteger(Utils.SoulUpgradeCost(LevelType.Ultimate, soul))}";
        costTexts[(int)LevelType.Passive].text = $"{Utils.FormatBigInteger(Utils.SoulUpgradeCost(LevelType.Passive, soul))}";
        costTexts[(int)LevelType.Soul].text = $"{Utils.FormatBigInteger(Utils.SoulUpgradeCost(LevelType.Soul, soul))}";
    }
}
