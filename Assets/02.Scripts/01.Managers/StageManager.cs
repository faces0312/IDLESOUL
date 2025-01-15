using ScottGarland;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class StageManager : SingletonDDOL<StageManager>
{
    [Header("StageData")]
    private int curStageID;
    private StageDB curStageData;
    private StageMap curStageMap;

    private List<StageMap> stageMapList = new List<StageMap>(); // 0 : 성 , 1 : 숲

    public int Chapter;
    public int Stage;
    public float MainStageModifier;

    //게임의 진척도를 저장하고있는 Model(data) 변수
    public UIStageProgressBarModel StageProgressModel;

    public StageMap CurStageMap { get => curStageMap; }
    public StageDB CurStageData { get => curStageData; }
    public int CurStageID { get => curStageID; }

    protected override void Awake()
    {
        base.Awake();
        StageProgressModel = new UIStageProgressBarModel();

        stageMapList.Add(Instantiate(Resources.Load<StageMap>(Const.STAGE_CASTHLE_MAP_PATH)));
        stageMapList.Add(Instantiate(Resources.Load<StageMap>(Const.STAGE_FORESET_MAP_PATH)));

        for (int i = 0; i < stageMapList.Count; i++)
        {
            stageMapList[i].gameObject.SetActive(false);

        }
    }

    public void StageSelect(int stageID)
    {
        Stage++;
        if (stageID > 7009)
        {
            stageID = 7000;
            Stage = 1;
            GameManager.Instance.player.UserData.ClearStageCycle++;
            Chapter = GameManager.Instance.player.UserData.ClearStageCycle;
            EventManager.Instance.Publish<AchieveEvent>(Enums.Channel.Achievement, new AchieveEvent(Enums.AchievementType.Clear, Enums.ActionType.Stage, 0));
        }
        curStageID = stageID;
    }

    public void StartSelectStage(int chapter, int stage)
    {
        StageDB tempStage = new StageDB();
        SetStageModifier(chapter, stage);
        Chapter = chapter;
        Stage = stage;

        for (int i = 0; i < stageMapList.Count; i++)
        {
            if (stageMapList[i].gameObject.activeSelf)
            {
                stageMapList[i].gameObject.SetActive(false);
                break;
            }
        }

        foreach (var item in DataManager.Instance.StageDB.ItemsList)
        {
            if (item.StageNum == stage && item.ChapterNum == 1)
            {
                tempStage = item;
                break;
            }

        }

        curStageData = tempStage;
        curStageID = curStageData.key;
        curStageMap = stageMapList[(int)curStageData.StageName];
        curStageMap.gameObject.SetActive(true);

        StageProgressModel.Initialize(CurStageData.SlayEnemyCount);
        UIManager.Instance.GetController<UIStageLabelController>().SetStage(curStageData);
        UIManager.Instance.ShowUI<UIStageLabelController>();
    }


    public void Init(bool curStageCheck = false)
    {
        if (DataManager.Instance.StageDB.GetByKey(GameManager.Instance.player.UserData.curStageID) == null)
        {
            Chapter = 1;
            Stage = 1;
            curStageID = 7000;
            GameManager.Instance.player.UserData.BestStageChapter = Chapter;
            GameManager.Instance.player.UserData.BestStageNum = Stage;
        }
        else
        {
            if(curStageCheck) //현재 머무르고 있는 스테이지 데이터 반영하여 다음스테이지를 세팅
            {
                Chapter = GameManager.Instance.player.UserData.ClearStageCycle;
                curStageID = GameManager.Instance.player.UserData.curStageID;
            }
            else //도달했던 최고 스테이지로 세팅 
            {
                Chapter = GameManager.Instance.player.UserData.BestStageChapter;
                Stage = GameManager.Instance.player.UserData.BestStageNum;
                curStageID = GameManager.Instance.player.UserData.curStageID;
            }
          
        }

        curStageData = DataManager.Instance.StageDB.GetByKey(CurStageID);
        SetStageModifier(GameManager.Instance.player.UserData.ClearStageCycle, curStageData.StageNum);
        GameManager.Instance.player.isDead = false;

        SaveStageData();

        for (int i = 0; i < stageMapList.Count; i++)
        {
            if (stageMapList[i].gameObject.activeSelf)
            {
                stageMapList[i].gameObject.SetActive(false);
                break;
            }
        }

        curStageMap = stageMapList[(int)curStageData.StageName];
        curStageMap.gameObject.SetActive(true);

        StageProgressModel.Initialize(CurStageData.SlayEnemyCount);

        Debug.Log("StageManager 세팅 완료!!");
    }

    public void SaveStageData()
    {
        GameManager.Instance.player.UserData.ClearStageCycle = Chapter;
        GameManager.Instance.player.UserData.curStageID = curStageID;
        GameManager.Instance.player.UserData.StageModifier = MainStageModifier;
    }

    private void SetStageModifier(int chapter, int stage)
    {
        float mod = 1;
        for (; chapter > 1; chapter--)
        {
            for(int i = 7000; i < 7009; i ++)
            {
                mod *= DataManager.Instance.StageDB.GetByKey(i).CurStageModifier;
            }
        }
        for (int i = 7000; i < 7000 + stage; i++)
        {
            mod *= DataManager.Instance.StageDB.GetByKey(i).CurStageModifier;
        }
        MainStageModifier = mod;
    }
}
