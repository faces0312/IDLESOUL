using DG.Tweening;
using TMPro;
using UnityEngine;

public static class Utils
{
    // 각종 유틸 메서드 정리

    public static readonly int POOL_KEY_PLAYERPROJECTILE = 10;

    public static bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return ((1 << layer) & layerMask) != 0;
    }

    /// <summary>
    /// 페이드아웃 효과 실행
    /// </summary>
    /// <param name="ease">적용할 Ease 타입</param>
    public static void StartFadeOut(CanvasGroup canvas, Ease ease, float duration)
    {
        if (canvas == null)
        {
            Debug.LogError("CanvasGroup이 할당되지 않았습니다.");
            return;
        }

        canvas.DOFade(0f, duration)
                   .SetEase(ease); // Ease 그래프 적용
    }

    /// <summary>
    /// 페이드인 효과 실행
    /// </summary>
    /// <param name="ease">적용할 Ease 타입</param>
    public static void StartFadeIn(CanvasGroup canvas, Ease ease, float duration)
    {
        if (canvas == null)
        {
            Debug.LogError("CanvasGroup이 할당되지 않았습니다.");
            return;
        }

        canvas.DOFade(1f, duration)
                   .SetEase(ease); // Ease 그래프 적용
    }

}
