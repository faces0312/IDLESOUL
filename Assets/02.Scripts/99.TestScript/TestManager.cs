using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TestManager : SingletonDDOL<TestManager>
{
    // 여러 기능들을 테스트 하기 위한 목적의 매니저 클래스
    // 형식을 신경쓰지 않고, 자유롭게 사용

    public StatHandler playerStatHandler;

    public void OnClickRegisterSoul()
    {
        GameManager.Instance.player.PlayerSouls.RegisterSoul("클라리스", new SoulMagician(11000));
        GameManager.Instance.player.PlayerSouls.RegisterSoul("클라리스", new SoulMagician(11000));
        GameManager.Instance.player.PlayerSouls.RegisterSoul("플뢰르", new SoulKnight(11001));
        GameManager.Instance.player.PlayerSouls.RegisterSoul("루엔", new SoulArcher(11002));
        GameManager.Instance.player.PlayerSouls.RegisterSoul("카르밀라", new SoulDummyRare(11003));
        GameManager.Instance.player.PlayerSouls.EquipSoul("클라리스", 0);
        GameManager.Instance.player.PlayerSouls.EquipSoul("플뢰르", 1);
        GameManager.Instance.player.PlayerSouls.EquipSoul("루엔", 2);
        GameManager.Instance.player.OnUpdateSoulStats?.Invoke();    // 착용 시 패시브 업데이트

        GameManager.Instance.player.PlayerSouls.SpawnSoul(0);

        playerStatHandler = GameManager.Instance.player.StatHandler;
    }
}
