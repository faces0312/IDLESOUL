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
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI powerText;
    private TextMeshProUGUI[] statusText;

    [Header("Level")]
    [SerializeField] private TextMeshProUGUI[] levelTexts;

    public void Initialize()
    {
        
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
        Debug.LogAssertion("플레이어 인포 UI 업데이트");
        // levelTexts[(int)LevelType.Soul].text = $"Lv. {soul.level}";

        // TODO : 소울 스텟도 업데이트 되어야함
        // UpdateStatus();
    }

    private void InitUI()
    {
        //// TODO : 소울 썸네일 삽입
        //thumbnail.sprite = soul.icon;

        //// TODO : 스킬 sprite 삽입
        //// icons[(int)LevelType.Default].sprite = 
        //// icons[(int)LevelType.Ultimate].sprite = 
        //// icons[(int)LevelType.Passive].sprite = 

        //skillNameTexts[(int)LevelType.Default].text = $"Lv. {soul.Skills[(int)SkillType.Default].skillName}";
        //skillNameTexts[(int)LevelType.Ultimate].text = $"Lv. {soul.Skills[(int)SkillType.Ultimate].skillName}";
        //skillNameTexts[(int)LevelType.Passive].text = $"Lv. {soul.Skills[(int)SkillType.Passive].skillName}";

        //skillDescriptionTexts[(int)LevelType.Default].text = $"Lv. {soul.Skills[(int)SkillType.Default].description}";
        //skillDescriptionTexts[(int)LevelType.Ultimate].text = $"Lv. {soul.Skills[(int)SkillType.Ultimate].description}";
        //skillDescriptionTexts[(int)LevelType.Passive].text = $"Lv. {soul.Skills[(int)SkillType.Passive].description}";
    }

    private void UpdateStatus()
    {
        //levelText.text = $"Lv. {soul.level}";
        //powerText.text = Utils.FormatBigInteger(soul.statHandler.CurrentStat.totalDamage);
        //statusText[(int)StatusType.Hp].text = Utils.FormatBigInteger(soul.statHandler.CurrentStat.maxHealth);
        //statusText[(int)StatusType.Atk].text = Utils.FormatBigInteger(soul.statHandler.CurrentStat.atk);
        //statusText[(int)StatusType.Def].text = Utils.FormatBigInteger(soul.statHandler.CurrentStat.def);
        //statusText[(int)StatusType.MoveSpeed].text = $"{soul.statHandler.CurrentStat.moveSpeed + 100f}%";
        //statusText[(int)StatusType.AtkSpeed].text = $"{soul.statHandler.CurrentStat.atkSpeed + 100f}%";
        //statusText[(int)StatusType.ReduceDamage].text = soul.statHandler.CurrentStat.reduceDamage.ToString();
        //statusText[(int)StatusType.CritChance].text = $"{soul.statHandler.CurrentStat.critChance}%";
        //statusText[(int)StatusType.CritDamage].text = $"{soul.statHandler.CurrentStat.critDamage}%";
        //statusText[(int)StatusType.CoolDown].text = $"{soul.statHandler.CurrentStat.coolDown}%";
    }
}
