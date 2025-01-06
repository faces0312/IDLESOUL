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
    /// <param name="playerStatLevel">현재 레벨</param>
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
                /*필요한 데이터를 StatUpgradeDB에서 호출해서 사용 */
                baseCost = DataManager.Instance.StatUpgradeDB.GetByKey(100).MaxHealthBaseCost;
                growRate = DataManager.Instance.StatUpgradeDB.GetByKey(100).MaxHealthGrowRate;
                /*플레이어의 스텟 레벨을 호출 */
                playerStatLevel = GameManager.Instance.player.StatHandler.BaseStat.MaxHealthLevel;
                constIncrease = 20;
                break;
            case Status.Atk:
                baseCost = DataManager.Instance.StatUpgradeDB.GetByKey(100).AtkBaseCost;
                growRate = DataManager.Instance.StatUpgradeDB.GetByKey(100).AtkGrowRate;
                playerStatLevel = GameManager.Instance.player.StatHandler.BaseStat.AtkLevel;
                constIncrease = 20;
                break;
            case Status.Def:
                baseCost = DataManager.Instance.StatUpgradeDB.GetByKey(100).DefBaseCost;
                growRate = DataManager.Instance.StatUpgradeDB.GetByKey(100).DefGrowRate;
                playerStatLevel = GameManager.Instance.player.StatHandler.BaseStat.DefLevel;
                constIncrease = 20;
                break;
            case Status.ReduceDmg:
                baseCost = DataManager.Instance.StatUpgradeDB.GetByKey(100).ReduceDamageBaseCost;
                growRate = DataManager.Instance.StatUpgradeDB.GetByKey(100).ReduceDamageGrowRate;
                playerStatLevel = GameManager.Instance.player.StatHandler.BaseStat.ReduceDamageLevel;
                constIncrease = 20;
                break;
            case Status.CritChance:
                baseCost = DataManager.Instance.StatUpgradeDB.GetByKey(100).CriticalRateBaseCost;
                growRate = DataManager.Instance.StatUpgradeDB.GetByKey(100).CriticalRateGrowRate;
                playerStatLevel = GameManager.Instance.player.StatHandler.BaseStat.CriticalRateLevel;
                constIncrease = 20;
                break;
            case Status.CritDmg:
                baseCost = DataManager.Instance.StatUpgradeDB.GetByKey(100).CriticalDamageBaseCost;
                growRate = DataManager.Instance.StatUpgradeDB.GetByKey(100).CriticalDamageGrowRate;
                playerStatLevel = GameManager.Instance.player.StatHandler.BaseStat.CriticalDamageLevel;
                constIncrease = 20;
                break;
        }

        //코스트가 증가하는 기본적인 적용 수식 중 하나 
        //int Cost = (int)(baseCost * Mathf.Pow(playerStatLevel, growRate) + (playerStatLevel * constIncrease));
        int Cost = (int)(baseCost * ((Mathf.Log(playerStatLevel + 1, growRate))));
        return new BigInteger(Cost);

    }

    /// <summary>
    /// 스텟 업그레이드시 적용되는 스탯 증가율 수식
    /// </summary>
    /// <param name="baseStat">스탯의 기본 값</param>
    /// <param name="playerStatLevel">스탯 현재 레벨</param>
    /// <param name="growRate">스탯 성장율 (예: 1.5~2.0 사이).</param>
    /// <param name="growthExponent">성장 곡선의 형태를 결정. (성장속도) </param>    
    /// <param name="constStat"> 해당 스텟의 추가 고정값 </param>
    /// <returns></returns>
    public static BigInteger UpgradePlayerStatBigInteger(Status statType , int nextLevelStat = 0)
    {
        //스탯 = 기본값 × (1 + 성장 계수 × (player의 현재 스텟 레벨 ^ 성장 지수))

        var baseStat = 0f;
        float growRate = 0;
        float growthExponent = 1.1f;
        int playerStatLevel = 0;
        BigInteger constStat = new BigInteger();
        BigInteger playerStat = new BigInteger();
        switch (statType)
        {
            case Status.Hp:
                /*필요한 데이터를 StatUpgradeDB에서 호출해서 사용 */
                baseStat = DataManager.Instance.StatUpgradeDB.GetByKey(100).MaxHealthBaseStat;
                growRate = DataManager.Instance.StatUpgradeDB.GetByKey(100).MaxHealthGrowRate;
                /*플레이어의 현재 스텟 레벨을 호출 */
                playerStatLevel = GameManager.Instance.player.StatHandler.BaseStat.MaxHealthLevel;
                constStat = DataManager.Instance.UserDB.GetByKey().MaxHealth;
                playerStat = GameManager.Instance.player.StatHandler.CurrentStat.maxHealth;
                break;
            case Status.Atk:
                baseStat = DataManager.Instance.StatUpgradeDB.GetByKey(100).AtkBaseStat;
                growRate = DataManager.Instance.StatUpgradeDB.GetByKey(100).AtkGrowRate;
                playerStatLevel = GameManager.Instance.player.StatHandler.BaseStat.AtkLevel;
                constStat = DataManager.Instance.UserDB.GetByKey().Atk;
                playerStat = GameManager.Instance.player.StatHandler.CurrentStat.atk;
                break;
            case Status.Def:
                baseStat = DataManager.Instance.StatUpgradeDB.GetByKey(100).DefBaseStat;
                growRate = DataManager.Instance.StatUpgradeDB.GetByKey(100).DefGrowRate;
                playerStatLevel = GameManager.Instance.player.StatHandler.BaseStat.DefLevel;
                constStat = DataManager.Instance.UserDB.GetByKey().Def;
                playerStat = GameManager.Instance.player.StatHandler.CurrentStat.def;
                break;
        }

        //스탯 = 기본값 × (1 + 성장 계수 × (player의 현재 스텟 레벨 ^ 성장 지수))
        //int statResult = (int)(baseStat * (1 + growRate * (Mathf.Pow(playerStatLevel + nextLevelStat, growthExponent))));

        int result = (int)(baseStat * (Mathf.Log(playerStatLevel + 1, growRate)) * 100); // 0.957511 -> 95.71..%

        return BigInteger.Divide(BigInteger.Multiply(playerStat, result),100) + constStat;

    }

    /// <summary>
    /// 스텟 업그레이드시 적용되는 스탯 증가율 수식
    /// </summary>
    /// <param name="baseStat">스탯의 기본 값</param>
    /// <param name="playerStatLevel">스탯 현재 레벨</param>
    /// <param name="growRate">스탯 성장율 (예: 1.5~2.0 사이).</param>
    /// <param name="growthExponent">성장 곡선의 형태를 결정. (성장속도) </param>
    /// <param name="constStat"> 해당 스텟의 추가 고정값 </param>
    /// <returns></returns>
    public static float UpgradePlayerStat(Status statType, int nextLevelStat = 0)
    {
        //스탯 = 기본값 × (1 + 성장 계수 × (player의 현재 스텟 레벨 ^ 성장 지수))

        float baseStat = 0f;
        float growRate = 0;
        float growthExponent = 1.1f;
        int playerStatLevel = 0;
        var constStat = 0f;

        switch (statType)
        {
            case Status.ReduceDmg:
                baseStat = DataManager.Instance.StatUpgradeDB.GetByKey(100).ReduceDamageBaseStat;
                growRate = DataManager.Instance.StatUpgradeDB.GetByKey(100).ReduceDamageGrowRate;
                playerStatLevel = GameManager.Instance.player.StatHandler.BaseStat.ReduceDamageLevel;
                constStat = DataManager.Instance.UserDB.GetByKey().ReduceDamage;
                break;
            case Status.CritChance:
                baseStat = DataManager.Instance.StatUpgradeDB.GetByKey(100).CriticalRateBaseStat;
                growRate = DataManager.Instance.StatUpgradeDB.GetByKey(100).CriticalRateGrowRate;
                playerStatLevel = GameManager.Instance.player.StatHandler.BaseStat.CriticalRateLevel;
                constStat = DataManager.Instance.UserDB.GetByKey().CriticalRate;
                break;
            case Status.CritDmg:
                baseStat = DataManager.Instance.StatUpgradeDB.GetByKey(100).CriticalDamageBaseStat;
                growRate = DataManager.Instance.StatUpgradeDB.GetByKey(100).CriticalDamageGrowRate;
                playerStatLevel = GameManager.Instance.player.StatHandler.BaseStat.CriticalDamageLevel;
                constStat = DataManager.Instance.UserDB.GetByKey().CriticalDamage;
                break;
        }

        //스탯 = 기본값 × (1 + 성장 계수 × (player의 현재 스텟 레벨 ^ 성장 지수))
        //float statResult = (int)(baseStat * (1 + growRate * (Mathf.Pow(playerStatLevel + nextLevelStat, growthExponent))));
        float statResult = baseStat * Mathf.Log(playerStatLevel + 1, growRate);

        return statResult + constStat;

    }

    /// <summary>
    /// 스킬 업그레이드시 적용되는 코스트 증가율 수식
    /// </summary>
    /// <param name="baseCost">초기 비용. 레벨 1에서의 기본 비용입니다.</param>
    /// <param name="level">현재 레벨</param>
    /// <param name="growRate">코스트 증가율 (예: 1.5~2.0 사이).</param>
    /// <param name="constantIncrease">레벨마다 고정적으로 추가되는 비용 (선택 사항).</param>
    /// <returns></returns>
    public static BigInteger SoulUpgradeCost(LevelType type, Soul soul)
    {
        int baseCost = 0;
        float growRate = 0;
        int level = 0;
        int constIncrease = 0;

        switch (type)
        {
            case LevelType.Default:
                /*필요한 데이터를 StatUpgradeDB에서 호출해서 사용 */
                baseCost = soul.Skills[(int)SkillType.Default].UpgradeCost;
                growRate = 2;
                /*소울의 스킬 레벨을 호출 */
                level = soul.Skills[(int)SkillType.Default].level;
                constIncrease = 20;
                break;
            case LevelType.Ultimate:
                baseCost = soul.Skills[(int)SkillType.Ultimate].UpgradeCost;
                growRate = 2;
                level = soul.Skills[(int)SkillType.Ultimate].level;
                constIncrease = 20;
                break;
            case LevelType.Passive:
                baseCost = soul.Skills[(int)SkillType.Passive].UpgradeCost;
                growRate = 2;
                level = soul.Skills[(int)SkillType.Passive].level;
                constIncrease = 20;
                break;
            case LevelType.Soul:
                baseCost = 1000;
                growRate = 2;
                /*소울의 레벨을 호출 */
                level = soul.level;
                constIncrease = 20;
                break;
        }

        //코스트가 증가하는 기본적인 적용 수식 중 하나 
        int Cost = (int)(baseCost * Mathf.Pow(level, growRate) + (level * constIncrease));

        return new BigInteger(Cost);
    }

    public static BigInteger CriticalCaculate(StatHandler Stat, BigInteger Damage)
    {
        //크리티컬 적용 
        if (Random.Range(0, 100) < Stat.CurrentStat.critChance)
        {
            //크리티컬 데미지 적용 , BigInteger 라이브러리는 소숫점 계산을 지원하지 않음
            //그러기에 해당 크리티컬 데미지 증가율을 곱한뒤 100을 나눠서 보정함(백분율)
            Damage = BigInteger.Multiply(Damage, (int)(Stat.CurrentStat.critDamage));
            Damage = BigInteger.Divide(Damage, 100);
        }

        return Damage;
    }
}
