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

        slots[index].soul = soul;
        slots[index].index = index;
        slots[index].soulName = soul.soulName;
        OnSquadChanged?.Invoke();
    }

    public void UnEquipSoul(int index)
    {
        slots[index].soul = null;
        slots[index].index = -1;
        slots[index].soulName = string.Empty;
        OnSquadChanged?.Invoke();
    }
}
