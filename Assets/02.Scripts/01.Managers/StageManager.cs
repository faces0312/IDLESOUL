using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StageManager : SingletonDDOL<StageManager>
{
    [Header("StageData")]
    private int curStageID;
    private StageDB curStageData;
    private StageMap curStageMap;

    public UIStageProgressBarModel StageProgressModel;
   
    public StageMap CurStageMap { get => curStageMap; }
    public StageDB CurStageData { get => curStageData; }
    public int CurStageID { get => curStageID;}

    protected override void Awake()
    {
        base.Awake();
        StageProgressModel = new UIStageProgressBarModel();
        curStageID = 7000; //é��1 Stage1
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

        StageProgressModel.Initialize(CurStageData.SlayEnemyCount);//Debug - ����� ���� UI�Ŵ��� �ʱ�ȭ�Ҷ� �� ���������

        Debug.Log("StageManager ���� �Ϸ�!!");
    }



}
