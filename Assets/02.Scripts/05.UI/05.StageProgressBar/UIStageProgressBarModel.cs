using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Model : GameManager�� óġ�� Enemy Count�� Model�̵�
//View : UIStageProgressBarView
//Controller : UIStageProgressBarController

[System.Serializable]
public class UIStageProgressBarModel : UIModel
{
    public event Action OnEventCurEnemyAddCount;
    private int curEnemySlayerCount; //ó���� Enemy ī��Ʈ
    private int bossTriggerEnemySlayerCount; //Boss�� ���忡 �ʿ��� ����Ʈ�� Enemy ī��Ʈ

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
        //������ ������������ Add�ϰԵ�
        if (!GameManager.Instance.isTryBoss)
        {
            curEnemySlayerCount ++;
            Debug.Log($"���� óġ�� ���� ���� : {curEnemySlayerCount} \" {bossTriggerEnemySlayerCount}");
            OnEventCurEnemyAddCount?.Invoke();
        }
    }
}
