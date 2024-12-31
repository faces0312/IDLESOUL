using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStageLabelView : MonoBehaviour, IUIBase
{
    [SerializeField] private TextMeshProUGUI stageLabal;

    public void HideUI()
    {
    }

    public void Initialize()
    {
    }

    public void ShowUI()
    {
        StageDB stagedb = StageManager.Instance.CurStageData;
        stageLabal.text = $"{SceneDataManager.Instance.Chapter} - {stagedb.StageNum} {stagedb.stageName}";
        SceneDataManager.Instance.Stage = stagedb.StageNum;
    }

    public void UpdateUI()
    {
    }

}
