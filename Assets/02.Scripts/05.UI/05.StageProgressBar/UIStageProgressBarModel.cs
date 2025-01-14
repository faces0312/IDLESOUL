using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Model : GameManager의 처치한 Enemy Count가 Model이됨
//View : UIStageProgressBarView
//Controller : UIStageProgressBarController

[System.Serializable]
public class UIStageProgressBarModel : UIModel
{
    public event Action OnEventCurEnemyAddCount;
    private int curEnemySlayerCount; //처지한 Enemy 카운트
    private int bossTriggerEnemySlayerCount; //Boss가 등장에 필요한 쓰러트린 Enemy 카운트

    public int CurEnemySlayerCount { get => curEnemySlayerCount; private set => curEnemySlayerCount = value; }
    public int BossTriggerEnemySlayerCount { get => bossTriggerEnemySlayerCount; private set => bossTriggerEnemySlayerCount = value; }

    public void Initialize(int bossTriggerCount)
    {
        bossTriggerEnemySlayerCount = bossTriggerCount;
        curEnemySlayerCount = 0;
    }

    public void CurCountDataClear()
    {
        curEnemySlayerCount = 0;
    }

    public void AddCurEnemyCount()
    {
        //보스가 등장했을때만 Add하게됨
        if (!GameManager.Instance.isTryBoss)
        {
            curEnemySlayerCount ++;
            Debug.Log($"현재 처치한 적의 갯수 : {curEnemySlayerCount} \" {bossTriggerEnemySlayerCount}");
            OnEventCurEnemyAddCount?.Invoke();
        }
    }
}
