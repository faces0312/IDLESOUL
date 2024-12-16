using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Model : GameManager의 처치한 Enemy Count가 Model이됨
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
        UpdateView();   // 초기 View 갱신
    }

    public override void OnHide()
    {
        view.HideUI();
    }

    public override void UpdateView()
    {
        float resultProgress = (stageProgressBarModel.CurEnemySlayerCount / (float)stageProgressBarModel.BossTriggerEnemySlayerCount);
        resultProgress = Mathf.Min(1,resultProgress);

        // Model 데이터를 기반으로 View 갱신
        //view.UpdateUI();
        stageProgressBarView.UpdateUIProgree(resultProgress);
    }

    public void BossTriggerCheck()
    {
        if (!GameManager.Instance.IsBoss && stageProgressBarModel.CurEnemySlayerCount >= stageProgressBarModel.BossTriggerEnemySlayerCount)
        {       
            Debug.Log($"보스 등장 조건을 만족 합니다.");
            EnemyManager.Instance.BossSpawn(StageManager.Instance.CurStageData.SummonBossID); // Debug ID값 고정시켜 사용

            //stageProgressBarModel.OnEventCurEnemyAddCount -= UpdateView;
            //stageProgressBarModel.OnEventCurEnemyAddCount -= BossTriggerCheck;
            OnHide();
        }
    }
}
