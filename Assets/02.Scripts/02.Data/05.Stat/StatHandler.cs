using ScottGarland;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class StatHandler
{
    [SerializeField] private StatType type;

    private Stat baseStat;
    private Stat currentStat;

    // 추가 스텟을 List로 관리 (장비, 패시브 등)
    private List<Stat> additionalStats = new List<Stat>();
    public Stat CurrentStat { get { return currentStat; } }
    
    public StatHandler(StatType type, int key = 0)
    {
        this.type = type;

        switch(type)
        {
            case StatType.Player:
                baseStat = StatConverter.PlayerStatConvert(key);
                break;
            case StatType.Soul:
                baseStat = StatConverter.SoulStatConvert(key);
                break;
            case StatType.Enemy:
                baseStat = StatConverter.EnemyStatConvert(key);
                break;
        }

        currentStat = new Stat(baseStat);

        Initialize(type);
    }

    private void Initialize(StatType type)
    {
        switch (type)
        {
            case StatType.Player:
                // TODO : 플레이어 스텟 핸들러를 연결해줌
                TestManager.Instance.playerStatHandler = this;
                break;
            case StatType.Soul:
                // TODO : 플레이가 레벨업, 장비를 장착할 때 소울들의 정보도 갱신이 되어야 한다.
                TestManager.Instance.OnUpdateSoulStats += UpdateSoulStats;
                UpdateSoulStats();
                break;
            case StatType.Enemy:
                break;
        }
    }

    private void UpdateStats(int level)
    {
        // TODO : 수치 적용 방법 => 현재는 레벨을 곱해줌
        // Enemy 는 Stage에 비례해서 수치 적용 => level과 stage 배율 다르면 오버로딩

        currentStat.health = BigInteger.Multiply(int.Parse(baseStat.health.ToString()), level);
        currentStat.maxHealth = currentStat.health;
        currentStat.atk = BigInteger.Multiply(int.Parse(baseStat.atk.ToString()), level);
        currentStat.def = BigInteger.Multiply(int.Parse(baseStat.def.ToString()), level);

        currentStat.moveSpeed = baseStat.moveSpeed * level;
        currentStat.atkSpeed = baseStat.atkSpeed * level;

        currentStat.reduceDamage = baseStat.reduceDamage * level;

        currentStat.critChance = baseStat.critChance * level;
        currentStat.critDamage = baseStat.critDamage * level;
        currentStat.coolDown = baseStat.coolDown * level;   // TODO : 0 일때 처리
    }

    private void UpdateSoulStats()
    {
        // TODO : 플레이어 스텟 불러오기 => 임시 값 사용중
        // 소울 현재 스텟 * 플레이어 현재 스텟 %
        Stat playerStat = TestManager.Instance.playerStatHandler.currentStat;

        // 기존에 적용되어 있는 추가 스텟을 해제
        Stat prevStats = new Stat();

        foreach(Stat stat in additionalStats)
        {
            prevStats += stat;
        }

        currentStat -= prevStats;
        additionalStats.Clear();

        // 추가 스텟을 새로 계산해서 추가
        Stat calcStat = new Stat();

        calcStat.health = BigInteger.Add(currentStat.maxHealth, BigInteger.Divide(BigInteger.Multiply(currentStat.maxHealth, playerStat.maxHealth), 100));
        calcStat.maxHealth = calcStat.health;
        calcStat.atk = BigInteger.Add(currentStat.atk, BigInteger.Divide(BigInteger.Multiply(currentStat.atk, playerStat.atk), 100));

        BigInteger mul = BigInteger.Multiply(currentStat.def, playerStat.def);
        BigInteger div = BigInteger.Divide(mul, 100);

        calcStat.def = BigInteger.Add(currentStat.def, div);

        calcStat.reduceDamage = currentStat.reduceDamage * (playerStat.reduceDamage * 0.01f + 1);
        calcStat.critChance = currentStat.critChance * (playerStat.critChance * 0.01f + 1);
        calcStat.critDamage = currentStat.critDamage * (playerStat.critDamage * 0.01f + 1);

        calcStat.atkSpeed = currentStat.atkSpeed * (playerStat.atkSpeed * 0.01f + 1);
        calcStat.moveSpeed = currentStat.moveSpeed * (playerStat.moveSpeed * 0.01f + 1);
        calcStat.coolDown = currentStat.coolDown * (playerStat.coolDown * 0.01f + 1);   // TODO : 0 일때 처리

        calcStat -= currentStat;

        additionalStats.Add(calcStat);

        currentStat += calcStat;
    }

    private Stat CalculateAdditionalStats()
    {
        Stat itemStats = new Stat();

        foreach (Stat stat in additionalStats)
        {
            itemStats += stat;
        }
        
        return itemStats;
    }

    public void LevelUp(int level)
    {
        //UpdateStats(level);
        currentStat = baseStat * level;  // 연산자 오버로딩 테스트

        if (type == StatType.Soul)
        {
            additionalStats.Clear();    // 추가 값이 사라졌으므로 초기화 시킨다.
            UpdateSoulStats();
        }
    }

    public void EquipItem(Stat itemStat)
    {
        additionalStats.Add(itemStat);

        Stat itemStatSum = CalculateAdditionalStats();

        currentStat += itemStatSum;
    }

    public void UnEquipItem(Stat itemStat)
    {
        additionalStats.Remove(itemStat);

        currentStat -= itemStat;
    }
}
