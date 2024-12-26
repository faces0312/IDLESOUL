using ScottGarland;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public enum Status
{
    Hp,
    Atk,
    Def,
    ReduceDmg,
    CritChance,
    CritDmg
}

public class StatHandler
{
    [SerializeField] private StatType type;

    private Stat baseStat;
    private Stat currentStat;

    // 추가 스텟을 List로 관리 (장비, 패시브 등)
    private List<Stat> additionalStats = new List<Stat>();
    public Stat CurrentStat { get { return currentStat; } }

    public StatHandler(StatType type, int key = 0 , UserData userData = null)
    {
        this.type = type;

        switch (type)
        {
            case StatType.Player:
                baseStat = StatConverter.PlayerStatConvert(userData);
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
                // TODO : 플레이어 스텟 핸들러를 연결해줌 => 적용 확인 시 주석 삭제
                GameManager.Instance.player.StatHandler = this;
                break;
            case StatType.Soul:
                // TODO : 플레이가 레벨업, 장비를 장착할 때 소울들의 정보도 갱신이 되어야 한다. => 적용 확인 시 주석 삭제
                GameManager.Instance.player.OnUpdateSoulStats += UpdateSoulStats;
                UpdateSoulStats();
                break;
            case StatType.Enemy:
                break;
        }
    }

    public void UpdateStats()
    {
        // TODO : 수치 적용 방법 => 현재는 레벨을 곱해줌
        // Enemy 는 Stage에 비례해서 수치 적용 => level과 stage 배율 다르면 오버로딩

        //currentStat.maxHealth = BigInteger.Multiply(int.Parse(currentStat.maxHealth.ToString()), currentStat.MaxHealthLevel * 3);
        currentStat.maxHealth = BigInteger.Add(int.Parse(currentStat.maxHealth.ToString()), currentStat.MaxHealthLevel * 100);
        //currentStat.maxHealth = currentStat.health;
        currentStat.atk = BigInteger.Add(int.Parse(currentStat.atk.ToString()), currentStat.atk * 30);
        currentStat.def = BigInteger.Add(int.Parse(currentStat.def.ToString()), currentStat.def * 30);

        currentStat.reduceDamage = currentStat.reduceDamage * currentStat.ReduceDamageLevel * 5;

        currentStat.critChance = currentStat.critChance * currentStat.CriticalRateLevel * 1;
        currentStat.critDamage = currentStat.critDamage * currentStat.CriticalDamageLevel * 1;
        currentStat.coolDown = baseStat.coolDown;   // TODO : 0 일때 처리
    }

    private void UpdateSoulStats()
    {
        // TODO : 플레이어 스텟 불러오기 => 적용 확인 시 주석 삭제
        // 소울 현재 스텟 * 플레이어 현재 스텟 %
        Stat playerStat = GameManager.Instance.player.StatHandler.currentStat;

        // 기존에 적용되어 있는 추가 스텟을 해제
        Stat prevStats = new Stat();

        foreach (Stat stat in additionalStats)
        {
            prevStats += stat;
        }

        currentStat -= prevStats;
        additionalStats.Clear();

        // 추가 스텟을 새로 계산해서 추가
        Stat calcStat = new Stat();

        //calcStat.health = BigInteger.Add(currentStat.maxHealth, BigInteger.Divide(BigInteger.Multiply(currentStat.maxHealth, playerStat.maxHealth), 100));
        calcStat.health = BigInteger.Divide(BigInteger.Multiply(currentStat.maxHealth, playerStat.maxHealth), 100);
        calcStat.maxHealth = calcStat.health;
        calcStat.atk = BigInteger.Divide(BigInteger.Multiply(currentStat.atk, playerStat.atk), 100);

        BigInteger mul = BigInteger.Multiply(currentStat.def, playerStat.def);
        BigInteger div = BigInteger.Divide(mul, 100);
        calcStat.def = div;

        calcStat.reduceDamage = currentStat.reduceDamage + (playerStat.reduceDamage * 0.01f + 1);
        calcStat.critChance = currentStat.critChance + (playerStat.critChance * 0.01f + 1);
        calcStat.critDamage = currentStat.critDamage + (playerStat.critDamage * 0.01f + 1);

        calcStat.atkSpeed = currentStat.atkSpeed + (playerStat.atkSpeed * 0.01f + 1);
        calcStat.moveSpeed = currentStat.moveSpeed + (playerStat.moveSpeed * 0.01f + 1);
        calcStat.coolDown = currentStat.coolDown + (playerStat.coolDown * 0.01f + 1);   // TODO : 0 일때 처리

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

    public void LevelUp(int level, Status type)
    {
        // Player 레벨업 메서드
        // TODO : 수치 적용 방법 => 현재는 레벨을 곱해줌

        switch(type)
        {
            case Status.Hp:
                currentStat.MaxHealthLevel += level;
                //currentStat.health = BigInteger.Multiply(baseStat.health, currentStat.MaxHealthLevel);
                //currentStat.maxHealth = currentStat.health;
                currentStat.maxHealth = BigInteger.Add(int.Parse(currentStat.maxHealth.ToString()), currentStat.MaxHealthLevel * 100);
                break;
            case Status.Atk:
                currentStat.AtkLevel += level;
                //currentStat.atk = BigInteger.Multiply(baseStat.atk, currentStat.AtkLevel);
                currentStat.atk = BigInteger.Add(int.Parse(currentStat.atk.ToString()), currentStat.AtkLevel * 3000);
                break;
            case Status.Def:
                currentStat.DefLevel += level;
                //currentStat.def = BigInteger.Multiply(baseStat.def, currentStat.DefLevel);
                currentStat.def = BigInteger.Add(int.Parse(currentStat.def.ToString()), currentStat.DefLevel * 30);
                break;
            case Status.ReduceDmg:
                currentStat.ReduceDamageLevel += level;
                //currentStat.reduceDamage = baseStat.reduceDamage * currentStat.ReduceDamageLevel;
                currentStat.reduceDamage = currentStat.reduceDamage * currentStat.ReduceDamageLevel * 5;
                break;
            case Status.CritChance:
                currentStat.CriticalRateLevel += level;
                //currentStat.critChance = baseStat.critChance * currentStat.CriticalRateLevel;
                currentStat.critChance = currentStat.critChance * currentStat.CriticalRateLevel * 1;
                break;
            case Status.CritDmg:
                currentStat.CriticalDamageLevel += level;
                //currentStat.critDamage = baseStat.critDamage * currentStat.CriticalDamageLevel;
                currentStat.critDamage = currentStat.critDamage * currentStat.CriticalDamageLevel * 1;
                break;
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
