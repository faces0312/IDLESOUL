using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : SingletonDDOL<StageManager>
{
    [Header("StageData")]
    private int curStageID;
    private StageDB curStageData;
    private StageMap curStageMap;

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
    }
    
    public void StageSelect(int stageID)
    {
        curStageID = stageID;
    }

    public void Init()
    {
        curStageData = DataManager.Instance.StageDB.GetByKey(curStageID);
        
        curStageMap = Resources.Load<StageMap>(curStageData.StageMapPath);
        Instantiate(curStageMap);

        StageProgressModel.Initialize(CurStageData.SlayEnemyCount);

        Debug.Log("StageManager 세팅 완료!!");
    }



}
