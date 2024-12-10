using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum LevelType
{
    Soul,
    Default,
    Ultimate,
    Passive
}

public class SoulInfoView : MonoBehaviour, IUIBase
{
    [SerializeField] private GameObject invenPanel;

    [Header("Skills")]
    [SerializeField] private GameObject defaultSkill;
    [SerializeField] private GameObject ultimateSkill;
    [SerializeField] private GameObject passiveSkill;

    [Header("Level")]
    [SerializeField] private TextMeshProUGUI[] levelText;

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
        levelText[(int)LevelType.Soul].text = $"Lv. {soul.level}";
    }

    public void UpdateDefault()
    {
        Debug.LogAssertion("소울 인포 스킬1 업데이트");
        levelText[(int)LevelType.Default].text = $"Lv. {soul.Skills[(int)SkillType.Default].level}";
    }

    public void UpdateUltimate()
    {
        Debug.LogAssertion("소울 인포 스킬2 업데이트");
        levelText[(int)LevelType.Ultimate].text = $"Lv. {soul.Skills[(int)SkillType.Ultimate].level}";
    }

    public void UpdatePassive()
    {
        Debug.LogAssertion("소울 인포 패시브 업데이트");
        levelText[(int)LevelType.Passive].text = $"Lv. {soul.Skills[(int)SkillType.Passive].level}";
    }

    private void ConnectSoul()
    {
        SoulInfoController controller = UIManager.Instance.GetController("SoulInfo") as SoulInfoController;
        soul = controller.SoulInfoModel.soul;
    }
}
