using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StageManager : SingletonDDOL<StageManager>
{
    [Header("StageData")]
    private int curStageID;
    private int newStageID;
    private StageDB curStageData;
    private StageMap curStageMap; 

    private List<StageMap> stageMapList = new List<StageMap>(); // 0 : 성 , 1 : 숲

    //게임의 진척도를 저장하고있는 Model(data) 변수
    public UIStageProgressBarModel StageProgressModel;
   
    public StageMap CurStageMap { get => curStageMap; }
    public StageDB CurStageData { get => curStageData; }
    public int CurStageID { get => curStageID;}

    protected override void Awake()
    {
        base.Awake();
        StageProgressModel = new UIStageProgressBarModel();
        curStageID = 7000; //챕터1 Stage1

        stageMapList.Add(Instantiate(Resources.Load<StageMap>(Const.STAGE_CASTHLE_MAP_PATH)));
        stageMapList.Add(Instantiate(Resources.Load<StageMap>(Const.STAGE_FORESET_MAP_PATH)));

        for (int i = 0; i < stageMapList.Count; i++)
        {
            stageMapList[i].gameObject.SetActive(false);
        }
    }
    
    public void StageSelect(int stageID)
    {
        if (stageID > 7009)
        {
            stageID = 7000;
            SceneDataManager.Instance.Chapter++;
            EventManager.Instance.Publish<AchieveEvent>(Enums.Channel.Achievement, new AchieveEvent(Enums.AchievementType.Clear, Enums.ActionType.Stage, 0));
        }
        curStageID = stageID;
    }

    public void Init()
    {
        Debug.Log(curStageID);
        curStageData = DataManager.Instance.StageDB.GetByKey(curStageID);
        SceneDataManager.Instance.MainStageModifier *= curStageData.CurStageModifier;

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



}
