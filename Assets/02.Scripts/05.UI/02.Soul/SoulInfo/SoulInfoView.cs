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

public class SoulInfoView : MonoBehaviour, IUIBase
{
    [SerializeField] private GameObject invenPanel;

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
        
    }

    public void ShowUI()
    {
        invenPanel.SetActive(true);

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
        Debug.LogAssertion("소울 인포 UI 업데이트");
        levelTexts[(int)LevelType.Soul].text = $"Lv. {soul.level}";
    }

    public void UpdateDefault()
    {
        Debug.LogAssertion("소울 인포 스킬1 업데이트");
        levelTexts[(int)LevelType.Default].text = $"Lv. {soul.Skills[(int)SkillType.Default].level}";
    }

    public void UpdateUltimate()
    {
        Debug.LogAssertion("소울 인포 스킬2 업데이트");
        levelTexts[(int)LevelType.Ultimate].text = $"Lv. {soul.Skills[(int)SkillType.Ultimate].level}";
    }

    public void UpdatePassive()
    {
        Debug.LogAssertion("소울 인포 패시브 업데이트");
        levelTexts[(int)LevelType.Passive].text = $"Lv. {soul.Skills[(int)SkillType.Passive].level}";
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
}
