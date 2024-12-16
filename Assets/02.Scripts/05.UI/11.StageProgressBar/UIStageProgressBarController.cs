using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Model : GameManager�� óġ�� Enemy Count�� Model�̵�
//View : UIStageProgressBarView
//Controller : UIStageProgressBarController

public class UIStageProgressBarController : UIController
{
    private UIStageProgressBarModel stageProgressBarModel;
    private UIStageProgressBarView stageProgressBarView;

    public override void Initialize(IUIBase view, UIModel model)
    {
        base.Initialize(view, model);

        stageProgressBarModel = model as UIStageProgressBarModel;
        stageProgressBarView = view as UIStageProgressBarView;

        stageProgressBarModel.OnEventCurEnemyAddCount += UpdateView;
        stageProgressBarModel.OnEventCurEnemyAddCount += BossTriggerCheck;
    }

    public override void OnShow()
    {
        view.ShowUI();
        UpdateView();   // �ʱ� View ����
    }

    public override void OnHide()
    {
        view.HideUI();
    }

    public override void UpdateView()
    {
        float resultProgress = (stageProgressBarModel.CurEnemySlayerCount / (float)stageProgressBarModel.BossTriggerEnemySlayerCount);
        resultProgress = Mathf.Min(1,resultProgress);

        // Model �����͸� ������� View ����
        //view.UpdateUI();
        stageProgressBarView.UpdateUIProgree(resultProgress);
    }

    public void BossTriggerCheck()
    {
        if (!GameManager.Instance.IsBoss && stageProgressBarModel.CurEnemySlayerCount >= stageProgressBarModel.BossTriggerEnemySlayerCount)
        {       
            Debug.Log($"���� ���� ������ ���� �մϴ�.");
            EnemyManager.Instance.BossSpawn(StageManager.Instance.CurStageData.SummonBossID); // Debug ID�� �������� ���

            //stageProgressBarModel.OnEventCurEnemyAddCount -= UpdateView;
            //stageProgressBarModel.OnEventCurEnemyAddCount -= BossTriggerCheck;
            OnHide();
        }
    }
}
