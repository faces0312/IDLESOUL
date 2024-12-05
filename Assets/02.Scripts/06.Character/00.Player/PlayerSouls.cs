using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSouls : MonoBehaviour
{
    private const int MAX_SOUL = 3;

    private int spawnIndex = -1;
    private Soul[] soulSlot = new Soul[MAX_SOUL];
    private Dictionary<string, Soul> soulDic = new Dictionary<string, Soul>();

    private GameObject[] spawnEffects = new GameObject[MAX_SOUL];

    public Soul CurrentSoul { get; private set; }
    public Soul[] SoulSlot { get { return soulSlot; } }

    private void Awake()
    {
        // TODO : 임시
        LoadSpawnEffects();
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
        // 현재 소환중인 소울과 같은 경우
        if (spawnIndex == index) return;

        // TODO : 소환 중인 소울이 있다면 소환을 해제
        // 소환을 해제하는 로직
        CurrentSoul = null;

        // TODO : 슬롯의 소울을 소환
        // 소환하는 로직
        CurrentSoul = soulSlot[index];
        spawnIndex = index;

        // TODO : 소울의 속성에 따라 소환 이펙트 변경 / 임시로 고정해둠
        if (spawnEffects[spawnIndex] == null)
            LoadSpawnEffects();

        Instantiate(spawnEffects[spawnIndex], transform.position, Quaternion.identity);
    }

    private void LoadSpawnEffects()
    {
        spawnEffects[0] = Resources.Load<GameObject>("Prefabs/Skills/SoulSpawn_Red");
        spawnEffects[1] = Resources.Load<GameObject>("Prefabs/Skills/SoulSpawn_Blue");
        spawnEffects[2] = Resources.Load<GameObject>("Prefabs/Skills/SoulSpawn_Yellow");
    }
}
