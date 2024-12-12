using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulSquadSlot : MonoBehaviour
{
    private SoulSquad soulSquad;
    private SoulInventory soulInventory;

    private Image thumbnail;
    private Button button;

    public Soul soul;
    public int index;
    public string soulName;

    private void Awake()
    {
        thumbnail = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickSlot);
    }

    private void OnClickSlot()
    {
        if(soulSquad == null)
            soulSquad = GameManager.Instance.player.PlayerSouls.SoulSquad;
        if (soulInventory == null)
            soulInventory = GameManager.Instance.player.PlayerSouls.SoulInventory;
        soulSquad.curIndex = index;
        soulInventory.SoulSquadSlot = this;
        soulInventory.UpdateThumbnail();
        UIManager.Instance.ShowUI("SoulInventory");
    }

    public void EquipSoul(Soul soul)
    {
        GameManager.Instance.player.PlayerSouls.EquipSoul(soul.soulName, index);
    }

    public void UnEquipSoul()
    {
        soul = null;
        soulName = string.Empty;
        GameManager.Instance.player.PlayerSouls.UnEquipSoul(index);
    }
}
