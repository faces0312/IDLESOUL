using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSouls : MonoBehaviour
{
    private const int MAX_SOUL = 3;

    public int spawnIndex;
    private Soul[] soulSlot = new Soul[MAX_SOUL];
    private Dictionary<string, Soul> soulDic = new Dictionary<string, Soul>();

    public Soul CurrentSoul { get; private set; }
    public Soul[] SoulSlot { get { return soulSlot; } }

    private void Start()
    {
        RegisterSoul("마법사 영혼", new SoulMagician(11000));
        RegisterSoul("전사 영혼", new SoulKnight(11001));
        EquipSoul("마법사 영혼", 0);
        EquipSoul("전사 영혼", 1);
        GameManager.Instance.player.OnUpdateSoulStats?.Invoke();    // 착용 시 패시브 업데이트

        SpawnSoul(0);
        spawnIndex = 0;
    }

    // 소울 등록
    public void RegisterSoul(string name, Soul soul)
    {
        soulDic.Add(name, soul);
    }

    // 소울 장착
    public void EquipSoul(string name, int index)
    {
        if (soulDic.TryGetValue(name, out Soul soul))
        {
            // 슬롯에 소울이 있다면 해제한다
            UnEquipSoul(index);

            // 해당하는 index에 소울을 장착
            soulSlot[index] = soul;
        }
    }

    // 소울 해제
    public void UnEquipSoul(int index)
    {
        // index에 해당하는 소울을 해제
        if (soulSlot[index] != null)
        {
            soulSlot[index] = null;
        }
    }

    // 소울 스왑
    public void SpawnSoul(int index)
    {
        // TODO : 소환 중인 소울이 있다면 소환을 해제
        // 소환을 해제하는 로직
        CurrentSoul = null;

        // TODO : 슬롯의 소울을 소환
        // 소환하는 로직
        CurrentSoul = soulSlot[index];
    }
}
