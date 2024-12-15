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
    public Sprite sprite;

    private void Awake()
    {
        if (thumbnail == null)
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
        thumbnail.sprite = sprite;
    }

    public void UnEquipSoul()
    {
        soul = null;
        soulName = string.Empty;
        thumbnail.sprite = null;
        GameManager.Instance.player.PlayerSouls.UnEquipSoul(index);
    }

    // TODO : 리팩토링 필요
    public void UpdateThumbnail()
    {
        if (thumbnail == null)
            thumbnail = GetComponent<Image>();
        thumbnail.sprite = sprite;
    }
}
