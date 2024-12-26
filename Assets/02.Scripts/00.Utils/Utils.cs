using DG.Tweening;
using UnityEngine;
using ScottGarland;
using static Cinemachine.DocumentationSortingAttribute;

public static class Utils
{
    //public static Fader fader = Object.Instantiate(Resources.Load<Fader>("Prefabs/UI/UIFade"));

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

    /// <summary>
    /// 스텟 업그레이드시 적용되는 코스트 증가율 수식
    /// </summary>
    /// <param name="baseCost">초기 비용. 레벨 1에서의 기본 비용입니다.</param>
    /// <param name="level">현재 레벨</param>
    /// <param name="growRate">코스트 증가율 (예: 1.5~2.0 사이).</param>
    /// <param name="constantIncrease">레벨마다 고정적으로 추가되는 비용 (선택 사항).</param>
    /// <returns></returns>
    public static BigInteger UpgradeCost(Status statType)
    {
        int baseCost = 0;
        float growRate = 0;
        int playerStatLevel = 0;
        int constIncrease = 0;

        switch (statType)
        {
            case Status.Hp:
                baseCost = DataManager.Instance.StatUpgradeDB.GetByKey(100).MaxHealthBaseCost;
                growRate = DataManager.Instance.StatUpgradeDB.GetByKey(100).MaxHealthGrowRate;
                playerStatLevel = GameManager.Instance.player.StatHandler.CurrentStat.MaxHealthLevel;
                constIncrease = 20;
                break;
            case Status.Atk:
                baseCost = DataManager.Instance.StatUpgradeDB.GetByKey(100).AtkBaseCost;
                growRate = DataManager.Instance.StatUpgradeDB.GetByKey(100).AtkGrowRate;
                playerStatLevel = GameManager.Instance.player.StatHandler.CurrentStat.AtkLevel;
                constIncrease = 20;
                break;
            case Status.Def:
                baseCost = DataManager.Instance.StatUpgradeDB.GetByKey(100).DefBaseCost;
                growRate = DataManager.Instance.StatUpgradeDB.GetByKey(100).DefGrowRate;
                playerStatLevel = GameManager.Instance.player.StatHandler.CurrentStat.DefLevel;
                constIncrease = 20;
                break;
            case Status.ReduceDmg:
                baseCost = DataManager.Instance.StatUpgradeDB.GetByKey(100).ReduceDamageBaseCost;
                growRate = DataManager.Instance.StatUpgradeDB.GetByKey(100).ReduceDamageGrowRate;
                playerStatLevel = GameManager.Instance.player.StatHandler.CurrentStat.ReduceDamageLevel;
                constIncrease = 20;
                break;
            case Status.CritChance:
                baseCost = DataManager.Instance.StatUpgradeDB.GetByKey(100).CriticalRateBaseCost;
                growRate = DataManager.Instance.StatUpgradeDB.GetByKey(100).CriticalRateGrowRate;
                playerStatLevel = GameManager.Instance.player.StatHandler.CurrentStat.CriticalRateLevel;
                constIncrease = 20;
                break;
            case Status.CritDmg:
                baseCost = DataManager.Instance.StatUpgradeDB.GetByKey(100).CriticalDamageBaseCost;
                growRate = DataManager.Instance.StatUpgradeDB.GetByKey(100).CriticalDamageGrowRate;
                playerStatLevel = GameManager.Instance.player.StatHandler.CurrentStat.CriticalDamageLevel;
                constIncrease = 20;
                break;
        }
        int Cost = (int)(baseCost * Mathf.Pow(playerStatLevel, growRate) + (playerStatLevel * constIncrease));

        return new BigInteger(Cost);

    }

}
