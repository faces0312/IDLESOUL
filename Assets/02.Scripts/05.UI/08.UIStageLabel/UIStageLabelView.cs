using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStageLabelView : MonoBehaviour, IUIBase
{
    [SerializeField] private TextMeshProUGUI stageLabal;
    private StageDB tempStage;

    public void HideUI()
    {
    }

    public void Initialize()
    {
    }

    public void ShowUI()
    {
        SetUI(StageManager.Instance.CurStageData);
    }

    public void UpdateUI()
    {

    }

    public void SetUI(StageDB stageDB)
    {
        tempStage = stageDB;
        stageLabal.text = $"{StageManager.Instance.Chapter} - {tempStage.StageNum} {tempStage.stageName}";
        StageManager.Instance.Stage = tempStage.StageNum;
    }
}
