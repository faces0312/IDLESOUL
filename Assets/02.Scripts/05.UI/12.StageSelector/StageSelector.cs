using UnityEngine;
using Enums;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine.UI;

public class StageSelector : MonoBehaviour
{
    [SerializeField] private Button exit;
    private RecycleScrollX recycleScroll;
    private List<StageDB> stageData;
    private List<StageDB> sortedData;
    private StageType stageType;
    private StageSelectorController controller;

    private void Start()
    {
        controller = new StageSelectorController();
        controller.StageSelector = this.gameObject;
        UIManager.Instance.RegisterController("stageSelectorController", controller);
        exit.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
        });
        sortedData = new List<StageDB>();
        stageData = DataManager.Instance.StageDB.ItemsList;
        recycleScroll = GetComponent<RecycleScrollX>();
        recycleScroll.SetContent += SetStage;
        recycleScroll.Init();
        this.gameObject.SetActive(false);
    }

    public void SetStageType(StageType stageType)
    {
        this.stageType = stageType;
    }

    private void SetStage(GameObject obj, int idx)
    {
        sortedData.Clear();
        foreach (var data in stageData)
        {
            if (data.StageType == (int)stageType)
            {
                sortedData.Add(data);
            }
        }
        obj.GetComponent<Stage>().SetData(idx);
    }
}
