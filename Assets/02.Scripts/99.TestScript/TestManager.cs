using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TestManager : SingletonDDOL<TestManager>
{
    // 여러 기능들을 테스트 하기 위한 목적의 매니저 클래스
    // 형식을 신경쓰지 않고, 자유롭게 사용

    [SerializeField] public GameObject effectPrefab;
    [SerializeField] public GameObject effectPrefab2;
    [SerializeField] public GameObject statPanel;

    [SerializeField] private GameObject enemy;

    public Soul TestSoul;
    public StatHandler playerStatHandler;
    public int playerLevel = 1;
    public int soulLevel = 1;

    public TextMeshProUGUI[] playerStats;
    public TextMeshProUGUI[] soulStats;

    public TestInventoryModel inventory;

    private void Start()
    {
        //GameManager.Instance.enemies.Add(enemy);

        if(statPanel != null)
            statPanel.SetActive(false);
    }

    public void StatViewUpdate()
    {
        TestSoul = GameManager.Instance.player.PlayerSouls.CurrentSoul;

        playerStats[0].text = playerStatHandler.CurrentStat.iD.ToString();
        playerStats[1].text = playerStatHandler.CurrentStat.health.ToString();
        playerStats[2].text = playerStatHandler.CurrentStat.maxHealth.ToString();
        playerStats[3].text = playerStatHandler.CurrentStat.atk.ToString();
        playerStats[4].text = playerStatHandler.CurrentStat.def.ToString();
        playerStats[5].text = playerStatHandler.CurrentStat.moveSpeed.ToString();
        playerStats[6].text = playerStatHandler.CurrentStat.atkSpeed.ToString();
        playerStats[7].text = playerStatHandler.CurrentStat.reduceDamage.ToString();
        playerStats[8].text = playerStatHandler.CurrentStat.critChance.ToString();
        playerStats[9].text = playerStatHandler.CurrentStat.critDamage.ToString();
        playerStats[10].text = playerStatHandler.CurrentStat.coolDown.ToString();

        soulStats[0].text = TestSoul.StatHandler.CurrentStat.iD.ToString();
        soulStats[1].text = TestSoul.StatHandler.CurrentStat.health.ToString();
        soulStats[2].text = TestSoul.StatHandler.CurrentStat.maxHealth.ToString();
        soulStats[3].text = TestSoul.StatHandler.CurrentStat.atk.ToString();
        soulStats[4].text = TestSoul.StatHandler.CurrentStat.def.ToString();
        soulStats[5].text = TestSoul.StatHandler.CurrentStat.moveSpeed.ToString();
        soulStats[6].text = TestSoul.StatHandler.CurrentStat.atkSpeed.ToString();
        soulStats[7].text = TestSoul.StatHandler.CurrentStat.reduceDamage.ToString();
        soulStats[8].text = TestSoul.StatHandler.CurrentStat.critChance.ToString();
        soulStats[9].text = TestSoul.StatHandler.CurrentStat.critDamage.ToString();
        soulStats[10].text = TestSoul.StatHandler.CurrentStat.coolDown.ToString();
    }

    public void OnUseDefaultSkill()
    {
        GameManager.Instance.player.PlayerSouls.CurrentSoul.UseSkill(GameManager.Instance.player.PlayerSouls.CurrentSoul.Skills[(int)SkillType.Default]);
    }

    public void OnUseUltimateSkill()
    {
        GameManager.Instance.player.PlayerSouls.CurrentSoul.UseSkill(GameManager.Instance.player.PlayerSouls.CurrentSoul.Skills[(int)SkillType.Ultimate]);
    }

    public void OnClickPlayerLevelUp(int level)
    {
        playerLevel += level;
        playerStatHandler.LevelUp(playerLevel);
        GameManager.Instance.player.OnUpdateSoulStats?.Invoke();
        StatViewUpdate();
    }

    public void OnClickSoulLevelUp(int level)
    {
        soulLevel += level;
        TestSoul.StatHandler.LevelUp(soulLevel);
        StatViewUpdate();
    }

    public void OnClickAddItem(string key)
    {
        inventory.AddItem(key);
    }

    public void OnClickRemoveItem(string key)
    {
        inventory.RemoveItem(key);
    }

    public void OnClickUpgradePassiveSkill()
    {
        TestSoul.UpgradeSkill(SkillType.Passive, 1);
        TestSoul.ApplyPassiveSkill();
        GameManager.Instance.player.OnUpdateSoulStats?.Invoke();
        StatViewUpdate();
    }

    public void OnClickRegisterSoul()
    {
        GameManager.Instance.player.PlayerSouls.RegisterSoul("클라리스", new SoulMagician(11000));
        GameManager.Instance.player.PlayerSouls.RegisterSoul("플뢰르", new SoulKnight(11001));
        GameManager.Instance.player.PlayerSouls.RegisterSoul("루엔", new SoulArcher(11002));
        GameManager.Instance.player.PlayerSouls.EquipSoul("클라리스", 0);
        GameManager.Instance.player.PlayerSouls.EquipSoul("플뢰르", 1);
        GameManager.Instance.player.PlayerSouls.EquipSoul("루엔", 2);
        GameManager.Instance.player.OnUpdateSoulStats?.Invoke();    // 착용 시 패시브 업데이트

        GameManager.Instance.player.PlayerSouls.SpawnSoul(0);

        playerStatHandler = GameManager.Instance.player.StatHandler;

        //StatViewUpdate();
    }

    public void OnClickCreateEffect()
    {
        //Instantiate(effectPrefab);
        Vector3 pos = GameManager.Instance.player.transform.position;
        pos += effectPrefab2.transform.position;
        Instantiate(effectPrefab2, pos, Quaternion.LookRotation(effectPrefab2.transform.forward));
    }

    public void OnClickSpawnSoul(int index)
    {
        GameManager.Instance.player.PlayerSouls.SpawnSoul(index);
    }

    public void OnClickViewUpdate()
    {
        StatViewUpdate();
    }
}
