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

    // 추가 스텟을 List로 관리
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
                //currentStat.exp = new BigInteger(baseStat.exp);
                //currentStat.MaxExp = new BigInteger(baseStat.MaxExp);

                // TODO : 플레이어 스텟 핸들러를 연결해줌
                TestManager.Instance.playerStatHandler = this;
                break;
            case StatType.Soul:
                // TODO : 스킬 정보

                // TODO : 플레이가 레벨업, 장비를 장착할 때 소울들의 정보도 갱신이 되어야 한다.
                //TestPlayerManager.Instance.OnUpdateSoulStats += UpdateStats;
                //TestPlayerManager.Instance.OnUpdateSoulStats += UpdateSoulStats;
                TestManager.Instance.OnUpdateSoulStats += UpdateSoulStats;
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
        currentStat.coolDown = baseStat.coolDown * level;
    }

    private void UpdateStats(Stat stat)
    {
        // 단순 덧셈

        currentStat.health += stat.health;
        currentStat.maxHealth += stat.maxHealth;
        currentStat.atk += stat.atk;
        currentStat.def += stat.def;

        currentStat.moveSpeed += stat.moveSpeed;
        currentStat.atkSpeed += stat.atkSpeed;

        currentStat.reduceDamage += stat.reduceDamage;

        currentStat.critChance += stat.critChance;
        currentStat.critDamage += stat.critDamage;
        currentStat.coolDown += stat.coolDown;
    }

    private void UpdateSoulStats()
    {
        // TODO : 플레이어 스텟 불러오기 => 임시 값 사용중
        // 소울 현재 스텟 * 플레이어 현재 스텟 %
        Stat playerStat = TestManager.Instance.playerStatHandler.currentStat;

        currentStat.health = BigInteger.Multiply(int.Parse(currentStat.maxHealth.ToString()), BigInteger.Add(BigInteger.Divide(playerStat.maxHealth, 100), 1));
        currentStat.maxHealth = currentStat.health;
        currentStat.atk = BigInteger.Multiply(int.Parse(currentStat.atk.ToString()), BigInteger.Add(BigInteger.Divide(playerStat.atk, 100), 1));
        currentStat.def = BigInteger.Multiply(int.Parse(currentStat.def.ToString()), BigInteger.Add(BigInteger.Divide(playerStat.def, 100), 1));

        currentStat.reduceDamage = currentStat.reduceDamage * (playerStat.reduceDamage * 0.01f + 1);
        currentStat.critChance = currentStat.critChance * (playerStat.critChance * 0.01f + 1);
        currentStat.critDamage = currentStat.critDamage * (playerStat.critDamage * 0.01f + 1);

        currentStat.atkSpeed = currentStat.atkSpeed * (playerStat.atkSpeed * 0.01f + 1);
        currentStat.moveSpeed = currentStat.moveSpeed * (playerStat.moveSpeed * 0.01f + 1);
        currentStat.coolDown = currentStat.coolDown * (playerStat.coolDown * 0.01f + 1);
    }

    private Stat CalculateAdditionalStats()
    {
        Stat itemStats = new Stat();

        foreach (Stat stat in additionalStats)
        {
            itemStats.health += stat.health;
            itemStats.atk += stat.atk;
            itemStats.def += stat.def;

            itemStats.moveSpeed += stat.moveSpeed;
            itemStats.atkSpeed += stat.atkSpeed;

            itemStats.reduceDamage += stat.reduceDamage;

            itemStats.critChance += stat.critChance;
            itemStats.critDamage += stat.critDamage;
            itemStats.coolDown += stat.coolDown;
        }

        return itemStats;
    }

    public void LevelUp(int level)
    {
        UpdateStats(level);

        if(type == StatType.Soul)
            UpdateSoulStats();
    }

    public void EquipItem(Stat itemStat)
    {
        additionalStats.Add(itemStat);

        Stat itemStatSum = CalculateAdditionalStats();

        UpdateStats(itemStatSum);
    }

    public void UnEquipItem(Stat itemStat)
    {
        additionalStats.Remove(itemStat);

        Stat itemStatSum = CalculateAdditionalStats();

        UpdateStats(itemStatSum);
    }
}
