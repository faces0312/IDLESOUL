using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

//Model : GameManager�� óġ�� Enemy Count�� Model�̵�
//View : UIStageProgressBarView
//Controller : UIStageProgressBarController

public class UIStageProgressBarView : MonoBehaviour, IUIBase
{
    [SerializeField] private RectTransform curStageProgress;

    public void Initialize()
    {
        if (curStageProgress == null)
        {
            curStageProgress = GetComponent<RectTransform>();
        }

        curStageProgress.localScale = new Vector3(0, 1, 1);

    }

    public void ShowUI()
    {
        //Boss ���� ���� ������ ���� ������ UI ���
        gameObject.SetActive(true);
        Utils.StartFadeIn(this.GetComponent<CanvasGroup>(), Ease.OutBounce, 1.0f);
    }

    public void HideUI()
    {
        //Boss ���� ���� ������ �Ǹ� �������
        Utils.StartFadeOut(this.GetComponent<CanvasGroup>(), Ease.OutBounce, 1.0f);
        gameObject.SetActive(false);
    }

    public void UpdateUI()
    {

    }

    public void UpdateUIProgree(float resultProgress)
    {
        curStageProgress.localScale = new Vector3(resultProgress, 1, 1);
    }

}
