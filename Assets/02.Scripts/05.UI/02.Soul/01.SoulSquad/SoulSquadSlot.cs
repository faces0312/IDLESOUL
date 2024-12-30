using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulSquadSlot : MonoBehaviour
{
    [SerializeField] private Sprite emptySprite;

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

    public void EquipSoul()
    {
        thumbnail.sprite = sprite;

        // 현재 소환중인 소울과 교체한 소울슬롯의 인덱스가 같을 경우 재 소환
        if (GameManager.Instance.player.PlayerSouls.SpawnIndex == index)
            GameManager.Instance.player.PlayerSouls.SpawnSoul(index, true);
    }

    public void UnEquipSoul()
    {
        soul = null;
        soulName = string.Empty;
        thumbnail.sprite = emptySprite;
    }

    // TODO : 리팩토링 필요
    public void UpdateThumbnail()
    {
        if (thumbnail == null)
            thumbnail = GetComponent<Image>();
        thumbnail.sprite = sprite;
    }
}
