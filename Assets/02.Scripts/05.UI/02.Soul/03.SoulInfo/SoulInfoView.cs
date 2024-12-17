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
        //Debug.LogAssertion("소울 인포 UI 업데이트");
        levelTexts[(int)LevelType.Soul].text = $"Lv. {soul.level}";

        // TODO : 소울 스텟도 업데이트 되어야함
        UpdateStatus();
    }

    public void UpdateDefault()
    {
        //Debug.LogAssertion("소울 인포 스킬1 업데이트");
        levelTexts[(int)LevelType.Default].text = $"Lv. {soul.Skills[(int)SkillType.Default].level}";
    }

    public void UpdateUltimate()
    {
        //Debug.LogAssertion("소울 인포 스킬2 업데이트");
        levelTexts[(int)LevelType.Ultimate].text = $"Lv. {soul.Skills[(int)SkillType.Ultimate].level}";
    }

    public void UpdatePassive()
    {
        //Debug.LogAssertion("소울 인포 패시브 업데이트");
        levelTexts[(int)LevelType.Passive].text = $"Lv. {soul.Skills[(int)SkillType.Passive].level}";

        // TODO : 소울 스텟도 업데이트 되어야함
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
        // 소울 썸네일 삽입
        thumbnail.sprite = soul.icon;

        // TODO : 스킬 sprite 삽입
        // icons[(int)LevelType.Default].sprite = 
        // icons[(int)LevelType.Ultimate].sprite = 
        // icons[(int)LevelType.Passive].sprite = 

        skillNameTexts[(int)LevelType.Default].text = $"Lv. {soul.Skills[(int)SkillType.Default].skillName}";
        skillNameTexts[(int)LevelType.Ultimate].text = $"Lv. {soul.Skills[(int)SkillType.Ultimate].skillName}";
        skillNameTexts[(int)LevelType.Passive].text = $"Lv. {soul.Skills[(int)SkillType.Passive].skillName}";

        skillDescriptionTexts[(int)LevelType.Default].text = $"Lv. {soul.Skills[(int)SkillType.Default].description}";
        skillDescriptionTexts[(int)LevelType.Ultimate].text = $"Lv. {soul.Skills[(int)SkillType.Ultimate].description}";
        skillDescriptionTexts[(int)LevelType.Passive].text = $"Lv. {soul.Skills[(int)SkillType.Passive].description}";
    }

    private void UpdateStatus()
    {
        levelText.text = $"Lv. {soul.level}";
        powerText.text = Utils.FormatBigInteger(soul.statHandler.CurrentStat.totalDamage);
        statusText[(int)StatusType.Hp].text = Utils.FormatBigInteger(soul.statHandler.CurrentStat.maxHealth);
        statusText[(int)StatusType.Atk].text = Utils.FormatBigInteger(soul.statHandler.CurrentStat.atk);
        statusText[(int)StatusType.Def].text = Utils.FormatBigInteger(soul.statHandler.CurrentStat.def);
        statusText[(int)StatusType.MoveSpeed].text = $"{soul.statHandler.CurrentStat.moveSpeed + 100f}%";
        statusText[(int)StatusType.AtkSpeed].text = $"{soul.statHandler.CurrentStat.atkSpeed + 100f}%";
        statusText[(int)StatusType.ReduceDamage].text = soul.statHandler.CurrentStat.reduceDamage.ToString();
        statusText[(int)StatusType.CritChance].text = $"{soul.statHandler.CurrentStat.critChance}%";
        statusText[(int)StatusType.CritDamage].text = $"{soul.statHandler.CurrentStat.critDamage}%";
        statusText[(int)StatusType.CoolDown].text = $"{soul.statHandler.CurrentStat.coolDown}%";
    }
}
