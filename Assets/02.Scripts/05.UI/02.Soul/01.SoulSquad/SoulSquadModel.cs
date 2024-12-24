using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoulSquadModel : UIModel
{
    public event Action OnSquadChanged;

    private SoulSquadSlot[] slots = new SoulSquadSlot[Const.MAX_SOUL];

    public void AddSlot(int index, SoulSquadSlot slot)
    {
        slots[index] = slot;
    }

    public void EquipSoul(int index, Soul soul)
    {
        if (slots[index].soul != null)
        {
            UnEquipSoul(index);
        }

        // 소울 인벤토리의 장착 표시 활성화
        GameManager.Instance.player.PlayerSouls.SoulInventory.GetSlot(soul).EquipSlot();

        slots[index].soul = soul;
        slots[index].soulName = soul.soulName;
        slots[index].sprite = soul.sprite;
        slots[index].UpdateThumbnail(); // TODO : View에서 호출 하도록 리펙토링필요
        OnSquadChanged?.Invoke();
    }

    public void UnEquipSoul(int index)
    {
        // 소울 인벤토리의 장착 표시 비활성화
        GameManager.Instance.player.PlayerSouls.SoulInventory.GetSlot(slots[index].soul).UnEquipSlot();

        slots[index].soul = null;
        slots[index].soulName = string.Empty;
        slots[index].sprite = null;
        OnSquadChanged?.Invoke();
    }

    public bool SearchSoul(Soul soul)
    {
        foreach (var slot in slots)
        { 
            if(slot.soul == soul)
            {
                return true;
            }
        }

        return false;
    }
}
