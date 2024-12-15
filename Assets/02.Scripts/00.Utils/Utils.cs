using DG.Tweening;
using UnityEngine;
using ScottGarland;

public static class Utils
{
    public static Fader fader = Object.Instantiate(Resources.Load<Fader>("Prefabs/UI/UIFade"));

    // 각종 유틸 메서드 정리

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

    public static float Percent(float current, float max)
    {
        return current != 0 && max != 0 ? current / max : 0;
    }

    /// <summary>
    /// BigInteger 1만 단위 표기
    /// </summary>
    private static readonly string[] UNITS = { "", "만", "억", "조", "경", "해" };

    public static string FormatBigInteger(BigInteger value)
    {
        int unitIndex = 0;
        BigInteger threshold = 10000;   // 나눌 단위 값

        string convert = string.Empty;

        // 자릿수 계산하기
        while (value >= threshold && unitIndex < UNITS.Length - 1)
        {
            BigInteger mod = value % threshold;

            string temp = convert;
            // 단위를 붙일 값이 0 이라면 생략한다.
            if (mod != 0)
                convert = $"{mod}{UNITS[unitIndex]}";
            else
                convert = string.Empty;
            convert += temp;

            value /= threshold;
            unitIndex++;
        }

        string result = $"{value}{UNITS[unitIndex]}";
        result += convert;

        return result;
    }

}
